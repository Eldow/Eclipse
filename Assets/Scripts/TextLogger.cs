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
    public AudioClip sound;
    public static TextLogger instance;
    public GameObject logger;

    void Awake()
    {
        //Check if there is already an instance of TextLogger
        if (instance == null)
            //if not, set it to this.
            instance = this;
        //If instance already exists:
        else if (instance != this)
            //Destroy this, this enforces our singleton pattern so there can only be one instance of TextLogger.
            Destroy(gameObject);

     
    }

    public void SetSpriteAndText(Sprite sprite, string text)
    {
        StopAllCoroutines();
        StartCoroutine(HideAfterDelay());
        logger.SetActive(true);
        textBox = GetComponentsInChildren<Text>()[0];
        image = GetComponentsInChildren<Image>()[1];
        if (routine != null)
            StopCoroutine(routine);
        textBox.text = "";
        typing = false;
        head = sprite;
        message = text;
        doublePress = false;
        activated = true;
    }
	// Use this for initialization
	void Start () {
	}
	
    IEnumerator HideAfterDelay()
    {
        yield return new WaitForSeconds(15f);
        doublePress = false;
        activated = false;
        logger.SetActive(false);
        message = null;
        typing = false;
    }
	// Update is called once per frame
	void Update () {
        if (activated && message != null)
        {
            image.sprite = head;
            if (!typing)
            {
                typing = true;
                routine = StartCoroutine(TypeText());
            }
            if (Input.GetButtonDown("Action") && doublePress)
            {
                doublePress = false;
                activated = false;
                logger.SetActive(false);
                message = null;
                typing = false;
            }
            if (Input.GetButtonDown("Action"))
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
            SoundManager.instance.RandomizeSfx(sound);
            textBox.text += letter;
            yield return new WaitForSeconds(0.05f);
        }
        doublePress = true;
    }
}
