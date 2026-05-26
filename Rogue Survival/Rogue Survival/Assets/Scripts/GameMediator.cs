using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMediator : MonoBehaviour
{
	// The amount of levels the player must complete in an environment
	private const int LEVELS_PER_ENVIRONMENT = 30;

	// The scene name for the sand level
	private const string SAND_SCENE = "Sand";

	// The scene name for the snow level
	private const string SNOW_SCENE = "Snow";

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

	// Whether or not the game is actively in play
	public bool IsGameRunning
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

		// Subscribe to the game over event
		Turn.OnGameOver += EndGame;

		// Mark the game as running
		IsGameRunning = true;

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
		PlayerController.Stats = new PlayerData ( 50, 3, 10 );

		// Set the current level
		CurrentLevel = 0;
	}

	// NewLevel is called when a new level needs to be procedurally generated
	private void NewLevel ( )
	{
		// Increment level
		CurrentLevel++;

		// Check for completion of the urban environment
		if ( CurrentLevel == LEVELS_PER_ENVIRONMENT + 1 )
		{
			// Lower the player's attack
			PlayerController.Stats.ModifyAttack ( -5 );

			// Load the sand level
			SceneManager.LoadScene ( SAND_SCENE );
		}
		// Check for completion of the sand environment
		else if ( CurrentLevel == ( LEVELS_PER_ENVIRONMENT * 2 ) + 1 )
		{
			// Lower the player's speed
			PlayerController.Stats.ModifySpeed ( -1 );

			// Load the snow level
			SceneManager.LoadScene ( SNOW_SCENE );
		}
		// Check for completion of the snow environment
		else if ( CurrentLevel == ( LEVELS_PER_ENVIRONMENT * 3 ) + 1 )
		{
			// Correct the days survived
			CurrentLevel = LEVELS_PER_ENVIRONMENT * 3;

			// Mark the game as over
			EndGame ( );

			// Display a win
			hud.DisplayGameOver ( true );
		}
		else
		{
			// Check for every third level
			if ( CurrentLevel % 3 == 0 )
			{
				// Increase the amount of obstacles
				board.IncreaseObstacles ( );
			}

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

	// EndGame is called to end the game
	private void EndGame ( )
	{
		// Mark that the game is no longer running
		IsGameRunning = false;
	}
}
