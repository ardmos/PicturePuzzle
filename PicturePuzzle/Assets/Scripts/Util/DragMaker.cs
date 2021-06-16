using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 드래그 메이커. 
/// 이걸 붙이면 드래그가 가능해짐.
/// Item용.
///
/// -드래그 시작
///   1. 부모 전환
///   2. EboxController에 드래그 시작된것을 알림
///   3. 인벤토리 닫기
/// -드래그 중
///   1. 마우스의 위치로 오브젝트 위치 이동
/// -드래그 끝
///   1. EboxController에 드래그 끝난것을 알
/// </summary>

public class DragMaker : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField]
    Vector2 startPoint;

    [SerializeField]
    Vector2 endPoint;

    //[SerializeField]
    //bool drag = false;  //사용안됨?

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        startPoint = eventData.pressPosition;

        //드래그 시작.  그리드레이아웃에서 벗어난다. 
        gameObject.transform.SetParent(GameObject.Find("=====TMP=====").transform);
        //EBoxController에 드래그 시작된걸 알림. 
        FindObjectOfType<EBoxController>().SetDragItem(gameObject);
        //Inventory를 닫아준다.
        FindObjectOfType<InventoryController>().CloseInventory();

    }

    public void OnDrag(PointerEventData eventData)
    {

        //drag = true;
        endPoint = eventData.position;

        //마우스 따라다님
        gameObject.transform.position = endPoint;
        //GameObject EBox2 = GameObject.Find("EBox2");
        //Debug.Log("localposition:" + gameObject.transform.localPosition + "\nposition:" + gameObject.transform.position + "\nEBox2 worldpos:" + EBox2.transform.position + "\nEBOx2 screenpos:" + Camera.main.WorldToScreenPoint(EBox2.transform.position));
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //drag = false;

        //끝난 자리에 멈춤.    
        //EBoxController에 드래그 끝났다고 알려줘야함
        FindObjectOfType<EBoxController>().ReleaseDragItem(gameObject);
    }

}
