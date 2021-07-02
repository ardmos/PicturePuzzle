using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//더블탭 가이드. 
//Ebox에 배치 성공시 호출. (호출된적 없을때만. 플레이어데이터 기준)

public class DoubleTab : MonoBehaviour
{
    
    //까만화면 눌리면 종료. 
    public void OnScreenClicked()
    {
        //더블탭 인식을 위해서 여기선 특별히 더블탭 기능을 실행해줌. 
        FindObjectOfType<Pic0_0Manager>().Try();
        gameObject.SetActive(false);
    }

}
