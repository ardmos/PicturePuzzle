using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 시도, 재시도 관련 처리.
/// 1. 화면 더블클릭시 시도 발동
/// 2. 재시도버튼 클릭시 재시도 발동
/// </summary>

public class Pic0_0Manager : MonoBehaviour
{
    public float lastTouchTime, currentTouchTime;
    float touchInterval = 0.3f;
    public float time;
    //현재 다람쥐 위치 저장할 변수
    [SerializeField]
    int curSquirrelPos = 0;
    //다람쥐 프리팹
    public GameObject squirrelPref;

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
            //안드로이드에서의 EventSystem.curren.IsPointerOverGameObject 처리
            if (Application.platform == RuntimePlatform.Android)
            {
                if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
                {
                    //Debug.Log("UI를 만났어요!~ 더블클릭이 발동되지 않아요");
                }
                else
                {
                    currentTouchTime = Time.time;
                    //더블터치가 맞는지 체크! 이전 터치와의 시간 간격으로 확인한다. 
                    if ((currentTouchTime - lastTouchTime) <= touchInterval)
                    {
                        //시도 기능 발동
                        //Debug.Log("시도 발동!! currentTouchTime:" + currentTouchTime + ", lastTouchTime:" + lastTouchTime + ", Interver:" + (currentTouchTime - lastTouchTime) + ", touchInterval:" + touchInterval);
                        Try();
                    }
                    //else Debug.Log("더블터치가 아닙니다!! currentTouchTime:" + currentTouchTime + ", lastTouchTime:" + lastTouchTime + ", Interver:" + (currentTouchTime - lastTouchTime) + ", touchInterval:" + touchInterval);

                    lastTouchTime = currentTouchTime;
                }
            }
            else
            {
                if (EventSystem.current.IsPointerOverGameObject())
                {
                    //Debug.Log("UI를 만났어요!~ 더블클릭이 발동되지 않아요");
                }
                else
                {
                    currentTouchTime = Time.time;
                    //더블터치가 맞는지 체크! 이전 터치와의 시간 간격으로 확인한다. 
                    if ((currentTouchTime - lastTouchTime) <= touchInterval)
                    {
                        //시도 기능 발동
                        //Debug.Log("시도 발동!! currentTouchTime:" + currentTouchTime + ", lastTouchTime:" + lastTouchTime + ", Interver:" + (currentTouchTime - lastTouchTime) + ", touchInterval:" + touchInterval);
                        Try();
                    }
                    //else Debug.Log("더블터치가 아닙니다!! currentTouchTime:" + currentTouchTime + ", lastTouchTime:" + lastTouchTime + ", Interver:" + (currentTouchTime - lastTouchTime) + ", touchInterval:" + touchInterval);

                    lastTouchTime = currentTouchTime;
                }
            }


        }
    }
    #endregion

    #region 시도
    public void Try()
    {
        /// <summary>
        /// 시도 메서드.
        /// 
        /// ***이 Try 메서드 자체는, 첫 실행은 더블터치로 실행되지만, 단계가 성공할 때 마다 해당 단계 성공 애니메이션의 끝에 애니메이션 이벤트로 호출된다.***
        /// 
        /// 현재 위치를 저장할 num값 하나 필요. 
        /// 이 값을 기준으로 진행함.
        /// 
        /// 현재 위치값에 따른 처리.
        /// 
        /// 1. 다음 이동할 EBox의 클리어상태 확인. (클리어상태는 EBox의 Full로 확인.)
        /// 2. 클리어 상태에 따라 다른 애니메이션 실행
        /// 3. 클리어상태 false일 경우 다음 진행 X.  true일 시 계속 실행. 
        /// 4. 마지막 EBox까지 모두 true였으면 clear 애니메이션 실행. 
        /// </summary> 

        GameObject[] EBoxes = FindObjectOfType<EBoxController>().EBoxes; 

        switch (curSquirrelPos)
        {
            //다람쥐 위치값에 따른 처리. isFull이면 성공Anim, 아니면 실패Anim
            case 0:             
                //시작 섬. 첫 번째 EBox의 isFull을 체크.
                if (EBoxes[curSquirrelPos].GetComponent<EBox>().GetFull())
                {
                    curSquirrelPos++;
                    Debug.Log("다음 목표인 " + curSquirrelPos + "번째 EBox는 Full 입니다. " + curSquirrelPos + "번째 성공 애니메이션을 실행합니다.");
                    //성공
                    FindObjectOfType<Squirrel>().SquirrelToEBox0();
                }
                else
                {
                    Debug.Log(curSquirrelPos + "번째 실패 애니메이션을 실행합니다. ");
                    //실패 애니메이션 재생
                    FindObjectOfType<Squirrel>().SquirrelToEBox0F();
                }
                break;
            case 1:               
                //첫 번째 EBox. 두 번째 EBox의 isFull을 체크.
                if (EBoxes[curSquirrelPos].GetComponent<EBox>().GetFull())
                {
                    curSquirrelPos++;
                    Debug.Log("다음 목표인 " + curSquirrelPos + "번째 EBox는 Full 입니다. " + curSquirrelPos + "번째 성공 애니메이션을 실행합니다.");
                    //성공
                    FindObjectOfType<Squirrel>().SquirrelToEBox1();
                }
                else
                {
                    Debug.Log(curSquirrelPos + "번째 실패 애니메이션을 실행합니다. ");
                    //실패 애니메이션 재생
                    FindObjectOfType<Squirrel>().SquirrelToEBox1F();
                }
                break;
            case 2: 
                //두 번째 EBox. 세 번째 EBox의 isFull을 체크.
                if (EBoxes[curSquirrelPos].GetComponent<EBox>().GetFull())
                {
                    curSquirrelPos++;
                    Debug.Log("다음 목표인 " + curSquirrelPos + "번째 EBox는 Full 입니다. " + curSquirrelPos + "번째 성공 애니메이션을 실행합니다.");
                    //성공
                    FindObjectOfType<Squirrel>().SquirrelToEBox2();
                }
                else
                {
                    Debug.Log(curSquirrelPos + "번째 실패 애니메이션을 실행합니다. ");
                    //실패 애니메이션 재생
                    FindObjectOfType<Squirrel>().SquirrelToEBox2F();
                }
                break;
            case 3:
                //다람쥐 위치가 3이라는건 이미 다 클리어했다는것.
                //클리어는 확인할필요 없음 그냥 이동. 
                //세 번째 EBox. Clear 애니메이션을 실행.
                Debug.Log("클리어 애니메이션을 실행합니다.");
                FindObjectOfType<Squirrel>().SquirrelToClear();                               
                break;
            default:
                break;
        }
    }
    #endregion

    #region 재시도
    public void ReTry()
    {
        //클리어시에는 재시도 버튼 비활성화.
        if (curSquirrelPos == 3)
        {
            //비활성화.
            return;
        }

        //curSquirrelPos 초기화.
        curSquirrelPos = 0;
        //다람쥐오브젝트 존재하지 않을경우 생성. 
        //존재할경우 파괴 후 생성.
        GameObject sqrl = GameObject.FindGameObjectWithTag("Squirrel");
        if (sqrl != null) //찾을 수 없으면 null이 반환됩니다. 
        {
            Destroy(sqrl);
        }
        //다람쥐 생성
        GameObject newSquirrel = Instantiate(squirrelPref) as GameObject;
        newSquirrel.transform.SetParent(GameObject.Find("=====GameObjects=====").transform);

        //EBox들 isFull 초기화.
        //EBox들 오브젝트 초기화. 
        FindObjectOfType<EBoxController>().ResetEBoxes();

        //인벤토리 초기화. (현재 씬 열릴 때 갖고있었던대로.)
        //여기 할 차례!!~



    }
    #endregion
}
