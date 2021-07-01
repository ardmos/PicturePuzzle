using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForTextAppear : MonoBehaviour
{
    public GameObject textObj;

    public void ActiveTextObj()
    {
        //Debug.Log("======");
        //Debug.Log(textObj.activeSelf);
        //Debug.Log("in");
        //Debug.Log("======");
        textObj.SetActive(true);
        //Debug.Log(textObj.activeSelf);


       
    }
}
