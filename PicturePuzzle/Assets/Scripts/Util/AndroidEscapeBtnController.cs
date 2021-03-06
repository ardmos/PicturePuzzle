using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 안드로이드 플랫폼일 경우 Escape 버튼 처리용도이지만, 원활한 테스트를 위해 데스크탑에서도 처리해줌. 
/// </summary>

public class AndroidEscapeBtnController : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        //if (Application.platform == RuntimePlatform.Android) 데스크탑에서도 해주기 위한 주석.
        //{
        if (Input.GetKey(KeyCode.Escape))
        {
            string curSceneName = SceneManager.GetActiveScene().name;
            //현재 씬에 따라 처리. 
            if (curSceneName.Contains("Title"))
            {
                //타이틀인 경우
                Application.Quit();
            }
            else if (curSceneName.Contains("Gallery"))
            {
                //갤러리인 경우
                SceneManager.LoadScene("Title");
            }
            else if (curSceneName.Contains("Pic"))
            {
                //사진인경우 각각 맞는 갤러리로 이동해야함. 다람쥐는 0번갤러리. 다른애들은 1번갤러리.
                if (curSceneName == "Pic0_0_Squirrel")
                {
                    SceneManager.LoadScene("Gallery0_0");
                    //상태저장 해주자.
                    FindObjectOfType<Pic0_0Manager>().SendDataToPlayerData();
                }
                else
                {
                    //폴라로이드상태면 메인카메라상태로 나간다.
                    if (FindObjectOfType<CameraController>().polaroidUI.activeSelf)
                    {
                        FindObjectOfType<CameraController>().MainCameraON();
                    }
                    else
                    {
                        SceneManager.LoadScene("Gallery0_1");

                        //각 그림 상태 저장을 위한 메서드 호출. 
                        if (curSceneName.Contains("Turtle")) { FindObjectOfType<TurtleSceneManager>().SendDataToPlayerData(); }
                        else if (curSceneName.Contains("Stone")) { FindObjectOfType<StoneSceneManager>().SendDataToPlayerData(); }
                        else if (curSceneName.Contains("Wood")) { FindObjectOfType<WoodSceneManager>().SendDataToPlayerData(); }
                    }                    
                }
            }
        }
        //}
    }
}
