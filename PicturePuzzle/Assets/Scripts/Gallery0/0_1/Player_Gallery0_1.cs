﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 0_1 플레이어 캐릭터 관리 스크립트.
/// 
/// 1. 등장, 퇴장 애니메이션 관리.
/// 
/// 
/// 
/// ----- 갤러리0_1 플레이어만의 애니메이션 -----
///   세 개의 그림 앞을 자연스럽게 오가는 애니메이션 처리. 
///   애니메이터 미사용. 
///   
///   1. 첫 입장시 애니메이터 사용. Entry->Stand 
///     1-1. 그대로 퇴장시 애니메이터 사용. Stand->Exit
///     1-2. 그림을 클릭시 애니메이터 비활성화, 스프라이트 변경, 그림 앞으로 이동. 
///       1. 현 스크립트에서 현재 위치를 기록.(매 이동 시마다.) 
///       2. 기록된 위치에 플레이어 존재. 
///       3. 다른 그림 클릭시 현 위치에서 해당 그림 앞까지 이동. 
///       4. 퇴장 클릭시 현 위치에서 퇴장위치까지 이동 후 씬 전환.
/// 
///   2. 필요한 것
///     1. 위치정보: 그림1, 그림2, 그림3, 퇴장
///     2. 플레이어 위치 상태 Enum
///     3. 플레이어 캐릭터 앞모습 이미지
///     4. 씬 로드시 플레이어 위치 상태Enum 값에 따른 애니메이션 스킵 및 플레이어 위치 설정.
///       1. 상태Enum값 
///         1. Entry    : 애니메이터를 통한 입장시
///         2. Stand    : 애니메이터를 통한 입장 완료시
///         3. Exit     : 애니메이터를 통한 퇴장시
///         4. Pic1     : 코드를 통한 Pic1로의 이동시
///         5. Pic2     : 코드를 통한 Pic2로의 이동시
///         6. Pic3     : 코드를 통한 Pic3로의 이동시
///         7. PicToExit: 코드를 통한 Pic에서의 퇴장시.
///     
/// </summary>

public class Player_Gallery0_1 : MonoBehaviour
{
    Animator animator;

    //위치정보 
    [SerializeField]
    Vector2 pic1, pic2, pic3, picToExit;
    //플레이어 위치 상태 Enum
    public enum PlayerPosState
    {
        Entry,
        Stand,
        Exit,
        Pic1,
        Pic2,
        Pic3,
        PicToExit
    }
    public PlayerPosState playerPosState;
    //플레이어 캐릭터 앞모습 이미지
    [SerializeField]
    Sprite playerFrontImg;


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

    #region 애니메이터 관리
    public void StartEntryAnim()
    {
        //등장 애니메이션 실행
        animator.SetBool("Entry", true);
        playerPosState = PlayerPosState.Entry;
    }
    public void StartStandAnim()
    {
        //제자리에 세워두기. 그림보고 나오면 제자리에. 
        animator.SetBool("Stand", true);
        playerPosState = PlayerPosState.Stand;
    }
    public void StartExitAnim()
    {
        //퇴장 애니메이션 실행
        //씬 이동 전에 캐릭터 애니메이션. 애니메이션이 끝나면 애니메이션 이벤트 컨트롤러로 씬 이동이 호출됨. 
        animator.SetBool("Exit", true);
        playerPosState = PlayerPosState.Exit;
    }
    #endregion

    #region 씬전환
    public void SceneTransferTo0_0()
    {
        //Exit애니메이션의 이벤트 컨트롤러로 호출됨.
        FindObjectOfType<Galler0_0Manager>().To0From1();        
    }
    #endregion

    #region 애니메이터 미사용 애니메이션 구현 부분
    /// ----- 갤러리0_1 플레이어만의 애니메이션 -----
    ///   세 개의 그림 앞을 자연스럽게 오가는 애니메이션 처리. 
    ///   애니메이터 미사용. 
    ///   
    ///   1. 첫 입장시 애니메이터 사용. Entry->Stand 
    ///     1-1. 그대로 퇴장시 애니메이터 사용. Stand->Exit
    ///     1-2. 그림을 클릭시 애니메이터 비활성화, 스프라이트 변경, 그림 앞으로 이동. 
    ///       1. 현 스크립트에서 현재 위치를 기록.(매 이동 시마다.) 
    ///       2. 기록된 위치에 플레이어 존재. 
    ///       3. 다른 그림 클릭시 현 위치에서 해당 그림 앞까지 이동. 
    ///       4. 퇴장 클릭시 현 위치에서 퇴장위치까지 이동 후 씬 전환.
    ///       


    //그림 클릭시 해당 그림 앞으로 이동 처리하는 부분. UGUI 버튼으로 처리함. 
    public void MoveToPic1()
    {
        //실제 이동
        //트랜스레이트로?
        //현재 위치를 기록
        playerPosState = PlayerPosState.Pic1;
    }
    public void MoveToPic2()
    {
        //실제 이동

        //현재 위치를 기록
        playerPosState = PlayerPosState.Pic2;
    }
    public void MoveToPic3()
    {
        //실제 이동

        //현재 위치를 기록
        playerPosState = PlayerPosState.Pic3;
    }

    //Stand가 아닐 때의 퇴장 처리.  퇴장이 시작될 때, 플레이어스테이트를 기반으로 판단하는 부분 추가 필요. 
    //어떤걸로 퇴장애니메이션 실행할지.
    public void MoveToPicToExit()
    {
        //실제 이동 

        //현재 위치를 기록
        playerPosState = PlayerPosState.PicToExit;
    }

    //현재 기록된 위치에 따라 플레이어 위치를 잡아주는 부분. 그림 보고 나왔을 때 같은 경우 처리.
    public void SetPlayerPos_BasedOn_PosState()
    {

    }
    #endregion

}