using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 움직이는 아이콘 이미지 컨트롤러. 
/// 
/// 얘를 세 개 정도 생성해서 슈슈슝 움직이는것처럼 보이게 한다. 
/// 레이어는 착착착. 
/// 
/// UI 이기 때문에 레이어는 오브젝트 순서대로 됨...
/// 따라서 
/// 
/// 3 장 순차적으로 만드는 제너레이터 <- 
/// 
/// 다 만들어지면 가장 나중 녀석부터 이동 시작 <- 근소한 차이로 진행
/// 
/// 
/// </summary>


public class ObjMove : MonoBehaviour
{
    //움직이는 아이콘 이미지 프리팹.
    public GameObject pref_MPII;

    //부모가 되어줄 캔버스 오브젝트.
    public GameObject canvas;

    //둘 중 어떤 애니메이션을 실행시킬지 결정하는 변수
    public string str;

    #region ToInven
    public void StartMoveToInven()
    {
        str = "ToInven";
        StartCoroutine(GenerateThreeObj());
        
    }
    #endregion

    #region ToEBox1
    public void StartMoveToEBox1()
    {
        str = "ToEBox1";
        StartCoroutine(GenerateThreeObj_ForDrag());
       
    }
    public void StopMoveToEBox1()
    {
        Debug.Log("StopMoveToEBox1");
        StopAllCoroutines();
    }
    #endregion

    //Canvas 밑에 오브젝트를 만드는 부분
    private void MoveToInvenObjGenerator()
    {
        GameObject gameObject = Instantiate(pref_MPII) as GameObject;
        gameObject.transform.SetParent(canvas.transform);
    }

    //이동을 시작하는 부분  2, 1, 0 순서. 투명도도 조절한다.
    private void StartMoveAnim(int layer)
    {
        float transparency;
        switch (layer)
        {
            case 0:
                transparency = 0.3f;
                break;
            case 1:
                transparency = 0.6f;
                break;
            case 2:
                transparency = 1f;
                break;
            default:
                transparency = 1f;
                break;
        }
        canvas.transform.GetChild(layer).gameObject.GetComponent<Image>().color = new Color(1, 1, 1, transparency);
        canvas.transform.GetChild(layer).gameObject.GetComponent<Animator>().SetBool(str, true);
    }
    

    IEnumerator GenerateThreeObj()
    {
        MoveToInvenObjGenerator();
        MoveToInvenObjGenerator();
        MoveToInvenObjGenerator();

        StartMoveAnim(2);
        yield return new WaitForSeconds(0.03f);
        StartMoveAnim(1);
        yield return new WaitForSeconds(0.03f);
        StartMoveAnim(0);
    }

    IEnumerator GenerateThreeObj_ForDrag()
    {
        while (true)
        {
            MoveToInvenObjGenerator();
            MoveToInvenObjGenerator();
            MoveToInvenObjGenerator();

            StartMoveAnim(2);
            yield return new WaitForSeconds(0.03f);
            StartMoveAnim(1);
            yield return new WaitForSeconds(0.03f);
            StartMoveAnim(0);

            yield return new WaitForSeconds(0.5f);
        }
        
    }

}
