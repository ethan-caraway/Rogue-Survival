public class TurnController
{
	// A delegate that takes an integer for the current turn as a parameter
	public delegate void TickHandler ( int turn );

	// Event for when a tick occurs
	public event TickHandler OnTick;

	// Whether or not the level is complete
	public bool IsLevelComplete = false;

	// Event for when the player completes the level
	public event System.Action OnLevelComplete;

	// The current turn of the game
	private int currentTurn = 1;

	// Tick is called to increment the current turn
	public void Tick ( )
	{
		// Increment turn
		currentTurn++;

		// Check for functions subscribed to the event
		if ( OnTick != null )
		{
			// Trigger the event
			OnTick ( currentTurn );
		}

		// Check if the level has been completed by the player
		if ( IsLevelComplete )
		{
			// Reset the level
			IsLevelComplete = false;

			// Reset the turn
			currentTurn = 1;

			// Check for functions subscribed to the completion event
			if ( OnLevelComplete != null )
			{
				// Trigger the completion event
				OnLevelComplete ( );
			}
		}
	}
}
