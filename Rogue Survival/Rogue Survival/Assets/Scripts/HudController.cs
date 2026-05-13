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

	// Init is called to initialize the HUD
	public void Init ( )
	{
		// Display the turn in the HUD
		UpdateTurn ( );

		// Subscribe to the OnTick event
		mediator.Turn.OnTick += UpdateTurn;
	}

	// UpdateTurn is called to update the turn display in the HUD
	private void UpdateTurn ( )
	{
		// Display the current turn
		turnText.text = $"Turn {mediator.Turn.CurrentTurn}";
	}
}
