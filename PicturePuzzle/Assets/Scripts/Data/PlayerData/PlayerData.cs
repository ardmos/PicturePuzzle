using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


/// <summary>
/// 플레이어데이타(정보 갖고있음), 리스트 형태로.  이름들.
/// 
/// </summary>

public class PlayerData : DontDestroy<PlayerData> 
{
    //플레이어가 보유한 아이템 리스트!
    [SerializeField]
    List<string> itemlist;
    //플레이어가 보유한 필름 카운트!
    [SerializeField]
    int filmCount = 8;
    //PicData _ 각 그림 씬 매니저로부터 보고 받음.  그림SceneManager(PlayerData에 기록) 그리고 씬이 종료될 때 -> PlayerData(최종 저장)
    //isrecorded로 씬이 처음 열린건지 열렸다 닫힌적이 있어서 저장된 씬 상태 데이터가 있는지 확인이 가능.
    //스테이지0
    //Turtle
    [Header("//Turtle")]
    //현재 배경 이미지 오브젝트, 이미지.
    public Sprite curBGObj_Turtle;
    //NPA들.  NPA 인덱스값, SetActive 상태값.
    public bool[] npa_Turtle_ActiveSelf;
    public bool isrecorded_Turtle;

    //Stone
    [Header("//Stone")]
    //현재 배경 이미지 오브젝트, 이미지.
    public Sprite curBGObj_Stone;
    //NPA들.  NPA 인덱스값, SetActive 상태값.
    public bool[] npa_Stone_ActiveSelf;
    public bool isrecorded_Stone;
    //눈사람 부수기 위한 카운트
    public int idxcount;

    //Wood
    [Header("//Wood")]
    //현재 배경 이미지 오브젝트, 이미지.
    public Sprite curBGObj_Wood;
    //NPA들.  NPA 인덱스값, SetActive 상태값.
    public bool[] npa_Wood_ActiveSelf;
    //애니메이션 상태값. 사람 넘어지는 애니메이션 bool값.
    public bool isFallTrue;
    public bool isrecorded_Wood;


    //스테이지1



    //스테이지2




    override protected void OnAwake()
    {
        npa_Turtle_ActiveSelf = new bool[2];
        npa_Stone_ActiveSelf = new bool[2];
        npa_Wood_ActiveSelf = new bool[2];
    }
    override protected void OnStart()
    {
        //테스트용
        //itemlist.Add("Turtle");
        //itemlist.Add("Stone");
        //itemlist.Add("Wood");
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
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    #endregion


    #region 아이템 Add Delete
    public void AddItem(string itemName)
    {
        itemlist.Add(itemName);
    }
    public void DeleteItem(string itemName)
    {
        itemlist.Remove(itemName);
    }
    #endregion

    #region 총 보유 아이템 리스트 Get
    public List<string> GetItemList()
    {
        return itemlist;
    }
    #endregion

    #region 폴라로이드 필름 Get Set Add Minus
    public int GetPlayerFilmCount()
    {
        return filmCount;
    }
    public void SetPlayerFilmCount(int n)
    {
        filmCount = n;
    }
    public void AddPlayerFilmCount()
    {
        filmCount++;
    }

    //마이너스에 성공하면 true, 실패하면 false 반환. 
    public bool MinusPlayerFilmCount()
    {
        if (filmCount == 0)
        {
            Debug.Log("필름이 없습니다!");
            filmCount = 0;
            return false;
        }
        else filmCount--; return true;
    }
    #endregion



}
