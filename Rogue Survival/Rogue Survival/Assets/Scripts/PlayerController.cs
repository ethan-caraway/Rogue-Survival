using UnityEngine;

public class PlayerController : MonoBehaviour
{
	// The controller for the board
	private BoardController board;

	// The current cell coordinates
	private Vector2Int cellPosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	// Spawn is called to position the player initially on the board
	public void Spawn ( BoardController boardController, Vector2Int cell )
	{
		// Store the reference to the board
		board = boardController;

		// Store the starting position
		cellPosition = cell;

		// Position player to the cell
		transform.position = board.GetCellPosition ( cellPosition );
	}
}
