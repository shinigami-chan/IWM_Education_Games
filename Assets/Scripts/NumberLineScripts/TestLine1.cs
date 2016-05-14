using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TestLine1 : MonoBehaviour {
	public Text randomNumberText;
	public Text minimumText;
	public Text maximumText;
	public Text ResultText;
	
	public Rigidbody2D player;
	private Renderer rend;
	private ContactPoint2D contact;
	
	public GameObject explosion;
	public GameObject arrow;
	
	public float minimum;
	public float maximum;
	private float swap;
	private float threshold;
	private int score;
	public int trials;
	
	private int randomNumber;
	private double outputContact;
	private float rightAmount;
	private float contactRight;
	private float contactFalse;
	private float amount;
	public int[] target;
	
	
	void Start () {


		trials = PlayerPrefs.GetInt ("Trials");
		player = GetComponent<Rigidbody2D> ();
		rend = GetComponent<Renderer> ();
		
		explosion.SetActive (false);
		arrow.SetActive(false);
		
		threshold = 7f;
		
		minimum = 0f;
		maximum = 100f;

		int[] target = {23, 10, 76, 81, 33, 45, 7, 18, 87, 57, 91, 
						42, 71, 60, 13, 3, 19, 37, 51, 97, 31, 14,
						84, 49, 62, 6, 14, 26, 30, 95};
		

	
		
		minimumText.text = minimum.ToString ();
		maximumText.text = maximum.ToString ();
		randomNumber = target [trials];
		randomNumberText.text = randomNumber.ToString();
	
		if (trials == 30) {
			Application.Quit();
		}

		
	}
	
	
	
	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.tag == "Mast") {
			player.transform.parent=other.transform;
			player.GetComponent<Rigidbody2D>().isKinematic = true;
			
			
			rend= other.gameObject.GetComponent<Renderer>();
			float width = rend.bounds.size.x;
			float rendBegin = rend.bounds.min.x;
			
			
			ContactPoint2D contact = other.contacts[0];
			Vector3 pos = contact.point;
			
			
			contactFalse = width/2 + pos.x;
			amount  = contactFalse /width;
			outputContact = (amount*(maximum - minimum)+ minimum);
			rightAmount = (randomNumber-minimum)/(maximum - minimum);
			contactRight = width * rightAmount;
			Vector3 rightPosition = new Vector3 (contactRight + rendBegin, transform.position.y + 0.84f, 0.0f);
			Debug.Log("Umgewandelt: " + outputContact);
			

			float cast = (float)(randomNumber-outputContact);
			int difference = (int)Mathf.Abs(cast);
			ResultText.text = "Dein Ergebnis: " + outputContact;
			
			//RandomNumber
			if(outputContact<randomNumber-threshold || outputContact>randomNumber+threshold){
				explosion.SetActive(true);
				arrow.SetActive(true);
				Destroy(explosion);
				Destroy(arrow);
				Instantiate(explosion, transform.position, transform.rotation);
				Instantiate(arrow, rightPosition, Quaternion.identity);
				score= score - difference;
				Debug.Log(score);
				PlayerPrefs.SetInt("Player Score easy", score);
				increaseTrials();
				StartCoroutine (WaitAndDisable (3f));
				
			}
			
			else{
				arrow.SetActive(true);
				Destroy(arrow);
				Instantiate(arrow, rightPosition, Quaternion.identity);
				score= score + 20;
				Debug.Log(score);
				PlayerPrefs.SetInt("Player Score easy", score);
				increaseTrials();
				StartCoroutine (WaitAndDisable (3f));
			}
			
		}
		
	}
	
	void increaseTrials(){
		trials++;
		PlayerPrefs.SetInt("Trials", trials);
		Debug.Log("Trials " + trials);
	}
	
	IEnumerator WaitAndDisable(float waitTime){
		yield return new WaitForSeconds (waitTime);
		Application.LoadLevel (Application.loadedLevel);
	}
	
	
	
}
