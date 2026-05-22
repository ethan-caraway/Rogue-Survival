using UnityEngine;

public class CellObject : MonoBehaviour
{
	// The controller for the board
	protected BoardController board;

	// The cell coordinates for this object
	protected Vector2Int coordinates;

	// Init is called when the cell object is initialized on the board
	public virtual void Init ( BoardController boardController, Vector2Int cell )
	{
		// Store the board controller
		board = boardController;

		// Store the coordinates for the object
		coordinates = cell;
	}

	// CanPlayerEnter is called before a player enters a cell with an object in it to see if the player is allowed to
	public virtual bool CanPlayerEnter ( )
	{
		// Allow the player to enter the cell by default
		return true;
	}

	// PlayerEntered is called when the player enters a cell with an object in it
	public virtual void PlayerEntered ( )
	{
		
	}
}
