using UnityEngine;
using System.Collections;

public class SolManager : MonoBehaviour {
	public int sample; 
	public int compteur; 
	public Transform bleu;
	public Transform rouge;
	public Transform vert;
	public Transform jaune; 
	public Transform bleuC;
	public Transform rougeC;
	public Transform vertC;
	public Transform jauneC; 


	public const int Y = -3; 
	// Use this for initialization
	void Start () {
		for (int y = 0; y < 4; y++) {

			for (int i = -8; i <= 52; i = i + 4) {
				switch(y){
				case 0:
					Instantiate (rouge, new Vector3 (i, Y, -1), Quaternion.identity);
					break;
				case 1:
					Instantiate (bleu, new Vector3 (i, Y, -2), Quaternion.identity);
					break;
				case 2:
					Instantiate (vert, new Vector3 (i, Y, -3), Quaternion.identity);
					break;
				case 3:
					Instantiate (jaune, new Vector3 (i, Y, -4), Quaternion.identity);
					break;
				}

			}

		}

		sample = Random.Range (1, 6) * 8;
		compteur = 0; 
		InvokeRepeating ("CreateNewfloor", 0.81f, 0.81f);
	}

	void CreateNewfloor(){
		if (compteur < sample) {

			for (int y = 0; y < 4; y++) {


				switch (y) {
				case 0:
					Instantiate (rouge, new Vector3 (52, Y, -1), Quaternion.identity);
					break;
				case 1:
					Instantiate (bleu, new Vector3 (52, Y, -2), Quaternion.identity);
					break;
				case 2:
					Instantiate (vert, new Vector3 (52, Y, -3), Quaternion.identity);
					break;
				case 3:
					Instantiate (jaune, new Vector3 (52, Y, -4), Quaternion.identity);
					break;
				}
			}
			compteur++;
		}
		else{
				sample = Random.Range (1, 6) * 8;
				compteur = 0; 
				for (int y = 0; y < 4; y++) {


					switch (y) {
					case 0:
						Instantiate (rougeC, new Vector3 (52, Y, -1), Quaternion.identity);
						break;
					case 1:
						Instantiate (bleuC, new Vector3 (52, Y, -2), Quaternion.identity);
						break;
					case 2:
						Instantiate (vertC, new Vector3 (52, Y, -3), Quaternion.identity);
						break;
					case 3:
						Instantiate (jauneC, new Vector3 (52, Y, -4), Quaternion.identity);
						break;
				}
			}
		} 
		

	}
		
}
