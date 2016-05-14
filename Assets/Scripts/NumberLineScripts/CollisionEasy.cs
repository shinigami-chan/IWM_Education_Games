using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;

public class CollisionEasy : MonoBehaviour {
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
	private int sum1;
	private int sum2;
	public static int randomNumber;
	public static int score;
	public int trials;
	public static double[] resultArr;

	public static int difference;
	public static double outputContact;
	private float rightAmount;
	private float contactRight;
	private float contactFalse;
	private float amount;
	public StreamWriter sr;
	
	// Use this for initialization
	void Start () {

		score = PlayerPrefs.GetInt("Player Score easy");
		trials = PlayerPrefs.GetInt ("Trials");
		player = GetComponent<Rigidbody2D> ();
		rend = GetComponent<Renderer> ();
		
		explosion.SetActive (false);
		arrow.SetActive(false);

		/*
		minimum = 0f;
		maximum = 1000f;
		threshold = 100;
		

		
		mult1 = Random.Range ((int)minimum +1 ,(int)maximum/10);
		mult2 = Random.Range ((int)minimum +1 ,(int)maximum/100);
		res = mult1 * mult2;
		randomNumberText.text = mult1 + " * " + mult2;
		
		minimumText.text = minimum.ToString ();
		maximumText.text = maximum.ToString ();
		randomNumberText.gameObject.active =true;
		StartCoroutine(GuiDisplayTimer());
		*/

		minimum = 0f;
		maximum = 20f;
		threshold = 1.4f;

		sum1 = Random.Range ((int)minimum +1 ,(int)maximum/2);
		sum2 = Random.Range ((int)minimum +1 ,(int)maximum/2);
		randomNumber = sum1 + sum2;
		
		minimumText.text = minimum.ToString ();
		maximumText.text = maximum.ToString ();
		randomNumberText.text = sum1 + " + " + sum2;

	}




	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.tag == "Mast") {

			player.transform.parent = other.transform;
			player.GetComponent<Rigidbody2D> ().isKinematic = true;
			
			
			rend = other.gameObject.GetComponent<Renderer> ();
			float width = rend.bounds.size.x;
			float rendBegin = rend.bounds.min.x;
			
			
			ContactPoint2D contact = other.contacts [0];
			Vector3 pos = contact.point;
			
			contactFalse = width / 2 + pos.x;
			amount = contactFalse / width;
			outputContact = (amount * (maximum - minimum) + minimum);
			rightAmount = (randomNumber - minimum) / (maximum - minimum);
			contactRight = width * rightAmount;
			Vector3 rightPosition = new Vector3 (contactRight + rendBegin, transform.position.y + 0.6f, 0.0f);
			Debug.Log ("Umgewandelt: " + outputContact);
			saveOutput();



			float cast = (float)(randomNumber-outputContact);
			difference = (int)Mathf.Abs(cast);
			resultText.text = "Dein Ergebnis: " + outputContact.ToString(("0.##"));


			if (outputContact < randomNumber - threshold || outputContact > randomNumber + threshold) {
				explosion.SetActive (true);
				arrow.SetActive (true);
				Destroy (explosion);
				Destroy (arrow);
				Instantiate (explosion, transform.position, transform.rotation);
				Instantiate (arrow, rightPosition, Quaternion.identity);
				score = score - difference;
				PlayerPrefs.SetInt ("Player Score easy", score);
				Debug.Log(score);
				increaseTrials();
				StartCoroutine (WaitAndDisable (3f));

			} else {
				arrow.SetActive (true);
				Destroy (arrow);
				Instantiate (arrow, rightPosition, Quaternion.identity);
				score = score + 10 - difference;
				PlayerPrefs.SetInt ("Player Score easy", score);
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

	void saveOutput(){
		StreamWriter sr = new StreamWriter (Application.persistentDataPath + @"\DataEasy.csv", true);
		sr.WriteLine (randomNumber + " " + outputContact);
		sr.Close();
	}

	IEnumerator WaitAndDisable(float waitTime){
		yield return new WaitForSeconds (waitTime);
		Application.LoadLevel (Application.loadedLevel);
	}
	

}

