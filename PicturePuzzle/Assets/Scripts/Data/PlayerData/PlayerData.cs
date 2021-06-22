using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 플레이어데이타(정보 갖고있음), 리스트 형태로.  이름들.
/// </summary>

public class PlayerData : MonoBehaviour
{
    //플레이어가 보유한 아이템 리스트!
    [SerializeField]
    List<string> itemlist;

    private void Start()
    {
        //테스트용
        itemlist.Add("Turtle");
        itemlist.Add("Stone");
        itemlist.Add("Wood");
    }

    #region 아이템 추가 삭제
    public void AddItem(string itemName)
    {
        itemlist.Add(itemName);
    }
    public void DeleteItem(string itemName)
    {
        itemlist.Remove(itemName);
    }
    #endregion

    #region 아이템 정보 읽어오기
    public List<string> GetItemList()
    {
        return itemlist;
    }
    #endregion



}
