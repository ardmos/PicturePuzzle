using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 시도, 재시도 관련 처리.
/// 1. 화면 더블클릭시 시도 발동
/// 2. 재시도버튼 클릭시 재시도 발동
/// </summary>

public class SamplePic0_0Manager : MonoBehaviour
{
    public float lastTouchTime, currentTouchTime;
    float touchInterval = 0.3f;
    public float time;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time = Time.time;
        DoubleTouchListener();
    }

    #region 더블클릭 인식 처리
    void DoubleTouchListener()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                Debug.Log("UI를 만났어요!~ 더블클릭이 발동되지 않아요");
            }
            else
            {
                currentTouchTime = Time.time;
                //더블터치가 맞는지 체크! 이전 터치와의 시간 간격으로 확인한다. 
                if ((currentTouchTime-lastTouchTime) <= touchInterval)
                {
                    //시도 기능 발동
                    //Debug.Log("시도 발동!! currentTouchTime:" + currentTouchTime + ", lastTouchTime:" + lastTouchTime + ", Interver:" + (currentTouchTime - lastTouchTime) + ", touchInterval:" + touchInterval);
                    Try();
                }
                else Debug.Log("더블터치가 아닙니다!! currentTouchTime:" + currentTouchTime + ", lastTouchTime:" + lastTouchTime + ", Interver:" + (currentTouchTime - lastTouchTime) + ", touchInterval:" + touchInterval);

                lastTouchTime = currentTouchTime;
            }
        }
    }
    #endregion

    #region 시도
    public void Try()
    {
        /// <summary>
        /// 시도 메서드.
        /// 현재 위치를 저장할 num값 하나 필요. 
        /// 이 값을 기준으로 진행함.
        /// 
        /// 1. 다음 이동할 EBox의 클리어상태 확인. 
        /// 2. 클리어 상태에 따라 다른 애니메이션 실행
        /// 3. 클리어상태 false일 경우 다음 진행 X.  true일 시 계속 실행. 
        /// 4. 마지막 EBox까지 모두 true였으면 clear 애니메이션 실행. 
        /// </summary> 


    }
    #endregion

    #region 재시도
    public void ReTry()
    {

    }
    #endregion
}
