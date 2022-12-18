using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flag : MonoBehaviour
{
    [HideInInspector] public List<Tile> neighbours;
    public Tile flagPosition;
    public bool beingAttacked;
    public List<BaseAIUnit> unitAttackingFlag;
    private int turno;

    private void Start()
    {
        neighbours = Grid.Instance.GetNeighboursUnit(flagPosition);
        turno = 0;
    }

    private void Update()
    {
        if (gameObject.tag == "Bandera_IA")
        {
            foreach (Tile tile in neighbours)
            {
                if (tile.occupiedUnit!= null && tile.occupiedUnit.player == Player.Human)
                {
                    Debug.Log("Estoy en la bandera!");
                    Debug.Log(turno);
                    if (turno == 0)
                    {
                        turno = TurnManager.turnCounter;
                    }
                    else if ((TurnManager.turnCounter - turno) == 3)
                    {
                        UnitManager._AIFlags.Remove(this);
                        ChangeFlagHuman();
                        UnitManager._playerFlags.Add(this);
                        turno = 0;
                    }
                }
            }
        }
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

    public void CheckUnitHumanInFlagAI()
    {
        foreach (Tile tile in neighbours)
        {
            if (tile.occupiedUnit != null && tile.occupiedUnit.player == Player.Human)
            {
                Grid.Instance.GetNearAIUnits(this, UnitManager._AIUnitsObjects);
            }
        }
    }


    public void ChangeFlagHuman()
    {
        this.gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
        this.gameObject.tag = "Bandera_Jugador";
    }

    public void ChangeFlagAI()
    {
        this.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        this.gameObject.tag = "Bandera_IA";
    }
}
