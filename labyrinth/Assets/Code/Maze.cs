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

    internal int width;
    internal int height;
    internal byte[,] map;

    // Start is called before the first frame update
    void Start()
    {
        width = (int)backGround.bounds.size.x;
        height = (int)backGround.bounds.size.y;
    }

}
