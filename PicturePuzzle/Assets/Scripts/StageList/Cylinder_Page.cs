using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cylinder_Page : MonoBehaviour
{
    public void FlipIsOver()
    {
        FindObjectOfType<BookController>().CallWhenFlipOver(gameObject);
    }
}
