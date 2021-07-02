using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitFromPicBtn : MonoBehaviour
{
    public void OnBtnClicked()
    {

        ///일단 Stage0 그림들 전용 나가기 버튼

        string curSceneName = SceneManager.GetActiveScene().name;
        //현재 씬에 따라 처리. 
        if (curSceneName.Contains("Pic"))
        {
            //사진인경우 각각 맞는 갤러리로 이동해야함. 다람쥐는 0번갤러리. 다른애들은 1번갤러리.
            if (curSceneName == "Pic0_0_Squirrel")
            {
                SceneManager.LoadScene("Gallery0_0");
            }
            else
            {
                SceneManager.LoadScene("Gallery0_1");

                //각 그림 상태 저장을 위한 메서드 호출. 
                if (curSceneName.Contains("Turtle")) FindObjectOfType<TurtleSceneManager>().SendDataToPlayerData();

                else if (curSceneName.Contains("Stone")) FindObjectOfType<StoneSceneManager>().SendDataToPlayerData();

                else if (curSceneName.Contains("Wood")) FindObjectOfType<WoodSceneManager>().SendDataToPlayerData();
            }
        }
    }
}
