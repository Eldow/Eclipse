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
                if (Switcher.instance.prof != null && !Switcher.instance.prof.GetComponent<Prof>().asleep)
                {
                    Switcher.instance.SetCurrentPlayer(Switcher.instance.profShadow);
                    //triggered = false;
                    swapped = true;
                }                
            }
            if (swapped && !Switcher.instance.IsInsideLightLayer(Switcher.instance.prof))
            {
                StartCoroutine(SwitchTo(Switcher.instance.prof));
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
        if(Switcher.instance.currentPlayer != null)
        {
            PlayerInterface player = Switcher.instance.currentPlayer.GetComponent(typeof(PlayerInterface)) as PlayerInterface;
            player.Idle();
            if (Switcher.instance.currentPlayer.transform.GetChild(0).GetComponent<Animal>() != null)
            {
                Switcher.instance.currentPlayer.transform.GetChild(0).GetComponent<Animal>().entered = false;
                Switcher.instance.currentPlayer.transform.GetChild(0).GetComponent<Animal>().exited = false;
            }
        }
        yield return new WaitForSeconds(0.1f);
        Switcher.instance.SetCurrentPlayer(o);
        swapped = false;
        triggered = false;
    }
    void OnTriggerStay2D(Collider2D other)
    {
        bool inside = true;
        if (other.gameObject.Equals(Switcher.instance.prof))
        {
            foreach(Collider2D coll in Switcher.instance.prof.GetComponentsInChildren<Collider2D>())
            {
                if (!gameObject.GetComponent<CircleCollider2D>().bounds.Contains(coll.bounds.center))
                {
                    inside = false;
                }
                    
            }
            if (inside)
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
