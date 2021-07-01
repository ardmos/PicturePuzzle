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
    public GameObject[] EBoxes;
    //어떤 EBox가 하이라이트된건지 확인해주는 변수.   설정안된 기본값 -1 
    [SerializeField]
    int hlEBoxIndx = -1;

    //더블탭 가이드를 위한 
    public GameObject doubleTabGuidObj;

    void Start(){
        //Ebox들 초기화. 
        InitializeEBoxes();
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
    void InitializeEBoxes()
    {
        try
        {
            foreach (var item in EBoxes)
            {
                //각각 EBox에서 실시간 처리해주도록 했습니다. 여기선 물방울만!
                //노말
                //item.transform.GetChild(0).gameObject.SetActive(true);
                //하이라이트
                //item.transform.GetChild(1).gameObject.SetActive(false);
                //물방울
                item.transform.GetChild(3).gameObject.SetActive(false);
            }
        }
        catch (System.Exception ex)
        {
            Debug.Log(ex.Message);
        }        
    }
    #endregion

    #region EBox들 리셋(리트라이 버튼을 위한)
    public void ResetEBoxes()
    {
        //EBox들 isFull 초기화.
        try
        {
            foreach (var item in EBoxes)
            {
                //일단, 진행중인 코루틴(애니메이션들 종료)
                item.GetComponent<EBox>().StopsAllAnimationCoroutines();

                if (item.transform.childCount == 5)
                {
                    //배치된 아이템이 있다면, 해당 아이템 파괴 
                    Destroy(item.transform.GetChild(item.transform.childCount-1).gameObject);
                    Debug.Log("아이템이 존재합니다. 리셋을 위해 파괴!");
                }
                item.GetComponent<EBox>().SetFull(false);

                //EBox 스테이트 노말로 초기화.
                item.GetComponent<EBox>().SetEBoxState(EBox.EBoxState.Normal);
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
        if (hlEBoxIndx == -1)
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
                hlEBoxIndx = -1;
                for(int i = 0; i<EBoxes.Length; i++)
                {
                    float x = Mathf.Abs(dragItem.transform.position.x - Camera.main.WorldToScreenPoint(EBoxes[i].transform.position).x);
                    float y = Mathf.Abs(dragItem.transform.position.y - Camera.main.WorldToScreenPoint(EBoxes[i].transform.position).y);
                    if (x <= 200 & y <= 200)
                    {
                        //가까운 것. 하이라이트 온.
                        EBoxes[i].GetComponent<EBox>().SetEBoxState(EBox.EBoxState.HighLighted);
                        hlEBoxIndx = i;                        
                    }
                    else
                    {
                        //먼 것. 노말 온.
                        EBoxes[i].GetComponent<EBox>().SetEBoxState(EBox.EBoxState.Normal);
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
            if (EBoxes[hlEBoxIndx].GetComponent<EBox>().GetFull())
            {
                //혹시 이미 배치가 끝난 EBox인가? 
                //드래그중인 오브젝트만 다시 인벤토리로 보내고 끝. 
                dragObject.transform.SetParent(FindObjectOfType<InventoryController>().inventoryObj.transform.GetChild(1));
                EBoxes[hlEBoxIndx].GetComponent<EBox>().SetEBoxState(EBox.EBoxState.Normal);
                
            }
            else
            {
                //이미 배치가 끝난 EBox가 아니라면
                //배치되려는것이 옳은 아이템인가?

                if (EBoxes[hlEBoxIndx].GetComponent<EBox>().GetItemName() != dragObject.GetComponent<ItemObj>().itemName)
                {
                    //옳은 아이템이 아닌 경우 다시 인벤토리로
                    dragObject.transform.SetParent(FindObjectOfType<InventoryController>().inventoryObj.transform.GetChild(1));
                    EBoxes[hlEBoxIndx].GetComponent<EBox>().SetEBoxState(EBox.EBoxState.Normal);

                    //오답용 퐁당Anim 진행. 
                    //
                    //일단 배치 후 
                    SettingItem(dragObject);
                    //해당 EBox에 있는 배치 실패 애니메이션 실행
                    EBoxes[hlEBoxIndx].GetComponent<EBox>().StartSetFailAnim();
                }
                else
                {
                    //정답인 경우 배치 진행
                    SettingItem(dragObject);
                    //해당 EBox 배치 완료 표시
                    EBoxes[hlEBoxIndx].GetComponent<EBox>().SetFull(true);
                    //드래그아이템 삭제
                    Destroy(dragObject);

                    //더블탭 튜토리얼
                    //튜토리얼을 진행한적이 없는가?
                    if(FindObjectOfType<PlayerData>().eBoxSuccess == false)
                    {
                        //더블탭 튜토리얼 진행. 
                        doubleTabGuidObj.SetActive(true);
                        FindObjectOfType<PlayerData>().eBoxSuccess = true;
                    }
                }
            }                      
        }
        catch (System.Exception ex)
        {
            Debug.Log(ex.Message);
        }
    }

    void SettingItem(GameObject mobject)
    {
        //새로운 오브젝트 생성
        GameObject newObject = new GameObject();
        //부모 설정
        newObject.transform.SetParent(EBoxes[hlEBoxIndx].transform);
        //위치 설정
        newObject.transform.position = EBoxes[hlEBoxIndx].transform.position;
        //Item 정보 유지를 위한 전달. 
        newObject.AddComponent<ItemObj>();
        newObject.GetComponent<ItemObj>().SetItem(mobject.GetComponent<ItemObj>().GetItemObjInfo());        
    }
    #endregion

}
