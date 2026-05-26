using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BoardController : MonoBehaviour
{
	// The mediator for the game
	[SerializeField]
	private GameMediator mediator;

	// The tilemap for the board
	[SerializeField]
	private Tilemap tilemap;

	// The grid for the board
	[SerializeField]
	private Grid grid;

	// The width and height of the board
	[SerializeField]
	private Vector2Int dimensions;

	// The list of available ground tiles for the board
	[SerializeField]
	private Tile [ ] groundTiles;

	// The list of available wall tiles for the board
	[SerializeField]
	private Tile [ ] wallTiles;

	// The prefab for the exit on the board
	[SerializeField]
	private ExitCellObject exitPrefab;

	// The list of prefabs of food to spawn on the board
	[SerializeField]
	private FoodObject [ ] foodPrefabs;

	// The list of prefabs of obstacles to spawn on the board
	[SerializeField]
	private ObstacleObject [ ] obstaclePrefabs;

	// The amount of food to spawn on the board
	[SerializeField]
	private int foodCount;

	// The minimum amount of obstacles to spawn on the board
	[SerializeField]
	private int minObstacleCount;

	// The maximum amount of obstacles to spawn on the board
	[SerializeField]
	private int maxObstacleCount;

	// The data for each cell in the board
	private CellData [ , ] boardData;

	// The list of empty cells during generation
	private List<Vector2Int> emptyCells = new List<Vector2Int> ( );

	// Init is called to initialize the board
	public void Init ( )
	{
		// Create a 2D array of cell data
		boardData = new CellData [ dimensions.x, dimensions.y ];

		// Clear any previous cells
		emptyCells.Clear ( );

		// Navigate each row of the board
		for ( int y = 0; y < dimensions.y; y++ )
		{
			// Navigate each column of the board
			for ( int x = 0; x < dimensions.x; x++ )
			{
				// Store a potential tile
				Tile tile;

				// Check for a border cell
				if ( x == 0 || x == dimensions.x - 1 || y == 0 || y == dimensions.y - 1 )
				{
					// Get a random wall tile
					tile = wallTiles [ Random.Range ( 0, wallTiles.Length ) ];

					// Prevent the player from moving to this cell
					boardData [ x, y ] = new CellData
					{
						IsPassable = false
					};
				}
				else
				{
					// Get a random ground tile
					tile = groundTiles [ Random.Range ( 0, groundTiles.Length ) ];

					// Allow the player to move to this cell
					boardData [ x, y ] = new CellData
					{
						IsPassable = true
					};

					// Mark the cell as empty
					emptyCells.Add ( new Vector2Int ( x, y ) );
				}

				// Paint the tile map with the random tile
				tilemap.SetTile ( new Vector3Int ( x, y, 0 ), tile );
			}
		}

		// Remove the spawn position for the player
		emptyCells.Remove ( new Vector2Int ( 1, 1 ) );

		// Get the cell coordinates for the exit
		Vector2Int exitCoordinates = new Vector2Int ( dimensions.x - 2, dimensions.y - 2 );

		// Spawn an exit object
		ExitCellObject exit = Instantiate ( exitPrefab );

		// Set the mediator for the exit
		exit.Mediator = mediator;

		// Add exit to the board
		AddObject ( exit, exitCoordinates );

		// Remove the exit position
		emptyCells.Remove ( exitCoordinates );

		// Populate the board with obstacles
		GenerateObstacles ( );

		// Populate the board with food
		GenerateFood ( );
	}

	// Clean is used to reset the board and delete any excess objects
	public void Clean ( )
	{
		// Check for board
		if ( boardData == null )
		{
			// End early as there is nothing to clean
			return;
		}

		// Navigate each row of the board
		for ( int y = 0; y < dimensions.y; y++ )
		{
			// Navigate each column of the board
			for ( int x = 0; x < dimensions.x; x++ )
			{
				// Get cell data
				CellData cell = boardData [ x, y ];

				// Check for data and object
				if ( cell != null && cell.ContainedObject != null )
				{
					// Delete the object on the board
					Destroy ( cell.ContainedObject.gameObject );
				}
			}
		}

		// Reset tiles
		tilemap.ClearAllTiles ( );
	}

	// IncreaseObstacles is used to increase the total amount of obstacles generated
	public void IncreaseObstacles ( )
	{
		// Increment minimum obstacles
		minObstacleCount++;

		// Increment maximum obstacles
		maxObstacleCount++;
	}

	// GetCellPosition is used to get the world space position of a cell by its coordinates
	public Vector3 GetCellPosition ( Vector2Int cell )
	{
		// Convert the cell position to world space
		return tilemap.GetCellCenterWorld ( (Vector3Int)cell );
	}

	// GetCellData is used for retrieving the data of a cell by its coordinates 
	public CellData GetCellData ( Vector2Int cell )
	{
		// Check for valid cell coordinates
		if ( cell.x < 0 || cell.x >= boardData.GetLength ( 0 ) || cell.y < 0 || cell.y >= boardData.GetLength ( 1 ) )
		{
			// Return no data
			return null;
		}

		// Return the data for the cell
		return boardData [ cell.x, cell.y ];
	}

	// GetCellTile is used to get a tile on the board
	public Tile GetCellTile ( Vector2Int cell )
	{
		// Return the tile at the cell coordinates
		return tilemap.GetTile<Tile> ( (Vector3Int)cell );
	}

	// SetCellTile is used to update a tile on the board
	public void SetCellTile ( Vector2Int cell, Tile tile )
	{
		// Set the new tile in the board
		tilemap.SetTile ( (Vector3Int)cell, tile );
	}

	// AddObject is used to initialize a cell object on the board
	private void AddObject ( CellObject obj, Vector2Int cell )
	{
		// Get the cell data for the cell
		CellData data = GetCellData ( cell );

		// Check for cell data
		if ( data != null )
		{
			// Set the position of the object
			obj.transform.position = GetCellPosition ( cell );

			// Store the object in the data
			data.ContainedObject = obj;

			// Initialize the object
			obj.Init ( this, cell );
		}
	}

	// GenerateObstacles is used to procedurally generate obstacles on the board
	private void GenerateObstacles ( )
	{
		// Get amount of obstacles to generate
		int obstacleCount = Random.Range ( minObstacleCount, maxObstacleCount );

		// Generate each obastacle
		for ( int i = 0; i < obstacleCount; i++ )
		{
			// Check for empty cells
			if ( emptyCells.Count < 1 )
			{
				// End generation
				return;
			}

			// Get random coordinates
			Vector2Int coordinates = emptyCells [ Random.Range ( 0, emptyCells.Count ) ];

			// Remove coordinates from the list of available cells
			emptyCells.Remove ( coordinates );

			// Get random obstacle prefab
			ObstacleObject prefab = obstaclePrefabs [ Random.Range ( 0, obstaclePrefabs.Length ) ];

			// Spawn an instance of the obstacle
			ObstacleObject obstacle = Instantiate ( prefab );

			// Add new object to the board
			AddObject ( obstacle, coordinates );
		}
	}

	// GenerateFood is used to procedurally generate food items on the board
	private void GenerateFood ( )
	{
		// Generate the total amount of food
		for ( int i = 0; i < foodCount; i++ )
		{
			// Check for empty cells
			if ( emptyCells.Count < 1 )
			{
				// End generation
				return;
			}

			// Get random coordinates
			Vector2Int coordinates = emptyCells [ Random.Range ( 0, emptyCells.Count ) ];

			// Remove coordinates from the list of available cells
			emptyCells.Remove ( coordinates );

			// Get random food prefab
			FoodObject prefab = foodPrefabs [ Random.Range ( 0, foodPrefabs.Length ) ];

			// Spawn an instance of the food
			FoodObject food = Instantiate ( prefab );

			// Add new object to the board
			AddObject ( food, coordinates );
		}
	}
}
