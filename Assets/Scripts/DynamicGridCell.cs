using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DynamicGridCell : MonoBehaviour {
	// Public variables
	public float spacingRatio;
	public RectTransform parent;

	// Use this for initialization
	void Start () {
		//parent = transform.GetComponent<RectTransform> ();


		Debug.Log (parent.rect);

		GridLayoutGroup grid = transform.GetComponent<GridLayoutGroup> ();
		RectOffset pad = grid.padding;
		Vector2 spa = grid.spacing;

		int count = grid.constraintCount;

		Debug.Log (parent.rect.width);

		Debug.Log (count);

		float width = CalcSpacingAndWidth (count, parent.rect.width);
		float spacing = spacingRatio * width;

		grid.cellSize = new Vector2 (width, width);
		grid.spacing = new Vector2 (spacing, spacing);

		pad.top = (int) spacing;
		pad.bottom = (int) spacing;
		pad.left = (int) spacing;
		pad.right = (int) spacing;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private float CalcSpacingAndWidth(int count, float width) {
		return width/(count+((count+1)*spacingRatio));
	}
}
