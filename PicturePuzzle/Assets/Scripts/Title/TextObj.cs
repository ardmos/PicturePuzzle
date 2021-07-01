using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextObj : MonoBehaviour
{
    public GameObject twinkleL, twinkleR;
    

    public void ActiveTwinkleLR()
    {
        //Debug.Log("out");
        if (twinkleL.activeSelf != true)
        {
//            Debug.Log("======");
 //           Debug.Log(twinkleL.activeSelf);
  //          Debug.Log(twinkleR.activeSelf);
   //         Debug.Log("in");
    //        Debug.Log("======");
            twinkleL.SetActive(true);
            twinkleR.SetActive(true);
      //      Debug.Log(twinkleL.activeSelf);
        //    Debug.Log(twinkleR.activeSelf);
        }
    }
}
