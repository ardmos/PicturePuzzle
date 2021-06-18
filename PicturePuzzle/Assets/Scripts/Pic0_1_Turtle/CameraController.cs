using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 카메라 전환과 메인카메라에서 폴라로이드카메라영역 이동 처리를 담당하는 카메라 컨트롤러.
/// 1. 메인 카메라와 폴라로이드 카메라 화면 전환 기능
/// 2. 화면 전환시 오디오 리스너도 전환.
/// 3. 화면 전환시 UI세트도 전환.
/// 4. 메인카메라일 시 화면 터치되는 위치로 폴라로이드카메라 영역 이동. ispolaroidCamera를 이용해 판단. 
///
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
    // Start is called before the first frame update
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
    }

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

    public void MovePolaroidAim()
    {                
        if (Input.GetMouseButtonDown(0))
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
