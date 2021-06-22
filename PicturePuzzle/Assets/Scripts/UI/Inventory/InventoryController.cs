using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// ***인벤토리 컨트롤러***
///   1. 인벤토리 버튼 클릭시 등장. (기본적으론 숨겨져있음)
///   2. 아이템 드래그 or 바깥버튼 클릭시 퇴장.
///   3. 인벤토리 채우기
/// </summary>

public class InventoryController : MonoBehaviour
{
	public GameObject inventoryObj;
    // Start is called before the first frame update
    void Start()
    {
		//기본적으로 숨겨두기.
		inventoryObj.SetActive(false);
    }
 
    public void OpenInventory()
	{
		inventoryObj.SetActive(true);
	}
    public void CloseInventory()
	{
		inventoryObj.SetActive(false);
	}

    #region 인벤토리 채우기 
    // InventroyController.cs ,  playerData 로 진행. 
    //  플레이어데이타(정보 갖고있음), 리스트 형태로.  이름들.  
    // 이름들 바탕으로 아이템 생성. 
    // 어차피 이 게임에 겹치는 이름은 없음. 
    // 씬 시작될 때마다 호출.
    public void MakeInventoryItems()
    {

    }
    #endregion
}
