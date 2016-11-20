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
        LayerMask layerMask = (1 << 8);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up, 1f, layerMask);
        float distance = 1f;
        if(hit.collider != null && hit.collider.gameObject.CompareTag("Ground"))
        {
            distance = Mathf.Abs(hit.point.y - transform.position.y);
        }
        if (Input.GetButtonDown("Switch") && Switcher.instance.IsInsideLightLayer(gameObject) && !Switcher.instance.currentPlayer.Equals(Switcher.instance.profShadow) && entered && distance != 0)
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
        yield return new WaitForSeconds(0.5f);
        entered = true;
    }
    IEnumerator WaitForExit()
    {
        exited = true;
        yield return new WaitForSeconds(0.5f);
        if(animal != null)
            animal.Idle();
        exited = false;
        entered = false;
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.Equals(Switcher.instance.profShadow) && Input.GetButtonDown("Switch") && !exited && !entered)
        {
            Switcher.instance.SetCurrentPlayer(gameObject.transform.parent.gameObject);
            Switcher.instance.profShadow.SetActive(false);
            StartCoroutine(WaitForEnter());
        }
    }
}
