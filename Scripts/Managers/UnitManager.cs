using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    public static UnitManager Instance;

    public List<Unit> _playerUnits = new List<Unit>();
    public List<Unit> _AIUnits = new List<Unit>();

    private void Awake()
    {
        Instance = this;
    }

    public void SpawnAIUnitsRandom()
    {
        var AIUnits = _AIUnits.Count;

        for(int i = 0; i < AIUnits; i++)
        {
            //Prefab
            //Instanciar la Unidad
            //Obtener una Tile aleatoria

            //Colocar la unidad en la Tile y decir que la tile esta ocupada.
        }
    }
}
