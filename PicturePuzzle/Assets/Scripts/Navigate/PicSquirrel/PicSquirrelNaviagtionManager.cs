using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
///  다람쥐씬 네비게이션 가이드 매니저. 
///  
///  
///  ///////지금은사용안함///////기존에 만들어뒀던 네비게이션들 최대 활용함. 1. 인벤토리 비어있는/안비어있는 경우의 확인 필요한 TakePic, DragItem 가이드 발동은 그대로 InventoryController - OpenInventory() 에서 처리하도록 둔다.
///  
///  인벤토리 체크 이외의 가이드들은 현 스크립트에서 관장. 
///  1. 현재 씬 첫 방문일시 : 카메라 왼 - 오, '가족들에게 가고싶어!' 출력
///  2. '다람쥐가 가족을 만날 수 있도록 다리를 만들어줘야해요' 
///  3. '다리로 놓을 아이템을 구하러 가봅시다' (arrowObj_exitBtn 활성화) (inventoryBtn 비활성화)
///  -종료-
///  
///  ++#. 첫 방문 아님 && 거북이 아이템 없음 : '거북이를 찍어보세요~!'
///                    && 거북이 아이템 있음 : 16. '인벤토리를 여신 다음에' (arrowObj_inventory 활성화)
///                                            17. '거북이를 드래그해서 배치해보세요' (guide_DragItem 활성화 _ 인벤토리컨트롤러에서.)
///                                            -종료-
///                                            

///                           
///  //다람쥐씬 공통
///  ==#. 1. 처음으로 다람쥐 Try 실패시 : '이런! 아직 다리가 부족한가보네요. 아이템이 더 필요해요'                                      
///                                       '일단 어서 우측 하단의 버튼을 사용해 다람쥐를 꺼내줍시다!' (arrow_Retry 활성화)
///                                       '서둘러요!!'
///                                       -종료-
///                                       
///       2. 처음으로 배치 실패시 : '해당 위치에 적합한 아이템이 아닌가보네요'
///                                 '다른곳에 다시 배치해봅시다'
///                                 -종료-
///                   배치 성공시 : 18. '잘하셨어요!'
///                                 19. '이제 화면을 더블클릭해서 다람쥐를 출발시킬수 있습니다.' (hand_doubleClick)
///                                 -종료-
///                                            
/// //갤러리0_0
///  4. '이쪽이예요!' (arrowObj_ToGallery0_1Btn 활성화) 
/// 
/// //갤러리0_1
///  5. '사진을 클릭해보세요' (arrowObj_Turtle 활성화)
///  
/// //Pic_Turtle
///  6. '앗! 저기 바구니가 수상하네요' (arrowObj_turtleObj 활성화)
///  7. '사진을 찍어봅시다'
///  8. '오른쪽 아래의 모드 변환 버튼을 눌러보세요' (arrowObj_changeModeBtn 활성화)
///  -종료-
///  
///    //폴라로이드 mode시
///  9. '잘하셨어요!'
///  10. '이제 사진을 카메라로 잘 조준해서 찍으면'
///  11. '아이템으로 만들수가 있습니다'
///  12. '조준은 화면 드래그로, 촬영은 오른쪽 버튼으로 하실 수 있어요' (arrowObj_takePicBtn 활성화)
///  13. '그럼 거북이를 먼저 찍어봅시다!'
///  -종료-
///  
///    //거북이 찍었을 시 
///    14. '훌륭해요!'
///    15. '어서 다람쥐에게로 돌아가서 거북이를 놓아줘봅시다'
///    -종료-
///    
/// </summary>

public class PicSquirrelNaviagtionManager : MonoBehaviour
{
    //네비게이션 가이드 목적
    public GameObject navTextObj, arrowObj_ExitBtn, arrowObj_inventory, guide_DragItem, hand_doubleClick, arrowObj_Retry;

    ///문장들
    ///현재 출력중인 문장
    List<string[]> totalSentences = new List<string[]>();
    [SerializeField]
    int curSentencesNum = 0;
    [SerializeField]
    int curSentenceNum_ = 0;
    //첫 방문시
    static string[] sentences_firstVisit = new string[7];

    //첫 방문 아님
    //거북이 없음
    string[] sentences_noTurtleItem = new string[1];
    //거북이 있음
    string[] sentences_yesTurtleItem = new string[2];

    //처음으로 다람쥐 try 실패시
    string[] sentences_firstTryFail = new string[3];

    //처음으로 아이템 배치 실패시
    string[] sentences_firstEBoxFail = new string[2];
    //배치 성공시
    string[] sentences_firstEBoxSuccess = new string[2];

    //고맙다 대사
    string[] sentences_ThanksRetry = new string[1];

    //비활성화 목적
    public GameObject inventoryBtn;

    PlayerData playerData;


    // Start is called before the first frame update
    void Start()
    {
        navTextObj.SetActive(false);
        arrowObj_ExitBtn.SetActive(false);
        arrowObj_inventory.SetActive(false);
        guide_DragItem.SetActive(false);
        hand_doubleClick.SetActive(false);
        arrowObj_Retry.SetActive(false);

        //첫 방문시 
        sentences_firstVisit[0] = "안녕, 난 다람쥐야!";
        sentences_firstVisit[1] = "드디어 내 목소리를 들을 수 있는 순수한 마음의 사람이 나타나다니";
        sentences_firstVisit[2] = "초면에 미안하지만 너에게 부탁이 있는데...";
        sentences_firstVisit[3] = "반대편 섬에 있는 가족들이 너무 그리워ㅠㅠ!";
        sentences_firstVisit[4] = "네가 나좀 도와줄래?";
        sentences_firstVisit[5] = "내가 건널 수 있도록 다리를 만들어주면 좋을 것 같은데";
        sentences_firstVisit[6] = "찾아보면 쓸만한 아이템들이 있을거야... 부탁할게!";

        //첫 방문 아님
        //거북이 없음
        sentences_noTurtleItem[0] = "거북이가 필요해요";
        //거북이 있음
        sentences_yesTurtleItem[0] = "와! 거북이를 구해왔구나! 인벤토리를 열고";
        sentences_yesTurtleItem[1] = "거북이를 드래그해서 배치해봐!";

        //처음으로 다람쥐 try 실패시
        sentences_firstTryFail[0] = "으앗! 이런!!!!";
        sentences_firstTryFail[1] = " 다람쥐가 빠졌어요! 아이템들을 더 찾아서 배치해야겠어요";
        sentences_firstTryFail[2] = " 우측 하단의 버튼을 사용하면 다람쥐를 꺼내줄 수 있습니다";
        //처음으로 아이템 배치 실패시
        sentences_firstEBoxFail[0] = "해당 위치에 적합한 아이템이 아닌가봐...";
        sentences_firstEBoxFail[1] = "다른 아이템을 배치해보자!";
        //배치 성공시
        sentences_firstEBoxSuccess[0] = "잘했어!";
        sentences_firstEBoxSuccess[1] = "이제 화면을 더블클릭하면 내가 출발해볼게";
        //고맙다 대사.
        sentences_ThanksRetry[0] = "다시 화이팅!";

        //0
        totalSentences.Add(sentences_firstVisit);
        totalSentences.Add(sentences_noTurtleItem);
        totalSentences.Add(sentences_yesTurtleItem);
        //3
        totalSentences.Add(sentences_firstTryFail);
        totalSentences.Add(sentences_firstEBoxFail);
        totalSentences.Add(sentences_firstEBoxSuccess);
        totalSentences.Add(sentences_ThanksRetry);

        playerData = FindObjectOfType<PlayerData>();
        //첫 방문인가? 
        if (playerData.guide_isNotFirstSquirrel == true)
        {
            //첫 방문이 아님

            //거북이 있음?
            string result = playerData.GetItemList().Find(item => item == "Turtle");
            if (result != "Turtle")
            {
                //거북이 없음
                StartCurrentTextGuide(1);
            }
            else
            {
                //거북이 있음
                StartCurrentTextGuide(2);
            }
        }
        else
        {
            //첫 방문임            
            playerData.guide_isNotFirstSquirrel = true;

            //카메라 왼-오 애니메이션 실행 (일단 생략)
            //첫 방문시 텍스트 가이드 발동
            StartCurrentTextGuide(0);
        }
    }

    //다람쥐 try 실패시 호출
    public void CallWhenTryFail()
    {
        //처음으로 다람쥐 try 실패시
        if (playerData.guide_isFirstTryFail_isDone == false)
        {
            playerData.guide_isFirstTryFail_isDone = true;
            StartCurrentTextGuide(3);
        }
    }
    //아이템 배치 실패시 호출
    public void CallWhenEBoxFail()
    {
        //처음으로 아이템 배치 실패시
        if (playerData.guide_isFirstEBoxFail_isDone == false)
        {
            playerData.guide_isFirstEBoxFail_isDone = true;
            StartCurrentTextGuide(4);
        }
    }
    //아이템 배치 성공시 호출
    public void CallWhenEBoxSuccess()
    {
        //처음으로 아이템 배치 성공시
        if (playerData.guide_isFirstEBoxSuccess_isDone == false)
        {
            playerData.guide_isFirstEBoxSuccess_isDone = true;
            StartCurrentTextGuide(5);
        }
    }
    //Retry버튼 클릭시 호출
    public void CallWhenRetry()
    {
        StartCurrentTextGuide(6);
    }

    //인벤토리 오픈시 호출. 드래그 가이드
    public void CallWhenInventoryOpened()
    {
        //드래그가이드 안했으면
        if (playerData.guide_isDragGuide_isDone == false)
        {
            playerData.guide_isDragGuide_isDone = true;
            guide_DragItem.SetActive(true);
        }
    }

    private void StartCurrentTextGuide(int curNum)
    {
        curSentencesNum = curNum;
        navTextObj.SetActive(true);
        PrintCurrentTextGuide(0);
    }
    public void PrintCurrentTextGuide(int num)
    {
        navTextObj.GetComponentInChildren<Text>().text = totalSentences[curSentencesNum][num];

        //navTextObj.SetActive(false);
        arrowObj_ExitBtn.SetActive(false);
        arrowObj_inventory.SetActive(false);
        guide_DragItem.SetActive(false);
        hand_doubleClick.SetActive(false);
        arrowObj_Retry.SetActive(false);
        inventoryBtn.SetActive(true);

        //상황별 이벤트 오브젝트 on/off
        switch (curSentencesNum)
        {
            case 0:
                inventoryBtn.SetActive(false);
                if (num == 6)
                {
                    //다리로 놓을만한...
                    arrowObj_ExitBtn.SetActive(true);
                }
                break;
            case 2:
                //거북이 있음
                inventoryBtn.SetActive(true);
                if(num == 0)
                {
                    //인벤토리를 여신 다음에...
                    arrowObj_inventory.SetActive(true);
                }
                if(num == 1)
                {
                    //드래그를...
                    guide_DragItem.SetActive(true);
                    guide_DragItem.GetComponent<ObjMove>().StartMoveToEBox1();
                }
                break;
            case 3:
                //첫 try 실패               
                if (num == 2)
                {
                    //서둘러요...
                    arrowObj_Retry.SetActive(true);
                }
                break;
            case 5:
                //첫 EBox 성공
                if (num == 1)
                {
                    //더블탭...
                    hand_doubleClick.SetActive(true);
                }
                break;
            default:

                break;
        }
    }
    public void OnButtonNavTextObjClicked()
    {
        //navTextObj 버튼이 클릭됐을 때. curSentenceNum을 증가시키며 다음 텍스트를 출력해주다가 
        //다음 텍스트가 없으면 navTextOjb를 비활성화한다. 
        
        curSentenceNum_++;

        if (curSentenceNum_ == totalSentences[curSentencesNum].Length)
        {
            //문장 한 세트가 끝난 경우 -> navTextObj 비활성화. 
            navTextObj.SetActive(false);
            curSentenceNum_ = 0;
            return;
        }

        PrintCurrentTextGuide(curSentenceNum_);
    }
}
