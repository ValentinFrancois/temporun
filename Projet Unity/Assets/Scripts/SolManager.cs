using UnityEngine;
using System.Collections;

public class SolManager : MonoBehaviour {
	private int sample; 
	public static int compteur; 
	public static int instru; 
	public Transform bleu;
	public Transform rouge;
	public Transform vert;
	public Transform jaune; 
	public Transform bleuC;
	public Transform rougeC;
	public Transform vertC;
	public Transform jauneC; 
	public Transform batterie_pf;
	public Transform piano_pf;
	public Transform basse_pf;
	public Transform guitare_pf;
	public Transform melodie_pf;
	public Transform synthe_pf;
	public Transform detecteur;
	
	Transform nouveau;
	Transform fils;
	
	float compteurTime;
	float currentTime = 0;

	int posAleat=3;

	private const int Y = -3; 
	// Use this for initialization
	
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

				switch (instru) {
					case 5:
					Instantiate (batterie_pf, new Vector3 (14,0,-2), Quaternion.identity);
						break;
					case 0:
					Instantiate (piano_pf, new Vector3 (14,0,-2), Quaternion.identity);
						break;
					case 1:
					Instantiate (basse_pf, new Vector3 (14,0,-2), Quaternion.identity);
						break;
					case 2:
					Instantiate (guitare_pf, new Vector3 (14,0,-2), Quaternion.identity);
						break;
					case 3:
					Instantiate (melodie_pf, new Vector3 (14,0,-2), Quaternion.identity);
						break;
					case 4:
					Instantiate (synthe_pf, new Vector3 (14,0,-2), Quaternion.identity);
						break;
				}
				switch (y) {
				case 0:
					nouveau = Instantiate (rougeC, new Vector3 (16, Y, -(1+posAleat)%4-1), Quaternion.identity) as Transform;
					nouveau.GetChild(1).GetComponent<ParticleSystemRenderer>().sortingOrder = ((1+posAleat)%4+1)*2;
					break;
				case 1:
					nouveau = Instantiate (bleuC, new Vector3 (16, Y, -(2+posAleat)%4-1), Quaternion.identity) as Transform;
					nouveau.GetChild(1).GetComponent<ParticleSystemRenderer>().sortingOrder = ((2+posAleat)%4+1)*2;
					break;
				case 2:
					nouveau = Instantiate (vertC, new Vector3 (16, Y, -(3+posAleat)%4-1), Quaternion.identity) as Transform;
					nouveau.GetChild(1).GetComponent<ParticleSystemRenderer>().sortingOrder = ((3+posAleat)%4+1)*2;
					break;
				case 3:
					nouveau = Instantiate (jauneC, new Vector3 (16, Y, -(4+posAleat)%4-1), Quaternion.identity) as Transform;
					nouveau.GetChild(1).GetComponent<ParticleSystemRenderer>().sortingOrder = ((4+posAleat)%4+1)*2;
					break;
				}
				Instantiate (detecteur, new Vector3 (16, 18, -2), Quaternion.identity);
			}
			if (instru < 4)
				instru++;
			else
				instru = 0; 
		} 
		

	}
	
	/*
	IEnumerator SpawnAtIntervals()
	{
		while(true){
			yield return new WaitForSeconds(0.5f);
			CreateNewfloor();
		}
		
	}
	
	void Awake(){
		StartCoroutine(SpawnAtIntervals());
	}*/
		
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
		
}
