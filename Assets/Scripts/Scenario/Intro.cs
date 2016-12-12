using UnityEngine;
using System.Collections;

public class Intro : MonoBehaviour {

    public Sprite profHead;
    public Sprite raHead;
    public Sprite apoHead;
	// Use this for initialization
	void Awake () {
        StartCoroutine(NextLevel());
	}
	IEnumerator NextLevel()
    {
        yield return new WaitForSeconds(5f);
        TextLogger.instance.SetSpriteAndText(profHead, "What is this shine ?!");
        yield return new WaitForSeconds(5f);
        TextLogger.instance.SetSpriteAndText(profHead, "It looks like the legendary solar disk !");
        yield return new WaitForSeconds(5f);
        GameObject.Find("Ra").GetComponent<SpriteRenderer>().enabled = true;
        GameObject.Find("RaShadow").GetComponent<SpriteRenderer>().enabled = true;
        TextLogger.instance.SetSpriteAndText(raHead, "How dare you lay your hands on the symbol of my power ?!");
        yield return new WaitForSeconds(5f);
        TextLogger.instance.SetSpriteAndText(raHead, "By the sunlight I curse you !");
        GameObject.Find("Torch").GetComponent<Torch>().Activate();
        yield return new WaitForSeconds(5f);
        GameObject.Find("Apophis").GetComponent<SpriteRenderer>().enabled = true;
        GameObject.Find("ApophisShadow").GetComponent<SpriteRenderer>().enabled = true;
        TextLogger.instance.SetSpriteAndText(apoHead, "Hahahahaha ! Thank you for helping me finding this one, human !");
        yield return new WaitForSeconds(5f);
        TextLogger.instance.SetSpriteAndText(raHead, "Apophis ?! No ! WAKE UP DUDE ! Don't let him steal my ...  !?");
        yield return new WaitForSeconds(5f);
        GameObject.Find("Apophis").GetComponent<SpriteRenderer>().enabled = false;
        GameObject.Find("ApophisShadow").GetComponent<SpriteRenderer>().enabled = false;
        GameObject.Find("SolarDisk").GetComponent<SpriteRenderer>().enabled = false;
        GameObject.Find("SolarDiskShadow").GetComponent<SpriteRenderer>().enabled = false;
        TextLogger.instance.SetSpriteAndText(raHead, "Oh, screw me.");
        yield return new WaitForSeconds(5f);
        TextLogger.instance.SetSpriteAndText(raHead, "It's terrible. If you don't get back the artifact, Apophis will be able to get greater power and destroy the humanity.");
        yield return new WaitForSeconds(15f);
        Stage.instance.NextStage();
    }
	// Update is called once per frame
	void Update () {
	
	}
}
