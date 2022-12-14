using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Grid : MonoBehaviour {

    [SerializeField] private Transform _cam;

	public static Grid Instance;

    public bool displayGridGizmos;
	public LayerMask unwalkableMask;
	public Vector3 gridWorldSize;
	public float nodeRadius;
	public TerrainType[] walkableRegions;
	public int obstacleProximityPenalty = 10;
	Dictionary<int,int> walkableRegionsDictionary = new Dictionary<int, int>();
	LayerMask walkableMask;
	public GameObject prefabTile;
	public GameObject tile_nonWalkable;
	Node[,] grid;
	[HideInInspector] public Tile[,] gridTiles;
	public GameObject bandera_IA;
	public GameObject bandera_Jugador;
	public int probabilidad;

    private int contBanIA = 0;
	private int contBanJug = 0;

	float nodeDiameter;
	int gridSizeX, gridSizeY;

	int penaltyMin = int.MaxValue;
	int penaltyMax = int.MinValue;

	void Awake() {
		Instance = this;

		nodeDiameter = nodeRadius*2;
		gridSizeX = Mathf.RoundToInt(gridWorldSize.x/nodeDiameter);
		gridSizeY = Mathf.RoundToInt(gridWorldSize.z/nodeDiameter);

		//tile.gameObject.transform.localScale += new Vector3(gridSizeX, 0, gridSizeY);
		
		foreach (TerrainType region in walkableRegions) {
			walkableMask.value |= region.terrainMask.value;
			walkableRegionsDictionary.Add((int)Mathf.Log(region.terrainMask.value,2),region.terrainPenalty);
		}
	}

	public int MaxSize {
		get {
			return gridSizeX * gridSizeY;
		}
	}

	public void StartGrid()
	{
        CreateGrid();
        drawGrid();
        //Debug.Log(GetRandomAISpawnTile());
        GameManager.Instance.UpdateGameState(GameState.SpawnAIUnits);
    }

	void CreateGrid() {
		grid = new Node[gridSizeX,gridSizeY];
		gridTiles = new Tile[gridSizeX, gridSizeY];
		Vector3 worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x/2 - new Vector3(0,1,0) * gridWorldSize.z/2;

		for (int x = 0; x < gridSizeX; x ++) {
			for (int y = 0; y < gridSizeY; y ++) {
				Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + new Vector3(0, 1, 0) * (y * nodeDiameter + nodeRadius);
				bool walkable = !(Physics.CheckSphere(worldPoint,nodeRadius,unwalkableMask));

				int movementPenalty = 0;


				Ray ray = new Ray(worldPoint + Vector3.up * 50, Vector3.down);
				RaycastHit hit;
				if (Physics.Raycast(ray,out hit, 100, walkableMask)) {
					walkableRegionsDictionary.TryGetValue(hit.collider.gameObject.layer, out movementPenalty);
				}

				if (!walkable) {
					movementPenalty += obstacleProximityPenalty;
				}


				grid[x,y] = new Node(walkable,worldPoint, x,y);
			}
		}

		//BlurPenaltyMap (3);

	}

	/*void BlurPenaltyMap(int blurSize) {
		int kernelSize = blurSize * 2 + 1;
		int kernelExtents = (kernelSize - 1) / 2;

		int[,] penaltiesHorizontalPass = new int[gridSizeX,gridSizeY];
		int[,] penaltiesVerticalPass = new int[gridSizeX,gridSizeY];

		for (int y = 0; y < gridSizeY; y++) {
			for (int x = -kernelExtents; x <= kernelExtents; x++) {
				int sampleX = Mathf.Clamp (x, 0, kernelExtents);
				penaltiesHorizontalPass [0, y] += grid [sampleX, y].movementPenalty;
			}

			for (int x = 1; x < gridSizeX; x++) {
				int removeIndex = Mathf.Clamp(x - kernelExtents - 1, 0, gridSizeX);
				int addIndex = Mathf.Clamp(x + kernelExtents, 0, gridSizeX-1);

				penaltiesHorizontalPass [x, y] = penaltiesHorizontalPass [x - 1, y] - grid [removeIndex, y].movementPenalty + grid [addIndex, y].movementPenalty;
			}
		}
			
		for (int x = 0; x < gridSizeX; x++) {
			for (int y = -kernelExtents; y <= kernelExtents; y++) {
				int sampleY = Mathf.Clamp (y, 0, kernelExtents);
				penaltiesVerticalPass [x, 0] += penaltiesHorizontalPass [x, sampleY];
			}

			int blurredPenalty = Mathf.RoundToInt((float)penaltiesVerticalPass [x, 0] / (kernelSize * kernelSize));
			grid [x, 0].movementPenalty = blurredPenalty;

			for (int y = 1; y < gridSizeY; y++) {
				int removeIndex = Mathf.Clamp(y - kernelExtents - 1, 0, gridSizeY);
				int addIndex = Mathf.Clamp(y + kernelExtents, 0, gridSizeY-1);

				penaltiesVerticalPass [x, y] = penaltiesVerticalPass [x, y-1] - penaltiesHorizontalPass [x,removeIndex] + penaltiesHorizontalPass [x, addIndex];
				blurredPenalty = Mathf.RoundToInt((float)penaltiesVerticalPass [x, y] / (kernelSize * kernelSize));
				grid [x, y].movementPenalty = blurredPenalty;

				if (blurredPenalty > penaltyMax) {
					penaltyMax = blurredPenalty;
				}
				if (blurredPenalty < penaltyMin) {
					penaltyMin = blurredPenalty;
				}
			}
		}

	}
	*/

	public List<Node> GetNeighbours(Node node) {
		List<Node> neighbours = new List<Node>();

		for (int x = -1; x <= 1; x++) {
			for (int y = -1; y <= 1; y++) {
				if (x == 0 && y == 0)
					continue;

				int checkX = node.gridX + x;
				int checkY = node.gridY + y;

				if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY) {
					neighbours.Add(grid[checkX,checkY]);
				}
			}
		}

		return neighbours;
	}

    public List<Tile> GetNeighboursUnit(Tile tile, int max)
    {
        List<Tile> neighbours = new List<Tile>();

        for (int x = -max; x <= max; x++)
        {
            for (int y = -max; y <= max; y++)
            {
                if (x == 0 && y == 0)
                    continue;
				else if (Mathf.Abs(x) == max && Mathf.Abs(y) == max)
				{
					continue;
				}
				else
				{ 
					int checkX = tile.posX + x;
					int checkY = tile.posY + y;

					if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY && gridTiles[checkX, checkY].isWalkeable 
						&& gridTiles[checkX, checkY].occupiedUnit == null)
					{
						neighbours.Add(gridTiles[checkX, checkY]);
					}
                }
            }
        }

        return neighbours;
    }

    public List<Tile> GetNeighboursUnitAtack(Tile tile, int max)
    {
        List<Tile> neighbours = new List<Tile>();

        for (int x = -max; x <= max; x++)
        {
            for (int y = -max; y <= max; y++)
            {
                if (x == 0 && y == 0)
                    continue;
                else if (Mathf.Abs(x) == max && Mathf.Abs(y) == max)
                {
                    continue;
                }
                else
                {
                    int checkX = tile.posX + x;
                    int checkY = tile.posY + y;

                    if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY && gridTiles[checkX, checkY].isWalkeable
                        && (gridTiles[checkX, checkY].occupiedUnit == null || gridTiles[checkX, checkY].occupiedUnit.player == Player.AI))
                    {
                        neighbours.Add(gridTiles[checkX, checkY]);
                    }
                }
            }
        }

        return neighbours;
    }

    public List<Tile> GetNeighboursUnit(Tile tile)
	{
		List<Tile> neighbours = new List<Tile>();

		for (int x = -1; x <= 1; x++)
		{
			for (int y = -1; y <= 1; y++)
			{
				if (x == 0 && y == 0)
					continue;
				else
				{
					int checkX = tile.posX + x;
					int checkY = tile.posY + y;

					if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY && gridTiles[checkX, checkY] != null)
					{
						neighbours.Add(gridTiles[checkX, checkY]);
					}
				}
			}
		}

		return neighbours;
	}

	public Node NodeFromWorldPoint(Vector3 worldPosition) {
		float percentX = (worldPosition.x + gridWorldSize.x/2) / gridWorldSize.x;
		float percentY = (worldPosition.y + gridWorldSize.z/2) / gridWorldSize.z;
		percentX = Mathf.Clamp01(percentX);
		percentY = Mathf.Clamp01(percentY);

		int x = Mathf.RoundToInt((gridSizeX-1) * percentX);
		int y = Mathf.RoundToInt((gridSizeY-1) * percentY);
		return grid[x,y];
	}

	void OnDrawGizmos() {
		Gizmos.DrawWireCube(transform.position,new Vector3(gridWorldSize.x, gridWorldSize.z, 0));
		if (grid != null && displayGridGizmos)
		{
			foreach (Node n in grid)
			{
				Gizmos.color = (n.walkable) ? Color.white : Color.red;
				Gizmos.DrawCube(n.worldPosition, Vector3.one * (nodeDiameter - .1f));
			}
		}
	}

	public void drawGrid()
    {
		//Tile tiles;
		float scale = ((grid[1, 0].worldPosition.x - grid[0, 0].worldPosition.x));
		if (grid != null)
		{
			foreach (Node n in grid)
			{
				int i = Random.Range(0, probabilidad+1);
				if (i == probabilidad)
				{
					n.walkable = false;
				}
                if (!n.walkable)
                {
                    var tiles = Instantiate(prefabTile, n.worldPosition, Quaternion.identity);
                    tiles.gameObject.transform.localScale = new Vector3(scale, scale, scale);
					Tile tile = tiles.GetComponent<Tile>();
					tile.SetCoord(n.gridX, n.gridY);
					tile.name = $"Tile {n.gridX} {n.gridY}";
					tile.isWalkeable = false;
					tile.SetNonWalkable();
					gridTiles[n.gridX, n.gridY] = tile;
				}
                else
                {
					var tiles = Instantiate(prefabTile, n.worldPosition + new Vector3(0, 0, 2) , Quaternion.identity);
					tiles.gameObject.transform.localScale = new Vector3(scale, scale, scale);
					Tile tile = tiles.GetComponent<Tile>(); 
                    tile.name = $"Tile {n.gridX} {n.gridY}";
					tile.SetCoord(n.gridX, n.gridY);
					tile.isWalkeable = true;
					gridTiles[n.gridX, n.gridY] = tile;
					
					var isOffset = (n.gridX % 2 == 0 && n.gridY % 2 != 0) || (n.gridX % 2 != 0 && n.gridY % 2 == 0);
					tile.Init(isOffset);

					float gridFloat = (float)n.gridX;
					float gridSizeFloat = (float)gridSizeX;
					Debug.Log(gridFloat / gridSizeFloat);
					if (gridFloat/gridSizeFloat < 0.3 && Random.Range(0, 30) == 0 && contBanJug < 3)
                    {
						var banderaJugador = Instantiate(bandera_Jugador, n.worldPosition + new Vector3(0, 0, 2), Quaternion.identity);
						UnitManager._playerFlags.Add(banderaJugador);
						tile.hasAFlag = true;
						contBanJug++;
                    }
                    else if (gridFloat / gridSizeFloat > 0.7 && Random.Range(0, 30) == 0 && contBanIA < 3)
                    {
						var banderaIA = Instantiate(bandera_IA, n.worldPosition + new Vector3(0, 0, 2), Quaternion.identity);
						UnitManager._AIFlags.Add(banderaIA);
						tile.hasAFlag = true;
						contBanIA++;
                    }
                }
			}
		}
    }

	public Tile GetRandomAISpawnTile()
	{
		var tile = new Tile();

		while (true)
		{
            int indexX = Random.Range(gridSizeX / 2, gridSizeX);
            int indexY = Random.Range(0, gridSizeY);

            if (gridTiles[indexX, indexY].isWalkeable && gridTiles[indexX, indexY].occupiedUnit == null)
            {
                tile = gridTiles[indexX, indexY];
				break;
            }
        }

		return tile;
	}

	public Tile GetRandomHumanSpawnTile()
    {
		var tile = new Tile();

        while (true)
        {
			int indexX = Random.Range(0, gridSizeX / 2);
			int indexY = Random.Range(0, gridSizeY - 1);

			if(gridTiles[indexX, indexY].isWalkeable && gridTiles[indexX, indexY].occupiedUnit == null && !gridTiles[indexX, indexY].hasAFlag)
            {
				tile = gridTiles[indexX, indexY];
				break;
            }
		}

		return tile;
    }

	public Tile GetAggressiveAISpawnTile(string unit)
	{
		var tile = new Tile();
		int indexX = 0;
		int indexY = 0;

		while (true)
		{
            switch (unit)
            {
                case "tank":
                    indexX = Random.Range(gridSizeX / 2, gridSizeX / 2 + gridSizeX / 6);
                    indexY = Random.Range(0, gridSizeY - 1);
                    break;
                case "melee":
                    indexX = Random.Range(gridSizeX / 2 + gridSizeX / 6, gridSizeX - gridSizeX / 6);
                    indexY = Random.Range(0, gridSizeY - 1);
                    break;
                default:
                    indexX = Random.Range(gridSizeX - gridSizeX / 6, gridSizeX);
                    indexY = Random.Range(0, gridSizeY - 1);
                    break;
            }

            if (gridTiles[indexX, indexY].occupiedUnit == null && gridTiles[indexX, indexY].isWalkeable && !gridTiles[indexX, indexY].hasAFlag)
            {
                tile = gridTiles[indexX, indexY];
				break;
            }
        }
		return tile;
	}

	[System.Serializable]
	public class TerrainType {
		public LayerMask terrainMask;
		public int terrainPenalty;
	}


}