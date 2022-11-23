using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ranged : MonoBehaviour
{
    private int health = 100;
    private int speed = 5;
    private int attackRange = 20;
    [HideInInspector] public int maxRange = 3;
    [HideInInspector] public GameObject tile;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Tile_Walkable")
        {
            tile = collision.gameObject;
        }
    }
}
