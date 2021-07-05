using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// StoneSceneManager. 
/// 1. 화면 터치 인식해서 눈사람 없어지도록. 배경 이미지 바꿔주고, 스노우팝 애니메이션 해줌.
/// 
/// 2. 그림 씬 정보 보고받아서 PlayerData에 저장. 
/// </summary>
public class StoneSceneManager : MonoBehaviour
{
    //배경 이미지들. 
    public Sprite[] stoneSceneBGImgs; // 0~3. 총 4장    
    public int idxcount=0;
    //현재 배경 이미지. 
    public SpriteRenderer bgsprite;
    //스노우팝 애니메이터
    public Animator snowPopAnimator;

    //씬 상태 기록
    PlayerData playerData;

    //씬이 끝날 때 호출되는 함수. esc키가 눌렸을 때 호출된다.
    public void SendDataToPlayerData()
    {
        //배경이미지 기록      
        playerData.curBGObj_Stone = bgsprite.sprite;
        playerData.isrecorded_Stone = true;
        playerData.idxcount = idxcount;
        //스크린샷으로 저장. 갤러리에서 보여주기 위한 이미지.
        FindObjectOfType<CameraViewGraber>().Snapshot();

        //NPA 기록 
        GameObject[] npaStoneArr = FindObjectOfType<CameraController>().npa_Stone;
        for (int i = 0; i < npaStoneArr.Length; i++)
        {                        
            playerData.npa_Stone_ActiveSelf[i] = npaStoneArr[i].transform.parent.gameObject.activeSelf;
        }
    }


    private void Start()
    {
        playerData = FindObjectOfType<PlayerData>();
        
        //씬이 시작될 때 playerData에 저장된 그림씬 데이터에 따라서 화면 구성. 
        //isrecored 여부에 따라. true일때만 읽어온대로 구성.
        if (playerData.isrecorded_Stone)
        {
            idxcount = playerData.idxcount;
            //배경이미지
            bgsprite.sprite = playerData.curBGObj_Stone;

            //NPA 로드. 
            //읽어와서 우리 npa들 SetActive에 적용. index 순서 같으니까 idx기준으로 끼리끼리.
            GameObject[] npaStoneArr = FindObjectOfType<CameraController>().npa_Stone;
            for (int i = 0; i < playerData.npa_Stone_ActiveSelf.Length; i++)
            {
                //Debug.Log("Start===itemIndex:" + i + ", 의 값:" + playerData.npa_Stone_ActiveSelf[i]);
                npaStoneArr[i].transform.parent.gameObject.SetActive(playerData.npa_Turtle_ActiveSelf[i]);
            }

            //애니메이터는 안썼음.
        }
    }




    //콜라이더로 터치 인식. 눈사람 콜라이더(현 스크립트 오브젝트에 달려있음.)
    private void OnMouseDown()
    {
        //터치 감지시마다 배경 이미지 바꿔주고 스노우팝 애니메이션 실행해줌. 마지막 3번 이미지까지 가면 터치 감지 안함. 
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (bgsprite.sprite == stoneSceneBGImgs[3])
            {
                //마지막 3번 이미지까지 가면 터치 감지 안함. 
                Debug.Log("3번 이미지 입니다.");
                //딱히 상관은 없지만 혹시 모르니 idcount값도 4로 만들어준다.
                idxcount = 4;
            }
            else
            {
                try
                {                    
                    if (idxcount < 4)
                    {
                        snowPopAnimator.SetTrigger("Pop");
                        bgsprite.sprite = stoneSceneBGImgs[idxcount];
                        idxcount++;
                        if (idxcount >= 4)
                        {
                            //스톤 npa 활성화. 
                            FindObjectOfType<CameraController>().npa_Stone[1].SetActive(true);
                        }
                    }
                }
                catch (System.Exception)
                {

                    throw;
                }
            }
        }
    }
}
