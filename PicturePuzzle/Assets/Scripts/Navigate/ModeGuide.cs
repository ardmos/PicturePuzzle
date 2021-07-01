using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeGuide : MonoBehaviour
{
    public void Start()
    {
        //가이드는 한 번만 나와야하니까 
        if (FindObjectOfType<PlayerData>().didShoot == true)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
            FindObjectOfType<PlayerData>().didShoot = true;
        }

    }

    public void OnScreenClicked()
    {
        gameObject.SetActive(false);
    }
}
