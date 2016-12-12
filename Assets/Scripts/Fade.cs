using UnityEngine;
using System.Collections;

public class Fade : MonoBehaviour
{
    public Texture2D fadeOutTexture;
    public float fadeSpeed = 1.8f;

    private int drawDepth = -1000;
    public float alpha = 1.0f;
    private int fadeDir = -1;


    void Awake()
    {
        alpha = 1.0f;
    }

    
    void Update()
    {
        {
            alpha = Mathf.Lerp(alpha, 0F, Time.deltaTime/2);
            GUI.color = new Color(.5F, .5F, .5F, alpha);
        }

    }

    void OnGUI()
    {
        GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, alpha);
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), fadeOutTexture);
    }

    void OnLevelWasLoaded()
    {
        alpha = 1.0f;
    }


}
