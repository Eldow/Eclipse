﻿using UnityEngine;
using System.Collections;

public class DirectionFlipper : MonoBehaviour {
    private MovableInterface movableEntity;
    private bool triggered;
	// Use this for initialization
	void Start () {
        triggered = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!triggered)
        {
            if (other.gameObject.CompareTag("Movable") || other.gameObject.CompareTag("Ennemy"))
            {
                movableEntity = other.gameObject.GetComponent(typeof(MovableInterface)) as MovableInterface;
                movableEntity.Flip();
                triggered = true;
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (triggered)
        {
            triggered = false;
        }
    }
}