﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// ***인벤토리 컨트롤러***
///   1. 인벤토리 버튼 클릭시 등장. (기본적으론 숨겨져있음)
///   2. 아이템 드래그 or 바깥버튼 클릭시 퇴장.
///   3. 아이템 드래그해서 배치. 성공시 폴라로이드에서 빠져나와 배치됨.
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

    //아이템 만들 차례! 아이템 만들고, 여기 버튼들 기능 연ㅔ

    public void OpenInventory()
	{
		inventoryObj.SetActive(true);
	}
    public void CloseInventory()
	{
		inventoryObj.SetActive(false);
	}






    public void PointerDown()
	{
		Debug.Log("PointerDown");
	}
	public void PointerUp()
    {

    }
	public void PointerClick()
    {

    }
	public void PointerEnter()
    {

    }
	public void PointerExit()
    {

    }
	public void BeginDrag()
    {

    }
    public void OnMouseDrag()
    {
        Debug.Log("00");
    }

    public void DragTest(PointerEventData data)
	{
		Debug.Log(data + "Dragged!" + gameObject.transform.position);
	}
 
}
