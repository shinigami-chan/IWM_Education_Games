using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Drifting : MonoBehaviour {
	public float speed = 0.035f;
	public float maxDistanceFromAnchor = 100f;


	public float distanceFromAnchor = 0f;
	public float width;

	public float x, y, z;
	public float wx, wy, wz;

	private Canvas canvas;

	public Vector2 stageDimensions;

	void Start()
	{
		updateCoords ();
		stageDimensions = new Vector2(Screen.width, Screen.height);

		canvas = GameObject.FindObjectOfType<Canvas> ().GetComponent<Canvas> ();

		// Scaled width
		width = transform.GetComponent<RectTransform> ().rect.width * canvas.scaleFactor; 
	}

	void Update()
	{
		Drift ();

		updateCoords ();
	}

	bool isCloudOnScreen() {

		if (x - (width / 2) > stageDimensions.x/* || x + (width / 2) < 0*/)
			return false;
		return true;
	}

	void updateCoords() {
		wx = transform.position.x;
		wy = transform.position.y;
		wz = transform.position.z;

		Vector3 position = Camera.main.WorldToScreenPoint (new Vector3 (wx, wy, wz));

		x = position.x;
		y = position.y;
		z = position.z;
	}

//	public static Vector3 GetWorldScale(Transform transform)
//	{
//		Vector3 worldScale = transform.localScale;
//		Transform parent = transform.parent;
//
//		while (parent != null)
//		{
//			worldScale = Vector3.Scale(worldScale,parent.localScale);
//			parent = parent.parent;
//		}
//
//		return worldScale;
//	}

	/// <summary>
	/// Takes World coordinates y and z and width to calculate the resetpoint 
	/// at the rightmost position at the left screen border that the cloud is 
	/// not visible
	/// </summary>
	/// <returns>The reset world point.</returns>
	/// <param name="width">Width.</param>
	/// <param name="y">The y coordinate.</param>
	/// <param name="z">The z coordinate.</param>
	private Vector3 CalculateResetWorldPoint(float width, float y, float z) {
		// Take Screen coordinates (half of width and copy the other coordinates)
		// and convert them to World Point

		Debug.Log ("y: " + y);
		return Camera.main.ScreenToWorldPoint (new Vector3 (-width / 2, y, z));
	}

	void Drift() {
		if (!isCloudOnScreen ())
			transform.position = CalculateResetWorldPoint (width, y, z);

		Vector3 newPosition = transform.position;

		newPosition.x += speed;

		transform.position = newPosition;
	}
}