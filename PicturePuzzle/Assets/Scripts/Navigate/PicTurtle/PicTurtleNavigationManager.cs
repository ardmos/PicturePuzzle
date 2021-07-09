using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
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

public class PicTurtleNavigationManager : MonoBehaviour
{
    public GameObject navTextObj, arrowObj_turtleObj, arrowObj_changeModeBtn, arrowObj_takePicBtn;

    //총 문장 컨트롤
    List<string[]> totalSentences = new List<string[]>();
    [SerializeField]
    int curSentencesNum = 0;
    [SerializeField]
    int curSentenceNum_ = 0;

    //첫 방문시
    string[] sentences_firstVisit = new string[4];
    //첫 방문 & 폴라로이드 모드시. 
    string[] sentences_PolaroidMode = new string[5];
    //거북이 찍었을 시.
    string[] sentences_takePicTurtleSuccess = new string[2];

    PlayerData playerData;
    // Start is called before the first frame update
    void Start()
    {
        navTextObj.SetActive(false);
        arrowObj_turtleObj.SetActive(false);
        arrowObj_changeModeBtn.SetActive(false);
        arrowObj_takePicBtn.SetActive(false);

        sentences_firstVisit[0] = "이런 사진들에서 필요한 아이템을 찾을 수 있어요";
        sentences_firstVisit[1] = "앗! 저기 바구니가 수상하네요!";
        sentences_firstVisit[2] = "수상한 초록색 공을 찍어봅시다";
        sentences_firstVisit[3] = "오른쪽 아래의 모드 변환 버튼을 눌러보세요";


        sentences_PolaroidMode[0] = "잘하셨어요!";
        sentences_PolaroidMode[1] = "이제 사진을 카메라로 잘 조준해서 찍으면";
        sentences_PolaroidMode[2] = "아이템으로 만들수가 있습니다";
        sentences_PolaroidMode[3] = "조준은 화면 드래그로, 촬영은 오른쪽 버튼으로 하실 수 있어요";
        sentences_PolaroidMode[4] = "그럼 초록색 공을 먼저 찍어봅시다!";

        sentences_takePicTurtleSuccess[0] = "세상에! 공이 바로 거북이였어요!!";
        sentences_takePicTurtleSuccess[1] = "어서 다람쥐에게로 돌아가서 거북이를 배치해봅시다";

        totalSentences.Add(sentences_firstVisit);
        totalSentences.Add(sentences_PolaroidMode);
        totalSentences.Add(sentences_takePicTurtleSuccess);

        playerData = FindObjectOfType<PlayerData>();
        //터틀씬 첫 방문이면 가이드 시작! 
        if (playerData.guide_isFirstVisit_Turtle_isDone == false)
        {
            playerData.guide_isFirstVisit_Turtle_isDone = true;
            StartCurrentTextGuide(0);
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
        arrowObj_turtleObj.SetActive(false);
        arrowObj_changeModeBtn.SetActive(false);
        arrowObj_takePicBtn.SetActive(false);


        //상황별 이벤트 오브젝트 on/off
        switch (curSentencesNum)
        {
            case 0:
                if (num == 1)
                {
                    //저기 바구니가...
                    arrowObj_turtleObj.SetActive(true);
                }
                else if(num == 3)
                {
                    //오른쪽 아래 모드 버튼...
                    arrowObj_changeModeBtn.SetActive(true);
                }
                break;
            case 1:
                if(num == 3)
                {
                    //촬영은 오른쪽 버튼...
                    arrowObj_takePicBtn.SetActive(true);
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
    public void CallWhenPolaroidCameraOn()
    {
        if (playerData.guide_isPolaroidMode_Turtle_isDone == false)
        {
            playerData.guide_isPolaroidMode_Turtle_isDone = true;

            //화살표 두 개 끄기.
            arrowObj_turtleObj.SetActive(false);
            arrowObj_changeModeBtn.SetActive(false);

            //폴라로이드 가이드 문장 시작. 
            StartCurrentTextGuide(1);
        }
    }
    public void CallWhenTurtlePicTaken()
    {
        if (playerData.guide_isTakeTurtlePicSuccess_Turtle_isDone == false)
        {
            playerData.guide_isTakeTurtlePicSuccess_Turtle_isDone = true;

            //화살표 끄기
            arrowObj_takePicBtn.SetActive(false);

            //문장 시작 
            StartCurrentTextGuide(2);
        }
    }

}
