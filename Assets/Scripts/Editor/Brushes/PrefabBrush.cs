using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "Prefab brush", menuName = "Brushes/Prefab brush")]
[CustomGridBrush(false, true, false, "Prefab Brush")]
public class PrefabBrush : GridBrushBase
{
	public GameObject prefab;
	public int zPosition = 0;

	private GameObject previousBrushTarget;
	private Vector3Int previousPosition;

	public override void Paint(GridLayout gridLayout, GameObject brushTarget, Vector3Int position)
	{
		//Check to see if a tile is already placed in that specific cell
		Transform itemInCell = GetObjectInCell(gridLayout, brushTarget.transform, new Vector3Int(position.x, position.y, zPosition));
		if(itemInCell)
		{ return; }
		if(position == previousPosition)
		{ return; }

		previousPosition = position;
		if(brushTarget)
		{ previousBrushTarget = brushTarget; }
		brushTarget = previousBrushTarget;

		//Don´t allow editing palettes.
		if(brushTarget.layer == 31)
		{ return; }

		GameObject instance = (GameObject)PrefabUtility.InstantiatePrefab(prefab);
		if(instance)
		{
			Undo.MoveGameObjectToScene(instance, brushTarget.scene, "Paint prefab");
			Undo.RegisterCreatedObjectUndo(instance, "Paint prefab");
			instance.transform.SetParent(brushTarget.transform);
			instance.transform.position = gridLayout.LocalToWorld(gridLayout.CellToLocalInterpolated(new Vector3(position.x, position.y, zPosition) + (Vector3.one * 0.5f)));
		}
	}

	public override void Erase(GridLayout gridLayout, GameObject brushTarget, Vector3Int position)
	{
		if(brushTarget)
		{ previousBrushTarget = brushTarget; }
		brushTarget = previousBrushTarget;

		//Don´t allow editing palettes.
		if (brushTarget.layer == 31)
		{ return; }

		Transform ereased = GetObjectInCell(gridLayout, brushTarget.transform, new Vector3Int(position.x, position.y, zPosition));
		if(ereased)
		{ Undo.DestroyObjectImmediate(ereased.gameObject); }
	}

	private static Transform GetObjectInCell(GridLayout gridlayout, Transform parent, Vector3Int position)
	{
		int childCount = parent.childCount;
		Vector3 min = gridlayout.LocalToWorld(gridlayout.CellToLocalInterpolated(position));
		Vector3 max = gridlayout.LocalToWorld(gridlayout.CellToLocalInterpolated(position + Vector3Int.one));
		Bounds bounds = new Bounds((max + min) * 0.5f, max - min); //Bounds of a tile

		//Checks child objects position against bounds of tile.
		/* Loops:
		 * for
		 * foreach
		 * while
		 * do...while
		 */

		for(int i = 0; i < childCount; i++)
		{
			Transform child = parent.GetChild(i);
			if(bounds.Contains(child.position))
			{
				return child;
			}
		}

		return null;
	}
}
