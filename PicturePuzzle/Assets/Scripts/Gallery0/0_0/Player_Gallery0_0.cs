using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 0_0 플레이어 캐릭터 관리 스크립트.
/// 
/// 1. 등장, 퇴장 애니메이션 관리.
/// 
/// </summary>

public class Player_Gallery0_0 : MonoBehaviour
{
    Animator animator;
    public void Start()
    {
        animator = GetComponent<Animator>();
        //씬이 시작되면 자동으로 등장 애니메이션 호출.
        StartEntryAnim();
    }

    public void StartEntryAnim()
    {
        //등장 애니메이션 실행
        //Play on Awake로 자동 실행됨. 
    }

    public void StartExitAnim()
    {
        //퇴장 애니메이션 실행
        //씬 이동 전에 캐릭터 애니메이션. 애니메이션이 끝나면 애니메이션 이벤트 컨트롤러로 씬 이동이 호출됨. 
        animator.SetBool("Exit", true);
    }

    public void SceneTransferTo0_1()
    {
        //Exit애니메이션의 이벤트 컨트롤러로 호출됨.
        FindObjectOfType<Galler0_0Manager>().To1From0();
    }
}
