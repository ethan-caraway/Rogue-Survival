using UnityEngine;

public class GameMediator : MonoBehaviour
{
	// The controller for the board
	[SerializeField]
	private BoardController board;

	// The controller for the player
	[SerializeField]
	private PlayerController player;

	// The controller for the HUD
	[SerializeField]
	private HudController hud;

	// The current level that the player is on
	public static int CurrentLevel
	{
		get;
		private set;
	}

	// The controller for the turns
	public TurnController Turn
	{
		get;
		private set;
	}

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	private void Start ( )
	{
		// Create the turn controller
		Turn = new TurnController ( );

		// Subscribe to the level completion event
		Turn.OnLevelComplete += NewLevel;

		// Check for player stats
		if ( PlayerController.Stats == null )
		{
			ResetRun ( );
		}

		// Start the level
		NewLevel ( );

		// Initialize the HUD
		hud.Init ( );
	}

	// ResetRun is called to reset the player and run data
	public void ResetRun ( )
	{
		// Set default player stats
		PlayerController.Stats = new PlayerData ( 5, 3, 10 );

		// Set the current level
		CurrentLevel = 0;
	}

	// NewLevel is called when a new level needs to be procedurally generated
	private void NewLevel ( )
	{
		// Increment level
		CurrentLevel++;

		// Reset the turns
		hud.UpdateHUD ( 1 );

		// Clear previous board
		board.Clean ( );

		// Create the board
		board.Init ( );

		// Place the player in its starting cell in the bottom left corner
		player.Spawn ( board, new Vector2Int ( 1, 1 ) );
	}
}
