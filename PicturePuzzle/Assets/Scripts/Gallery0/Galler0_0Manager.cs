using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Gallery 0_0과 0_1 모두를 총괄하는 매니저 스크립트.
/// 
/// 1. 각 그림들 클릭시 그림 씬으로 화면전환.
/// 2. Gallery0_0과 0_1을 오가는 버튼의 관리.
/// 
/// </summary>

public class Galler0_0Manager : MonoBehaviour
{
    public Player_Gallery0_1 player_Gallery0_1;


    #region 그림들 여는 버튼
    public void Button_Squirrel()
    {
        //"Pic0_0_Squirrel"
        SceneManager.LoadScene(3);
    }
    public void Button_Turtle()
    {
        //"Pic0_1_Turtle"
        //SeneManager.LoadScene(4); player 애니메이션이 추가됐음.
        player_Gallery0_1.MoveToPic1();
    }
    public void Button_Stone()
    {
        //"Pic0_2_Stone"
        //SceneManager.LoadScene(5);
        player_Gallery0_1.MoveToPic2();
    }
    public void Button_Wood()
    {
        //"Pic0_3_Wood"
        //SceneManager.LoadScene(6);
        player_Gallery0_1.MoveToPic3();
    }
    #endregion

    #region 갤러리0_0과 갤러리0_1의 화면 전환 버튼
    public void To1From0()
    {
        //이동 애니메이션은 Player_Gallery0_0.cs에 있음. 
        //0_0에서 0_1로 이동. First 상태로 만들어주고 넘어감. 
        FindObjectOfType<PlayerSceneStateController>().SetPlayerSceneState(PlayerSceneStateController.PlayerSceneState.First);
        SceneManager.LoadScene("Gallery0_1");
    }
    public void To0From1()
    {
        //이동 애니메이션은 Player_Gallery0_1.cs에 있음. 
        //0_1에서 0_0으로 이동
        SceneManager.LoadScene("Gallery0_0");
    }
    #endregion    

    public void ToTitleFrom0()
    {
        //0_0에서 타이틀로 이동. 
        SceneManager.LoadScene("Title");
    }
}
