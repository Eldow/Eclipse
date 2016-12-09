using UnityEngine;
using System.Collections;

public class Key : MonoBehaviour {

    private Door door;
    private GameObject linkedKey;
    public AudioClip key;
	// Use this for initialization
	void Start () {
        if (gameObject.layer == 8)
        {
            linkedKey = gameObject.transform.GetChild(0).gameObject;
            door = transform.parent.gameObject.GetComponent<Door>();
        }
        else
        {
            linkedKey = gameObject.transform.parent.gameObject;
            door = transform.parent.gameObject.transform.parent.GetComponent<Door>();
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
    IEnumerator UnlockDoor()
    {
        door.Unlock();
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
        linkedKey.SetActive(false);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SoundManager.instance.PlaySingle(key);
            StartCoroutine(UnlockDoor());
        }
    }
}
