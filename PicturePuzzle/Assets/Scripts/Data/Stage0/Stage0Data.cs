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
    //Textture를 통해 현재 그림의 상태를 갤러리에 띄운다.
    public Texture texture0;



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
            //그림씬으로부터 전달받은 텍스쳐가 있는 경우에만.
            if (texture0 != null)
            {
                GameObject.Find("SquirrelPic").GetComponent<RawImage>().enabled = true;
                GameObject.Find("SquirrelPic").GetComponent<RawImage>().texture = texture0;
            }
            //없으면 텍스쳐 끄기
            else GameObject.Find("SquirrelPic").GetComponent<RawImage>().enabled = false;
        }
        else if (SceneManager.GetActiveScene().name.Contains("Gallery0_1"))
        {

        }
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    #endregion
}
