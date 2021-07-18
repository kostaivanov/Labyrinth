using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindPathEnemy : MonoBehaviour
{
    internal Maze maze;
    [SerializeField] private Material closedMaterial;
    [SerializeField] private Material openMaterial;

    List<PathMarker> open = new List<PathMarker>();
    List<PathMarker> closed = new List<PathMarker>();

    [SerializeField] private GameObject start;
    [SerializeField] private GameObject end;
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

        List<LocationOnTheMap> locations = new List<LocationOnTheMap>();

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

        }
    }

   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
