﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Switcher : MonoBehaviour
{
    //Layer consts
    const int PHYSICAL = 8;
    const int SHADOW = 9;
    const int PLAYERSHADOW = 10;
    const int SHADOWFIXED = 11;
    const int PHYSICALFIXED = 12;
    //Current player
    public static Switcher instance = null;
    public GameObject currentPlayer = null;
    private List<GameObject> lightCircles;
    private List<GameObject> backLightCircles;
    public GameObject prof;
    public GameObject profShadow;
    public AudioClip deathSound;

    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        Setup();
    }
    public void Setup()
    {
        Physics2D.IgnoreLayerCollision(PHYSICAL, SHADOW, true);
        Physics2D.IgnoreLayerCollision(PLAYERSHADOW, SHADOWFIXED, true);
        Physics2D.IgnoreLayerCollision(SHADOWFIXED, SHADOW, true);
        Physics2D.IgnoreLayerCollision(PLAYERSHADOW, PHYSICALFIXED, true);
        SetupLightCircles();
        SetCurrentPlayer(prof);
    }
    //Any player --> Any player 
    public void SetCurrentPlayer(GameObject player)
    {
        Debug.Log(player);
        // Disable old player
        GameObject oldPlayer = currentPlayer;
        if (oldPlayer != null)
        {
            if (oldPlayer.Equals(prof))
            {
                prof.GetComponent<Prof>().Disable();
                if(profShadow.GetComponent<ProfShadow>() != null)
                    profShadow.GetComponent<ProfShadow>().Enable();
            }
            else if (oldPlayer.Equals(profShadow))
            {
                profShadow.GetComponent<SpriteRenderer>().enabled = false;
            } else
            {
                profShadow.SetActive(true);
            }
        }
        //Enable new player
        currentPlayer = player;
        if (currentPlayer.Equals(profShadow))
        {
            foreach (Collider2D coll in profShadow.GetComponents<Collider2D>())
            {
                coll.enabled = true;
            }
            profShadow.transform.parent = null;
            profShadow.GetComponent<SpriteRenderer>().enabled = true;
            prof.GetComponent<Prof>().Disable();
        }
        if (currentPlayer.Equals(prof) && prof != null)
        {
            foreach(Collider2D coll in profShadow.GetComponents<Collider2D>())
            {
                coll.enabled = false;
            }
            profShadow.transform.parent = prof.transform;
            profShadow.transform.position = prof.transform.position + new Vector3(0, 2.48f);
            profShadow.GetComponent<SpriteRenderer>().enabled = true;
            profShadow.GetComponent<ProfShadow>().Disable();
            prof.GetComponent<Prof>().Enable();
        }
        if(currentPlayer != null)
        {
            currentPlayer.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
            //Camera focus
            Camera.main.GetComponent<SmoothCamera>().target = currentPlayer.GetComponent<Transform>();
        }
    }
    public void SetupLightCircles()
    {
        lightCircles = new List<GameObject>();
        backLightCircles = new List<GameObject>();
        foreach (GameObject o in GameObject.FindObjectsOfType<GameObject>())
        {
            if (o.CompareTag("Light"))
            {
                lightCircles.Add(o);
            }
            if (o.CompareTag("BackLight"))
            {
                backLightCircles.Add(o);
            }
        }
        EnableLightColliders(false);
    }
    public bool IsInsideLightLayer(GameObject player)
    {
        if(player != null)
        {
            foreach (GameObject o in lightCircles)
            {
                if (o.GetComponent<Collider2D>().bounds.Contains(player.GetComponent<Collider2D>().bounds.center))
                {
                    return true;
                }
            }
            return false;
        }
        return false;
    }
    public void EnableLightColliders(bool enable)
    {
        foreach (GameObject o in backLightCircles)
        {
            o.GetComponent<Collider2D>().enabled = enable;
        }
    }
    public void KillPlayer(GameObject o)
    {
       StartCoroutine(Kill(o));
    }

    IEnumerator Kill(GameObject player)
    {
        GameObject o = player;
        if(o.Equals(currentPlayer) || o.Equals(prof))
            SoundManager.instance.RandomizeSfx(deathSound);
        if (o.Equals(prof) || o.Equals(profShadow))
        {
            prof.GetComponent<Animator>().SetBool("dead", true);
            profShadow.GetComponent<Animator>().SetBool("dead", true);
        }
        yield return new WaitForSeconds(0.7f);
        Destroy(o);
        if (player.Equals(currentPlayer) || player.Equals(prof))
            Stage.instance.ResetStage();
    }

}

