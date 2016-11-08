using UnityEngine;
using System.Collections;

public class TextTrigger : MonoBehaviour {
    public GameObject logger;
    public Sprite head;
    public string message;
    private bool fired;
	// Use this for initialization
	void Start () {
        fired = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.Equals(Switcher.instance.currentPlayer) && !fired)
        {
            logger.GetComponent<TextLogger>().SetSpriteAndText(head, message);
            logger.GetComponent<TextLogger>().activated = true;
            fired = true;
        }
    }
}
