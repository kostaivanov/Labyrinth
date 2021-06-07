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

    internal void Teleport(GameObject player, GameObject teleportObject)
    {
        if (Mathf.Abs(player.transform.position.x - teleportObject.transform.position.x) > 1.5f)
        {
            if (player.transform.position.x > teleportObject.transform.position.x)
            {
                player.transform.position = teleportObject.transform.GetChild(1).position;
            }
            else
            {
                player.transform.position = teleportObject.transform.GetChild(0).position;
            }
        }
    }

    internal void ChangeFrameCamera(GameObject player, GameObject[] cameras, GameObject teleportObject)
    {
        string[] name = teleportObject.name.Split(new char[] { '-', '>' }, System.StringSplitOptions.RemoveEmptyEntries);
        string nextFrameIndex = name[name.Length - 1];
        string previousFrameIndex = name[name.Length - 2];
        Debug.Log(previousFrameIndex);


        if (player.transform.position.x > teleportObject.transform.position.x)
        {
            cameras[int.Parse(nextFrameIndex)].SetActive(false);
            cameras[int.Parse(previousFrameIndex)].SetActive(true);
        }
        else
        {
            cameras[int.Parse(previousFrameIndex)].SetActive(false);
            cameras[int.Parse(nextFrameIndex)].SetActive(true);
        }
    }
}
