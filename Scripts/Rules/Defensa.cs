using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defensa : MonoBehaviour
{
    public static Defensa Instance;
    public bool isDefending = false;

    private void Awake()
    {
        Instance = this;
    }
    public void DefensaAI(BaseAIUnit unit)
    {
        Debug.Log(unit);
        Tile objectiveTile = null;
        Tile lastTile = unit.occupiedTile;
        if (unit._flagToDefend != null && unit._flagToDefend.neighbours.Contains(unit.occupiedTile))
        {
            unit.state = State.AIDefending;

        }
        else if (unit.state == State.AIRetire)
        {
            isDefending = true;
            objectiveTile = unit.moveTo(Grid.Instance.GetNeighboursUnit(unit.occupiedTile, unit.getRange(unit.type)), unit._flagToDefend.flagPosition);

            lastTile.occupiedUnit = null;
            unit.occupiedTile = objectiveTile;
            objectiveTile.occupiedUnit = unit;
            unit.transform.position = objectiveTile.transform.position;
        }
    }
}