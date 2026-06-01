using UnityEngine;

public class EnemyObject : CellObject
{
	// The amount of health the enemey should have
	[SerializeField]
	private int maxHealth;

	// The amount of health the enemy currently has
	private int currentHealth;

	// EnemyObject.Init is called when an enemy is initialized on the board
	public override void Init ( BoardController boardController, Vector2Int cell )
	{
		// Set the enemy's health
		currentHealth = maxHealth;

		// Initialize the cell object
		base.Init ( boardController, cell );
	}

	// EnemyObject.CanPlayerEnter is called before a player enters a cell with an enemy in it to see if the player is allowed to
	// If the player is not allowed to enter the cell, the player will attack the enemy
	public override bool CanPlayerEnter ( )
	{
		// Decrease the enemy's health by the player's attack stat
		currentHealth -= PlayerController.Stats.Attack;

		// Check if the enemy is destroyed
		if ( currentHealth <= 0 )
		{
			// Destroy the enemey
			Destroy ( gameObject );
		}

		// Return that player is not allowed to enter the cell
		return false;
	}
}
