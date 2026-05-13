using UnityEngine;

public class GameMediator : MonoBehaviour
{
	// The controller for the board
	[SerializeField]
	private BoardController board;

	// The controller for the player
	[SerializeField]
	private PlayerController player;

	// The controller for the HUD
	[SerializeField]
	private HudController hud;

	// The controller for the turns
	public TurnController Turn
	{
		get;
		private set;
	}

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
		// Create the turn controller
		Turn = new TurnController ( );

		// Create the board
		board.Init ( );

		// Place the player in its starting cell in the bottom left corner
		player.Spawn ( board, new Vector2Int ( 1, 1 ) );

		// Initialize the HUD
		hud.Init ( );
	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
