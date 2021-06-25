using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 0_1 플레이어 캐릭터 관리 스크립트.
/// 
/// 1. 등장, 퇴장 애니메이션 관리.
/// 
/// 2. 등장시 뒷모습. 
/// 3. 각 그림 선택 시 앞모습으로 바뀜.
/// 4. 선택한 그림 앞으로 이동.
/// 5. 그림 보고 나왔을 때 그대로 있음. 
/// 6. 그 상태에서 퇴장 누르면 해당 위치에서부터 자연스럽게 퇴장. 
///
/// 
/// </summary>

public class Player_Gallery0_1 : MonoBehaviour
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
    public void StartStandAnim()
    {
        //제자리에 세워두기. 그림보고 나오면 제자리에. 
        animator.SetBool("Stand", true);
    }
    public void StartExitAnim()
    {
        //퇴장 애니메이션 실행
        //씬 이동 전에 캐릭터 애니메이션. 애니메이션이 끝나면 애니메이션 이벤트 컨트롤러로 씬 이동이 호출됨. 
        animator.SetBool("Exit", true);
    }

    public void SceneTransferTo0_0()
    {
        //Exit애니메이션의 이벤트 컨트롤러로 호출됨.
        FindObjectOfType<Galler0_0Manager>().To0From1();
    }
}
