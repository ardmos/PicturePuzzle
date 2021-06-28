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
    #region Gallery0_0 부분. 
    //플레이어 스테이트 관리
    public enum PlayerSceneState
    {
        First,
        NotFirst,
        Stand,
    }
    //첫 재생 관리   Gallery0_0. 
    [SerializeField]
    PlayerSceneState playerSceneState;
    #endregion


    #region Gallery0_1 부분.
    //플레이어 위치 상태 Enum
    public enum PlayerPosState
    {
        Entry,
        Stand,
        Exit,
        Pic1,
        Pic2,
        Pic3,
        PicToExit
    }
    public PlayerPosState playerPosState;
    #endregion




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

        //첫 재생 관리. Gallery0_0
        //1. 타이틀씬이라면 스테이트 First
        //false 만드는거라면 다른곳에서 함. 신경 안써도 된다. 현재 씬이 타이틀인지 확인해서 true만 만들기
        if (scene.name.Equals("Title")) playerSceneState = PlayerSceneState.First;
        else if(scene.name.Contains("Pic")) playerSceneState = PlayerSceneState.Stand;

        //Gallery0_1 
        //그림 앞으로 이동 중, esc를 통한 씬 전환시, 위치 설정이 그림 앞으로 되어있는 문제 해결을 위한 부분. 
        if (scene.name.Equals("Title")) playerPosState = PlayerPosState.Entry;
        else if (scene.name.Equals("Gallery0_0")) playerPosState = PlayerPosState.Entry;
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
