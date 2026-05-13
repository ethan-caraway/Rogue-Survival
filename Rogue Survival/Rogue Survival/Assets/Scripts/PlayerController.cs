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
		if ( moveAction.WasPressedThisFrame ( ) )
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

		// Store the starting position
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
			// Store the new cell coordinates
			cellPosition += moveInput;

			// Move the player to the new cell
			transform.position = board.GetCellPosition ( cellPosition );

			// Increment the turn
			mediator.Turn.Tick ( );
		}
	}
}
