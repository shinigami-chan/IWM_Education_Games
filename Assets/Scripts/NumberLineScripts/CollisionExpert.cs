using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CollisionExpert : MonoBehaviour {
	public Text randomNumberText;
	public Text minimumText;
	public Text maximumText;
	public Text resultText;
	
	public Rigidbody2D player;
	private Renderer rend;
	private ContactPoint2D contact;
	
	public GameObject explosion;
	public GameObject arrow;
	
	public float minimum;
	public float maximum;
	private float swap;
	public static float threshold;
	public int score;
	public int trials;
	
	public static int randomNumber;
	public static int difference;
	public static double outputContact;
	private float rightAmount;
	private float contactRight;
	private float contactFalse;
	private float amount;
	
	// Use this for initialization
	void Start () {

		score = PlayerPrefs.GetInt("Player Score expert");
		trials = PlayerPrefs.GetInt ("Trials");

		player = GetComponent<Rigidbody2D> ();
		rend = GetComponent<Renderer> ();
		
		explosion.SetActive (false);
		arrow.SetActive(false);

		minimum = (int) Random.Range (0,1000);
		maximum = (int) Random.Range (0,1000);

		if (minimum > maximum) {
			swap = minimum;
			minimum = maximum;
			maximum = swap;
		}

		threshold = (maximum - minimum) * 0.07f;
		
		
		//single number
		randomNumber = (int) Random.Range (minimum, maximum);
		randomNumberText.text = randomNumber.ToString ();
		
		
		minimumText.text = minimum.ToString ();
		maximumText.text = maximum.ToString ();
		
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
			Vector3 rightPosition = new Vector3 (contactRight + rendBegin, transform.position.y + 0.6f, 0.0f);
			Debug.Log("Umgewandelt: " + outputContact);

			float cast = (float)(randomNumber-outputContact);
			int difference = (int)Mathf.Abs(cast);
			resultText.text = "Dein Ergebnis: " + outputContact.ToString(("0.##"));
			
			//RandomNumber
			if(outputContact<randomNumber-threshold || outputContact>randomNumber+threshold){
				explosion.SetActive(true);
				arrow.SetActive(true);
				Destroy(explosion);
				Destroy(arrow);
				Instantiate(explosion, transform.position, transform.rotation);
				Instantiate(arrow, rightPosition, Quaternion.identity);
				calculateScore(difference);
				PlayerPrefs.SetInt ("Player Score expert", score);
				increaseTrials();
				StartCoroutine (WaitAndDisable (3f));
			}

			else{
				arrow.SetActive(true);
				Destroy(arrow);
				Instantiate(arrow, rightPosition, Quaternion.identity);
				score = score + 20 - (int)(difference/5);
				PlayerPrefs.SetInt ("Player Score expert", score);
				increaseTrials();
				StartCoroutine (WaitAndDisable (3f));
			}
		}

	}

	void calculateScore(int difference){
		if (difference > 500)
			score = score - difference / 20;
		else
			score = score - difference / 10;
	}

	void increaseTrials(){
		trials++;
		PlayerPrefs.SetInt("Trials", trials);
		Debug.Log("Trials " + trials);
	}

	IEnumerator WaitAndDisable(float waitTime){
		yield return new WaitForSeconds (waitTime);
		SceneManager.LoadScene(Application.loadedLevel);

	}
	
}

