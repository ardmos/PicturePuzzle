using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleSceneManager : MonoBehaviour
{
    //현재 배경 이미지. 
    public SpriteRenderer bgsprite;

    //씬 상태 기록되는곳.
    PlayerData playerData;

    //씬이 끝날 때 호출되는 함수. esc키가 눌렸을 때 호출된다.
    public void SendDataToPlayerData()
    {
        //배경이미지 기록      
        playerData.curBGObj_Turtle = bgsprite.sprite;
        playerData.isrecorded_Turtle = true;
        //스크린샷으로 저장. 갤러리에서 보여주기 위한 이미지.
        FindObjectOfType<CameraViewGraber>().Snapshot();
        //Debug.Log("TurtleSceneManager");

        //NPA 기록 
        GameObject[] npaTurtleArr = FindObjectOfType<CameraController>().npa_Turtle;
        for (int i = 0; i < npaTurtleArr.Length; i++)
        {
            playerData.npa_Turtle_ActiveSelf[i] = npaTurtleArr[i].transform.parent.gameObject.activeSelf;
        }
    }


    private void Start()
    {
        playerData = FindObjectOfType<PlayerData>();

        //씬이 시작될 때 playerData에 저장된 그림씬 데이터에 따라서 화면 구성. 
        //isrecored 여부에 따라. true일때만 읽어온대로 구성.
        if (playerData.isrecorded_Turtle)
        {
            //배경이미지
            bgsprite.sprite = playerData.curBGObj_Turtle;

            //NPA 로드. 
            //읽어와서 우리 npa들 SetActive에 적용. index 순서 같으니까 idx기준으로 끼리끼리.
            GameObject[] npaTurtleArr = FindObjectOfType<CameraController>().npa_Turtle;
            for (int i = 0; i < playerData.npa_Turtle_ActiveSelf.Length; i++)
            {
                //Debug.Log("Start===itemIndex:" + i + ", 의 값:" + playerData.npa_Turtle_ActiveSelf[i]);
                npaTurtleArr[i].transform.parent.gameObject.SetActive(playerData.npa_Turtle_ActiveSelf[i]);
            }

            //애니메이터는 안썼음.
        }
    }

}
