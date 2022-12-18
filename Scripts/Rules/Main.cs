using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
   
    public void TurnAI(BaseAIUnit unit)
    {
        Defensa.Instance.DefensaAI(unit);
        Movimiento.Instance.MovimientoAI(unit);
        Ataque.Instance.AtaqueAI(unit);
    }
    void Update()
    {
        
    }
}
