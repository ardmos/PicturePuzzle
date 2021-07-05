using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// NPA 컨트롤하는 스크립트.
/// 
/// 1. 폴라로이드 카메라의 중심 에임이 가까워지면 StartAnim 애니메이션을 실행한다. 
/// 
/// </summary>


public class NPA : MonoBehaviour
{

    [SerializeField]
    CameraController cameraController;
    [SerializeField]
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        cameraController = FindObjectOfType<CameraController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(cameraController.polaroidCamera.gameObject.transform.position, transform.position) <= cameraController.dis)
        {
            // 근접 했으면
            animator.SetBool("StartAnim", true);
            //Debug.Log("근접!!");
        }
        else
        {
            // 근접 안했으면
            animator.SetBool("StartAnim", false);
            //Debug.Log("dis:"+ cameraController.dis);
            //Debug.Log("distance:" + Vector3.Distance(cameraController.polaroidCamera.gameObject.transform.position, transform.position));
        }
    }
}
