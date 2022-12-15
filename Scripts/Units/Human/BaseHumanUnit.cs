using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseHumanUnit : BaseUnit
{
    private List<Tile> neighbours;

    public int movementRange, attackRange;
    public float totalHealth, actualHealth, damage;

    private void Awake()
    {
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

    public void getDamage(float damage)
    {
        actualHealth -= damage;
    }

    public void Attack(float damage, BaseAIUnit unit)
    {
        damage = CalculateDamage(damage, unit);
        unit.getDamage(damage);
    }

    public List<Tile> UnitHighlight(int maxRange, Tile tile)
    {
        neighbours = Grid.Instance.GetNeighboursUnit(tile, maxRange);
        foreach (Tile n in neighbours)
            {
                if (n == null)
                {
                    continue;
                }
                else
                {
                    n.SetUnitHighlight();
                    n.unitCanMoveTo = true;
                }
            }
        return neighbours;
    }

    public List<Tile> UnitHighlightAtack(int maxRange, Tile tile)
    {
        neighbours = Grid.Instance.GetNeighboursUnitAtack(tile, maxRange);

        foreach(Tile n in neighbours)
        {
            if(n == null)
            {
                continue;
            }
            else
            {
                n.SetUnitHighlightAtack();
            }
        }
        return neighbours;
    }

    public void UnitHighlightDisable(List<Tile> neighbours1)
    {
        if (neighbours1.Count != 0)
        {
            foreach (Tile n in neighbours1)
            {
                if (n == null)
                {
                    continue;
                }
                else
                {
                    n.DisableUnitHighlight();
                    n.unitCanMoveTo = false;
                }
            }
        }
    }

    public void UnitHighlightDisableAtack(List<Tile> neighbours1)
    {
        if(neighbours1.Count != 0)
        {
            foreach(Tile n in neighbours1)
            {
                if(n == null)
                {
                    continue;
                }
                else
                {
                    n.DisableUnitHighlightAtack();
                }
            }
        }
    }
}
