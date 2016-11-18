using UnityEngine;
using System.Collections;

public class FriableBlock : MonoBehaviour, ActivableInterface {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public bool isActivated()
    {
        return true;
    }
    public void Activate()
    {

    }

    public void Desactivate()
    {
        StartCoroutine(BreakBlock());
    }

    IEnumerator BreakBlock()
    {
        gameObject.GetComponent<Animator>().SetBool("broken", true);
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
