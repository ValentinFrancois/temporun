using UnityEngine;
using System.Collections;

public class PersoController : MonoBehaviour {

	bool TouchSol = true; 
	int position; 
	Vector3 move = new Vector3();
	// Use this for initialization
	void Start () {
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
				move.z = -1.76f;

				position = 0;


				break;
			case 2:
				move.z = -2.76f;
				position = 1;

				break;
			case 3:
				move.z = -3.76f;
				position = 2;

				break;
			default :
				break; 


			}
		}

		if (Input.GetKeyDown (KeyCode.DownArrow)) {
			switch (position) {

			case 0:
				move.z = -2.76f;
				position = 1; 
				break; 
			case 1:
				move.z = -3.76f;
				position = 2;
				break; 
			case 2:
				move.z = -4.76f;
				position = 3;

				break;
			case 3:
				break; 
			default :
				break;
			}
		}



		if (Input.GetKeyDown (KeyCode.Space)&& TouchSol == true) {
			move.y = 0; 

		}

		transform.position = move; 
	}
	void OnCollisionEnter(){
		TouchSol = true;
	}

	void OnCollisionExit(){
		TouchSol = false;
	}
}
