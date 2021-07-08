using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorrectObjGuide : MonoBehaviour
{
    //얘는 씬 실행시가 아닌 폴라로이드 실행 시 ! 호출.

    public void Start() {
        Debug.Log("Start");
        gameObject.SetActive(false);
    }
    public void GuidDo()
    {
        try
        {
            //올바른 오브젝트 가이드.
            //가이드는 한 번만 나와야하니까 
            if (FindObjectOfType<PlayerData>().guide_didPicturedCorrectObj == true)
            {
                gameObject.SetActive(false);
            }
            else
            {
                gameObject.SetActive(true);
                FindObjectOfType<PlayerData>().guide_didPicturedCorrectObj = true;
            }
        }
        catch (System.Exception ex)
        {
            Debug.Log(ex.Message);
        }
    }

    public void OnScreenClicked()
    {
        gameObject.SetActive(false);
    }
}
