using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Squirrel : MonoBehaviour
{
    /// <summary>
    /// 다람쥐.
    /// 각 EBox로의 이동 성공/실패, 최동 클리어 애니메이션 보유.
    ///
    ///
    ///
    /// 포물선 최대 높이 처리 필요.
    /// </summary>


    /*
// Start is called before the first frame update
void Start()
{

}

// Update is called once per frame
void Update()
{
    //EBox0으로 이동 성공하는 경우.
    //transform.position = Vector3.Slerp(transform.position,FindObjectOfType<EBoxController>().EBoxes[0].transform.position, 0.1f);
    Vector2 startPos = transform.position;
    Vector2 endPos = FindObjectOfType<EBoxController>().EBoxes[0].transform.position;
    Vector2 centerPos = (startPos + endPos) / 2;
    transform.position = Vector2.Lerp(Vector2.Lerp(startPos, centerPos, 0.1f), Vector2.Lerp(centerPos, endPos, 0.1f), 0.1f);
}
*/


        //Unity doc 예시...  높이 관련??
    public Transform sunrise;
    public Transform sunset;
    public float journeyTime = 1.0F;
    private float startTime;
    void Start()
    {
        startTime = Time.time;
    }
    void Update()
    {
        Vector3 center = (sunrise.position + sunset.position) * 0.5F;
        center -= new Vector3(0, 1, 0);
        Vector3 riseRelCenter = sunrise.position - center;
        Vector3 setRelCenter = sunset.position - center;
        float fracComplete = (Time.time - startTime) / journeyTime;
        transform.position = Vector3.Slerp(riseRelCenter, setRelCenter, fracComplete);
        transform.position += center;
    }
}
