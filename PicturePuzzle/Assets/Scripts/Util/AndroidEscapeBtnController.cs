using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 안드로이드 플랫폼일 경우 Escape 버튼 처리
/// </summary>

public class AndroidEscapeBtnController : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                //현재 씬에 따라 처리. 
                switch (SceneManager.GetActiveScene().buildIndex)
                {
                    case 0:
                        //타이틀. 이어야하지만 지금은 갤러리.  타이틀 없는 상황. 아래도 마찬가지로 테스트 현재 상황 반영
                        Application.Quit();                        
                        break;
                    case 1:
                        //다람쥐                    
                    case 2:
                        //거북이
                    case 3:
                        //돌
                    case 4:
                        //나무
                        //모두 갤러리씬으로.
                        SceneManager.LoadScene(0);
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
