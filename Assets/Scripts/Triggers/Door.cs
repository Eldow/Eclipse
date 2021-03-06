﻿using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {
    public bool locked;
    private Collider2D coll;
    private Animator anim;
    private AudioSource sound;
	// Use this for initialization
	void Start () {
        sound = gameObject.GetComponent<AudioSource>();
        locked = true;
        anim = GetComponent<Animator>();
        anim.SetBool("locked", locked);
    }

    public void Unlock()
    {
        locked = false;
    }
	
    IEnumerator ToNextStage()
    {
        anim.SetBool("locked", locked);
        Switcher.instance.prof.GetComponent<Animator>().Play("victory");
        Switcher.instance.profShadow.GetComponent<Animator>().Play("victory");
        yield return new WaitForSeconds(1.5f);
        GameObject.Find("Stage").GetComponent<Stage>().NextStage();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.Equals(Switcher.instance.prof) && !locked)
        {
            sound.Play();
            StartCoroutine(ToNextStage());
        }
    }
}
