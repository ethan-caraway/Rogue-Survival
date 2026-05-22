using UnityEngine;
using UnityEngine.Tilemaps;

public class ExitCellObject : CellObject
{
	// The tile for displaying the exit on the board
	[SerializeField]
	private Tile exitTile;

	// The mediator for the game
	public GameMediator Mediator;

	// ExitCellObject.Init is called when an exit is initialized on the board
	public override void Init ( BoardController boardController, Vector2Int cell )
	{
		// Set the cell object data
		base.Init ( boardController, cell );

		// Update the board with this exit tile
		board.SetCellTile ( coordinates, exitTile );
	}

	// ExitCellObject.PlayerEntered is called when the player enters the exit cell
	public override void PlayerEntered ( )
	{
		// Mark level as complete
		Mediator.Turn.IsLevelComplete = true;
	}
}
