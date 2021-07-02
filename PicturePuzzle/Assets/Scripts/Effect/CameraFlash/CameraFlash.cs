using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 카메라 찍는 플래시 이펙트 애니메이션을 관리하는 스크립트.
/// 
/// </summary>


public class CameraFlash : MonoBehaviour
{
    //외부에서 얘 호출하면 됨. ex CameraController.
    public void ActiveCameraFlash()
    {
        //플래시 발동.
        GetComponent<Animator>().SetTrigger("Flash");
    }
}
