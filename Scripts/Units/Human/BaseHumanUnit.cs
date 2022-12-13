using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseHumanUnit : BaseUnit
{
    private List<Tile> neighbours;
    // Start is called before the first frame update

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
