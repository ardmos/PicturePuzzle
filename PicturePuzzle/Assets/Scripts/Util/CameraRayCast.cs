using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 터치한 위치에 레이캐스트 발사하고, 맞은게 있으면 돌려주는 스크립트
/// 
/// </summary>

public class CameraRayCast : MonoBehaviour
{
    private RaycastHit hit;
    float maxDistance = 300f; // Mathf.Infinity 도 있음.  

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            FireRaser(Input.mousePosition);
        }
    }


    public void FireRaser(Vector3 targetPos)
    {
        Ray ray = Camera.main.ScreenPointToRay(targetPos);

        Debug.Log("Called : " + Camera.main.ScreenToWorldPoint(targetPos));
        if (Physics.Raycast(ray, out hit, maxDistance))
        {
            Debug.Log(hit.collider.gameObject.name);
            Debug.DrawRay(ray.origin, ray.direction * 10f, Color.green, 5f);
        }
        else
        {
            Debug.DrawRay(ray.origin, ray.direction * 10f, Color.red, 5f);
        }
    }
}
