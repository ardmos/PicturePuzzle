using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 카메라 전환을 담당하는 카메라 컨트롤러.
/// 1. 메인 카메라와 폴라로이드 카메라 화면 전환 기능
/// 2. 화면 전환시 오디오 리스너도 전환.
/// 3. 화면 전환시 UI세트도 전환.
/// 
/// </summary>

public class CameraController : MonoBehaviour
{
    public Camera mainCamera, polaroidCamera;
    public AudioListener mainListener, polaroidListener;
    public bool ispolaroidCamera;

    // Start is called before the first frame update
    void Start()
    {
        MainCameraON();
    }

    // Update is called once per frame
    void Update()
    {
        
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
        polaroidListener.enabled = true;
        polaroidCamera.enabled = true;
        mainListener.enabled = false;
        mainCamera.enabled = false;
        ispolaroidCamera = true;
    }
    public void MainCameraON()
    {
        polaroidListener.enabled = false;
        polaroidCamera.enabled = false;
        mainListener.enabled = true;
        mainCamera.enabled = true;
        ispolaroidCamera = false;
    }
}
