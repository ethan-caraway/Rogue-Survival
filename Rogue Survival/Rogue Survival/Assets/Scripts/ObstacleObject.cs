using UnityEngine;
using UnityEngine.Tilemaps;

public class ObstacleObject : CellObject
{
	// The tile for displaying an obstacle on the board
	[SerializeField]
	private Tile obstacleTile;

	[SerializeField]
	private Tile damageTile;

	// The total amount of health the obstacle has
	[SerializeField]
	private int maxHealth;

	// The current amount of health the obastacle has
	private int currentHealth;

	// The origanal tile on the board that the obstacle is replacing
	private Tile originalTile;

	// ObstacleObject.Init is called when an obstacle is initialized on the board
	public override void Init ( BoardController boardController, Vector2Int cell )
	{
		// Set the cell object data
		base.Init ( boardController, cell );

		// Set health
		currentHealth = maxHealth;

		// Get the original tile being replaced
		originalTile = board.GetCellTile ( coordinates );

		// Update the board with this obstacle tile
		board.SetCellTile ( coordinates, obstacleTile );
	}

	// ObstacleObject.CanPlayerEnter is called before a player enters a cell with an obstacle in it to see if the player is allowed to
	// If the player is not allowed to enter the cell, the player will attack the obstacle
	public override bool CanPlayerEnter ( )
	{
		// Decrease the obstacle's health by the player's attack stat
		currentHealth -= PlayerController.Stats.Attack;

		// Check if the obstacle is destroyed
		if ( currentHealth <= 0 )
		{
			// Reset the board to the original tile
			board.SetCellTile ( coordinates, originalTile );

			// Destroy the obstacles
			Destroy ( gameObject );
		}
		// Check if the obstacle is half damaged
		else if ( currentHealth <= maxHealth / 2 )
		{
			// Replace the obstacle with the damage tile
			board.SetCellTile ( coordinates, damageTile );
		}

		// Return that player is not allowed to enter the cell
		return false;
	}
}
