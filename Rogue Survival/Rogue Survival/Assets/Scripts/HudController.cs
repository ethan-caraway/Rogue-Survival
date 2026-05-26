using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HudController : MonoBehaviour
{
	// The name of the first level scene
	private const string URBAN_SCENE = "Urban";

	// The mediator for the game
	[SerializeField]
	private GameMediator mediator;

	// The UI element containing the HUD elements
	[SerializeField]
	private GameObject hudContainer;

	// The text element for displaying the level
	[SerializeField]
	private TMP_Text levelText;

	// The text element for displaying the turn
	[SerializeField]
	private TMP_Text turnText;

	// The text element for displaying the player's health stat
	[SerializeField]
	private TMP_Text healthText;

	// The text element for displaying the player's speed stat
	[SerializeField]
	private TMP_Text speedText;

	// The text element for displaying the player's attack stat
	[SerializeField]
	private TMP_Text attackText;

	// The UI element containing the game over menu
	[SerializeField]
	private GameObject gameOverContainer;

	// The text element for the title of the game over menu
	[SerializeField]
	private TMP_Text titleText;

	// The button element for the retry button in the game over menu 
	[SerializeField]
	private GameObject retryButton;

	// The text element for display the game summary in the game over menu
	[SerializeField]
	private TMP_Text summaryText;

	[SerializeField]
	private Color32 speedColor;

	// Init is called to initialize the HUD
	public void Init ( )
	{
		// Subscribe to the OnTick event
		mediator.Turn.OnTick += UpdateHUD;

		// Subscribe to the OnGameOver event
		mediator.Turn.OnGameOver += DisplayGameOver;
	}

	// UpdateHUD is called to update the turn display in the HUD
	public void UpdateHUD ( int turn )
	{
		// Display the current level
		levelText.text = $"Day {GameMediator.CurrentLevel}";

		// Display the current turn
		turnText.text = $"Turn {turn}";

		// Display the player's current stats
		UpdateStats ( );
	}

	// DisplayGameOver is called to display the game over menu for a win or loss
	public void DisplayGameOver ( bool isWin )
	{
		// Hide the HUD
		hudContainer.SetActive ( false );

		// Display the game over menu
		gameOverContainer.SetActive ( true );

		// Check for win
		if ( isWin )
		{
			// Display a win
			titleText.text = "Congratulations";
		}
		else
		{
			// Display a loss
			titleText.text = "Game Over";
		}

		// Check total number of days survived
		if ( GameMediator.CurrentLevel > 1 )
		{
			// Display the summary
			summaryText.text = $"You survived {GameMediator.CurrentLevel} days";
		}
		else
		{
			// Display the summary
			summaryText.text = $"You survived {GameMediator.CurrentLevel} day";
		}
	}

	// Retry is called to restart a run of the game
	public void Retry ( )
	{
		// Reset the level
		mediator.ResetRun ( );

		// Reload the first level
		SceneManager.LoadScene ( URBAN_SCENE );
	}

	// UpdateStats is called to update the stats displayed in the HUD
	private void UpdateStats ( )
	{
		// Display the player's current health
		healthText.text = $"{PlayerController.Stats.CurrentHealth}/{PlayerController.Stats.MaxHealth}";

		// Get the current speed color
		string currentSpeedColor = "white";

		// Check for increased current speed
		if ( PlayerController.Stats.CurrentSpeed > PlayerController.Stats.MaxSpeed )
		{
			// Make current speed green
			currentSpeedColor = $"#{ColorUtility.ToHtmlStringRGB ( speedColor )}";
		}

		// Get the max speed color
		string maxSpeedColor = "white";

		// Check for lowered speed
		if ( PlayerController.Stats.MaxSpeed < PlayerController.Stats.InitialMaxSpeed )
		{
			// Make max speed red
			maxSpeedColor = "red";
		}

		// Display the player's current speed energy
		speedText.text = $"<color={currentSpeedColor}>{PlayerController.Stats.CurrentSpeed}</color>/<color={maxSpeedColor}>{PlayerController.Stats.MaxSpeed}</color>";

		// Check for lowered attack
		if ( PlayerController.Stats.Attack < PlayerController.Stats.InitialAttack )
		{
			// Make attack stat red
			attackText.color = Color.red;
		}
		else
		{
			// Make attack stat white
			attackText.color = Color.white;
		}

		// Display the player's current attack
		attackText.text = PlayerController.Stats.Attack.ToString ( );
	}

	// DisplayGameOver is called to display the game over menu
	private void DisplayGameOver ( )
	{
		// Display a loss
		DisplayGameOver ( false );
	}
}
