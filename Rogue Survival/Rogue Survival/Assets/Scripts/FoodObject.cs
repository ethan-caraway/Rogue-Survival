using UnityEngine;

public class FoodObject : CellObject
{
	[SerializeField]
	private int healthGain;

	[SerializeField]
	private int energyGain;

	// FoodObject.PlayerEntered is called when the player enters a cell with food in it
	public override void PlayerEntered ( )
	{
		// Gain health for the player
		PlayerController.Stats.CurrentHealth += healthGain;

		// Gain energy for the player
		PlayerController.Stats.CurrentEnergy += energyGain;

		// Destroy the food
		Destroy ( gameObject );
	}
}
