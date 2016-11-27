using UnityEngine;
using System.Collections;

public class BouncyBall : MonoBehaviour {
    public float angle;
	// Use this for initialization
	void Start () {
        angle = Random.Range(-70, 70);
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
	
	// Update is called once per frame
	void Update () {
        transform.position = Vector3.MoveTowards(transform.position, transform.GetChild(0).transform.position, 5f * Time.deltaTime);
	}

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
        }
    }
}
