using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAIUnit : BaseUnit
{
    public int movementRange, attackRange, totalHealth, actualHealth, damage;

    public Tile _flagToDefend;
    public AIPlayer aiPlayer;

    private void Awake()
    {
        state = State.AIMoving;
    }

    public int getRange(Type type)
    {
        switch (type)
        {
            case(Type.Heavy):
                movementRange = 1;
                break;
            case (Type.Ranged):
                movementRange = 3;
                break;
            default:
                movementRange = 2;
                break;
        }
        return movementRange;
    }

    public Tile moveTo(List<Tile> disponibleTiles, Tile target)
    {
        var tile = new Tile();
        float max = Mathf.Infinity;

        foreach(Tile t in disponibleTiles)
        {
            if(Vector3.Distance(t.transform.position, target.transform.position) < max)
            {
                tile = t;
                max = Vector3.Distance(t.transform.position, target.transform.position);
            }
        }
        return tile;
    }
}
