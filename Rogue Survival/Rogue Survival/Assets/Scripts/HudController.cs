using TMPro;
using UnityEngine;

public class HudController : MonoBehaviour
{
	// The mediator for the game
	[SerializeField]
	private GameMediator mediator;

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

	// Init is called to initialize the HUD
	public void Init ( )
	{
		// Subscribe to the OnTick event
		mediator.Turn.OnTick += UpdateHUD;
	}

	// UpdateHUD is called to update the turn display in the HUD
	public void UpdateHUD ( int turn )
	{
		// Display the current turn
		turnText.text = $"Turn {turn}";

		// Display the player's current stats
		UpdateStats ( );
	}

	// UpdateStats is called to update the stats displayed in the HUD
	private void UpdateStats ( )
	{
		// Display the player's current health
		healthText.text = $"{PlayerController.Stats.CurrentHealth}/{PlayerController.Stats.MaxHealth}";

		// Display the player's current speed energy
		speedText.text = $"{PlayerController.Stats.CurrentSpeed}/{PlayerController.Stats.MaxSpeed}";

		// Display the player's current attack
		attackText.text = PlayerController.Stats.Attack.ToString ( );
	}
}
