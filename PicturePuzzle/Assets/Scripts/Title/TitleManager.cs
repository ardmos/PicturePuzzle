using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 타이틀씬 매니저. 
/// 버튼 기능 관리. 
/// 
/// 
/// </summary>

public class TitleManager : MonoBehaviour
{

    public void ButtonToGallery0_0()
    {
        SceneManager.LoadScene("StageList");
    }
}
