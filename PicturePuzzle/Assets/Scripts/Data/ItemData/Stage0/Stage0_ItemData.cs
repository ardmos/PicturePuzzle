using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 스테이지0에 존재하는 모든 아이템 정보를 담고있는 스크립트. 
/// 
/// 다른곳에서 요청이 들어오면 해당 아이템 정보를 Item 클래스 형태로 반환해준다.
/// 
/// Item클래스의 정보
/// 1. 이름
/// 2. 폴라로이드이미지
/// 3. 오브젝트이미지
/// 
/// </summary>

public class Stage0_ItemData : DontDestroy<Stage0_ItemData>
{
    [SerializeField]
    List<Item> itemData;


    #region Get 요청 처리
    public Item GetItem(string itemName)
    {
        //못찾으면 null 반환
        return itemData.Find(item => item.itemName == itemName);
    }
    #endregion
}
