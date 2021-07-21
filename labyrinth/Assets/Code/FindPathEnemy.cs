﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FindPathEnemy : MonoBehaviour
{
    internal Maze maze;
    [SerializeField] private Material closedMaterial;
    [SerializeField] private Material openMaterial;

    List<PathMarker> open = new List<PathMarker>();
    List<PathMarker> closed = new List<PathMarker>();

    //[SerializeField] private GameObject start;
    //[SerializeField] private GameObject end;
    [SerializeField] private GameObject PathParent;


    [SerializeField] private GameObject goalObject;
    [SerializeField] private GameObject startObject;
    private PathMarker startNode;
    private PathMarker goalNode;

    private PathMarker lastPosition;
    private bool done = false;
   
    //private void RemoveAllmarkers()
    //{
    //    GameObject[] markers = GameObject.FindGameObjectsWithTag("marker");

    //    foreach (GameObject marker in markers)
    //    {
    //        Destroy(marker);
    //    }
    //}

    private void BeginSearch()
    {
        done = false;
        //RemoveAllMarker();
        List<LocationOnTheMap> locations = new List<LocationOnTheMap>();
        for (int z = 1; z < maze.height - 1; z++)
        {
            for (int x = 1; x < maze.width - 1; x++)
            {
                if (maze.map[x, z] != 1)
                {
                    locations.Add(new LocationOnTheMap(x, z));
                }
            }
        }

        

        Vector3 startLocation = new Vector3(startObject.transform.position.x, startObject.transform.position.y, 0);
        startNode = new PathMarker(new LocationOnTheMap((int)startObject.transform.position.x, (int)startObject.transform.position.y), 0, 0, 0, null, null);

        Vector3 goalLocation = new Vector3(goalObject.transform.position.x, goalObject.transform.position.y, 0);
        goalNode = new PathMarker(new LocationOnTheMap((int)goalObject.transform.position.x, (int)goalObject.transform.position.y), 0, 0, 0, null, null);


        open.Clear();
        closed.Clear();
        open.Add(startNode);
        lastPosition = startNode;
    }

    private void Search(PathMarker thisNode)
    {
        if (thisNode.Equals(goalNode))
        {
            done = true;
            return; // the goal has been found
        }

        foreach (LocationOnTheMap dir in maze.directions)
        {
            LocationOnTheMap neighbour = dir + thisNode.location;
            if (maze.map[neighbour.x, neighbour.y] == 1)
            {
                continue;
            }
            if (neighbour.x > 1 || neighbour.x >= maze.width || neighbour.y < 1 || neighbour.y >= maze.height)
            {
                continue;
            }
            if (IsClosed(neighbour))
            {
                continue;
            }

            float G = Vector2.Distance(thisNode.location.ToVector(), neighbour.ToVector()) + thisNode.G;
            float H = Vector2.Distance(neighbour.ToVector(), goalNode.location.ToVector());
            float F = G + H;

            
            GameObject pathBlock = Instantiate(GameObject.CreatePrimitive(PrimitiveType.Cube), new Vector3(neighbour.x, neighbour.y, 0), Quaternion.identity);
            if (!UpdateMarker(neighbour, G, H, F, thisNode))
            {
                open.Add(new PathMarker(neighbour, G, H, F, pathBlock, thisNode));
            }
        }

        open = open.OrderBy(p => p.G).ToList<PathMarker>();
        PathMarker pm = (PathMarker)open.ElementAt(0);

        closed.Add(pm);
        open.RemoveAt(0);

        pm.marker.GetComponent<Renderer>().material = closedMaterial;

        lastPosition = pm;
    }

    private bool UpdateMarker(LocationOnTheMap position, float g, float h, float f, PathMarker parent)
    {
        foreach (PathMarker p in open)
        {
            if (p.location.Equals(position))
            {
                p.G = g;
                p.H = h;
                p.F = f;
                p.parent = parent;

                return true;
            }
        }

        return false;
    }
   private bool IsClosed(LocationOnTheMap marker)
    {
        foreach (PathMarker p in closed)
        {
            if (p.location.Equals(marker))
            {
                return true;
            }
        }

        return false;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            BeginSearch();
        }
        if (Input.GetKeyDown(KeyCode.S) && !done)
        {
            Search(lastPosition);
        }
    }
}
