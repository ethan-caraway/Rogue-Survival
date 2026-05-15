public class PlayerData
{
	// PlayerData is a constructor for the PlayerData class
	public PlayerData ( int health, int speed )
	{
		// Set the health
		MaxHealth = health;
		CurrentHealth = health;

		// Set the speed
		MaxSpeed = speed;
		CurrentSpeed = speed;
	}

	// The current amount of health the player has
	public int CurrentHealth;

	// The current counter for the speed stat the player has
	public int CurrentSpeed;

	// The maximum amount of health the player can have
	public int MaxHealth
	{
		get;
		private set;
	}

	// The maximum counter for the speed stat the player can have 
	public int MaxSpeed
	{
		get;
		private set;
	}
}
