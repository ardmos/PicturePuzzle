using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Attach this script to a Camera
//Also attach a GameObject that has a Renderer (e.g. a cube) in the Display field
//Press the space key in Play mode to capture

public class CameraViewGraber : MonoBehaviour
{

    /*
    // Grab the camera's view when this variable is true.
    bool grab;

    // The "m_Display" is the GameObject whose Texture will be set to the captured image.
    //public Renderer m_Display;   

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
            //ScreenCapture.
            //Create a new texture with the width and height of the screen
            Texture2D texture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
            //Read the pixels in the Rect starting at 0,0 and ending at the screen's width and height
            texture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0, false);
            texture.Apply();

            FindObjectOfType<Stage0Data>().sprite_Squirrel = Sprite.Create(texture, new Rect(0, 0, Screen.width, Screen.height), new Vector2(0.5f, 0.5f));

            grab = false;
        }
    }    
    */

    IEnumerator Capturing()
    {
        yield return new WaitForEndOfFrame();
        Texture2D img = ScreenCapture.CaptureScreenshotAsTexture();
        Rect rect = new Rect(0, 0, img.width, img.height);

        if (SceneManager.GetActiveScene().name.Contains("Squirrel"))
        {
            FindObjectOfType<Stage0Data>().sprite_Squirrel = Sprite.Create(img, rect, Vector2.one * .5f);
        }
        else if (SceneManager.GetActiveScene().name.Contains("Turtle"))
        {
            FindObjectOfType<Stage0Data>().sprite_Turtle = Sprite.Create(img, rect, Vector2.one * .5f);
        }
        else if (SceneManager.GetActiveScene().name.Contains("Stone"))
        {
            FindObjectOfType<Stage0Data>().sprite_Stone = Sprite.Create(img, rect, Vector2.one * .5f);
        }
        else if (SceneManager.GetActiveScene().name.Contains("Wood"))
        {
            FindObjectOfType<Stage0Data>().sprite_Wood = Sprite.Create(img, rect, Vector2.one * .5f);
        }

        //Debug.Log("Capturing");
    }

    public void Snapshot()
    {
        //Debug.Log("SnapShot");
        StartCoroutine(Capturing());
    }
}
