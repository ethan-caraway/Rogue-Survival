using UnityEngine;

public class PlayerData
{
	// PlayerData is a constructor for the PlayerData class
	public PlayerData ( int health, int energy, int attack )
	{
		// Set the health stat
		MaxHealth = health;
		CurrentHealth = health;

		// Set the speed stat
		InitialMaxEnergy = energy;
		MaxEnergy = energy;
		CurrentEnergy = energy;

		// Set the attack stat
		InitialAttack = attack;
		Attack = attack;
	}

	// The current amount of health the player has
	public int CurrentHealth;

	// The current counter for the energy stat the player has
	public int CurrentEnergy;

	// The maximum amount of health the player can have
	public int MaxHealth
	{
		get;
		private set;
	}

	// The initial maximum counter for the energy stat the player can have
	public int InitialMaxEnergy
	{
		get;
		private set;
	}

	// The maximum counter for the energy stat the player can have 
	public int MaxEnergy
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

	// ModifyEnergy is used to modify the player's current max energy stat
	public void ModifyEnergy ( int energyDelta )
	{
		// Change the current max energy
		// Ensure a max energy of at least 1
		MaxEnergy = Mathf.Max ( 1, MaxEnergy + energyDelta );

		// Check if the current energy exceeds the new max speed
		if ( CurrentEnergy > MaxEnergy )
		{
			// Change the current energy to reflect the change in max energy
			CurrentEnergy = Mathf.Max ( 1, CurrentEnergy + energyDelta );
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
