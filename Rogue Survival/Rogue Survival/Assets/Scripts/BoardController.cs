using UnityEngine;
using UnityEngine.Tilemaps;

public class BoardController : MonoBehaviour
{
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

	// The data for each cell in the board
	private CellData [ , ] boardData;

	// Init is called to initialize the board
	public void Init ( )
	{
		// Create a 2D array of cell data
		boardData = new CellData [ dimensions.x, dimensions.y ];

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
				}

				// Paint the tile map with the random tile
				tilemap.SetTile ( new Vector3Int ( x, y, 0 ), tile );
			}
		}
	}

	// Update is called once per frame
	void Update ( )
	{

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
}
