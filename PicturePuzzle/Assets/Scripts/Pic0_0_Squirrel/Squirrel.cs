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
    /// 포물선 최대 높이 처리 필요. <-- 애니메이션으로 해결했음.
    /// </summary>
    /// 

    [SerializeField]
    Animator animator, sFamilyAnimator;

    private void Start()
    {
        sFamilyAnimator = GameObject.Find("SquirrelFamily").GetComponent<Animator>();
    }

    public void SquirrelTry()
    {
        //다람쥐 시도!   성공 애니메이션에서 애니메이션 이벤트로 호출됨
        FindObjectOfType<Pic0_0Manager>().Try();
    }



    #region 성공 실패 애니메이션 실행 
    public void SquirrelToEBox0()
    {
        animator.SetBool("SToEBox0", true);
    }
    public void SquirrelToEBox0F()
    {
        animator.SetBool("SToEBox0F", true);
    }
    public void SquirrelToEBox1()
    {
        animator.SetBool("SToEBox1", true);
    }
    public void SquirrelToEBox1F()
    {
        animator.SetBool("SToEBox1F", true);
    }
    public void SquirrelToEBox2()
    {
        animator.SetBool("SToEBox2", true);
    }
    public void SquirrelToEBox2F()
    {
        animator.SetBool("SToEBox2F", true);
    }
    public void SquirrelToClear()
    {
        animator.SetBool("SToClear", true);
    }
    public void SquirrelFamilyCheer()
    {
        //다람쥐와 다람쥐 가족 환호 애니메이션 실행
        animator.SetBool("SCheer", true);
        sFamilyAnimator.SetBool("SCheer", true);
    }

    public void SquirrelFail()
    {
        //실패 애니메이션
        StartCoroutine(MoveToBottom(1f, 0.01f, 0.05f));
    }
    #endregion


    #region 실패시 아래로 내려가는 애니메이션
    IEnumerator MoveToBottom(float totalTime, float timeinterval, float step)
    {
        //일단 애니메이터 비활성화
        animator.enabled = false;

        //내려가기 전에 스프라이트 레이어 변환시켜줘야함. 물 속으로 들어가게 하기 위해서! 백그라운드 2로 바꿔주기.
        gameObject.GetComponent<SpriteRenderer>().sortingLayerName = "BackGround";
        gameObject.GetComponent<SpriteRenderer>().sortingOrder = 2;

        //물체 아래로 내려가는 애니메이션
        float totalAnimTime = totalTime;
        while (totalAnimTime >= 0)
        {
            yield return new WaitForSeconds(timeinterval);
            totalAnimTime -= timeinterval;
            try
            {                 
                Vector2 objPos = gameObject.transform.position;
                gameObject.transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y - step);
                //Debug.Log(gameObject.transform.position.y + ", step:" + step + ", totalAnimTime:" + totalAnimTime);
            }
            catch (System.Exception ex)
            {
                Debug.Log(ex.Message);
            }
        }
        //가라앉기 끝나면 오브젝트 삭제
        try
        {            
            Destroy(gameObject);
        }
        catch (System.Exception ex)
        {
            Debug.Log(ex.Message);
        }
    }
    #endregion

}
