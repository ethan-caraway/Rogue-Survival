using UnityEngine;

public class PlayerData
{
	// PlayerData is a constructor for the PlayerData class
	public PlayerData ( int health, int speed, int attack )
	{
		// Set the health stat
		MaxHealth = health;
		CurrentHealth = health;

		// Set the speed stat
		InitialMaxSpeed = speed;
		MaxSpeed = speed;
		CurrentSpeed = speed;

		// Set the attack stat
		InitialAttack = attack;
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

	// The initial maximum counter for the speed stat the player can have
	public int InitialMaxSpeed
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

	// The initial amount of damage the player applies with an attack
	public int InitialAttack
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

	// ModifySpeed is used to modify the player's current max speed stat
	public void ModifySpeed ( int speedDelta )
	{
		// Change the current max speed
		// Ensure a max speed of at least 1
		MaxSpeed = Mathf.Max ( 1, MaxSpeed + speedDelta );

		// Check if the current speed exceeds the new max speed
		if ( CurrentSpeed > MaxSpeed )
		{
			// Change the current speed to reflect the change in max speed
			CurrentSpeed = Mathf.Max ( 1, CurrentSpeed + speedDelta );
		}
	}

	// ModifyAttack is used to modify the player's current attack stat
	public void ModifyAttack ( int attackDelta )
	{
		// Change the current attack
		// Ensure an attack of at least 1
		Attack = Mathf.Max ( 1, Attack + attackDelta );
	}
}
