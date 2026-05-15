using UnityEngine;

public class FoodObject : CellObject
{
	[SerializeField]
	private int healthGain;

	[SerializeField]
	private int speedGain;

	// FoodObject.PlayerEntered is called when the player enters a cell with food in it.
	public override void PlayerEntered ( )
	{
		// Gain health for the player
		PlayerController.Stats.CurrentHealth += healthGain;

		// Gain speed energy for the player
		PlayerController.Stats.CurrentSpeed += speedGain;

		// Destroy the food
		Destroy ( gameObject );
	}
}
