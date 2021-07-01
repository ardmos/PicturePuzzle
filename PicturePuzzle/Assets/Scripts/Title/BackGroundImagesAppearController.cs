using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundImagesAppearController : MonoBehaviour
{
    //폴라로이드
    public GameObject photo;
    //압정
    public GameObject photo용압정;
    //텍스트 등장
    public GameObject forTextAppear;
    //텍스트 호버링
    public GameObject text;
    //트윙클 좌 우 동시
    public GameObject twinkleL, twinkleR;
    //게임을 시작하려면 
    public GameObject 게임을시작하려면;



    // Start is called before the first frame update
    void Start()
    {
        //처음엔 다 비활성화
        photo.SetActive(false);
        photo용압정.SetActive(false);
        forTextAppear.SetActive(false);
        text.SetActive(false);
        twinkleL.SetActive(false);
        twinkleR.SetActive(false);
        게임을시작하려면.SetActive(false);

        ActivatePhoto();
    }

    public void ActivatePhoto()
    {
        //1빠. 
        photo.SetActive(true);
    }
}
