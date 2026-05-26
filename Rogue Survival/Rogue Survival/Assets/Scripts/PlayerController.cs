using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
	// The name of the move action
	private const string MOVE_ACTION = "Move";

	// The mediator for the game
	[SerializeField]
	private GameMediator mediator;

	// The PlayerInput component
	[SerializeField]
	private PlayerInput playerInput;

	// The stats for the player
	public static PlayerData Stats;

	// The controller for the board
	private BoardController board;

	// The current cell coordinates
	private Vector2Int cellPosition;

	// The move action
	private InputAction moveAction;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
		// Store move action
		moveAction = playerInput.actions [ MOVE_ACTION ];
    }

    // Update is called once per frame
    void Update()
    {
		// Check for on key down
		if ( mediator.IsGameRunning && moveAction.WasPressedThisFrame ( ) )
		{
			// Move the player
			Move ( moveAction.ReadValue<Vector2> ( ) );
		}
    }

	// Spawn is called to position the player initially on the board
	public void Spawn ( BoardController boardController, Vector2Int cell )
	{
		// Store the reference to the board
		board = boardController;

		// Move the player to the starting coordinates
		MoveToCell ( cell );
	}

	// MoveToCell is called to position the player to a given cell
	private void MoveToCell ( Vector2Int cell )
	{
		// Store the cell coordinates
		cellPosition = cell;

		// Position player to the cell
		transform.position = board.GetCellPosition ( cellPosition );
	}

	// Move is call when the Move action is triggered on key press
	private void Move ( Vector2 input )
	{
		// Get input values and convert them to an integer vector
		Vector2Int moveInput = Vector2Int.RoundToInt ( input );

		// Get the data for the target cell to move to
		CellData cellData = board.GetCellData ( cellPosition + moveInput );

		// Check if the cell can be moved to
		if ( cellData != null && cellData.IsPassable )
		{
			// Check for an object in the cell
			if ( cellData.ContainedObject == null )
			{
				// Move the player to the new cell
				MoveToCell ( cellPosition + moveInput );
			}
			// Check if the player is allowed to move to the cell with an object
			else if ( cellData.ContainedObject.CanPlayerEnter ( ) )
			{
				// Move the player to the new cell
				MoveToCell ( cellPosition + moveInput );

				// Trigger the player entering the cell of the object
				cellData.ContainedObject.PlayerEntered ( );
			}

			// Use up some speed energy for the move
			ConsumeSpeed ( );

			// Increment the turn
			mediator.Turn.Tick ( );
		}
	}

	// ConsumeSpeed is called each move to consume some of the player's current speed energy
	private void ConsumeSpeed ( )
	{
		// Decrement the player's current speed energy
		Stats.CurrentSpeed--;

		// Check if all of the player's current speed energy as been consumed
		if ( Stats.CurrentSpeed <= 0 )
		{
			// Reset the player's current speed energy
			Stats.CurrentSpeed = Stats.MaxSpeed;

			// Decrement the player's current health
			Stats.CurrentHealth--;
		}

		// Ensure the player's current health is within the correct range
		Stats.CurrentHealth = Mathf.Clamp ( Stats.CurrentHealth, 0, Stats.MaxHealth );

		// Check for remaining health
		CheckHealth ( );
		
	}

	// CheckHealth is called each turn to check if the player still has health
	private void CheckHealth ( )
	{
		// Check for no remaining health
		if ( Stats.CurrentHealth <= 0 )
		{
			// Mark the game as over
			mediator.Turn.IsGameOver = true;

			// Delete the player
			Destroy ( gameObject );
		}
	}
}
