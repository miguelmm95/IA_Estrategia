using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAIUnit : BaseUnit
{
    public int movementRange, attackRange;
    public float totalHealth, actualHealth, damage;

    public Flag _flagToDefend;
    public Flag _flagToAttack;
    public AIPlayer aiPlayer;
    public BaseHumanUnit _playerTarget;
    public int turnsInFlag;

    private void Awake()
    {
        turnsInFlag = 0;

        state = State.AIMoving;

        switch (type)
        {
            case (Type.Heavy):
                movementRange = 1;
                attackRange = 1;
                totalHealth = 200;
                actualHealth = totalHealth;
                damage = 10;
                break;
            case (Type.Ranged):
                movementRange = 3;
                attackRange = 3;
                totalHealth = 75;
                actualHealth = totalHealth;
                damage = 10;
                break;
            default:
                movementRange = 2;
                attackRange = 1;
                totalHealth = 100;
                actualHealth = totalHealth;
                damage = 20;
                break;
        }

    }

    public List<Tile> GetVision(Tile actualTile, int range)
    {
        return Grid.Instance.GetNeighboursUnit(actualTile, range + 1);
    }

    public int getRange(Type type)
    {
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

    public void getDamage(float damage)
    {
        actualHealth -= damage;
    }

    public void Attack(float damage, BaseHumanUnit unit)
    {
        damage = CalculateDamage(damage, unit);
        unit.getDamage(damage);
    }
}
