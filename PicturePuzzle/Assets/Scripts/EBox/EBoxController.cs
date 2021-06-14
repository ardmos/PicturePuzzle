using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EBoxController : MonoBehaviour
{
    //드래그중인 오브젝트. 평소엔 null.
    GameObject dragItem = null;
    //드래그중인지
    bool isdragging = false;
    //EBox 1 2 3
    public GameObject[] Ebox;


    // Update is called once per frame
    void Update()
    {
        if (dragItem != null)
        {
            //드래그중인 아이템이 있으면. 위치감지시스템 시작.
            StartTrace();
        }
    }

    public void SetDragItem(GameObject dragObject)
    {
        //드래그 시작시 호출
        dragItem = dragObject;
        isdragging = true;

    }
    public void ReleaseDragItem(GameObject dragObject)
    {
        //놓은 곳 위치 체크를 해야함. 
        dragItem = null;
        isdragging = false;

        //어디서 놓았는지 확인 후 가까운 EBox가 있다면 배치 처리. 가까운 EBox가 없으면 아무 처리 안함

    }
    void StartTrace()
    {       
        if (isdragging)
        {
            //드래그중일때만 추적. EBox와 가까워지면 해당 EBox 하이라이트.

            foreach (var item in Ebox)
            {
                float x = Mathf.Abs(dragItem.transform.position.x - Camera.main.WorldToScreenPoint(item.transform.position).x);
                float y = Mathf.Abs(dragItem.transform.position.y - Camera.main.WorldToScreenPoint(item.transform.position).y);
                if(x<=200 & y <= 200)
                {
                    //가까운 것. 하이라이트 온.
                    item.transform.GetChild(0).gameObject.SetActive(false);
                    item.transform.GetChild(1).gameObject.SetActive(true);
                }
                else
                {
                    //먼 것. 노말 온.
                    item.transform.GetChild(0).gameObject.SetActive(true);
                    item.transform.GetChild(1).gameObject.SetActive(false);
                }
            }

            

        }
    }
}
