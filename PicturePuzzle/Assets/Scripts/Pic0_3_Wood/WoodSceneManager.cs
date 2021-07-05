using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodSceneManager : MonoBehaviour
{
    //현재 배경 이미지. 
    public SpriteRenderer bgsprite;

    //씬 상태 기록되는곳.
    PlayerData playerData;

    //사람 쿵야 애니메이터
    public Animator humanAnimator;
    

    //씬이 끝날 때 호출되는 함수. esc키가 눌렸을 때 호출된다.
    public void SendDataToPlayerData()
    {
        //배경이미지 기록      
        playerData.curBGObj_Wood = bgsprite.sprite;
        playerData.isrecorded_Wood = true;
        //스크린샷으로 저장. 갤러리에서 보여주기 위한 이미지.
        FindObjectOfType<CameraViewGraber>().Snapshot();

        //NPA 기록 
        GameObject[] npaWoodArr = FindObjectOfType<CameraController>().npa_Wood;
        for (int i = 0; i < npaWoodArr.Length; i++)
        {
            playerData.npa_Wood_ActiveSelf[i] = npaWoodArr[i].transform.parent.gameObject.activeSelf;
        }

        //쿵야 상태 기록
        if (!playerData.isFallTrue)
        {
            playerData.isFallTrue = humanAnimator.GetBool("Fall");
        }
 
    }


    private void Start()
    {
        playerData = FindObjectOfType<PlayerData>();

        //씬이 시작될 때 playerData에 저장된 그림씬 데이터에 따라서 화면 구성. 
        //isrecored 여부에 따라. true일때만 읽어온대로 구성.
        if (playerData.isrecorded_Wood)
        {
            //배경이미지
            bgsprite.sprite = playerData.curBGObj_Wood;

            //NPA 로드. 
            //읽어와서 우리 npa들 SetActive에 적용. index 순서 같으니까 idx기준으로 끼리끼리.
            GameObject[] npaWoodArr = FindObjectOfType<CameraController>().npa_Wood;
            for (int i = 0; i < playerData.npa_Wood_ActiveSelf.Length; i++)
            {
                //Debug.Log("Start===itemIndex:" + i + ", 의 값:" + playerData.npa_Wood_ActiveSelf[i]);
                npaWoodArr[i].transform.parent.gameObject.SetActive(playerData.npa_Wood_ActiveSelf[i]);
            }

            //애니메이터 상태 로드.
            if (playerData.isFallTrue) //넘어져있다가 참이면.
            {
                humanAnimator.SetBool("Fallen", true);
            }
            
        }
    }
}
