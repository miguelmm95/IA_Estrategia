using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heavy : MonoBehaviour
{
    private int health = 500;
    private int speed = 15;
    private int attackRange = 5;
    [HideInInspector] public int maxRange = 1;
    [HideInInspector] public GameObject tile;


    void Start()
    {
        
    }


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
