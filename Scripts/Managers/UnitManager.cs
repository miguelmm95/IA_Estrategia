using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    public static UnitManager Instance;

    public static List<string> _playerUnits = new List<string>();
    public static List<string> _AIUnits = new List<string>();

    public GameObject meleeUnit;
    public GameObject tankUnit;
    public GameObject rangedUnit;

    private void Awake()
    {
        Instance = this;
    }

    public void SpawnAIUnitsRandom()
    {
        var AIUnits = _AIUnits.Count;

        for(int i = 0; i < AIUnits; i++)
        {
            switch(_AIUnits[i]){
                case "melee":
                    var melee = Instantiate(meleeUnit);
                    var spawnTileMelee = Grid.Instance.GetRandomAISpawnTile();
                    melee.transform.position = spawnTileMelee.transform.position;
                    spawnTileMelee.unit = melee;
                    break;

                case "tank":
                    var tank = Instantiate(tankUnit);
                    var spawnTileTank = Grid.Instance.GetRandomAISpawnTile();
                    tank.transform.position = spawnTileTank.transform.position;
                    spawnTileTank.unit = tank;
                    break;

                case "ranged":
                    var ranged = Instantiate(rangedUnit);
                    var spawnTileRanged = Grid.Instance.GetRandomAISpawnTile();
                    ranged.transform.position = spawnTileRanged.transform.position;
                    spawnTileRanged.unit = ranged;
                    break;
            }
        }

        GameManager.Instance.UpdateGameState(GameState.PlayerTurn);
    }
}
