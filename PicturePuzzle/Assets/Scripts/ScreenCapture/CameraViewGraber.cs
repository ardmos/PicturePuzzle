using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Attach this script to a Camera
//Also attach a GameObject that has a Renderer (e.g. a cube) in the Display field
//Press the space key in Play mode to capture

public class CameraViewGraber : MonoBehaviour
{
    // Grab the camera's view when this variable is true.
    bool grab;

    // The "m_Display" is the GameObject whose Texture will be set to the captured image.
    //public Renderer m_Display;
    public Renderer m_Display;    

    private void Update()
    {
        //Press space to start the screen grab
        if (Input.GetKeyDown(KeyCode.Space))
            grab = true;
    }

    //계속호출.  
    private void OnPostRender()
    {
        if (grab)
        {
            //Create a new texture with the width and height of the screen
            Texture2D texture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
            //Read the pixels in the Rect starting at 0,0 and ending at the screen's width and height
            texture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0, false);
            texture.Apply();
            //Check that the display field has been assigned in the Inspector
            //if (m_Display != null)
            //{
                //byte[] bytes = texture.EncodeToPNG();

                //string path = "Assets/Resources/" + "cap2" + ".png";
                //string path = Application.persistentDataPath + "/cap1" + ".png";

                //Debug.Log(path);
                //알아서 만들고 쓰고 닫고.  이미 존재하면 덮어쓰고.
                //System.IO.File.WriteAllBytes(path, bytes);

                //FindObjectOfType<Stage0Data>().SetSprite(Sprite.Create(texture, new Rect(0, 0, Screen.width / 2, Screen.height / 2), new Vector2(0.5f, 0.5f)));

                //Give your GameObject with the renderer this texture
                //m_Display.material.mainTexture = texture;
                FindObjectOfType<Stage0Data>().texture = texture;
                //m_Display.sprite = Sprite.Create(texture, new Rect(0, 0, Screen.width/2, Screen.height/2), new Vector2(0.5f, 0.5f));
            //}
            //Reset the grab state
            grab = false;
        }
    }
}
