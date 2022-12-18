using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flag : MonoBehaviour
{
    [HideInInspector] public List<Tile> neighbours;
    public Tile flagPosition;
    public bool beingAttacked;
    public List<BaseAIUnit> unitAttackingFlag;

    private void Start()
    {
        neighbours = Grid.Instance.GetNeighboursUnit(flagPosition);
    }


    public bool CheckUnit(BaseAIUnit unit)
    {
        foreach (Tile tile in neighbours)
        {
            if (unit.occupiedTile == tile)
            {
                return true;
            }
        }
        return false;
    }

    public void ChangeFlagAI()
    {
        this.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        this.gameObject.tag = "Bandera_IA";
    }
}
