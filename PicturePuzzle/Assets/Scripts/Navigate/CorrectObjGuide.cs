using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorrectObjGuide : MonoBehaviour
{
    //얘는 씬 실행시가 아닌 폴라로이드 실행 시 ! 호출.

    //public void Start()
    public void GuidDo()
    {

        //이거 원래는 여기서 해야하는데 지금 급하니까 카메라컨트롤러에서.
    }

    public void OnScreenClicked()
    {
        gameObject.SetActive(false);
    }
}
