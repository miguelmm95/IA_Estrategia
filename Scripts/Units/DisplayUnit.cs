using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayUnit : MonoBehaviour
{
    static public string SelectedUnit = "None";

    public void selectInfantry()
    {
        SelectedUnit = "melee";
    }

    public void selectHeavy()
    {
        SelectedUnit = "tank";
    }

    public void selectRanged()
    {
        SelectedUnit = "ranged";
    }
}
