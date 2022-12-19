using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{

    public bool isDefending = false;
    public int turn;

    public void TurnAI(BaseAIUnit unit)
    {
        Defensa.DefensaAI(unit, isDefending);
        Movimiento.MovimientoAI(unit, turn);
        Ataque.AtaqueAI(unit);
    }
    void Update()
    {
        
    }
}
