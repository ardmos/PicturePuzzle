using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 플레이어 스테이트 관리 컨트롤러.
/// 
///  - 첫 재생 관련. 
///    타이틀씬으로 가면 첫 재생으로 변환.
///    (이후 첫 재생 애니메이션이 끝나는 부분에서 첫 재생 아닌걸로 자동 체크함. 그건 신경 안써도 됨.)
///  
///  - 그림보러가면 스탠드 스테이트로 변환.
/// </summary>

public class PlayerSceneStateController : DontDestroy<PlayerSceneStateController>
{
    //플레이어 스테이트 관리
    public enum PlayerSceneState
    {
        First,
        NotFirst,
        Stand,        
    }

    //첫 재생 관리    
    [SerializeField]
    PlayerSceneState playerSceneState;

    #region 첫 재생 관리
    void OnEnable()
    {
        //Debug.Log(SceneManager.GetActiveScene().name);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //Debug.Log("OnSceneLoaded: " + scene.name);
        //Debug.Log(mode);

        //첫 재생 관리. 
        //1. 타이틀씬이라면 스테이트 First
        //false 만드는거라면 다른곳에서 함. 신경 안써도 된다. 현재 씬이 타이틀인지 확인해서 true만 만들기
        if (scene.name.Equals("Title")) playerSceneState = PlayerSceneState.First;
        else if(scene.name.Contains("Pic")) playerSceneState = PlayerSceneState.Stand;
    }
    // called when the game is terminated
    void OnDisable()
    {
        //Debug.Log("OnDisable");
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    public PlayerSceneState GetPlayerSceneState()
    {
        return playerSceneState;
    }
    public void SetPlayerSceneState(PlayerSceneState value)
    {
        playerSceneState = value;
    }

    #endregion
}
