using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EBoxSucAnimObj : MonoBehaviour
{
    //오브젝트 배치에 필요한 정보.
    public Item dragItemData;


    //현 정답효과 애니메이션 오브젝트 파괴 및, 정답 오브젝트 배치.
    public void SelfDestroy()
    {
        //정답 오브젝트 배치.
        FindObjectOfType<EBoxController>().SettingItem(dragItemData);

        //현 오브젝트 파괴
        Destroy(gameObject);
    }
}
