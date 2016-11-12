﻿using UnityEngine;
using System.Collections;

public class LightCircle : MonoBehaviour {
    private bool triggered;
    private bool swapped;
    public bool activated;
    PlayerInterface player;

	// Use this for initialization
	void Start () {
        triggered = false;
        swapped = false;
        activated = true;
    }
	
	// Update is called once per frame
	void Update () {
        if (activated)
        {
            if (triggered & !swapped)
            {
                Switcher.instance.SetCurrentPlayer(Switcher.instance.profShadow);
                triggered = false;
                swapped = true;
            }
        } else
        {
            if (swapped)
            {
                StartCoroutine(SwitchTo(Switcher.instance.prof));
            }
        }
	}
    IEnumerator SwitchTo(GameObject o)
    {
        PlayerInterface player = Switcher.instance.currentPlayer.GetComponent(typeof(PlayerInterface)) as PlayerInterface;
        player.Idle();
        yield return new WaitForSeconds(0.1f);
        Switcher.instance.SetCurrentPlayer(o);
        swapped = false;
        triggered = false;
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.Equals(Switcher.instance.prof))
        {
            if (gameObject.GetComponentInChildren<Collider2D>().bounds.Contains(other.bounds.min)
                && gameObject.GetComponentInChildren<Collider2D>().bounds.Contains(other.bounds.max))
                triggered = true;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.Equals(Switcher.instance.prof))
        {
            triggered = false;
        }
    }
}
