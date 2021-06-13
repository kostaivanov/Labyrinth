using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportController
{

    internal void Teleport(GameObject player, GameObject teleportObject)
    {
        if (Mathf.Abs(player.transform.position.x - teleportObject.transform.position.x) > 1.5f)
        {
            if (player.transform.position.x > teleportObject.transform.position.x)
            {
                if (teleportObject.transform.childCount > 2)
                {
                    if (player.transform.position.y < teleportObject.transform.position.y)
                    {
                        player.transform.position = teleportObject.transform.GetChild(0).position;
                    }
                    else
                    {
                        player.transform.position = teleportObject.transform.GetChild(2).position;
                    }
                }
                else
                {
                    player.transform.position = teleportObject.transform.GetChild(0).position;
                }
            }
            else
            {
                if (teleportObject.transform.childCount > 2)
                {
                    if (player.transform.position.y < teleportObject.transform.position.y)
                    {
                        player.transform.position = teleportObject.transform.GetChild(1).position;
                    }
                    else
                    {
                        player.transform.position = teleportObject.transform.GetChild(3).position;
                    }
                }
                else
                {
                    player.transform.position = teleportObject.transform.GetChild(1).position;
                }
            }
        }
    }

    internal void ChangeFrameCamera(GameObject player, GameObject[] cameras, GameObject teleportObject)
    {
        string[] name = teleportObject.name.Split(new char[] { '-', '>' }, System.StringSplitOptions.RemoveEmptyEntries);

        string firstFrameIndex = name[0];
        string secondFrameIndex = name[name.Length - 1];

        int angleValue = CalculateAngle(player, teleportObject);

        if (angleValue < 0)
        {
            cameras[int.Parse(secondFrameIndex)].SetActive(false);
            cameras[int.Parse(firstFrameIndex)].SetActive(true);
        }
        else
        {
            cameras[int.Parse(firstFrameIndex)].SetActive(false);
            cameras[int.Parse(secondFrameIndex)].SetActive(true);
        }
    }

    private int CalculateAngle(GameObject player, GameObject teleportObject)
    {
        Vector3 pFrwd = player.transform.up;
        Vector3 rDir = teleportObject.transform.position - player.transform.position;

        float dot = pFrwd.x * rDir.x + pFrwd.y * rDir.y;
        float angle = Mathf.Acos(dot / (pFrwd.magnitude * rDir.magnitude));

        //Debug.Log("Angle: " + angle * Mathf.Rad2Deg);
        //Debug.Log("Unity angle: " + Vector3.Angle(pFrwd, rDir));

        Debug.DrawRay(player.transform.position, pFrwd * 15, Color.green, 2);
        Debug.DrawRay(player.transform.position, rDir, Color.red, 2);

        int clockWise = 1;
        if (CrossProduct(pFrwd, rDir).z < 0)
        {
            clockWise = -1;
        }

        //Unity calculation on the angle
        //float unityAngle = Vector3.SignedAngle(pFrwd, rDir, this.transform.forward);
        //Debug.Log("forward: " + this.transform.forward);

        //this.transform.Rotate(0, 0, unityAngle * 0.02f);

        //this.transform.Rotate(0, 0, angle * Mathf.Rad2Deg * clockWise);

        return clockWise;
    }

    private Vector3 CrossProduct(Vector3 v, Vector3 w)
    {
        float xMult = v.y * w.z - v.z * w.y;
        float yMult = v.z * w.x - v.x * w.z;
        float zMult = v.x * w.y - v.y * w.x;

        Vector3 crossProd = new Vector3(xMult, yMult, zMult);
        return crossProd;
    }
}
