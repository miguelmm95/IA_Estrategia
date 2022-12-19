using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flag : MonoBehaviour
{
    [HideInInspector] public List<Tile> neighbours;
    public Tile flagPosition;
    public bool beingAttacked;
    public List<BaseHumanUnit> unitHumanAttackingAIFlag;
    public List<BaseAIUnit> unitAIDefendingFlag;
    public List<BaseAIUnit> unitAIAttackinHumanFlag;
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
                    if (!unitHumanAttackingAIFlag.Contains((BaseHumanUnit)tile.occupiedUnit))
                    {
                        unitHumanAttackingAIFlag.Add((BaseHumanUnit)tile.occupiedUnit);
                    }

                    if (turno == 0)
                    {
                        turno = TurnManager.turnCounter;
                    }
                    else if ((TurnManager.turnCounter - turno) == 3)
                    {
                        UnitManager._AIFlags.Remove(this);
                        ChangeFlagHuman();
                        UnitManager._playerFlags.Add(this);
                        unitHumanAttackingAIFlag.Clear();
                        foreach(BaseAIUnit unit in unitAIDefendingFlag)
                        {
                            unit.turnsInFlag = 0;
                            unit._flagToDefend = null;
                            unit.state = State.AIMoving;
                        }
                        unitAIDefendingFlag.Clear();
                        turno = 0;
                        if (UnitManager._AIFlags.Count == 0)
                        {
                            GameManager.Instance.UpdateGameState(GameState.Victory);
                        }
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
                beingAttacked = true;
            }
        }
    }


    public void ChangeFlagHuman()
    {
        this.gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
        this.gameObject.tag = "Bandera_Jugador";
        beingAttacked = false;
    }

    public void ChangeFlagAI()
    {
        this.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        this.gameObject.tag = "Bandera_IA";
    }
}
