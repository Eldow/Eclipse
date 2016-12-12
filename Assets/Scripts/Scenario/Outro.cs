using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Outro : MonoBehaviour {
    public Sprite raHead, profHead;
	// Use this for initialization
	void Start () {
        StartCoroutine(OutroScript());
	}
	IEnumerator OutroScript()
    {
        TextLogger.instance.SetSpriteAndText(raHead, "I forgot to tell you...");
        yield return new WaitForSeconds(5f);
        TextLogger.instance.SetSpriteAndText(raHead, "If you had to make this while being cursed...");
        yield return new WaitForSeconds(5f);
        TextLogger.instance.SetSpriteAndText(raHead, "It's just because i don't really know...");
        yield return new WaitForSeconds(5f);
        TextLogger.instance.SetSpriteAndText(raHead, "How to remove it.");
        yield return new WaitForSeconds(5f);
        TextLogger.instance.SetSpriteAndText(profHead, "...");
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(9);
    }
	// Update is called once per frame
	void Update () {
	
	}
}
