using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
///     2. 플레이어 위치 상태 Enum -> PlayerSceneStateController에서 관리. 
///     2-1. PlayerSceneStateController 필요.
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
///     5. Vector3.MoveTowards를 Update에서 사용하기 위한 string 변수. string whereTo.  값이 Wait면 현 위치 대기.
///       1. 이동거리 계산을 위한 float speed값, step값. 
///     6. Pic1, Pic2는 이미지를 y축으로 반전시켜야함. FlipX = true
///     7. 앞모습 이미지 적용을 위한 spriteRenderer 컴포넌트. 
///     
/// </summary>

public class Player_Gallery0_1 : MonoBehaviour
{
    Animator animator;

    //위치정보 
    [SerializeField]
    Vector2 pic1, pic2, pic3, picToExit;
    //위치정보 설정 쉽도록 오브젝트 드래그드롭.
    [SerializeField]
    Transform pic1PosObj, pic2PosObj, pic3PosObj, picToExitPosObj;
    // 2-1. PlayerSceneStateController 필요.
    [SerializeField]
    PlayerSceneStateController playerSceneStateController;
    //플레이어 캐릭터 앞모습 이미지
    [SerializeField]
    Sprite playerFrontImg;
    //앞모습 이미지 적용을 위한 spriteRenderer 컴포넌트.
    SpriteRenderer spriteRenderer;
    //MoveTowards를 위한
    [SerializeField]
    string whereTo;
    //이동시에 이동 거리 계산을 위한 speed, step.
    float speed, step;

    public void Start()
    {
        //이동목표들의 위치정보 Vector화. 
        InitTargetPoistions();

        try
        {
            //씬이 시작되면 자동으로 등장 애니메이션 호출.
            animator = GetComponent<Animator>();
            //스프라이트렌더러 찾기.
            spriteRenderer = GetComponent<SpriteRenderer>();

            //PlayerSceneStateController 설정.
            playerSceneStateController = FindObjectOfType<PlayerSceneStateController>();

            //playerPosState가 Pic1, Pic2, Pic3이면  애니메이터 대신 SetPlayerPos_BasedOn_PosState()로 대기 애니메이션 실행. 
            if (playerSceneStateController.playerPosState == PlayerSceneStateController.PlayerPosState.Pic1 || playerSceneStateController.playerPosState == PlayerSceneStateController.PlayerPosState.Pic2 || playerSceneStateController.playerPosState == PlayerSceneStateController.PlayerPosState.Pic3)
            {
                //애니메이터 비활성화.
                animator.enabled = false;
                SetPlayerPos_BasedOn_PosState();
            }
            else
            {
                if (playerSceneStateController.GetPlayerSceneState() == PlayerSceneStateController.PlayerSceneState.First)
                {
                    //First면 EntryAnim 실행.
                    StartEntryAnim();
                    playerSceneStateController.SetPlayerSceneState(PlayerSceneStateController.PlayerSceneState.NotFirst);
                }
                else
                {
                    //PlayerSceneState가 First이 아닌 경우들.
                    StartStandAnim();
                }
            }

        }
        catch (System.Exception ex)
        {
            Debug.Log(ex.Message);
        }
    }
    public void Update()
    {

        switch (whereTo)
        {
            case "Pic1":
                //목적지에 도착했는가? //도착했으면 Wait로 만들고, 씬 이동.                 
                if (Vector3.Distance(transform.position, pic1) < 0.001f) { whereTo = "Wait"; SceneManager.LoadScene("Pic0_1_Turtle"); }
                else speed = 10f; step = speed * Time.deltaTime; transform.position = Vector3.MoveTowards(transform.position, pic1, step);
                break;
            case "Pic2":
                //목적지에 도착했는가? //도착했으면 Wait로 만들고, 씬 이동.                 
                if (Vector3.Distance(transform.position, pic2) < 0.001f) { whereTo = "Wait"; SceneManager.LoadScene("Pic0_2_Stone"); }
                else speed = 10f; step = speed * Time.deltaTime; transform.position = Vector3.MoveTowards(transform.position, pic2, step);
                break;
            case "Pic3":
                //목적지에 도착했는가? //도착했으면 Wait로 만들고, 씬 이동.                
                if (Vector3.Distance(transform.position, pic3) < 0.001f) { whereTo = "Wait"; SceneManager.LoadScene("Pic0_3_Wood"); }
                else speed = 10f; step = speed * Time.deltaTime; transform.position = Vector3.MoveTowards(transform.position, pic3, step);
                break;
            case "PicToExit":
                //목적지에 도착했는가? //도착했으면 Wait로 만들고, 씬 이동.                 
                if (Vector3.Distance(transform.position, picToExit) < 0.001f) { whereTo = "Wait"; SceneManager.LoadScene("Gallery0_0"); }
                else speed = 18f; step = speed * Time.deltaTime; transform.position = Vector3.MoveTowards(transform.position, picToExit, step);
                break;
            case "Wait":
                //현 위치 대기
                break;
            default:
                whereTo = "Wait";
                //Debug.Log("whereTo의 값이 " + whereTo + " 입니다. 확인이 필요합니다.");
                break;
        }
    }

    #region 애니메이터 관리
    public void StartEntryAnim()
    {
        //등장 애니메이션 실행
        animator.SetBool("Entry", true);
        playerSceneStateController.playerPosState = PlayerSceneStateController.PlayerPosState.Entry;
    }
    public void StartStandAnim()
    {
        //제자리에 세워두기. 그림보고 나오면 제자리에. 
        animator.SetBool("Stand", true);
        playerSceneStateController.playerPosState = PlayerSceneStateController.PlayerPosState.Stand;
    }
    public void StartExitAnim()
    {
        //퇴장 애니메이션 실행
        //씬 이동 전에 캐릭터 애니메이션. 애니메이션이 끝나면 애니메이션 이벤트 컨트롤러로 씬 이동이 호출됨. 


        //만약 State가 Pic1, 2, 3중 하나인 경우라면,  다른 퇴장 애니메이션을 시작해야함.
        if(playerSceneStateController.playerPosState == PlayerSceneStateController.PlayerPosState.Pic1 || playerSceneStateController.playerPosState == PlayerSceneStateController.PlayerPosState.Pic2 || playerSceneStateController.playerPosState == PlayerSceneStateController.PlayerPosState.Pic3)
        {
            MoveToPicToExit();
        }
        else
        {
            animator.SetBool("Exit", true);
            playerSceneStateController.playerPosState = PlayerSceneStateController.PlayerPosState.Exit;
        }

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


    //그림 클릭시 해당 그림 앞으로 이동 처리하는 부분. UGUI 버튼으로 처리함.  실제 이동은 Update에서 돌려줘야한다.
    public void MoveToPic1()
    {
        //실제 이동
        //트랜스레이트로? -> V3.MoveTowards로.(현pos, 타겟pos, step 최대이동거리 즉 이동할 거리(Time.delta 곱하기 speed. 요거때문에 업데이트에서 해줘야함. 프레임당 이동.))
        //Calculate a position between the points specified by current and target, moving no farther than the distance specified by maxDistanceDelta.
        //Update

        //애니메이터 비활성화. 
        animator.enabled = false;
        //whereTo 사용.
        whereTo = "Pic1";
        //현재 위치를 기록
        playerSceneStateController.playerPosState = PlayerSceneStateController.PlayerPosState.Pic1;
        //이미지 변경. 
        spriteRenderer.sprite = playerFrontImg;
        spriteRenderer.flipX = true;
    }
    public void MoveToPic2()
    {
        //애니메이터 비활성화. 
        animator.enabled = false;
        //실제 이동
        whereTo = "Pic2";
        //현재 위치를 기록
        playerSceneStateController.playerPosState = PlayerSceneStateController.PlayerPosState.Pic2;
        //이미지 변경. 
        spriteRenderer.sprite = playerFrontImg;
        spriteRenderer.flipX = true;
    }
    public void MoveToPic3()
    {
        //애니메이터 비활성화. 
        animator.enabled = false;
        //실제 이동
        whereTo = "Pic3";
        //현재 위치를 기록
        playerSceneStateController.playerPosState = PlayerSceneStateController.PlayerPosState.Pic3;
        //이미지 변경. 
        spriteRenderer.sprite = playerFrontImg;
        spriteRenderer.flipX = false;
    }

    //Stand가 아닐 때의 퇴장 처리.  퇴장이 시작될 때, 플레이어스테이트를 기반으로 판단하는 부분 추가 필요. 
    //어떤걸로 퇴장애니메이션 실행할지.
    public void MoveToPicToExit()
    {
        //애니메이터 비활성화. 
        animator.enabled = false;
        //실제 이동 
        whereTo = "PicToExit";
        //현재 위치를 기록
        playerSceneStateController.playerPosState = PlayerSceneStateController.PlayerPosState.PicToExit;
        //무조건 왼쪽을 바라봐야함.         
        spriteRenderer.flipX = false;
    }

    //현재 기록된 위치에 따라 플레이어 위치를 잡아주는 부분. 그림 보고 나왔을 때 같은 경우 처리. Start에서 호출.
    public void SetPlayerPos_BasedOn_PosState()
    {
        switch (playerSceneStateController.playerPosState)
        {
            case PlayerSceneStateController.PlayerPosState.Entry:
                break;
            case PlayerSceneStateController.PlayerPosState.Stand:
                break;
            case PlayerSceneStateController.PlayerPosState.Exit:
                break;
            case PlayerSceneStateController.PlayerPosState.Pic1:
                transform.position = pic1;
                spriteRenderer.sprite = playerFrontImg;
                spriteRenderer.flipX = true;
                break;
            case PlayerSceneStateController.PlayerPosState.Pic2:
                transform.position = pic2;
                spriteRenderer.sprite = playerFrontImg;
                spriteRenderer.flipX = true;
                break;
            case PlayerSceneStateController.PlayerPosState.Pic3:
                transform.position = pic3;
                spriteRenderer.sprite = playerFrontImg;
                spriteRenderer.flipX = false;
                break;
            case PlayerSceneStateController.PlayerPosState.PicToExit:
                break;
            default:
                break;
        }
    }

    //이동목표지점들 pos를 vector화.
    public void InitTargetPoistions()
    {
        pic1 = pic1PosObj.position;
        pic2 = pic2PosObj.position;
        pic3 = pic3PosObj.position;
        picToExit = picToExitPosObj.position;
    }
    #endregion

}
