using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportController
{
    //[SerializeField] private GameObject[] teleportPoints;
    //[SerializeField] private GameObject player, teleport;
    int currentTeleport = 0;

    // Start is called before the first frame update
    //void Start()
    //{
        
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    Debug.Log(Mathf.Abs(player.transform.position.x - teleport.transform.position.x));

    //}

    internal void Teleport(GameObject player, GameObject teleportObj)
    {
        if (Mathf.Abs(player.transform.position.x - teleportObj.transform.position.x) > 3f)
        {
            if (player.transform.position.x > teleportObj.transform.position.x)
            {
                player.transform.position = teleportObj.transform.GetChild(1).position;
            }
            else
            {
                player.transform.position = teleportObj.transform.GetChild(0).position;

            }
        }
    }
}
