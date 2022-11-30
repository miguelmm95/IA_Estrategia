using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfantryUnit : BaseHumanUnit
{
    private int health = 250;
    private int speed = 10;
    private int attackRange = 10;
    [HideInInspector] public int maxRange = 2;
    public GameObject tile;

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
