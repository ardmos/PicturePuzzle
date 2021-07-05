using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// EBox
/// 1. 이미지 배치 여부 확인
/// 2. State 확인
/// 3. State에 따른 이미지 효과 자동 처리
///
///
/// ***** 옳은 아이템이 배치됐는지 확인하는 절차가 필요함 *****
/// 그것을 위한.  아이템 네임을 EBox가 미리 갖고 있다가.  오려는 아이템의 이름을 확인 후 배치 허용한다.
/// </summary>
 


public class EBox : MonoBehaviour
{
    [SerializeField]
    bool isfull;
    public enum EBoxState
    {
        Normal,
        HighLighted,
        LightsOut
    }
    [SerializeField]
    EBoxState eBoxState = EBoxState.Normal;    
    //옳은 아이템인가 확인을 위한 이름값. 다른곳에서 호출해서 씀.
    [SerializeField]
    string itemName;

    #region Get-Set
    public void SetFull(bool boolean)
    {
        if (boolean)
        {
            isfull = true;
            if (itemName == "Stone")
            {
                //Stone EBox일 경우 Stone 가라앉기 시작
                StoneCheckTime();
            }
        }
        else isfull = false;
    }
    public bool GetFull()
    {
        return isfull;
    }

    public void SetEBoxState(EBoxState state)
    {
        eBoxState = state;
    }
    public EBoxState GetEBoxState()
    {
        return eBoxState;
    }

    public string GetItemName()
    {
        return itemName;
    }
    #endregion

    // Update is called once per frame
    void Update()
    {
        //상태에 따른 EBox 이미지 효과 처리
        if (isfull || eBoxState == EBoxState.LightsOut)
        {
            //노말 하이라이트 둘 다 아닌 경우. 다 끄기 LightsOut
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(false);
            transform.GetChild(2).gameObject.SetActive(false);
        }   
        else if (eBoxState == EBoxState.Normal)
        {
            //노말
            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(1).gameObject.SetActive(false);
            //핀 설치 
            transform.GetChild(2).gameObject.SetActive(true);
        }
        else if (eBoxState == EBoxState.HighLighted)
        {
            //하이라이트
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(true);
            //핀 설치 
            transform.GetChild(2).gameObject.SetActive(true);
        }
    }

    #region 모든 코루틴 종료
    public void StopsAllAnimationCoroutines()
    {
        try
        {
            //물방울 원위치
            gameObject.transform.GetChild(3).localPosition = Vector3.zero;
            gameObject.transform.GetChild(3).gameObject.SetActive(false);
        }
        catch (System.Exception ex)
        {
            Debug.Log(ex.Message);
        }
        StopAllCoroutines();
    }
    #endregion

    #region 배치 실패 애니메이션. 오브젝트는 그냥 슝~ 내려가고 물 튀는 애니메이션 실행.
    public void StartSetFailAnim()
    {
        //슝 ~ 내려가고
        StartCoroutine(MoveToBottom(1f, 0.01f, 0.05f));
        //물 튀는 애니메이션 실행
        StartCoroutine(WaterPop());
    }
    #endregion

    #region 배치 성공 애니메이션. 샤라락~ 파칭!
    public void StartSetSuccessAnim()
    {

    }
    #endregion



    ///
    ///Stone인 경우 호출되는 부분.  시간 지나면 가라앉아요! 
    ///1. 시간 체크. 
    ///2. 시간이 지나면, 가라앉기Anim 재생.  
    ///3. 가라앉기Anim 끝나는 부분에 애니메이션이벤트로 isFull을 False로 만듦.     
    ///
    #region 돌 시간 지나면 가라앉아요~!
    public void StoneCheckTime()
    {
        //시간 체크 후 시간이 되면 가라앉기Anim 실행
        StartCoroutine(StartStoneCheckTimeAndAnim());
    }    
    IEnumerator StartStoneCheckTimeAndAnim()
    {
        //3초 후 가라앉아요~!
        Debug.Log("6초 후 가라앉아요~!");
        yield return new WaitForSeconds(6f);
        //가라앉기Anim 실행
        Debug.Log("가라앉기Anim 실행");
        StartCoroutine(MoveToBottom(2f, 0.1f, 0.1f));
        SetFull(false);
        //EBox 효과 소등.
        eBoxState = EBoxState.LightsOut;
    }
    #endregion

    #region 애니메이션 모음
    IEnumerator MoveToBottom(float totalTime, float timeinterval, float step)
    {
        //물체 아래로 내려가는 애니메이션
        float totalAnimTime = totalTime;
        while (totalAnimTime >= 0)
        {
            yield return new WaitForSeconds(timeinterval);
            totalAnimTime -= timeinterval;
            try
            {
                //돌 오브젝트 하강
                //오브젝트는 항상 EBox오브젝트의 마지막 자식일것이기 때문에 아래처럼 진행
                int objidx = gameObject.transform.childCount - 1;
                //Debug.Log(gameObject.transform.childCount);
                Vector2 objPos = gameObject.transform.GetChild(objidx).position;
                gameObject.transform.GetChild(objidx).position = new Vector2(objPos.x, objPos.y - step);
            }
            catch (System.Exception ex)
            {
                Debug.Log(ex.Message);
            }
        }
        //가라앉기 끝나면 오브젝트 삭제
        try
        {
            int objidx = gameObject.transform.childCount - 1;
            Destroy(gameObject.transform.GetChild(objidx).gameObject);
        }
        catch (System.Exception ex)
        {
            Debug.Log(ex.Message);
        }
    }
    IEnumerator WaterPop()
    {
        //물 튀는 애니메이션
        //물 오브젝트 활성화. 물방울은 항상 4번째 오브젝트
        //
        int objidx = 3;
        gameObject.transform.GetChild(objidx).gameObject.SetActive(true);
        float totalAnimTime = 0.3f;
        while (totalAnimTime >= 0)
        {
            yield return new WaitForSeconds(0.01f);
            totalAnimTime -= 0.01f;
            try
            {
                //돌 오브젝트 상승                
                Vector2 objPos = gameObject.transform.GetChild(objidx).position;
                gameObject.transform.GetChild(objidx).position = new Vector2(objPos.x, objPos.y + 0.05f);
            }
            catch (System.Exception ex)
            {
                Debug.Log(ex.Message);
            }
        }
        //물이 최고높이에 도달하면 0.3초 후에 비활성화, 원위치됨.
        yield return new WaitForSeconds(0.3f);
        try
        {            
            gameObject.transform.GetChild(objidx).localPosition = Vector3.zero;
            gameObject.transform.GetChild(objidx).gameObject.SetActive(false);
        }
        catch (System.Exception ex)
        {
            Debug.Log(ex.Message);
        }
    }
    #endregion


}
