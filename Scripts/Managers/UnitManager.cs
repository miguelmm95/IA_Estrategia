using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    public static UnitManager Instance;

    public static List<string> _playerUnits = new List<string>();
    public static List<string> _AIUnits = new List<string>();
    public static List<BaseAIUnit> _AIUnitsObjects = new List<BaseAIUnit>();
    public static List<GameObject> _playerFlags = new List<GameObject>();
    public static List<GameObject> _AIFlags = new List<GameObject>();
    public static List<Tile> vecinosAntiguos = new List<Tile>();

    public BaseUnit meleeAI;
    public BaseUnit tankAI;
    public BaseUnit rangedAI;
    public InfantryHuman meleeHuman;
    public HeavyHuman tankHuman;
    public RangedHuman rangedHuman;

    public static BaseHumanUnit selectedHumanUnit;

    private void Awake()
    {
        Instance = this;
    }

    public void SelectedHero(BaseHumanUnit unit)
    {
        selectedHumanUnit = unit;
    }

    public void SpawnAIUnitsRandom()
    {
        var AIUnits = _AIUnits.Count;

        for(int i = 0; i < AIUnits; i++)
        {
            switch(_AIUnits[i]){
                case "melee":
                    var melee = Instantiate(meleeAI);
                    _AIUnitsObjects.Add((BaseAIUnit)melee);
                    var spawnTileMelee = Grid.Instance.GetRandomAISpawnTile();
                    melee.transform.position = spawnTileMelee.transform.position;
                    spawnTileMelee.occupiedUnit = melee;
                    InfantryAI unitMelee = melee.GetComponent<InfantryAI>();
                    unitMelee.occupiedTile = spawnTileMelee;
                    break;

                case "tank":
                    var tank = Instantiate(tankAI);
                    _AIUnitsObjects.Add((BaseAIUnit)tank);
                    var spawnTileTank = Grid.Instance.GetRandomAISpawnTile();
                    tank.transform.position = spawnTileTank.transform.position;
                    spawnTileTank.occupiedUnit = tank;
                    HeavyAI unitTank = tank.GetComponent<HeavyAI>();
                    unitTank.occupiedTile = spawnTileTank;
                    break;

                case "ranged":
                    var ranged = Instantiate(rangedAI);
                    _AIUnitsObjects.Add((BaseAIUnit)ranged);
                    var spawnTileRanged = Grid.Instance.GetRandomAISpawnTile();
                    ranged.transform.position = spawnTileRanged.transform.position;
                    spawnTileRanged.occupiedUnit = ranged;
                    RangedAI unitRanged = ranged.GetComponent<RangedAI>();
                    unitRanged.occupiedTile = spawnTileRanged;
                    break;
            }
        }
        SpawnHumanUnitRandom();
        GameManager.Instance.UpdateGameState(GameState.PlayerTurn);
    }

    public void SpawnHumanUnitRandom()
    {

        foreach (string name in _playerUnits)
        {
            switch (name)
            {
                case "melee":
                    var melee = Instantiate(meleeHuman);
                    var spawnTileMelee = Grid.Instance.GetRandomHumanSpawnTile();
                    melee.transform.position = spawnTileMelee.transform.position;
                    spawnTileMelee.occupiedUnit = melee;
                    melee.occupiedTile = spawnTileMelee;
                    break;
                case "tank":
                    var tank = Instantiate(tankHuman);
                    var spawnTileTank = Grid.Instance.GetRandomHumanSpawnTile();
                    tank.transform.position = spawnTileTank.transform.position;
                    spawnTileTank.occupiedUnit = tank;
                    tank.occupiedTile = spawnTileTank;
                    break;
                case "ranged":
                    var ranged = Instantiate(rangedHuman);
                    var spawnTileRanged = Grid.Instance.GetRandomHumanSpawnTile();
                    ranged.transform.position = spawnTileRanged.transform.position;
                    spawnTileRanged.occupiedUnit = ranged;
                    ranged.occupiedTile = spawnTileRanged;
                    break;
            }
        }
    }

    public flagManagement getRandomHumanFlag()
    {
        GameObject _object = _playerFlags[Random.Range(0, _playerFlags.Count - 1)];

        return _object.GetComponent<flagManagement>();
    }
}
