using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class ScreenCapture : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //string path = Application.persistentDataPath + "\\squirrelCap.jpg";
        string path = "Assets/Resources/squirrelCap.jpg";
        Debug.Log(path);

        StartCoroutine(SaveScreeJpg(path));

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SaveScreeJpg(string filePath)
    {
        yield return new WaitForEndOfFrame();

        Texture2D texture = new Texture2D(Screen.width, Screen.height);
        texture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        texture.Apply();
        byte[] bytes = texture.EncodeToJPG();
        //FindObjectOfType<Stage0Data>().squirrel = texture;

        StreamWriter streamWriter = new StreamWriter(filePath);

        File.Create(filePath);
        File.WriteAllBytes(filePath, bytes);
        DestroyImmediate(texture);

    }

}
