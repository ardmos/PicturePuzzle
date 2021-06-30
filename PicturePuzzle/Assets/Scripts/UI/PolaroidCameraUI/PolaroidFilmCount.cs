using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 폴라로이드 필름 카운트 실시간 반영해주는 스크립트. 
/// PlayerData.cs로부터 읽어와서 띄워주기.
/// </summary>


public class PolaroidFilmCount : MonoBehaviour
{
    public Text text;
    public PlayerData playerData;

    private void Start()
    {
        text = GetComponentInChildren<Text>();
        playerData = FindObjectOfType<PlayerData>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "X " + playerData.GetPlayerFilmCount().ToString();
    }
}
