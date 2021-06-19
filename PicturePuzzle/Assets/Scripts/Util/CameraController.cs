using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 카메라 전환과 메인카메라에서 폴라로이드카메라영역 이동 처리를 담당하는 카메라 컨트롤러.
/// 1. 메인 카메라와 폴라로이드 카메라 화면 전환 기능
/// 2. 화면 전환시 오디오 리스너도 전환.
/// 3. 화면 전환시 UI세트도 전환.
/// 4. 메인카메라일 시 화면 터치되는 위치로 폴라로이드카메라 영역 이동. ispolaroidCamera를 이용해 판단. 드래그로도 이동 가능하도록 수정 0619
/// 5. 확대된 상태에서도 움직일 수 있게 
///
///
/// 카메라 촬영 기능 만들 차례임.  잠깐 이미지 넣고 옴!
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
    void Start()
    {
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
                        polaroidCamera.transform.position = new Vector3(polaroidCamPos.x + movedis.x, polaroidCamPos.y + movedis.y, -10f);
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
}
