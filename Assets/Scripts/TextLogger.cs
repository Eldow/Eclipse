using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextLogger : MonoBehaviour {

    public bool activated;
    private Sprite head;
    private string message;

    private Text textBox;
    private Image image;
    private Canvas canvas;
    private bool typing = false;
    private Coroutine routine;
    private bool doublePress;

    public void SetSpriteAndText(Sprite sprite, string text)
    {
        head = sprite;
        message = text;
        doublePress = false;
    }
	// Use this for initialization
	void Start () {
        textBox = GetComponentsInChildren<Text>()[0];
        image = GetComponentsInChildren<Image>()[1];
        canvas = GetComponentInChildren<Canvas>();
	}
	
	// Update is called once per frame
	void Update () {
        if (activated && message != null)
        {
            canvas.enabled = true;
            image.sprite = head;
            if (!typing)
            {
                Debug.Log(message);
                typing = true;
                routine = StartCoroutine(TypeText());
            }
            if (Input.anyKeyDown && doublePress)
            {
                doublePress = false;
                activated = false;
                canvas.enabled = false;
                message = null;
                typing = false;
            }
            if (Input.anyKeyDown)
            {
                StopCoroutine(routine);
                textBox.text = "";
                textBox.text = message;
                doublePress = true;
            }
        }
	}

    IEnumerator TypeText()
    {
        foreach (char letter in message.ToCharArray())
        {
            textBox.text += letter;
            yield return new WaitForSeconds(0.02f);
        }
    }
}
