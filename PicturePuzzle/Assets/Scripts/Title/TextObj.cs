using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextObj : MonoBehaviour
{
    public GameObject twinkleL, twinkleR;
    

    public void ActiveTwinkleLR()
    {
        if (twinkleL.activeSelf != true)
        {
            twinkleL.SetActive(true);
            twinkleR.SetActive(true);
        }
    }
}
