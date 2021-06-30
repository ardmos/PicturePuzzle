using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// StoneSceneManager. 
/// 화면 터치 인식해서 눈사람 없어지도록. 배경 이미지 바꿔주고, 스노우팝 애니메이션 해줌.
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
    
    //눈사람 콜라이더(현 스크립트 오브젝트에 달려있음.)


    private void OnMouseDown()
    {
        //터치 감지시마다 배경 이미지 바꿔주고 스노우팝 애니메이션 실행해줌. 마지막 3번 이미지까지 가면 터치 감지 안함. 
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (idxcount >= 4)
            {
                //마지막 3번 이미지까지 가면 터치 감지 안함. 
                Debug.Log("3번 이미지 입니다.");
            }
            else
            {
                snowPopAnimator.SetTrigger("Pop");
                bgsprite.sprite = stoneSceneBGImgs[idxcount];
                idxcount++;
                if(idxcount >= 4)
                {
                    //스톤 npa 활성화. 
                    FindObjectOfType<CameraController>().npa_Stone[1].SetActive(true);
                }
            }

        }
    }
}
