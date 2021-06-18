using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// EBox
/// 1. 이미지 배치 여부 확인
/// 2. State 확인
/// 3. State에 따른 이미지 효과 자동 처리
///
///
/// ***** 옳은 아이템이 배치됐는지 확인하는 절차가 필요함 *****
/// 그것을 위한.  아이템 네임을 EBox가 미리 갖고 있다가.  오려는 아이템의 이름을 확인 후 배치 허용한다.
/// </summary>

public class EBox : MonoBehaviour
{
    [SerializeField]
    bool isfull;
    public enum EBoxState
    {
        Normal,
        HighLighted
    }
    [SerializeField]
    EBoxState eBoxState = EBoxState.Normal;
    //옳은 아이템인가 확인을 위한 이름값. 다른곳에서 호출해서 씀.
    [SerializeField]
    string itemName;

    #region Get-Set
    public void SetFull()
    {
        isfull = true;
    }
    public bool GetFull()
    {
        return isfull;
    }

    public void SetEBoxState(EBoxState state)
    {
        eBoxState = state;
    }
    public EBoxState GetEBoxState()
    {
        return eBoxState;
    }

    public string GetItemName()
    {
        return itemName;
    }
    #endregion


    // Update is called once per frame
    void Update()
    {
        //상태에 따른 EBox 이미지 효과 처리
        if (isfull)
        {
            //이미지 배치된 상태
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(false);
            //핀도 뽑아준다.
            transform.GetChild(2).gameObject.SetActive(false);
        }   
        else if (eBoxState == EBoxState.Normal)
        {
            //노말
            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(1).gameObject.SetActive(false);
        }
        else
        {
            //하이라이트
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(true);
        }
    }
}
