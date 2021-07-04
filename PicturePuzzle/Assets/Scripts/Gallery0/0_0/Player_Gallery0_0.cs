using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 0_0 플레이어 캐릭터 관리 스크립트.
/// 
/// 1. 등장, 퇴장 애니메이션 관리.
/// 2. 현재 씬 첫 재생일 때 EntryAnim(). 
///    아닐때는 EntryAnimFrom0_1() 재생. (PlayerData의 정보를 바탕으로 첫 재생 여부 판단.)
/// 3. (PlayerData.cs에서 관리하는 부분.) 타이틀씬을 한 번 다녀오면 첫 재생으로 친다.
/// 4. 그림 보고 나왔을때는 그냥 Stand 애니메이션 재생.
/// 
/// </summary>

public class Player_Gallery0_0 : MonoBehaviour
{
    Animator animator;

    public void Start()
    {
        animator = GetComponent<Animator>();
        //씬이 시작되면 자동으로 등장 애니메이션 호출.
        try
        {
            if (FindObjectOfType<PlayerSceneStateController>().GetPlayerSceneState() == PlayerSceneStateController.PlayerSceneState.First)
            {
                //First면 EntryAnim 실행. 실행하면서 NotFirst로 바꿔줌.
                StartEntryAnim();
                FindObjectOfType<PlayerSceneStateController>().SetPlayerSceneState(PlayerSceneStateController.PlayerSceneState.NotFirst);
            }
            else if(FindObjectOfType<PlayerSceneStateController>().GetPlayerSceneState() == PlayerSceneStateController.PlayerSceneState.NotFirst)
            {
                //NotFirst면 EntryAnimFrom0_1실행
                StartEntryAnimFrom0_1();
            }
            else
            {
                //둘 다 아니고 Stand면 스탠드 애니메이션 실행
                StartStandAnim();
            }
        }
        catch (System.Exception ex)
        {
            Debug.Log(ex.Message);
        }

    }

    public void StartEntryAnim()
    {
        //등장 애니메이션 실행
        animator.SetBool("Entry", true);
    }

    public void StartEntryAnimFrom0_1()
    {
        //0_1로부터의 등장.
        animator.SetBool("EntryFrom0_1", true);
    }

    public void StartToGallery0_1Anim()
    {
        //Gallery0_1로 이동.
        animator.SetBool("ToGallery0_1", true);
    }

    public void StartStandAnim()
    {
        //제자리에 세워두기. 그림보고 나오면 제자리에. 
        animator.SetBool("Stand", true);
    }

    public void StartExitAnim()
    {
        //퇴장 애니메이션 실행. Title로.
        //씬 이동 전에 캐릭터 애니메이션. 애니메이션이 끝나면 애니메이션 이벤트 컨트롤러로 씬 이동이 호출됨. 
        animator.SetBool("Exit", true);
    }



    public void SceneTransferTo0_1()
    {
        //ToGallery0_1애니메이션의 이벤트 컨트롤러로 호출됨.
        FindObjectOfType<Galler0_0Manager>().To1From0();
    }
    public void SceneTransferToTitle()
    {
        //Exit애니메이션의 이벤트 컨트롤러로 호출됨.
        FindObjectOfType<Galler0_0Manager>().ToTitleFrom0();
    }
}
