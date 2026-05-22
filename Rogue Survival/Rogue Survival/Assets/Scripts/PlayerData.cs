public class PlayerData
{
	// PlayerData is a constructor for the PlayerData class
	public PlayerData ( int health, int speed, int attack )
	{
		// Set the health stat
		MaxHealth = health;
		CurrentHealth = health;

		// Set the speed stat
		MaxSpeed = speed;
		CurrentSpeed = speed;

		// Set the attack stat
		Attack = attack;
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

	// The amount of damage the player applies with an attack
	public int Attack
	{
		get;
		private set;
	}
}
