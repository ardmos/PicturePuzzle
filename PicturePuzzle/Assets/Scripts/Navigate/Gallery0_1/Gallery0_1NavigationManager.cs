using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 길안내. 
/// 
/// 다람쥐 미션 받았고, 갤러리0_0 오른쪽버튼 활성화도 했고. 한 경우 
/// 거북이 그림으로 안내 시작. 
/// 
/// </summary>

public class Gallery0_1NavigationManager : MonoBehaviour
{
    public GameObject picTurtle, picStone, picWood, arrowObj;

    // Start is called before the first frame update
    void Start()
    {
        if (FindObjectOfType<PlayerData>().guide_isNotFirstSquirrel == true &&
    FindObjectOfType<PlayerData>().guide_Gallery0_0RightBtn_isDone == true &&
    FindObjectOfType<PlayerData>().guide_Gallery0_1TurtlePic_isDone == false)
        {
            FindObjectOfType<PlayerData>().guide_Gallery0_1TurtlePic_isDone = true;
            arrowObj.SetActive(true);
            picStone.SetActive(false);
            picWood.SetActive(false);
        }
        else { arrowObj.SetActive(false);
            picStone.SetActive(true);
            picWood.SetActive(true);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
