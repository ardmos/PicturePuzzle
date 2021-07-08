using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Camera <-> PolaroidCamera 모드 바꾸기 가이드 실행시켜주는 스크립트. 
/// </summary>

public class ModeGuide : MonoBehaviour
{
    public void Start()
    {
        //가이드는 한 번만 나와야하니까 
        if (FindObjectOfType<PlayerData>().guide_ChangeCameraMode == true)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
            FindObjectOfType<PlayerData>().guide_ChangeCameraMode = true;
        }

    }

    public void OnScreenClicked()
    {
        gameObject.SetActive(false);
    }
}
