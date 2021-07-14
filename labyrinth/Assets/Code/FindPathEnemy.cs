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

    private PathMarker goalNode;
    private PathMarker startNode;

    private PathMarker lastPosition;
    private bool done = false;

    private void RemoveAllmarkers()
    {
        GameObject[] markers = GameObject.FindGameObjectsWithTag("marker");

        foreach (GameObject marker in markers)
        {
            Destroy(marker);
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
