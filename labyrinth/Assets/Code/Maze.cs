using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maze : MonoBehaviour
{
    internal List<LocationOnTheMap> directions = new List<LocationOnTheMap>
    {
        new LocationOnTheMap(1, 0), // right neighbour
        new LocationOnTheMap(0, 1), // up neighbour
        new LocationOnTheMap(-1, 0), // left neighbour
        new LocationOnTheMap(0, -1), // down neighbour
    };

    [SerializeField] internal SpriteRenderer backGround;
    [SerializeField] private PolygonCollider2D groundCollider;
    [SerializeField] private LayerMask groundLayer;

    internal int width;
    internal int height;
    internal byte[,] map;

    // Start is called before the first frame update
    void Start()
    {
        width = (int)backGround.bounds.size.x;
        height = (int)backGround.bounds.size.y;
    }
    void InitialiseMap()
    {
        map = new byte[width, height];
        for (int z = 0; z < height; z++)
            for (int x = 0; x < width; x++)
            {
                map[x, z] = 0;     //1 = wall  0 = corridor
            }
    }

    private void FindMapSpace()
    {
        groundCollider = gameObject.GetComponent<PolygonCollider2D>();
        Collider2D[] overlap = Physics2D.OverlapAreaAll(groundCollider.bounds.min, groundCollider.bounds.max, groundLayer);
        if (overlap.Length > 1)
        {
            Debug.Log(string.Format("Found {0} overlapping object(s)", overlap.Length - 1));
        }

        foreach (Collider2D item in overlap)
        {
            Debug.Log(item.transform.position.x);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            FindMapSpace();
        }
    }
}
