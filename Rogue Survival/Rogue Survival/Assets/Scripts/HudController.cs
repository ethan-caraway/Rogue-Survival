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

	[SerializeField]
	private TMP_Text healthText;

	[SerializeField]
	private TMP_Text speedText;

	// Init is called to initialize the HUD
	public void Init ( )
	{
		// Display the turn in the HUD
		UpdateHUD ( 1 );

		// Subscribe to the OnTick event
		mediator.Turn.OnTick += UpdateHUD;
	}

	// UpdateHUD is called to update the turn display in the HUD
	private void UpdateHUD ( int turn )
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
	}
}
