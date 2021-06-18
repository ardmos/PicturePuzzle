using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 1. EBox 초기화
/// 2. 아이템의 DragMaker로부터 보고받는 부분. (드래그 시작, 드래그 끝)
/// 3. 아이템 위치 추적
/// 4. EBox에 아이템 배치 처리
/// </summary>

public class EBoxController : MonoBehaviour
{
    //드래그중인 오브젝트. 평소엔 null.
    GameObject dragItem;
    //드래그중인지
    bool isdragging;
    //EBox 1 2 3
    public GameObject[] Eboxes;
    //어떤 EBox가 하이라이트된건지 확인해주는 변수.    
    public GameObject hlEbox;

    void Start(){
        //Ebox들 초기화. 
        InitializeEBox();
    }

    // Update is called once per frame
    void Update()
    {
        if (dragItem != null)
        {
            //드래그중인 아이템이 있으면. 위치감지시스템 시작.
            StartTrace();
        }
    }

    #region EBox 초기화
    void InitializeEBox()
    {
        try
        {
            foreach (var item in Eboxes)
            {
                item.transform.GetChild(0).gameObject.SetActive(true);
                item.transform.GetChild(1).gameObject.SetActive(false);
            }
        }
        catch (System.Exception ex)
        {
            Debug.Log(ex.Message);
        }        
    }
    #endregion

    #region Item의 DragMaker로부터 호출되는 메서드
    public void SetDragItem(GameObject dragObject)
    {
        //드래그 시작시 호출
        dragItem = dragObject;
        isdragging = true;

    }
    public void ReleaseDragItem(GameObject dragObject)
    {        
        //어디서 놓았는지 확인 후 가까운 EBox가 있다면 배치 처리. 가까운 EBox가 없으면 다시 인벤토리로 되돌리기.
        //일단 hlEbox 찾아감.  -> 진짜 하이라이트 되어있는지에 따른 처리.
        if (hlEbox == null)
        {
            //하이라이트되어있지않은 상태. 다시 인벤토리로.
            dragObject.transform.SetParent(FindObjectOfType<InventoryController>().inventoryObj.transform.GetChild(1));
        }
        else
        {
            //하이라이트된 상태. 해당 Ebox위치에 이미지 배치.
            SetItemOnWorld(dragObject);            
        }

        dragItem = null;
        isdragging = false;
        
    }
    #endregion

    #region 아이템 위치 추적
    void StartTrace()
    {
        try
        {
            if (isdragging)
            {
                //드래그중일때만 추적. EBox와 가까워지면 해당 EBox 하이라이트.
                hlEbox = null;
                foreach (var item in Eboxes)
                {
                    float x = Mathf.Abs(dragItem.transform.position.x - Camera.main.WorldToScreenPoint(item.transform.position).x);
                    float y = Mathf.Abs(dragItem.transform.position.y - Camera.main.WorldToScreenPoint(item.transform.position).y);
                    if (x <= 200 & y <= 200)
                    {
                        //가까운 것. 하이라이트 온.
                        item.GetComponent<EBox>().SetEBoxState(EBox.EBoxState.HighLighted);
                        hlEbox = item;
                    }
                    else
                    {
                        //먼 것. 노말 온.
                        item.GetComponent<EBox>().SetEBoxState(EBox.EBoxState.Normal);
                    }
                }
            }
        }
        catch (System.Exception ex)
        {
            Debug.Log(ex.Message);
        }
    }
    #endregion

    #region 아이템 배치
    void SetItemOnWorld(GameObject dragObject)
    {
        try
        {
            //옳은 아이템인가?
            if (hlEbox.GetComponent<EBox>().GetItemName() != dragObject.GetComponent<Item>().itemName)
            {
                //옳은 아이템이 아닌 경우 다시 인벤토리로
                dragObject.transform.SetParent(FindObjectOfType<InventoryController>().inventoryObj.transform.GetChild(1));
                hlEbox.GetComponent<EBox>().SetEBoxState(EBox.EBoxState.Normal);
            }
            else
            {
                //정답인 경우 배치 진

                //새로운 오브젝트 생성
                GameObject newObject = new GameObject();
                //부모 설정
                newObject.transform.SetParent(hlEbox.transform);
                //위치 설정
                newObject.transform.position = hlEbox.transform.position;                                
                //Item 정보 유지를 위한 전달. 
                newObject.AddComponent<Item>();
                newObject.GetComponent<Item>().SetItem(dragObject.GetComponent<Item>());
                //해당 EBox 배치 완료 표시
                hlEbox.GetComponent<EBox>().SetFull();                
                //드래그아이템 삭제
                Destroy(dragObject);
            }
            

        }
        catch (System.Exception ex)
        {
            Debug.Log(ex.Message);
        }
    }
    #endregion

}
