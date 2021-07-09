using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Stage0의 갤러리 미니사이즈 사진들을 저장하는곳. 
/// 
/// </summary>
public class Stage0Data : DontDestroy<Stage0Data>
{
    //스프라이트ㄹ 통해 현재 그림의 상태를 갤러리에 띄운다.    
    //Gallery0
    public Sprite sprite_Squirrel; // 다람쥐
    //Gallery1
    public Sprite sprite_Turtle, sprite_Stone, sprite_Wood;

    #region 인스펙터에서 안보이는 내부의 정보 확인용. 디버깅용.
    void OnEnable()
    {
        // 씬 매니저의 sceneLoaded에 체인을 건다.
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // 체인을 걸어서 이 함수는 매 씬마다 호출된다.
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (SceneManager.GetActiveScene().name.Contains("Gallery0_0"))
        {
            if (sprite_Squirrel != null)
            {                
                //GameObject.Find("SquirrelPic").GetComponent<SpriteRenderer>().sprite = sprite_Squirrel;
            }
        }
        else if (SceneManager.GetActiveScene().name.Contains("Gallery0_1"))
        {
            if (sprite_Turtle != null)
            {
                //GameObject.Find("TurtlePic").GetComponent<SpriteRenderer>().sprite = sprite_Turtle;
            }
            if (sprite_Stone != null)
            {
                //GameObject.Find("StonePic").GetComponent<SpriteRenderer>().sprite = sprite_Stone;
            }
            if (sprite_Wood != null)
            {
                //GameObject.Find("WoodPic").GetComponent<SpriteRenderer>().sprite = sprite_Wood;
            }
        }
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    #endregion
}
