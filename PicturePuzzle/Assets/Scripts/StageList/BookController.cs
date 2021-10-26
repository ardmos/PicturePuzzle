using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 북 컨트롤러.
/// 페이지 넘기는것을 관장.
///
/// 
///   1. 다음 페이지로 넘기기 ToTheNextPage
///   2. 이전 페이지로 넘기기 ToThePrevPage
/// 
/// 
/// 
/// *** 예시용 ***
/// 각 페이지 넘기기 종료 보고는
///  1. 실린더페이지 애니메이션 끝에서
///  2. 실린어베이지.cs의 플립이스오버 메서드-> 현 스크립트의 콜웬플립오버.
///
/// 페이지 넘기기 실행은
///  1. 현 스크립트 콜웬플립오버->각실린더의애니메이터.셋불(SetBool)
/// **************
/// 
/// </summary>

public class BookController : MonoBehaviour
{
    //페이지들.
    [SerializeField]    
    Animator[] cylinder_Page;


    //다음 페이지로 넘기기. (현재 페이지의 인덱스 넘버를 전해줘야합니다.)
    public void ToTheNextPage(int indexnum)
    {
        cylinder_Page[indexnum].SetTrigger("FlipToTheNextPage");
    }

    //이전 페이지로 넘기기. (현재 페이지의 인덱스 넘버를 전해줘야합니다.)
    public void ToThePrevPage(int indexnum)
    {
        cylinder_Page[indexnum].SetTrigger("FlipToThePrevPage");
    }


    /*
    //예시용 자동Flip 구현을 위한 부분.
    public void CallWhenFlipOver(GameObject cur_cylinder_Page)
    {
        //오브젝트 네임의 가장 마지막 글자 확인.
        int curFlipedPageNum = int.Parse(cur_cylinder_Page.name[cur_cylinder_Page.name.Length - 1].ToString());

        //마지막 페이지인 경우
        if (cylinder_Page.Length-1 == curFlipedPageNum)
        {
            Debug.Log("Book Page Flip is End");
        }
        else
        {
            Debug.Log(cur_cylinder_Page.name);
            Debug.Log(curFlipedPageNum);
            //나머지 경우는 다음 페이지 플립.
            cylinder_Page[curFlipedPageNum + 1].SetBool("Flip", true);
        }
    }
    */
}
