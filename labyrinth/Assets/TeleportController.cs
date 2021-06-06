using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportController : MonoBehaviour
{
    [SerializeField] private GameObject[] teleportPoints;
    [SerializeField] private GameObject player, teleport;
    int currentTeleport = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(Input.GetAxisRaw("Jump"));

    }

    private void Teleport(GameObject teleportObj)
    {
        if (Mathf.Abs(player.transform.position.x - teleport.transform.position.x) > 1.5f)
        {
            float step = 1f * Time.deltaTime; // calculate distance to move

        }
    }
}
