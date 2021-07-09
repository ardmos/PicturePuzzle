using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

/// <summary>
/// 카메라 전환과 메인카메라에서 폴라로이드카메라영역 이동 처리를 담당하는 카메라 컨트롤러.
/// 1. 메인 카메라와 폴라로이드 카메라 화면 전환 기능
/// 2. 화면 전환시 오디오 리스너도 전환.
/// 3. 화면 전환시 UI세트도 전환.
/// 4. 메인카메라일 시 화면 터치되는 위치로 폴라로이드카메라 영역 이동. ispolaroidCamera를 이용해 판단. 드래그로도 이동 가능하도록 수정 0619
/// 5. 확대된 상태에서도 움직일 수 있게 
///
///
/// 카메라 촬영 기능 만들 차례임.  
/// 6. 촬영 기능 (폴라로이드 카메라 확대 화면일 시 작동.)
///   1. 촬영 가능 오브젝트들은 중앙에 NoticePhotoAble 이미지를 띄운다.
///   2. 촬영버튼 클릭 시 처리.
///   3. 확대축소 기능
/// </summary>

public class CameraController : MonoBehaviour
{
    //카메라들
    public Camera mainCamera, polaroidCamera;
    //오디오리스너들
    public AudioListener mainListener, polaroidListener;
    public bool ispolaroidCamera;
    //UI세트들
    public GameObject mainUI, polaroidUI;
    //폴라로이드 에임 이미지 오브젝
    public GameObject polaroidAimObj;
    //폴라로이드 확대 화면 이동을 위한 포지션변수
    Vector3 beforePos = Vector3.zero, currentPos;
    //플레이어데이타
    PlayerData playerData;


    //촬영기능 

    //NPA들. 하이어라키상 순서대로. [0]은 테니스,  [1]은 거북이.
    //Turtle
    public GameObject[] npa_Turtle;
    //Stone
    public GameObject[] npa_Stone;
    //Wood
    public GameObject[] npa_Wood;

    //폴라로이드 필름 카운트는 PlayerData.cs에. 
    //배경화면 변경이 필요한 경우를 위한. 배경화면 오브젝트.
    public SpriteRenderer backGroundImage;

    //배경화면 변경시 쓰일 스프라이트 이미지들. 
    //Turtle
    [Header("//Turtle")]
    public Sprite nonTurtleImg;
    //Stone
    [Header("//Stone")]
    public Sprite nonStoneImg;
    //Wood
    [Header("//Wood")]
    public Sprite nonFishImg;
    public Animator humanAnimator;


    //가이드용. 
    public GameObject correctGuidObj;


    //촬영가능 범위 dis
    public float dis = 10.01f;

    void Start()
    {
        playerData = FindObjectOfType<PlayerData>();
        MainCameraON();
    }

    // Update is called once per frame
    void Update()
    {
        if (!ispolaroidCamera)
        {
            //main카메라일때만.
            MovePolaroidAim();
        }
        else
        {
            //Polaroid카메라일때. 화면 움직이기
            MoveBigPolaroidAim();
        }
    }

    #region 메인-폴라로이드 카메라 전환
    public void ChangeCamera()
    {
        if (ispolaroidCamera)
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
        //Turtle 가이드.  가이드 화살표들 꺼주기. 
        if (SceneManager.GetActiveScene().name.Contains("Turtle"))
        {
            FindObjectOfType<PicTurtleNavigationManager>().CallWhenPolaroidCameraOn();
        }



        //위치는 PolaroidAimObj의 위치. 단 z좌표는 -10 유지
        polaroidCamera.gameObject.transform.position = new Vector3(polaroidAimObj.transform.position.x, polaroidAimObj.transform.position.y, -10);

        polaroidListener.enabled = true;
        polaroidCamera.enabled = true;
        mainListener.enabled = false;
        mainCamera.enabled = false;
        ispolaroidCamera = true;
        mainUI.SetActive(false);
        polaroidUI.SetActive(true);
        polaroidAimObj.SetActive(false);
        ActivateNPA();
    }
    public void MainCameraON()
    {
        polaroidListener.enabled = false;
        polaroidCamera.enabled = false;
        mainListener.enabled = true;
        mainCamera.enabled = true;
        ispolaroidCamera = false;
        mainUI.SetActive(true);
        polaroidUI.SetActive(false);
        polaroidAimObj.SetActive(true);
        DeactivateNPA();
    }
    #endregion

    #region 조그만 폴라로이드 에임 움직이기    
    public void MovePolaroidAim()
    {
        //폴라로이드 에임 이동. 

        //if (Input.GetMouseButtonDown(0))  드래그시에도 따라 움직이게 하기 위해서 아래처럼 처리. 0619
        if (Input.GetMouseButton(0))
        {
            //안드로이드인 경우에는 EventSystem.curren.IsPointerOverGameObject 이것의 처리를 다르게 해주자!
            if(Application.platform == RuntimePlatform.Android)
            {
                if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
                {
                    //Debug.Log("UI를 만났어요!~ 폴라로이드 에임은 이동하지 않아요");
                }
                else
                {
                    Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    polaroidAimObj.transform.position = new Vector3(worldMousePos.x, worldMousePos.y);
                }
            }
            else
            {
                if (EventSystem.current.IsPointerOverGameObject())
                {
                    //Debug.Log("UI를 만났어요!~ 폴라로이드 에임은 이동하지 않아요");
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

    #region 확대된 폴라로이드 에임 움직이기
    public void MoveBigPolaroidAim()
    {
        //Before-Current 만큼 이동시키기 
        //작은폴라로이드에임도 실시간으로 변하는 위치 반영시켜주기

        if (Input.GetMouseButton(0))
        {
            if (Application.platform == RuntimePlatform.Android)
            {
                if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
                {
                    //Debug.Log("UI를 만났어요!~ 폴라로이드 에임은 이동하지 않아요");
                }
                else
                {
                    if (beforePos != Vector3.zero)
                    {
                        currentPos = polaroidCamera.ScreenToWorldPoint(Input.mousePosition);
                        Vector3 polaroidCamPos = polaroidCamera.transform.position;
                        Vector3 movedis = (beforePos - currentPos);
                        //큰 화면 이동
                        polaroidCamera.transform.position = new Vector3(polaroidCamPos.x + movedis.x, polaroidCamPos.y + movedis.y, -10f);
                        //조그만 에임도 변하는 위치 반영
                        polaroidAimObj.transform.position = new Vector2(polaroidCamera.transform.position.x, polaroidCamera.transform.position.y);
                        //Debug.Log("beforePos:" + beforePos + ", currentPos:" + currentPos + ", movedis:" + movedis);
                    }
                    beforePos = polaroidCamera.ScreenToWorldPoint(Input.mousePosition);
                }
            }
            else
            {
                if (EventSystem.current.IsPointerOverGameObject())
                {
                    //Debug.Log("UI를 만났어요!~ 폴라로이드 에임은 이동하지 않아요");
                }
                else
                {
                    if (beforePos != Vector3.zero)
                    {
                        currentPos = polaroidCamera.ScreenToWorldPoint(Input.mousePosition);
                        Vector3 polaroidCamPos = polaroidCamera.transform.position;
                        Vector3 movedis = (beforePos - currentPos);
                        polaroidCamera.transform.position = new Vector3(polaroidCamPos.x + movedis.x, polaroidCamPos.y + movedis.y, -10f);
                        polaroidAimObj.transform.position = new Vector2(polaroidCamera.transform.position.x, polaroidCamera.transform.position.y);
                        //Debug.Log("beforePos:" + beforePos + ", currentPos:" + currentPos + ", movedis:" + movedis);
                    }
                    beforePos = polaroidCamera.ScreenToWorldPoint(Input.mousePosition);
                }
            }                       
        }

        if (Input.GetMouseButtonUp(0))
        {
            //한 번 손이 떼지고, 드래그가 끝나면 다음 드래그를 새롭게 시작할 때
            //이전 드래그의 위치값이 관여하는일이 없도록 초기화 시켜준다. 
            beforePos = Vector3.zero;
            
        }


    }
    #endregion

    #region 카메라 촬영 기능 (폴라로이드 카메라 활성시 작동하는 부분.)
    // 1. 촬영 가능 오브젝트들은 중앙에 NoticePhotoAble (이하 NPA) 이미지를 띄운다.
    //   1. 유니티 인스펙터를 통해 등록받은 NPA 오브젝트들을 활용. SetActive. 

    public void DeactivateNPA()
    {
        foreach (var item in npa_Turtle) item.SetActive(false);
        foreach (var item in npa_Stone) item.SetActive(false);
        foreach (var item in npa_Wood) item.SetActive(false);
    }
    public void ActivateNPA()
    {
        foreach (var item in npa_Turtle) item.SetActive(true);

        //Stone 씬의 경우 조금 특별. 눈이 다 사라져야 돌이 드러난다. 
        //처음엔 스키(npa_Stone[0])만 보임.  npa_Stone[1] 돌은 스톤씬매니저에서. 활성화시켜줌.
        if (SceneManager.GetActiveScene().name.Contains("Stone"))
            npa_Stone[0].SetActive(true);
        
        
        foreach (var item in npa_Wood) item.SetActive(true);
    }

    // 2. 촬영버튼 클릭 시 처리.
    public void OnButton_TakePictureClicked()
    {
        //Flash 이펙트. 
        FindObjectOfType<CameraFlash>().ActiveCameraFlash();

        //폴라로이드 필름 카운트 1 차감. 차감에 성공 했다면 촬영버튼 처리 진행. 실패했다면 이미 남은 필름이 없는것. 촬영버튼 진행 안함.
        if (playerData.MinusPlayerFilmCount())
        {
            //에임 중앙과 촬영 가능 오브젝트의 중앙이 일치했는가? . 월드좌표 Distance로 계산. 10.001 이하면 일치했다고 봄. 

            //그림별로 따로 체크 

            //Turtle
            if (SceneManager.GetActiveScene().name.Contains("Turtle")) IsPicSuccess(npa_Turtle);

            //Stone
            else if (SceneManager.GetActiveScene().name.Contains("Stone")) IsPicSuccess(npa_Stone);

            //Wood
            else if (SceneManager.GetActiveScene().name.Contains("Wood")) IsPicSuccess(npa_Wood);           
        }
    }

    //촬영버튼 클릭시 사진이 제대로 찍혔나 확인하는 메서드. 
    private void IsPicSuccess(GameObject[] npa_arr)
    {
        foreach (var item in npa_arr)
        {
            if (Vector3.Distance(polaroidCamera.gameObject.transform.position, item.transform.position) <= dis)
            {
                //일치했다면 촬영 성공
                Debug.Log("촬영 성공" + Vector3.Distance(polaroidCamera.gameObject.transform.position, item.transform.position));

                //성공했으니까 성공 애니메이션 실행시켜주고 
                FindObjectOfType<ObjMove>().StartMoveToInven();

                //일단 NPA의 부모 오브젝트 비활성화 시켜주고
                item.transform.parent.gameObject.SetActive(false);


                //Turtle
                if (SceneManager.GetActiveScene().name.Contains("Turtle"))
                {
                    //turtle(npa_Turtle[1])이라면 추가적으로 배경 이미지를 NonTurtle 이미지로 변경.
                    if (npa_Turtle[1] == item)
                    {
                        backGroundImage.sprite = nonTurtleImg;

                        //가이드가 필요하면 가이드 시작. 알아서 하는 메서드 호출
                        FindObjectOfType<PicTurtleNavigationManager>().CallWhenTurtlePicTaken();
                    }
                }
                //Stone
                else if (SceneManager.GetActiveScene().name.Contains("Stone"))
                {
                    //Stone

                    //스톤씬은 추가적으로 눈사람의 상태를 체크해주는 과정이 필요함.  눈사람 상태 체크는 StoneSceneManager에서 해줌. 거기다 물어보기.
                    if(FindObjectOfType<StoneSceneManager>().idxcount >=4)
                    {
                        //현재 눈사람이 다 부숴지고 돌이 드러난 상태. 

                        //stone(npa_Stone[1])이라면 추가적으로 배경 이미지를 NonStone 이미지로 변경.
                        if (npa_Stone[1] == item) backGroundImage.sprite = nonStoneImg;
                    }

                    
                }
                //Wood
                else if (SceneManager.GetActiveScene().name.Contains("Wood"))
                {
                    //Wood
                    //Wood(npa_Wood[0])라면 쿵야 애니메이션 재생
                    if (npa_Wood[0] == item) humanAnimator.SetBool("Fall", true);
                    //fish(npa_Wood[1])라면 추가적으로 배경 이미지를 NonFish 이미지로 변경. 
                    else if (npa_Wood[1] == item) backGroundImage.sprite = nonFishImg;
                }               

                //PlayerData에 아이템 리스트 추가. npa의 부모 오브젝트 네임을 넣어주면 됨. 
                playerData.AddItem(item.transform.parent.gameObject.name);
                //그리고 인벤토리 갱신
                FindObjectOfType<InventoryController>().ResetInventory();
            }
            else
            {
                //일치하지 않았다면 촬영 실패
                Debug.Log("촬영 실패 Dis:" + Vector3.Distance(polaroidCamera.gameObject.transform.position, item.transform.position) + ", camera:" + polaroidCamera.gameObject.transform.position + ", obj:" + item.transform.position);
            }
        }
    }

    // 3. 확대축소 기능
    
    
    #endregion
}
