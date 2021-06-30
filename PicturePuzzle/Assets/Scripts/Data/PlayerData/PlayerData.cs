using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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

    override protected void OnStart()
    {
        //테스트용
        //itemlist.Add("Turtle");
        //itemlist.Add("Stone");
        //itemlist.Add("Wood");
    }

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
