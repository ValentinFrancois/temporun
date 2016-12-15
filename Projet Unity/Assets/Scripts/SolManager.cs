using UnityEngine;
using System.Collections;

public class SolManager : MonoBehaviour {
	private int sample; 
	private int compteur; 
	public static int instru; 
	public Transform bleu;
	public Transform rouge;
	public Transform vert;
	public Transform jaune; 
	public Transform bleuC;
	public Transform rougeC;
	public Transform vertC;
	public Transform jauneC; 

	int posAleat=3;

	private const int Y = -3; 
	// Use this for initialization
	void Start () {

		for (int y = 0; y < 4; y++) {

			for (int i = -14; i <= 16; i = i + 2) {
				switch(y){
				case 0:
					Instantiate (rouge, new Vector3 (i, Y, -(1+posAleat)%4-1), Quaternion.identity);
					break;
				case 1:
					Instantiate (bleu, new Vector3 (i, Y, -(2+posAleat)%4-1), Quaternion.identity);
					break;
				case 2:
					Instantiate (vert, new Vector3 (i, Y, -(3+posAleat)%4-1), Quaternion.identity);
					break;
				case 3:
					Instantiate (jaune, new Vector3 (i, Y, -(4+posAleat)%4-1), Quaternion.identity);
					break;
				}

			}

		}


		sample = 0;
		compteur = 0; 
		InvokeRepeating ("CreateNewfloor", 0.46f, 0.5f);
	}

	void CreateNewfloor(){
		if (compteur < sample-1) {

			for (int y = 0; y < 4; y++) {


				switch (y) {
				case 0:
					Instantiate (rouge, new Vector3 (16, Y, -(1+posAleat)%4-1), Quaternion.identity);
					break;
				case 1:
					Instantiate (bleu, new Vector3 (16, Y, -(2+posAleat)%4-1), Quaternion.identity);
					break;
				case 2:
					Instantiate (vert, new Vector3 (16, Y, -(3+posAleat)%4-1), Quaternion.identity);
					break;
				case 3:
					Instantiate (jaune, new Vector3 (16, Y, -(4+posAleat)%4-1), Quaternion.identity);
					break;
				}
			}
			compteur++;
		}
		else{

			sample = 16; 
			compteur = 0; 
			posAleat = Random.Range(1,4);
			for (int y = 0; y < 4; y++) {


				switch (y) {
				case 0:
					Instantiate (rougeC, new Vector3 (16, Y, -(1+posAleat)%4-1), Quaternion.identity);
					break;
				case 1:
					Instantiate (bleuC, new Vector3 (16, Y, -(2+posAleat)%4-1), Quaternion.identity);
					break;
				case 2:
					Instantiate (vertC, new Vector3 (16, Y, -(3+posAleat)%4-1), Quaternion.identity);
					break;
				case 3:
					Instantiate (jauneC, new Vector3 (16, Y, -(4+posAleat)%4-1), Quaternion.identity);
					break;
				}
			}
			if (instru < 4)
				instru++;
			else
				instru = 0; 
		} 
		

	}
		
}
