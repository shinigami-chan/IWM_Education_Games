using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CollisionMedium : MonoBehaviour {
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
	public static float threshold;
	private int score;
	public int trials;

	public static int randomNumber;
	public static int difference;
	public static double outputContact;
	private float rightAmount;
	private float contactRight;
	private float contactFalse;
	private float amount;


	void Start () {

		score = PlayerPrefs.GetInt("Player Score medium");
		trials = PlayerPrefs.GetInt ("Trials");

		player = GetComponent<Rigidbody2D> ();
		rend = GetComponent<Renderer> ();

		explosion.SetActive (false);
		arrow.SetActive(false);

		threshold = 7f;
	
		minimum = 0f;
		maximum = 100f;
	
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
			Destroy(player);
			
	
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
			difference = (int)Mathf.Abs(cast);
			ResultText.text = "Dein Ergebnis: " + outputContact.ToString(("0.##"));

			//RandomNumber
			if(outputContact<randomNumber-threshold || outputContact>randomNumber+threshold){
				explosion.SetActive(true);
				arrow.SetActive(true);
				Destroy(explosion);
				Destroy(arrow);
				Instantiate(explosion, transform.position, transform.rotation);
				Instantiate(arrow, rightPosition, Quaternion.identity);
				score = score - difference;
				PlayerPrefs.SetInt ("Player Score medium", score);
				increaseTrials();
				StartCoroutine (WaitAndDisable (3f));

			}

			else{
				arrow.SetActive(true);
				Destroy(arrow);
				Instantiate(arrow, rightPosition, Quaternion.identity);
				score = score + 20 - difference;
				PlayerPrefs.SetInt ("Player Score medium", score);
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
		SceneManager.LoadScene(Application.loadedLevel);
	}

		

}
