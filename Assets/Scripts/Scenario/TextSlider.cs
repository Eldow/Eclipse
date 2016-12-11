using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class TextSlider : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine(ToMainMenu());
	}
	
	// Update is called once per frame
	void Update () {
        transform.GetChild(0).position = new Vector2(transform.GetChild(0).position.x, transform.GetChild(0).position.y + 1f);
	}

    IEnumerator ToMainMenu()
    {
        yield return new WaitForSeconds(55f);
        SceneManager.LoadScene(0);
    }
}
