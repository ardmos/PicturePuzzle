using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

///
/// 유저가 카메라로 오브젝트를 촬영하면 해당 오브젝트가 아이템이 되는 게임에서 카메라의 기능을 구현한 소스 코드 입니다. 
/// 
/// ChangeCamera 메서드에서 MainCameraON 메서드와 PolaroidCameraON 메서드를 번갈아 호출하며 유저가 일반(메인카메라) 모드와 촬영용(폴라로이드) 카메라 모드를 오갈 수 있도록 만들었고 
/// MovePolaroidAim_mainCameraState 와 MovePolaroidAim_polaroidCameraState 메서드를 사용해서 유저가 화면 에임을 이동시킬 수 있도록 만들었습니다.
/// ActivateNPA와 DeactivateNPA 메서드를 통해 유저에게 촬영 가능한 오브젝트에 대한 힌트를 제공하게끔 만들었습니다.(NPA는 'NoticePhotoAble'의 약자로, 촬영 가능한 오브젝트들을 뜻의 제가 임의로 정한 약자 입니다^^;)
/// 마지막으로 OnButton_TakePictureClicked 메서드를 통해 촬영 기능을 구현했습니다.
/// 
///

public class CameraController : MonoBehaviour
{
    #region 변수들 입니다.

    public Camera mainCamera, polaroidCamera;    
    public AudioListener mainListener, polaroidListener;
    public bool isPolaroidCamera;
    //카메라 모드가 변할 때 모드에 맞춰 UI를 변경할 때 쓰일 변수들 입니다. 
    public GameObject mainUI, polaroidUI;
    //폴라로이드 카메라 에임 이미지 오브젝트 입니다.
    public GameObject polaroidAimObj;
    //폴라로이드 카메라 확대 화면 이동을 위한 포지션변수 입니다.
    Vector3 beforePos = Vector3.zero, currentPos;
    PlayerData playerData;

    //촬영 가능한 아이템들을 알려주는 힌트 이미지 오브젝트를 저장하는 변수들 입니다.
    //Turtle
    public GameObject[] npa_Turtle;
    //Stone
    public GameObject[] npa_Stone;
    //Wood
    public GameObject[] npa_Wood;

    //촬영으로 특정 오브젝트가 유저의 인벤토리로 사라지게 되면, 해당 오브젝트가 없는 형태의 이미지로 배경 이미지를 바꿔주게끔 구현했습니다. 
    //그 때 쓰이는 배경화면 오브젝트와 이미지들입니다.    
    public SpriteRenderer backGroundImage;
    [Header("//Turtle")]
    public Sprite nonTurtleImg;
    [Header("//Stone")]
    public Sprite nonStoneImg;
    [Header("//Wood")]
    public Sprite nonFishImg;   
    public Animator humanAnimator;

    //유저가 게임을 처음 플레이할 시 동작할 튜토리얼용 오브젝트 입니다. 
    public GameObject correctGuidObj;

    //촬영가능 범위 변수 입니다. 촬영 가능한 오브젝트와 카메라 에임의 중심의 거리가 해당 변수보다 가까우면 촬영이 가능한것으로 간주합니다. 이 때 촬영하기 버튼이 클릭되면 촬영에 성공하도록 만들었습니다.
    public float dis = 10.01f;
    #endregion

    void Start()
    {
        //카메라의 필름 개수등을 가지고 있는 플레이어의 정보를 얻어오는 부분입니다.
        playerData = FindObjectOfType<PlayerData>();
        //메인 카메라 모드를 시작합니다.
        MainCameraON();
    }

    void Update()
    {
        //isPolaroidCamera 변수로 현재 카메라의 상태를 확인하도록 만들었습니다.
        if (!isPolaroidCamera)
        {
            //Main카메라 상태일 때
            MovePolaroidAim_mainCameraState();
        }
        else
        {
            //Polaroid카메라 상태일 때
            MovePolaroidAim_polaroidCameraState();
        }
    }

    #region 메인-폴라로이드 카메라 전환 기능을 구현한 부분입니다.
    //카메라 변경 버튼이 클릭되었을 때 실행되는 메서드 입니다.
    public void ChangeCamera()
    {
        if (isPolaroidCamera)
        {
            MainCameraON();
        }
        else
        {
            PolaroidCameraON();
        }
    }
    public void PolaroidCameraON()
    {
        //튜토리얼 가이드 관련 내용 입니다.
        if (SceneManager.GetActiveScene().name.Contains("Turtle"))
        {
            FindObjectOfType<PicTurtleNavigationManager>().CallWhenPolaroidCameraOn();
        }

        //Main카메라 모드에서의 PolaroidAimObj의 위치를 Polaroid카메라의 중심 위치로 설정해주는 부분입니다.
        polaroidCamera.gameObject.transform.position = new Vector3(polaroidAimObj.transform.position.x, polaroidAimObj.transform.position.y, -10);

        polaroidListener.enabled = true;
        polaroidCamera.enabled = true;
        mainListener.enabled = false;
        mainCamera.enabled = false;
        isPolaroidCamera = true;
        mainUI.SetActive(false);
        polaroidUI.SetActive(true);
        polaroidAimObj.SetActive(false);
        //촬영 가능 아이템 힌트를 활성화하는 부분입니다. 
        ActivateNPA();
    }
    public void MainCameraON()
    {
        polaroidListener.enabled = false;
        polaroidCamera.enabled = false;
        mainListener.enabled = true;
        mainCamera.enabled = true;
        isPolaroidCamera = false;
        mainUI.SetActive(true);
        polaroidUI.SetActive(false);
        polaroidAimObj.SetActive(true);
        //촬영 가능 아이템 힌트를 비활성화하는 부분입니다.
        DeactivateNPA();
    }
    #endregion

    #region 메인카메라 상태에서 폴라로이드 카메라 에임 움직이는 기능을 구현한 부분입니다.
    public void MovePolaroidAim_mainCameraState()
    {
        //폴라로이드카메라 에임 이동. 
        if (Input.GetMouseButton(0))
        {
            //EventSystem.curren.IsPointerOverGameObject를 이용해서 UI가 터치되었는지를 확인하도록 했습니다.

            //안드로이드 플랫폼일 때
            if (Application.platform == RuntimePlatform.Android)
            {
                if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
                {
                    //UI를 터치했기 때문에 에임은 이동하지 않습니다.
                }
                else
                {
                    Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    polaroidAimObj.transform.position = new Vector3(worldMousePos.x, worldMousePos.y);
                }
            }
            //안드로이드 플랫폼이 아닐 때
            else
            {
                if (EventSystem.current.IsPointerOverGameObject())
                {
                    //UI를 클릭했기 때문에 에임은 이동하지 않습니다.
                }
                else
                {
                    Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    polaroidAimObj.transform.position = new Vector3(worldMousePos.x, worldMousePos.y);
                }
            }           
        }        
    }
    #endregion

    #region 폴라로이드 카메라 상태에서 카메라 에임 움직이는 기능을 구현한 부분입니다.
    public void MovePolaroidAim_polaroidCameraState()
    {                
        //마찬가지로 EventSystem.curren.IsPointerOverGameObject를 이용해서 UI가 터치되었는지를 확인하도록 했습니다.
        if (Input.GetMouseButton(0))
        {
            //안드로이드 플랫폼일 때
            if (Application.platform == RuntimePlatform.Android)
            {
                if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
                {
                    //UI를 터치했기 때문에 에임은 이동하지 않습니다.
                }
                else
                {
                    if (beforePos != Vector3.zero)
                    {
                        currentPos = polaroidCamera.ScreenToWorldPoint(Input.mousePosition);
                        Vector3 polaroidCamPos = polaroidCamera.transform.position;
                        Vector3 movedis = (beforePos - currentPos);
                        polaroidCamera.transform.position = new Vector3(polaroidCamPos.x + movedis.x, polaroidCamPos.y + movedis.y, -10f);
                        //메인카메라 상태에서 보여지는 작은폴라로이드 카메라의 에임의 위치를 폴라로이드 카메라의 에임 위치와 일치하도록 지속적으로 업데이트 해줍니다.
                        polaroidAimObj.transform.position = new Vector2(polaroidCamera.transform.position.x, polaroidCamera.transform.position.y);
                    }
                    beforePos = polaroidCamera.ScreenToWorldPoint(Input.mousePosition);
                }
            }
            //안드로이드 플랫폼이 아닐 때
            else
            {
                if (EventSystem.current.IsPointerOverGameObject())
                {
                    //UI를 클릭했기 때문에 에임은 이동하지 않습니다.
                }
                else
                {
                    if (beforePos != Vector3.zero)
                    {
                        currentPos = polaroidCamera.ScreenToWorldPoint(Input.mousePosition);
                        Vector3 polaroidCamPos = polaroidCamera.transform.position;
                        Vector3 movedis = (beforePos - currentPos);
                        polaroidCamera.transform.position = new Vector3(polaroidCamPos.x + movedis.x, polaroidCamPos.y + movedis.y, -10f);
                        //메인카메라 상태에서 보여지는 작은폴라로이드 카메라의 에임의 위치를 폴라로이드 카메라의 에임 위치와 일치하도록 지속적으로 업데이트 해줍니다.
                        polaroidAimObj.transform.position = new Vector2(polaroidCamera.transform.position.x, polaroidCamera.transform.position.y);
                    }
                    beforePos = polaroidCamera.ScreenToWorldPoint(Input.mousePosition);
                }
            }                       
        }
        if (Input.GetMouseButtonUp(0))
        {
            //한 번 손이 떼어지고, 드래그가 끝나면 다음 드래그를 새롭게 시작할 때 
            //이전 드래그의 위치값이 관여하는일이 없도록 값을 초기화 시켜주었습니다. 
            beforePos = Vector3.zero;            
        }
    }
    #endregion

    #region 카메라 촬영 관련 기능을 구현한 부분입니다. (폴라로이드 카메라 활성화시 작동하는 부분입니다.)

    //1. 아직 촬영되지 않은 촬영 가능 오브젝트들은 유저들에게 힌트가 될 수 있도록 중앙에 '촬영 가능 이미지(npa)'를 띄우도록 했습니다.
    public void DeactivateNPA()
    {
        foreach (var item in npa_Turtle) item.SetActive(false);
        foreach (var item in npa_Stone) item.SetActive(false);
        foreach (var item in npa_Wood) item.SetActive(false);
    }
    public void ActivateNPA()
    {
        foreach (var item in npa_Turtle) item.SetActive(true);
        if (SceneManager.GetActiveScene().name.Contains("Stone"))
            npa_Stone[0].SetActive(true);              
        foreach (var item in npa_Wood) item.SetActive(true);
    }

    //2. 촬영버튼 클릭 시 촬영 기능을 구현한 부분입니다.
    public void OnButton_TakePictureClicked()
    {
        //화면이 번쩍 하는 Flash 이펙트를 줬습니다.
        FindObjectOfType<CameraFlash>().ActiveCameraFlash();

        //MinusPlayerFilmCount()를 호출해서 playerData에 있는 폴라로이드 카메라의 필름 카운트를 1 차감해줬습니다. 
        //차감에 성공 했다면 촬영버튼 처리를 진행했고, 실패했다면 이미 남은 필름이 없으니 촬영을 진행하지 않도록 했습니다.
        if (playerData.MinusPlayerFilmCount())
        {           
            //Turtle
            if (SceneManager.GetActiveScene().name.Contains("Turtle")) IsPicSuccess(npa_Turtle);
            //Stone
            else if (SceneManager.GetActiveScene().name.Contains("Stone")) IsPicSuccess(npa_Stone);
            //Wood
            else if (SceneManager.GetActiveScene().name.Contains("Wood")) IsPicSuccess(npa_Wood);           
        }
    }

    //촬영버튼 클릭시 사진이 제대로 찍혔나 확인하는 메서드 입니다.
    private void IsPicSuccess(GameObject[] npa_arr)
    {
        foreach (var item in npa_arr)
        {
            if (Vector3.Distance(polaroidCamera.gameObject.transform.position, item.transform.position) <= dis)
            {
                //성공 애니메이션을 실행시켜주는 부분입니다.
                FindObjectOfType<ObjMove>().StartMoveToInven();

                //촬영된 오브젝트의 힌트를 꺼주는 부분입니다.
                item.transform.parent.gameObject.SetActive(false);

                //각 씬별로 특정 아이템이 촬영되었을 때 배경 이미지를 해당 아이템이 존재하지 않는 버젼의 배경 이미지로 변경해주는 부분 입니다. 
                SettingBGImage_WhenSuccess(item);

                //촬영에 성공한 아이템을 PlayerData의 아이템 리스트에 추가해주는 부분입니다.
                playerData.AddItem(item.transform.parent.gameObject.name);
                //플레이어의 인벤토리를 갱신해주는 부분입니다.
                FindObjectOfType<InventoryController>().ResetInventory();
            }
            else
            {                
                Debug.Log("촬영 실패 Dis:" + Vector3.Distance(polaroidCamera.gameObject.transform.position, item.transform.position) + ", camera:" + polaroidCamera.gameObject.transform.position + ", obj:" + item.transform.position);
            }
        }
    }

    //각 씬별로 특정 아이템이 촬영되었을 때 배경 이미지를 해당 아이템이 존재하지 않는 버젼의 배경 이미지로 변경해주는 부분 입니다. 
    void SettingBGImage_WhenSuccess(GameObject item) {
        //Turtle
        if (SceneManager.GetActiveScene().name.Contains("Turtle"))
        {
            if (npa_Turtle[1] == item)
            {
                backGroundImage.sprite = nonTurtleImg;
                FindObjectOfType<PicTurtleNavigationManager>().CallWhenTurtlePicTaken();
            }
        }
        //Stone
        else if (SceneManager.GetActiveScene().name.Contains("Stone"))
        {
            if (FindObjectOfType<StoneSceneManager>().idxcount >= 4)
            {
                if (npa_Stone[1] == item) backGroundImage.sprite = nonStoneImg;
            }
        }
        //Wood
        else if (SceneManager.GetActiveScene().name.Contains("Wood"))
        {
            if (npa_Wood[0] == item) humanAnimator.SetBool("Fall", true);
            else if (npa_Wood[1] == item) backGroundImage.sprite = nonFishImg;
        }
    }    
    
    #endregion
}
