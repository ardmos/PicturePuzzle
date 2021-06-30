using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// StoneSceneManager. 
/// 화면 터치 인식해서 눈사람 없어지도록. 배경 이미지 바꿔줌. 
/// </summary>
public class StoneSceneManager : MonoBehaviour
{
    //배경 이미지들. 
    public Sprite[] stoneSceneBGImgs; // 0~3. 총 4장
    //현재 배경 이미지. 
    public SpriteRenderer bgsprite;

    // Update is called once per frame
    void Update()
    {
        //터치 감지시마다 배경 이미지 바꿔줌. 마지막 3번 이미지까지 가면 터치 감지 안함. 
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            //여기 할 차례.
        }
    }
}
