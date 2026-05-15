using UnityEngine;

public class CellData
{
	// Whether or not the play can move to the cell in the board
	public bool IsPassable;

	// The object (if any) that is contained within the cell in the board
	public CellObject ContainedObject;
}