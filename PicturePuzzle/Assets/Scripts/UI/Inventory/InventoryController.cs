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
	public GameObject inventoryObj, inventoryGridLayoutObj;
    public PlayerData playerData;
    public Stage0_ItemData stage0_ItemData;
    // Start is called before the first frame update
    void Start()
    {
		//기본적으로 숨겨두기.
		inventoryObj.SetActive(false);

        //씬 시작되면 인벤토리 채우기 실행
        MakeInventoryItems();
    }
 
    public void OpenInventory()
	{
		inventoryObj.SetActive(true);
        FindObjectOfType<PicSquirrelNaviagtionManager>().CallWhenInventoryOpened();
    }
    public void CloseInventory()
	{
		inventoryObj.SetActive(false);
	}

    #region 인벤토리 초기화
    public void ResetInventory()
    {
        // 인벤토리 초기화 메서드. (재도전, 아이템 습득시 호출)

        //   1.인벤토리 아이템들 전부 삭제
        for(int n = inventoryGridLayoutObj.transform.childCount - 1; n>=0; n--)
        {
            //뒤에서부터 삭제. 
            Destroy(inventoryGridLayoutObj.transform.GetChild(n).gameObject);
        }

        //   2.인벤토리 채우기
        MakeInventoryItems();
    }
    #endregion

    #region 인벤토리 채우기 
    /// <summary>
    /// InventroyController.cs ,  playerData 로 진행. 
    ///  플레이어데이타(정보 갖고있음), 리스트 형태로.  이름들.  
    /// 이름들 바탕으로 아이템 생성. 
    /// 어차피 이 게임에 겹치는 이름은 없음. 
    /// 씬 시작될 때마다 호출.
    /// 
    /// ItemData 에 현 스테이지에 존재하는 모든 아이템의 정보가 들어있음.  여기에 요청하면 정보가 담긴 아이템 클래스를 받을 수 있음. 
    /// 그 클래스를 넣어서 새롭게 만들어주면 됨. 
    /// 
    /// </summary>
    public void MakeInventoryItems()
    {
        //플레이어가 보유중인 아이템 이름이 담긴 리스트를 받아와서
        playerData = FindObjectOfType<PlayerData>();
        List<string> itemNameList = playerData.GetItemList();

        //해당되는 이름을 가진 아이템의 정보를 ItemData에서 받아다가 실제 아이템 오브젝트를 만든다. 
        stage0_ItemData = FindObjectOfType<Stage0_ItemData>();

        foreach (string itemName in itemNameList)
        {
            //이름으로 아이템 정보 찾아오면, 인벤토리 그리드 레이아웃을 부모로 하는 새로운 오브젝트를 만들어줍니다. 
            //못찾으면 null 반환
            if (stage0_ItemData.GetItem(itemName) == null)
            {
                Debug.Log(itemName + " <-- 아이템 이름을 다시 확인해주세요! 해당 이름의 아이템이 ItemData에 존재하지 않습니다.");
                //break;
                return;
            }
            Item item = stage0_ItemData.GetItem(itemName);

            //새로운 오브젝트 생성
            GameObject newObject = new GameObject();
            //부모 설정
            newObject.transform.SetParent(inventoryGridLayoutObj.transform);
            //위치 설정
            newObject.transform.localPosition = Vector3.zero;
            //Item 정보 유지를 위한 전달. 
            newObject.AddComponent<ItemObj>();
            newObject.GetComponent<ItemObj>().SetItem(item);
            //Drag 기능 추가를 위한 드래그 컴포넌트 추가
            newObject.AddComponent<DragMaker>();
            //Image 컴포넌트는 추가 안해줘도 ItemObj가 알아서 추가합니다.  InitIMG() 확인.
        }
    }
    #endregion
}
