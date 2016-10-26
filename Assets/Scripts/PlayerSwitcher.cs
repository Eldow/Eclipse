using UnityEngine;
using System.Collections;



public static class PlayerSwitcher {
    const int PHYSICAL = 8;
    const int SHADOW = 9;

    private static ArrayList shadowWorld = new ArrayList();

    public static void StartWith(GameObject currentPlayer)
    {
        GameObject[] objects = GameObject.FindObjectsOfType(typeof(GameObject)) as GameObject[];
        foreach (GameObject o in objects)
        {
            if (o.layer == SHADOW)
            {
                shadowWorld.Add(o);
            }
        }
        //Player is in physical world, need to disable shadow world
        if (currentPlayer.layer == PHYSICAL)
        {
            Camera.main.backgroundColor = Color.black;
            EnableShadowWorld(false);
        }
        //Player is in shadow world, need to enable it
        else if (currentPlayer.layer == SHADOW)
        {
            EnableShadowWorld(true);
            Camera.main.backgroundColor = Color.white;
        }
        foreach(GameObject o in GameObject.FindGameObjectsWithTag("Player"))
        {
            if(o != currentPlayer)
            {
                o.SetActive(false);
            }
        }
        //Activate player & Focus camera
        currentPlayer.SetActive(true);
        Camera.main.GetComponent<SmoothCamera>().target = currentPlayer.GetComponent<Transform>();
    }
    public static void Switch(GameObject currentPlayer, GameObject otherPlayer)
    {
        //Nothing to do if the player is in the same world
        if (currentPlayer.layer != otherPlayer.layer)
        {
            //Player was in physical world, need to enable shadow world
            if (currentPlayer.layer == PHYSICAL)
            {
                Camera.main.backgroundColor = Color.white;
                EnableShadowWorld(true);
            }
            //Player was in shadow world, need to disable it
            else if (currentPlayer.layer == SHADOW)
            {
                EnableShadowWorld(false);
                Camera.main.backgroundColor = Color.black;
            }
        }
        //Swap players
        currentPlayer.SetActive(false);
        otherPlayer.SetActive(true);
        //Focus camera
        Camera.main.GetComponent<SmoothCamera>().target = otherPlayer.GetComponent<Transform>();
    }
    //Loop through game objects, disable those in the shadow layer
    private static void EnableShadowWorld(bool active)
    {
        foreach(Object o in shadowWorld)
        {
            GameObject go = (GameObject)o;
            go.SetActive(active);
        }
    }
}
