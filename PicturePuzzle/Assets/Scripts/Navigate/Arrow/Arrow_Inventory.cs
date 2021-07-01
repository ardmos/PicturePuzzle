using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 네비게이션 화살표 중.  인벤토리를 가르키는 화살표 처리.
/// 
/// 까만 화면을 누르면 현 네비게이션 비활성화.
/// 다른 네비게이션에서도 사용 가능. 
/// </summary>

public class Arrow_Inventory : MonoBehaviour
{
    public void Start()
    {
        //가이드는 한 번만 나와야하니까 
        if (FindObjectOfType<PlayerData>().stage0_firstInvenArrow == true)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
            FindObjectOfType<PlayerData>().stage0_firstInvenArrow = true;
        }

    }

    public void OnScreenClicked()
    {
        gameObject.SetActive(false);
    }
}
