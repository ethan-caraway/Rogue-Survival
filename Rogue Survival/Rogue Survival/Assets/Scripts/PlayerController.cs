using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
	// The list of animation states for the player
	private enum PlayerState
	{
		IDLE,
		MOVE,
		ATTACK
	}

	// The name of the move action
	private const string MOVE_ACTION = "Move";

	// The name of the IsMoving animation parameter
	private const string MOVE_ANIM_PARAMETER = "IsMoving";

	// The name of the Attack animation parameter
	private const string ATTACK_ANIM_PARAMETER = "Attack";

	// The mediator for the game
	[SerializeField]
	private GameMediator mediator;

	// The sprite renderer for the player
	[SerializeField]
	private SpriteRenderer spriteRenderer;

	// The PlayerInput component
	[SerializeField]
	private PlayerInput playerInput;

	// The Animator component
	[SerializeField]
	private Animator animator;

	// The speed at which the player moves
	[SerializeField]
	private float moveSpeed;

	// The stats for the player
	public static PlayerData Stats;

	// The controller for the board
	private BoardController board;

	// The current cell coordinates
	private Vector2Int cellPosition;

	// The move action
	private InputAction moveAction;

	// The animation state the player is currently in
	private PlayerState state = PlayerState.IDLE;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start ( )
	{
		// Store move action
		moveAction = playerInput.actions [ MOVE_ACTION ];
	}

	// Update is called once per frame
	void Update ( )
	{
		// Check the current state
		switch ( state )
		{
			case PlayerState.IDLE:
				// Check for on key down
				if ( mediator.IsGameRunning && moveAction.WasPressedThisFrame ( ) )
				{
					// Move the player
					Move ( moveAction.ReadValue<Vector2> ( ) );
				}
				break;

			case PlayerState.MOVE:
				// Move the player toward the cell
				AnimateMove ( );
				break;
		}
	}

	// Spawn is called to position the player initially on the board
	public void Spawn ( BoardController boardController, Vector2Int cell )
	{
		// Store the reference to the board
		board = boardController;

		// Move the player to the starting coordinates
		MoveToCell ( cell, true );
	}

	// OnAttackComplete is called when the attack animation completes
	public void OnAttackComplete ( )
	{
		// Reset the player state
		state = PlayerState.IDLE;

		// End the player's current turn
		EndTurn ( );
	}

	// MoveToCell is called to position the player to a given cell
	private void MoveToCell ( Vector2Int cell, bool isImmediate )
	{
		// Store the cell coordinates
		cellPosition = cell;

		// Check for moving to the cell immediately
		if ( isImmediate )
		{
			// Position player to the cell
			transform.position = board.GetCellPosition ( cellPosition );
		}
		else
		{
			// Set the player's state to moving
			state = PlayerState.MOVE;

			// Trigger the walk animation
			animator.SetBool ( MOVE_ANIM_PARAMETER, true );
		}
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
			// Check for moving to the right
			if ( moveInput.x > 0 )
			{
				// Face the sprite to the right
				spriteRenderer.flipX = false;
			}
			// Check for moving to the left
			else if ( moveInput.x < 0 )
			{
				// Face the sprite to the left
				spriteRenderer.flipX = true;
			}

			// Check for an object in the cell
			if ( cellData.ContainedObject == null )
			{
				// Move the player to the new cell
				MoveToCell ( cellPosition + moveInput, false );
			}
			// Check if the player is allowed to move to the cell with an object
			else if ( cellData.ContainedObject.CanPlayerEnter ( ) )
			{
				// Move the player to the new cell
				MoveToCell ( cellPosition + moveInput, false );
			}
			else
			{
				// Mark that the player is attacking the cell object
				state = PlayerState.ATTACK;

				// Play the attack animation
				animator.SetTrigger ( ATTACK_ANIM_PARAMETER );
			}
		}
	}

	// AnimateMove is called to animate the player walking toward a cell
	private void AnimateMove ( )
	{
		// Get the target position
		Vector3 targetPosition = board.GetCellPosition ( cellPosition );

		// Move the player toward the cell
		transform.position = Vector3.MoveTowards ( transform.position, targetPosition, moveSpeed * Time.deltaTime );

		// Check if the player has reached the cell
		if ( transform.position == targetPosition )
		{
			// Mark that the player is no longer moving
			state = PlayerState.IDLE;

			// End the walk animation
			animator.SetBool ( MOVE_ANIM_PARAMETER, false );

			// Get the data for the target cell to move to
			CellData cellData = board.GetCellData ( cellPosition );

			// Check for an object in the cell
			if ( cellData.ContainedObject != null )
			{
				// Trigger the player entering the cell of the object
				cellData.ContainedObject.PlayerEntered ( );
			}

			// End the turn after the animation has completed
			EndTurn ( );
		}
	}

	// ConsumeEnergy is called each move to consume some of the player's current energy
	private void ConsumeEnergy ( )
	{
		// Decrement the player's current energy
		Stats.CurrentEnergy--;

		// Check if all of the player's current energy as been consumed
		if ( Stats.CurrentEnergy <= 0 )
		{
			// Reset the player's current energy
			Stats.CurrentEnergy = Stats.MaxEnergy;

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

	// EndTurn is called to end the player's current turn
	private void EndTurn ( )
	{
		// Use up some energy for the move
		ConsumeEnergy ( );

		// Increment the turn
		mediator.Turn.Tick ( );
	}
}
