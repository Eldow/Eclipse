using UnityEngine;
using System.Collections;

public class PoisonballShadow : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.Equals(Switcher.instance.currentPlayer))
        {
            Switcher.instance.KillPlayer(Switcher.instance.currentPlayer);
        }
    }
}
