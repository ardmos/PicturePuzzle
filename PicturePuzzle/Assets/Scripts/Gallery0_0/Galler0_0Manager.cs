using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Galler0_0Manager : MonoBehaviour
{
    #region 그림들 여는 버튼
    public void Button_Squirrel()
    {
        //"Pic0_0_Squirrel"
        SceneManager.LoadScene(1);
    }
    public void Button_Turtle()
    {
        //"Pic0_1_Turtle"
        SceneManager.LoadScene(2);
    }
    public void Button_Stone()
    {
        //"Pic0_2_Stone"
        SceneManager.LoadScene(3);
    }
    public void Button_Wood()
    {
        //"Pic0_3_Wood"
        SceneManager.LoadScene(4);
    }
    #endregion
}
