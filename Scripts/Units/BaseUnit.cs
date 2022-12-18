using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseUnit : MonoBehaviour
{
    public Tile occupiedTile;
    public Player player;
    public Type type;
    public State state;
    public int turnsInFlag;

    public int GetMaxRange(Type unit)
    {
        int range = 0;

        switch (unit)
        {
            case (Type.Infantry):
                range = 2;
                break;
            case (Type.Heavy):
                range = 1;
                break;
            default:
                range = 3;
                break;
        }

        return range;
    }

    public int GetRangeAtack(Type unit)
    {
        int atackRange = 1;

        switch (unit)
        {
            case (Type.Ranged):
                atackRange = 3;
                break;
        }

        return atackRange;
    }

    public float CalculateDamage(float damage, BaseUnit unit)
    {
        switch (unit.type)
        {
            case (Type.Heavy):
                if(type == Type.Infantry)
                {
                    damage = damage * 1.5f;
                }else if(type == Type.Ranged)
                {
                    damage = damage * 0.75f;
                }
                break;
            case (Type.Ranged):
                if(type == Type.Heavy)
                {
                    damage = damage * 1.5f;
                }
                else if(type == Type.Infantry)
                {
                    damage = damage * 0.75f;
                }
                break;
            default:
                if(type == Type.Ranged)
                {
                    damage = damage * 1.5f;
                }
                else if (type == Type.Heavy)
                {
                    damage = damage * 0.75f;
                }
                break;
        }
        return damage;
    }

}