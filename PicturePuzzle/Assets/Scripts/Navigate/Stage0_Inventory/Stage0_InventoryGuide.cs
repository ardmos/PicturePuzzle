using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 스테이지 0 인벤토리 가이드. 
/// 
/// 1. 인벤토리가 비어있을경우 아이템 구해오라는 텍스트 출력. 및 화면 클릭시 도움말 종료. 
/// 
/// // 
/// 1. 인벤토리가 실행이 된 경우.
///     1. 비어있을 시 0번 가이드
///     2. 아이템이 있을 시 1번 가이드
/// </summary>

public class Stage0_InventoryGuide : MonoBehaviour
{
    // 종료시킬 도움말 오브젝트 0번 가이드
    public GameObject stage0_InventoryGuideObj;
    // 1번 가이드
    public GameObject stage0_InventoryGuideObj1;

    // 자식을 확인할 그리드 레이아웃
    public GameObject gridLayout;

    // Start is called before the first frame update
    void Start()
    {
        //처음엔 가이드 비활성화. 
        //이제 Pic0_0매니저.cs에서 해줌.
    }

    //인벤토리 오픈 호출시
    public void InventoryIsOpened()
    {
        //인벤토리가 실행된 경우

        //아이템이 있는가?  그리드레이아웃의 자식 숫자 확인.
        Debug.Log(gridLayout.transform.childCount);
        if (gridLayout.transform.childCount==0)
        {
            //비었으면 0번 가이드. 
            stage0_InventoryGuideObj.SetActive(true);
        }
        else
        {
            if (FindObjectOfType<PlayerData>().guide1done == false)
            {
                //있으면 1번 가이드.
                stage0_InventoryGuideObj1.SetActive(true);
                //근데 1번 가이드의 경우 한 번만 떠야하니까 
                FindObjectOfType<PlayerData>().guide1done = true;
            }            
        }
    }

    public void OnScreenClicked()
    {
        stage0_InventoryGuideObj.SetActive(false);
    }
    public void OnScreenClicked1()
    {
        stage0_InventoryGuideObj1.SetActive(false);
    }

}
