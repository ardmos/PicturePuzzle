using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 아이템 클래스.  
/// 이름
/// 폴라로이드용IMG
/// 오브젝트용IMG
/// 부모 인식 이미지 자동변환 기능
/// </summary>

[System.Serializable]
public class ItemObj : MonoBehaviour
{
    //이름
    public string itemName;
    //폴라로이드용IMG, 오브젝트용IMG
    public Sprite polaroidIMG, objectIMG;

    // Start is called before the first frame update
    void Start()
    {
        InitIMG();
        gameObject.name = itemName;
    }
    public void SetItem(Item item)
    {
        //아이템 정보 전달.
        itemName = item.itemName;
        polaroidIMG = item.polaroidIMG;
        objectIMG = item.objectIMG;
    }

    public Item GetItemObjInfo()
    {
        //현 아이템오브젝트의 정보를 Item클래스 형태로 얻고싶을 때 호출. 
        return new Item(itemName, polaroidIMG, objectIMG);
    }

    void InitIMG()
    { 
        //부모 인식 이미지 자동 변환
        if(transform.parent.gameObject.transform == FindObjectOfType<InventoryController>().inventoryObj.transform.GetChild(1))
        {
            //인벤토리에 들어있을 때
            try
            {
                if (gameObject.GetComponent<Image>() == null) gameObject.AddComponent<Image>();
                gameObject.GetComponent<Image>().sprite = polaroidIMG;
            }
            catch (System.Exception ex)
            {
                Debug.Log(ex.Message);
            }            
        }
        else
        {
            //그 외의 경우. 인벤토리에서 나와있을 때. EBox에 배치된 경우.
            //SpriteRenderer 레이어 설정
            try
            {                
                if (gameObject.GetComponent<SpriteRenderer>() == null) gameObject.AddComponent<SpriteRenderer>();
                gameObject.GetComponent<SpriteRenderer>().sprite = objectIMG;
                gameObject.GetComponent<SpriteRenderer>().sortingLayerName = "BackGround";
                gameObject.GetComponent<SpriteRenderer>().sortingOrder = 2;
            }
            catch (System.Exception ex)
            {
                Debug.Log(ex.Message);
            }            
        }
    }
}
