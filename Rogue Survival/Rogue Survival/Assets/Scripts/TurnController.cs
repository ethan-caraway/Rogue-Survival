public class TurnController
{
	// A delegate that takes an integer for the current turn as a parameter
	public delegate void TickHandler ( int turn );

	// Event for when a tick occurs
	public event TickHandler OnTick;

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
	}
}
