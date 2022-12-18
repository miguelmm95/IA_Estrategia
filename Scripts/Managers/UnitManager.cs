using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    public static UnitManager Instance;

    public static List<string> _playerUnits = new List<string>();
    public static List<string> _AIUnits = new List<string>();
    public static List<BaseAIUnit> _AIUnitsObjects = new List<BaseAIUnit>();
    public static List<BaseHumanUnit> _humanUnitsObjects = new List<BaseHumanUnit>();

    public static List<Flag> _playerFlags = new List<Flag>();
    public static List<Flag> _AIFlags = new List<Flag>();
    

    public static List<Tile> vecinosAntiguos = new List<Tile>();

    public BaseUnit meleeAI;
    public BaseUnit tankAI;
    public BaseUnit rangedAI;
    public InfantryHuman meleeHuman;
    public HeavyHuman tankHuman;
    public RangedHuman rangedHuman;

    public static BaseHumanUnit selectedHumanUnit;

    [SerializeField] private GameObject _inGameUI;
    [SerializeField] private GameObject menuManager;

    public static int contadorUnidades;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        manualAIActivation();
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
        _inGameUI.SetActive(true);
        menuManager.GetComponent<TurnManager>().SearchUI();
    }

    public void SpawnAIUnitsAggressive()
    {
        var AIUnits = _AIUnits.Count;

        for(int i = 0; i < AIUnits; i++)
        {
            switch (_AIUnits[i])
            {
                case "tank":
                    var tank = Instantiate(tankAI);
                    _AIUnitsObjects.Add((BaseAIUnit)tank);
                    var spawnTileTank = Grid.Instance.GetAggressiveAISpawnTile("tank");
                    tank.transform.position = spawnTileTank.transform.position;
                    spawnTileTank.occupiedUnit = tank;
                    HeavyAI unitTank = tank.GetComponent<HeavyAI>();
                    unitTank.occupiedTile = spawnTileTank;
                    break;
                case "melee":
                    var melee = Instantiate(meleeAI);
                    _AIUnitsObjects.Add((BaseAIUnit)melee);
                    var spawnTileMelee = Grid.Instance.GetAggressiveAISpawnTile("melee");
                    melee.transform.position = spawnTileMelee.transform.position;
                    spawnTileMelee.occupiedUnit = melee;
                    InfantryAI unitMelee = melee.GetComponent<InfantryAI>();
                    unitMelee.occupiedTile = spawnTileMelee;
                    break;
                default:
                    var ranged = Instantiate(rangedAI);
                    _AIUnitsObjects.Add((BaseAIUnit)ranged);
                    var spawnTileRanged = Grid.Instance.GetAggressiveAISpawnTile("ranged");
                    ranged.transform.position = spawnTileRanged.transform.position;
                    spawnTileRanged.occupiedUnit = ranged;
                    RangedAI unitRanged = ranged.GetComponent<RangedAI>();
                    unitRanged.occupiedTile = spawnTileRanged;
                    break;
            }
        }
        GameManager.Instance.UpdateGameState(GameState.PlayerTurn);
        _inGameUI.SetActive(true);
        menuManager.GetComponent<TurnManager>().SearchUI();
    }

    public void SpawnAIUnitsDefensive()
    {
        var AIUnits = _AIUnits.Count;

        for (int i = 0; i < AIUnits; i++)
        {
            switch (_AIUnits[i])
            {
                case "tank":
                    var tank = Instantiate(tankAI);
                    _AIUnitsObjects.Add((BaseAIUnit)tank);
                    var spawnTileTank = Grid.Instance.GetDefensiveAISpawnTile("tank");
                    tank.transform.position = spawnTileTank.transform.position;
                    spawnTileTank.occupiedUnit = tank;
                    HeavyAI unitTank = tank.GetComponent<HeavyAI>();
                    unitTank.occupiedTile = spawnTileTank;
                    break;
                case "melee":
                    var melee = Instantiate(meleeAI);
                    _AIUnitsObjects.Add((BaseAIUnit)melee);
                    var spawnTileMelee = Grid.Instance.GetDefensiveAISpawnTile("melee");
                    melee.transform.position = spawnTileMelee.transform.position;
                    spawnTileMelee.occupiedUnit = melee;
                    InfantryAI unitMelee = melee.GetComponent<InfantryAI>();
                    unitMelee.occupiedTile = spawnTileMelee;
                    break;
                default:
                    var ranged = Instantiate(rangedAI);
                    _AIUnitsObjects.Add((BaseAIUnit)ranged);
                    var spawnTileRanged = Grid.Instance.GetDefensiveAISpawnTile("ranged");
                    ranged.transform.position = spawnTileRanged.transform.position;
                    spawnTileRanged.occupiedUnit = ranged;
                    RangedAI unitRanged = ranged.GetComponent<RangedAI>();
                    unitRanged.occupiedTile = spawnTileRanged;
                    break;
            }
        }
        //SpawnHumanUnitRandom();
        GameManager.Instance.UpdateGameState(GameState.UnitPlacement);
        _inGameUI.SetActive(true);
        menuManager.GetComponent<TurnManager>().SearchUI();
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
                    _humanUnitsObjects.Add((BaseHumanUnit)melee);
                    break;
                case "tank":
                    var tank = Instantiate(tankHuman);
                    var spawnTileTank = Grid.Instance.GetRandomHumanSpawnTile();
                    tank.transform.position = spawnTileTank.transform.position;
                    spawnTileTank.occupiedUnit = tank;
                    tank.occupiedTile = spawnTileTank;
                    _humanUnitsObjects.Add((BaseHumanUnit)tank);
                    break;
                case "ranged":
                    var ranged = Instantiate(rangedHuman);
                    var spawnTileRanged = Grid.Instance.GetRandomHumanSpawnTile();
                    ranged.transform.position = spawnTileRanged.transform.position;
                    spawnTileRanged.occupiedUnit = ranged;
                    ranged.occupiedTile = spawnTileRanged;
                    _humanUnitsObjects.Add((BaseHumanUnit)ranged);
                    break;
            }
        }
    }

    /*public Flag getRandomHumanFlag()
    {
        GameObject _object = new GameObject();

        if (_playerFlags.Count != 0)
        {
            _object = _playerFlags[Random.Range(0, _playerFlags.Count)];
        }

        return _object.GetComponent<Flag>();
    }*/

    public void manualAIActivation()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameManager.Instance.UpdateGameState(GameState.AITurn);
        }
    }

    public void ActiveAIUnits()
    {
        foreach (BaseAIUnit unit in _AIUnitsObjects)
        {
            unit.aiPlayer.enabled = true;
        }
    }

    public void DeactivateAIUnits()
    {
        foreach (BaseAIUnit unit in _AIUnitsObjects)
        {
            unit.aiPlayer.enabled = false;
        }
    }

    public void RestartHumanUnits()
    {
        foreach(BaseHumanUnit unit in _humanUnitsObjects)
        {
            unit.state = State.humanUnselected;
        }
    }

    public void RestartAIUnits()
    {
        foreach(BaseAIUnit unit in _AIUnitsObjects)
        {
            unit.state = State.AIMoving;
        }
    }
}
