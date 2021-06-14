using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EBoxController : MonoBehaviour
{
    //드래그중인 오브젝트. 평소엔 null.
    GameObject dragItem = null;
    //드래그중인지
    bool isdragging = false;

    // Start is called before the first frame update
    void Start()
    {
        
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

    public void SetDragItem(GameObject dragObject)
    {
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


        }
    }
}
