using UnityEngine;
using System.Collections;

public class PersoController : MonoBehaviour {
	//Music variables
	AudioSource Drum1; 
	AudioSource Drum2;
	AudioSource Piano; 
	AudioSource Basse; 
	AudioSource Guitare;
	AudioSource Melodie;
	private AudioSource[] tabad; 
	//Drum1
	public AudioClip D11; 
	public AudioClip D12;
	public AudioClip D13;
	public AudioClip D14;
	//Drum2
	public AudioClip D21;
	public AudioClip D22;
	public AudioClip D23;
	public AudioClip D24;
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
	public int instru = 0; //Permet de gerer les differente categorie d'intrument

	bool TouchSol = true; 

	int position; 
	Vector3 move = new Vector3();


	// Use this for initialization
	void Start () {
		//Definition des AudioSources
		tabad = GetComponents<AudioSource> ();
		Drum1 = tabad[0]; 
		Drum2 = tabad[1]; 
		Piano = tabad [2];
		Basse = tabad[3];
		Guitare = tabad[4]; 
		Melodie = tabad[5];

		//Lecture du clip initial
		Drum1.clip = D13;
		Drum1.Play (); 

		position = 0;
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
			//move.y = 0;
			transform.Translate(0,2,0);

		} 

	}
	void OnCollisionEnter(Collision c){
		TouchSol = true;
		instru = Random.Range (0, 5);
		if (c.gameObject.tag == "TRouge" || c.gameObject.tag == "TJaune" || c.gameObject.tag == "TBleu" || c.gameObject.tag == "TVert") {
			switch (instru) {

			case 0:
				if (c.gameObject.tag == "TRouge") {
					Drum1.clip = D11; 
				} else if (c.gameObject.tag == "TBleu") {
					Drum1.clip = D12; 
				} else if (c.gameObject.tag == "TVert") {
					Drum1.clip = D13; 
				} else if (c.gameObject.tag == "TJaune") {
					Drum1.clip = D14; 
				}
				Drum1.Play ();
				break; 

			case 1:
				if (c.gameObject.tag == "TRouge") {
					Drum2.clip = D21; 
				} else if (c.gameObject.tag == "TBleu") {
					Drum2.clip = D22; 
				} else if (c.gameObject.tag == "TVert") {
					Drum2.clip = D23; 
				} else if (c.gameObject.tag == "TJaune") {
					Drum2.clip = D24; 
				}
				Drum2.Play ();
				break; 

			case 2:
				if (c.gameObject.tag == "TRouge") {
					Piano.clip = P1; 
				} else if (c.gameObject.tag == "TBleu") {
					Piano.clip = P2; 
				} else if (c.gameObject.tag == "TVert") {
					Piano.clip = P3; 
				} else if (c.gameObject.tag == "TJaune") {
					Piano.clip = P4; 
				}
				Piano.Play ();
				break; 

			case 3:
				if (c.gameObject.tag == "TRouge") {
					Basse.clip = B1; 
				} else if (c.gameObject.tag == "TBleu") {
					Basse.clip = B2; 
				} else if (c.gameObject.tag == "TVert") {
					Basse.clip = B3; 
				} else if (c.gameObject.tag == "TJaune") {
					Basse.clip = B4; 
				}
				Basse.Play ();
				break; 

			case 4:
				if (c.gameObject.tag == "TRouge") {
					Guitare.clip = G1; 
				} else if (c.gameObject.tag == "TBleu") {
					Guitare.clip = G2; 
				} else if (c.gameObject.tag == "TVert") {
					Guitare.clip = G3; 
				} else if (c.gameObject.tag == "TJaune") {
					Guitare.clip = G4; 
				}
				Guitare.Play ();
				break; 

			case 5:
				if (c.gameObject.tag == "TRouge") {
					Melodie.clip = M1; 
				} else if (c.gameObject.tag == "TBleu") {
					Melodie.clip = M2; 
				} else if (c.gameObject.tag == "TVert") {
					Melodie.clip = M3; 
				} else if (c.gameObject.tag == "TJaune") {
					Melodie.clip = M4; 
				}
				Melodie.Play ();
				break; 

			}
		}
	}



		

	void OnCollisionExit(){
		TouchSol = false;
	}

}
