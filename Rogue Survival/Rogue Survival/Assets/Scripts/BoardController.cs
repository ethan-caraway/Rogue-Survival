using UnityEngine;
using UnityEngine.Tilemaps;

public class BoardController : MonoBehaviour
{
	// A class for storing data for each cell in the board
	private class CellData
	{
		// Whether or not the play can move to the cell in the board
		public bool IsPassable;
	}

	// The tilemap for the board
	[SerializeField]
	private Tilemap tilemap;

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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
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
					boardData [ x, y ].IsPassable = false;
				}
				else
				{
					// Get a random ground tile
					tile = groundTiles [ Random.Range ( 0, groundTiles.Length ) ];

					// Allow the player to move to this cell
					boardData [ x, y ].IsPassable = true;
				}

				// Paint the tile map with the random tile
				tilemap.SetTile ( new Vector3Int ( x, y, 0 ), tile );
			}
		}
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
