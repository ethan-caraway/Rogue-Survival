public class TurnController
{
	// Event for when a tick occurs
	public event System.Action OnTick;

	// The current turn of the game
	private int currentTurn = 1;

	// The read-only property of the current turn of the game
	public int CurrentTurn
	{
		get
		{
			return currentTurn;
		}
	}

	// Tick is called to increment the current turn
	public void Tick ( )
	{
		// Increment turn
		currentTurn++;

		// Check for functions subscribed to the event
		if ( OnTick != null )
		{
			// Trigger the event
			OnTick ( );
		}
	}
}
