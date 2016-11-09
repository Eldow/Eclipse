using UnityEngine;
using System.Collections;

public class Animal : MonoBehaviour
{
    private bool exited = false;
    public bool entered = false;
    private PlayerInterface animal;

    // Use this for initialization
    void Start()
    {
        animal = gameObject.transform.parent.gameObject.GetComponent(typeof(PlayerInterface)) as PlayerInterface;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Switch") && Switcher.instance.IsInsideLightLayer() && !Switcher.instance.currentPlayer.Equals(Switcher.instance.profShadow) && entered)
        {
            Switcher.instance.profShadow.SetActive(true);   
            Switcher.instance.profShadow.transform.position = transform.position;
            Switcher.instance.SetCurrentPlayer(Switcher.instance.profShadow);
            StartCoroutine(WaitForExit());
        }
    }
    IEnumerator WaitForEnter()
    {
        entered = false;
        yield return new WaitForSeconds(1f);
        entered = true;
    }
    IEnumerator WaitForExit()
    {
        exited = true;
        yield return new WaitForSeconds(1f);
        if(animal != null)
            animal.Idle();
        exited = false;
        entered = false;
    }
    void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.Equals(Switcher.instance.profShadow) && !exited && !entered)
        {
            Switcher.instance.SetCurrentPlayer(gameObject.transform.parent.gameObject);
            Switcher.instance.profShadow.SetActive(false);
            StartCoroutine(WaitForEnter());
        }
    }
}
