using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ProjectileDragging : MonoBehaviour {

	//Dragging
	public float maxStretch = 1.3f;

	public float maxPath;
	public LineRenderer catapultLineLeft; 
	public LineRenderer catapultLineRight;
	public LineRenderer trajectory;
	//public float resetSpeed = 0.025f;  
	public Rigidbody2D player;



	private bool isDestroyed;
	private bool clickedOn;
	private SpringJoint2D spring;
	private Transform catapult;
	private Ray rayToMouse;
	private Ray leftCatapultToProjectile;
	private Ray pathToProjectile;
	private float maxStretchSqr;
	private float circleRadius;
	private Vector2 prevVelocity;
	RuntimePlatform platform;





	void Awake () {
		spring = GetComponent<SpringJoint2D> ();
		catapult = spring.connectedBody.transform;

	

	}



	void Start () {
	
		LineRendererSetup ();
		maxStretchSqr = maxStretch * maxStretch;
		
		spring = player.GetComponent <SpringJoint2D> ();
		leftCatapultToProjectile = new Ray (catapultLineLeft.transform.position, Vector3.zero);
		rayToMouse = new Ray (player.position, Vector3.zero);
	}
	
	// Update is called once per frame
	void Update () {

		if(platform == RuntimePlatform.Android || platform == RuntimePlatform.IPhonePlayer){
			if(Input.touchCount > 0) {
				if(Input.GetTouch(0).phase == TouchPhase.Began){
					clickedOn=true;
				}	
			}
			else{
				if(Input.GetTouch(0).phase ==  TouchPhase.Ended)
					clickedOn = false;
			}
		}

		if (clickedOn) 
			Dragging ();
			
		
		if (spring != null) {
			if(!GetComponent<Rigidbody2D>().isKinematic && prevVelocity.sqrMagnitude > GetComponent<Rigidbody2D>().velocity.sqrMagnitude){
				Destroy(spring);
		
				GetComponent<Rigidbody2D>().velocity = prevVelocity;



			}
			if(!clickedOn)
				prevVelocity = GetComponent<Rigidbody2D>().velocity;

			LineRendererUpdate();
			
		} 



		else {
			catapultLineLeft.enabled = false;
			catapultLineRight.enabled = false;
			trajectory.enabled = false;
		}
		
		
	}
	
	void LineRendererSetup (){
		catapultLineLeft.SetPosition (0, catapultLineLeft.transform.position);
		catapultLineRight.SetPosition (0, catapultLineRight.transform.position);
		trajectory.SetPosition (0, trajectory.transform.position);
		
		catapultLineLeft.sortingLayerName = "Scene";
		catapultLineRight.sortingLayerName = "Scene";
		trajectory.sortingLayerName = "Scene";
		
		catapultLineLeft.sortingOrder = 2;
		catapultLineRight.sortingOrder = 2;
		trajectory.sortingOrder = 2;
		//Application.CaptureScreenshot ("Screenshot" + i +".jpeg");
	}

	void OnMouseDown (){
		spring.enabled = false;
		clickedOn = true;
	}
	
	void OnMouseUp (){
		spring.enabled = true;
		GetComponent<Rigidbody2D>().isKinematic = false;
		clickedOn = false;
	}
	
	
	void Dragging (){ 
		Vector3 mouseWorldPoint = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		Vector2 catapultToMouse = mouseWorldPoint - catapult.position;

		if (catapultToMouse.sqrMagnitude > maxStretchSqr) {
			rayToMouse.direction = catapultToMouse;
			mouseWorldPoint = rayToMouse.GetPoint (maxStretch);
		}

		mouseWorldPoint.z = 0f;
		transform.position = mouseWorldPoint;
	
	}

	void LineRendererUpdate(){
		
		Vector2 catapultToProjectile = transform.position - catapultLineLeft.transform.position;
		leftCatapultToProjectile.direction = catapultToProjectile;



		Vector3 holdPoint = leftCatapultToProjectile.GetPoint (catapultToProjectile.magnitude+ 0.03f);
		catapultLineLeft.SetPosition (1, holdPoint);
		catapultLineRight.SetPosition (1, holdPoint);
		trajectory.SetPosition (1, holdPoint);
	
	}



}


