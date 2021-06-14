using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonTest : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    [SerializeField]
    Vector2 startPoint;

    [SerializeField]
    Vector2 endPoint;

    [SerializeField]
    bool drag = false;  //사용안됨?

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        startPoint = eventData.pressPosition;

        //드래그 시작.  그리드레이아웃에서 벗어난다. 
        gameObject.transform.SetParent(GameObject.Find("=====Inventory=====").transform);
        //gameObject.transform.SetParent(GameObject.Find("=====ObjectAreas=====").transform);

    }

    public void OnDrag(PointerEventData eventData)
    {
        drag = true;
        endPoint = eventData.position;

        //마우스 따라다님
        gameObject.transform.position = endPoint;
        GameObject EBox2 = GameObject.Find("EBox2");
        //Debug.Log("localposition:" + gameObject.transform.localPosition + "\nposition:" + gameObject.transform.position + "\nEBox2 worldpos:" + EBox2.transform.position + "\nEBOx2 screenpos:" + Camera.main.WorldToScreenPoint(EBox2.transform.position));
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        drag = false;

        //끝난 자리에 멈춤.        
    }

}
