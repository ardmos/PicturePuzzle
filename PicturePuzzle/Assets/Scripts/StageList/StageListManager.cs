using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 스테이지 리스트! 
/// 
/// 하는 일.
/// 1. CameraRayCast.cs로부터 터치에 성공한 오브젝트를 받아와서 확인하고 알맞는 기능에 연결해준다. 
/// 
/// 알맞는 기능들
/// 1. 페이지 넘기기  
/// 2. 스테이지 입장  
///
/// 
/// </summary>

public class StageListManager : MonoBehaviour
{
    public BookController bookController;

    //1. CameraRayCast.cs로부터 터치에 성공한 오브젝트를 받아와서 확인하고 알맞는 기능에 연결해준다. 
    public void WhenPlayerTouchedSomething(GameObject mgameObject)
    {
        //만약 페이지 넘기기 버튼이라면? 책장을 넘겨야함.  BookController의 ToTheNextPage

        //다음 페이지로 넘기기일 경우
        if (mgameObject.CompareTag("FlipToTheNextPageButton"))
        {
            bookController.ToTheNextPage(int.Parse(mgameObject.name));
        }
        //이전 페이지로 넘기기일 경우
        else if (mgameObject.CompareTag("FlipToThePrevPageButton"))
        {
            bookController.ToThePrevPage(int.Parse(mgameObject.name));
        }


        //페이지 넘기기 버튼이 아니었다면! 버튼의 이름이 바로 씬의 이름! 버튼의 이름을 사용해서 씬을 불러와주세요!
        else
        {
            try
            {
                SceneManager.LoadScene(mgameObject.name);
            }
            catch (System.Exception)
            {
                throw;
            }            
        }
    }
}
