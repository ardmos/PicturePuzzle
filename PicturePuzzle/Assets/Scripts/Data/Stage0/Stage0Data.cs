using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Stage0의 갤러리 미니사이즈 사진들을 저장하는곳. 
/// 
/// </summary>
public class Stage0Data : DontDestroy<Stage0Data>
{
    public SpriteRenderer squirrel;
    public Sprite sprite;

    public void SetSprite(Sprite img)
    {
        sprite = img;
    }


    #region 인스펙터에서 안보이는 PlayerData 내부의 정보 확인용. 디버깅용.
    void OnEnable()
    {
        // 씬 매니저의 sceneLoaded에 체인을 건다.
        SceneManager.sceneLoaded += OnSceneLoaded;

    }

    // 체인을 걸어서 이 함수는 매 씬마다 호출된다.
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //Debug.Log(npa_Stone_ActiveSelf.Length);
        //for (int i = 0; i < npa_Stone_ActiveSelf.Length; i++)
        //{
        //    Debug.Log(i+", "+npa_Stone_ActiveSelf[i]);
        //}
        if (SceneManager.GetActiveScene().name.Contains("Gallery0_0"))
        {
            squirrel = GameObject.Find("Test").GetComponent<SpriteRenderer>();
            squirrel.sprite = sprite;
        }
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    #endregion
}
