using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 갤러리0_0 가이드 네미베이션 매니저. 
/// 유저의 시선을 다람쥐 그림으로 끄는것이 목표. 
/// 
/// 갤러리 Entry 애니메이션이 끝났을 때 guide_HeyFromSquirrel이 false인 경우 발동. 
/// 
/// navTextObj 활성화
/// "잠깐만!"
/// "여기야 여기" (arrowObj 활성화)
/// "휴, 역시 넌 내 목소리가 들리는구나?"
/// "나좀 도와줄래?"
/// navTextObj 비화성화
/// 
/// (fingetObj 활성화)
/// 
/// 
/// 2. 길안내용. 
/// 우측 버튼 활성화. 
/// 
/// 
/// </summary>

public class Gallery0_0NavigationManager : MonoBehaviour
{

    public GameObject navTextObj, arrowObj, fingerObj, moveToGallery0_1Btn, arrowObj_RightBtn;
    string[] sentences = new string[5];
    
    public int curSentenceNum;


    // Start is called before the first frame update
    void Start()
    {
        navTextObj.SetActive(false);
        arrowObj.SetActive(false);
        fingerObj.SetActive(false);
        //오른쪽 버튼도 숨겨두기. 

        
        sentences[0] = "잠깐만!";
        sentences[1] = "여기야 여기!";
        sentences[2] = "휴, 역시 넌 내 목소리가 들리는구나?";
        sentences[3] = "나좀 도와줄래?";
        sentences[4] = "그림을 터치해봐";


        //다람쥐 미션 받은 경우. 오른쪽 버튼 활성화. 
        if(FindObjectOfType<PlayerData>().guide_isNotFirstSquirrel == true &&
            FindObjectOfType<PlayerData>().guide_Gallery0_0RightBtn_isDone == false)
        {
            FindObjectOfType<PlayerData>().guide_Gallery0_0RightBtn_isDone = true;
            arrowObj_RightBtn.SetActive(true);            
        }
        else {
            arrowObj_RightBtn.SetActive(false); 
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CheckWhenPlayerEntryIsEnd()
    {
        if (FindObjectOfType<PlayerData>().guide_HeyFromSquirrel == false)
        {
            //다람쥐: 잠깐만! 네이베이션 오브젝트 활성화. 
            navTextObj.SetActive(true);
            PrintNavTextObjText(0);
            //갤러리0_1로 이동 버튼 비활성화.
            moveToGallery0_1Btn.SetActive(false);
            FindObjectOfType<PlayerData>().guide_HeyFromSquirrel = true;
        }
        else
        {
            //다람쥐: 잠깐만! 오브젝트 비활성화인 채로 두고 

            //갤러리0_1로 이동 버튼 활성화.
            moveToGallery0_1Btn.SetActive(true);
        }
    }

    public void OnButtonNavTextObjClicked()
    {
        //navTextObj 버튼이 클릭됐을 때. curSentenceNum을 증가시키며 다음 텍스트를 출력해주다가 
        //다음 텍스트가 없으면 navTextOjb를 비활성화한다. 
        curSentenceNum++;

        if (curSentenceNum == sentences.Length)
        {
            //다음 텍스트가 없는 경우 -> navTextObj 비활성화. 
            navTextObj.SetActive(false);
            //fingerObj 활성화
            fingerObj.SetActive(true);
            return;
        }


        PrintNavTextObjText(curSentenceNum);
    }

    private void PrintNavTextObjText(int num)
    {
        navTextObj.GetComponentInChildren<Text>().text = sentences[num];

        if (num == 1)
        {
            //arrowObj 활성화
            arrowObj.SetActive(true);
        }
        else arrowObj.SetActive(false);
    }
}
