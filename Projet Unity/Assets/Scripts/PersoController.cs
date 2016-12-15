using UnityEngine;
using System.Collections;

public class PersoController : MonoBehaviour {
	Animator animator;
	//Music variables
	AudioSource Drum; 
	AudioSource Piano; 
	AudioSource Basse; 
	AudioSource Guitare;
	AudioSource Melodie;

	private AudioSource[] tabad; 

	//Drum
	public AudioClip D11; 
	public AudioClip D12;
	public AudioClip D13;
	public AudioClip D14;
	//Piano
	public AudioClip P1; 
	public AudioClip P2; 
	public AudioClip P3; 
	public AudioClip P4;
	//Basse 
	public AudioClip B1; 
	public AudioClip B2; 
	public AudioClip B3; 
	public AudioClip B4; 
	//Guitare
	public AudioClip G1; 
	public AudioClip G2; 
	public AudioClip G3; 
	public AudioClip G4; 
	//Melodie
	public AudioClip M1; 
	public AudioClip M2; 
	public AudioClip M3; 
	public AudioClip M4; 

	public GameObject solMan; 
	private bool TouchSol = true; 
	private bool Saut = false; //To know if the player just jumped when he arrive on the middle of gate. 


	int position; 
	public int instru = 0; 
	private int instruCheck = -2; 
	Vector3 move = new Vector3();
	public Transform vie; 
	static public int perso_vie; 
	private SpriteRenderer batterie;
	private SpriteRenderer piano;
	private SpriteRenderer basse;
	private SpriteRenderer guitare;
	private SpriteRenderer melodie;
	private SpriteRenderer synthe;

	// Use this for initialization
	void Start () {

		//Definition des AudioSources
		tabad = GetComponents<AudioSource> ();
		Drum = tabad[0]; 
		Piano = tabad [1];
		Basse = tabad[2];
		Guitare = tabad[3]; 
		Melodie = tabad[4];

		perso_vie = 3;
		position = 0;
		Instantiate (vie, new Vector3 (-6, -5, -5), Quaternion.Euler(20,0,0));
		Instantiate (vie, new Vector3 (-10, -5, -5),  Quaternion.Euler(20,0,0));
		Instantiate (vie, new Vector3 (-8, -5, -5),  Quaternion.Euler(20,0,0));

		batterie = GameObject.FindGameObjectWithTag("batterie").GetComponent<SpriteRenderer>();
		piano = GameObject.FindGameObjectWithTag("piano").GetComponent<SpriteRenderer>();
		basse = GameObject.FindGameObjectWithTag("basse").GetComponent<SpriteRenderer>();
		guitare = GameObject.FindGameObjectWithTag("guitare").GetComponent<SpriteRenderer>();
		melodie = GameObject.FindGameObjectWithTag("melodie").GetComponent<SpriteRenderer>();
		synthe = GameObject.FindGameObjectWithTag("Synthe").GetComponent<SpriteRenderer>();
	}


	// Update is called once per frame
	void Update () {
		move = transform.position;

		// Récupération des touches haut et bas
		if (Input.GetKeyDown (KeyCode.UpArrow)) {
			switch (position) {
			case 0:
				break;

			case 1:
				move.z = -1;
				position = 0;
				break;

			case 2:
				move.z = -2;
				position = 1;
				break;

			case 3:
				move.z = -3;
				position = 2;
				break;

			default :
				break; 

			}
		}


		if (Input.GetKeyDown (KeyCode.DownArrow)) {
			switch (position) {

			case 0:
				move.z = -2;
				position = 1; 
				break; 
			case 1:
				move.z = -3;
				position = 2;
				break; 
			case 2:
				move.z = -4;
				position = 3;

				break;
			case 3:
				break; 
			default :
				break;
			}
		}
		transform.position = move; 


		if (Input.GetKeyDown (KeyCode.Space) && TouchSol == true) {
			TouchSol = false; 
			Saut = true; 
			transform.Translate(0,2,0);


		} 

	}


	void OnCollisionEnter(Collision c){
		TouchSol = true;

		if (c.gameObject.tag == "TRouge" ||c.gameObject.tag == "TBleu" ||c.gameObject.tag == "TJaune" ||c.gameObject.tag == "TVert"){
			
			instru = SolManager.instru;
			if (instruCheck == instru) //To know if the sample is launch for the gate 
			{
				
			} 
			else 
			{
				
				instruCheck = instru;  
				if (Saut == false) {
					switch (instru) {

					case 0:
						if (c.gameObject.tag == "TRouge") {
							batterie.color = Color.red;
							Drum.clip = D11; 
						} else if (c.gameObject.tag == "TBleu") {
							batterie.color = Color.blue;
							Drum.clip = D12; 
						} else if (c.gameObject.tag == "TVert") {
							batterie.color = Color.green;
							Drum.clip = D13; 
						} else if (c.gameObject.tag == "TJaune") {
							batterie.color = Color.yellow;
							Drum.clip = D14; 
						}
						Drum.Play ();
						break; 

					case 1:
						if (c.gameObject.tag == "TRouge") {
							piano.color = Color.red;
							Piano.clip = P1; 
						} else if (c.gameObject.tag == "TBleu") {
							piano.color = Color.blue;
							Piano.clip = P2; 
						} else if (c.gameObject.tag == "TVert") {
							piano.color = Color.green;
							Piano.clip = P3; 
						} else if (c.gameObject.tag == "TJaune") {
							piano.color = Color.yellow;
							Piano.clip = P4; 
						}
						Piano.Play ();
						break; 

					case 2:
						if (c.gameObject.tag == "TRouge") {
							basse.color = Color.red;
							Basse.clip = B1; 
						} else if (c.gameObject.tag == "TBleu") {
							basse.color = Color.blue;
							Basse.clip = B2; 
						} else if (c.gameObject.tag == "TVert") {
							basse.color = Color.green;
							Basse.clip = B3; 
						} else if (c.gameObject.tag == "TJaune") {
							basse.color = Color.yellow;
							Basse.clip = B4; 
						}
						Basse.Play ();
						break; 

					case 3:
						if (c.gameObject.tag == "TRouge") {
							guitare.color = Color.red;
							Guitare.clip = G1; 
						} else if (c.gameObject.tag == "TBleu") {
							guitare.color = Color.blue;
							Guitare.clip = G2; 
						} else if (c.gameObject.tag == "TVert") {
							guitare.color = Color.green;
							Guitare.clip = G3; 
						} else if (c.gameObject.tag == "TJaune") {
							guitare.color = Color.yellow;
							Guitare.clip = G4; 
						}
						Guitare.Play ();
						break; 

					case 4:
						if (c.gameObject.tag == "TRouge") {
							melodie.color = Color.red;
							Melodie.clip = M1; 
						} else if (c.gameObject.tag == "TBleu") {
							melodie.color = Color.blue;
							Melodie.clip = M2; 
						} else if (c.gameObject.tag == "TVert") {
							melodie.color = Color.green;
							Melodie.clip = M3; 
						} else if (c.gameObject.tag == "TJaune") {
							melodie.color = Color.yellow;
							Melodie.clip = M4; 
						}
						Melodie.Play ();
						break; 

					}
				}
			}
			

		}
		Saut = false; 
	}


		

}
