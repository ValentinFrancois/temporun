using UnityEngine;
using System.Collections;

public class PersoController : MonoBehaviour {
	//Music variables
	AudioSource Drum1; 
	AudioSource Drum2;
	AudioSource Piano; 
	AudioSource Basse; 
	AudioSource Melodie1;
	AudioSource Melodie2;
	private AudioSource[] tabad; 
	public AudioClip D11; 
	public AudioClip D12;
	public AudioClip D13;
	public AudioClip P1; 
	public AudioClip M1; 

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
		Melodie1 = tabad[4]; 
		Melodie2 = tabad[5];

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
		if (c.gameObject.tag == "TJaune" || c.gameObject.tag == "TRouge" || c.gameObject.tag == "TBleu" || c.gameObject.tag == "TVert") {
			Melodie1.clip = M1; 
			Melodie1.Play ();

		}


	}
		

	void OnCollisionExit(){
		TouchSol = false;
	}

}
