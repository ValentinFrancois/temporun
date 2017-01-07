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
	public Transform plan;
	public Transform particles;
	Transform nouveau;
	Transform fils;
	int instruCheck;
	int debut;

	public static int posAleat=3;
	public static int oldPosAleat;

	private const float Y = -3f; 
	

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
			if (debut < 6){
				instru = debut;
				debut++;
			}
			else {
				instru = Random.Range(0,6);
				while (instru == instruCheck){
					instru = Random.Range(0,6);
				}
				instruCheck = instru;
			}
			sample = 16; 
			compteur = 0; 
			oldPosAleat = posAleat;
			posAleat = Random.Range(1,4);
			for (int y = 0; y < 4; y++) {
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
				switch (instru) {
					case 0:
					Instantiate (batterie_pf, new Vector3 (14,0,-1.5f), Quaternion.identity);
					if (GameObject.Find("Perso").GetComponent<PersoController>().clips[0]==null){
						BoxCollider box = nouveau.GetComponent<BoxCollider>();
						box.size = new Vector3(0.1654738f,1.273532f,1f);
						box.center = new Vector3(0.1567505f,0.1367659f,0);
					}
						break;
					case 1:
					Instantiate (piano_pf, new Vector3 (14,0,-1.5f), Quaternion.identity);
					if (GameObject.Find("Perso").GetComponent<PersoController>().clips[1]==null){
						BoxCollider box = nouveau.GetComponent<BoxCollider>();
						box.size = new Vector3(0.1654738f,1.273532f,1f);
						box.center = new Vector3(0.1567505f,0.1367659f,0);
					}
						break;
					case 2:
					Instantiate (basse_pf, new Vector3 (14,0,-1.5f), Quaternion.identity);
					if (GameObject.Find("Perso").GetComponent<PersoController>().clips[2]==null){
						BoxCollider box = nouveau.GetComponent<BoxCollider>();
						box.size = new Vector3(0.1654738f,1.273532f,1f);
						box.center = new Vector3(0.1567505f,0.1367659f,0);
					}
						break;
					case 3:
					Instantiate (guitare_pf, new Vector3 (14,0,-1.5f), Quaternion.identity);
					if (GameObject.Find("Perso").GetComponent<PersoController>().clips[3]==null){
						BoxCollider box = nouveau.GetComponent<BoxCollider>();
						box.size = new Vector3(0.1654738f,1.273532f,1f);
						box.center = new Vector3(0.1567505f,0.1367659f,0);
					}
						break;
					case 4:
					Instantiate (melodie_pf, new Vector3 (14,0,-1.5f), Quaternion.identity);
					if (GameObject.Find("Perso").GetComponent<PersoController>().clips[4]==null){
						BoxCollider box = nouveau.GetComponent<BoxCollider>();
						box.size = new Vector3(0.1654738f,1.273532f,1f);
						box.center = new Vector3(0.1567505f,0.1367659f,0);
					}
						break;
					case 5:
					Instantiate (synthe_pf, new Vector3 (14,0,-1.5f), Quaternion.identity);
					if (GameObject.Find("Perso").GetComponent<PersoController>().clips[5]==null){
						BoxCollider box = nouveau.GetComponent<BoxCollider>();
						box.size = new Vector3(0.1654738f,1.273532f,1f);
						box.center = new Vector3(0.1567505f,0.1367659f,0);
					}
						break;
				}
			}
			Quaternion rot = Quaternion.identity;
			rot.eulerAngles = new Vector3(-90,0,0);
			Instantiate(particles, new Vector3(16,Y,-2.5f), rot ,nouveau);
			Instantiate (detecteur, new Vector3 (16, 18, -2), Quaternion.identity);
			Instantiate (plan, new Vector3 (17f, 18, -2), Quaternion.identity);

		} 
		

	}
		
	void Start () {
			//instru = 0;
			instruCheck = 5;
			debut = 0;
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
		InvokeRepeating ("CreateNewfloor", 0.45f, 0.5f);
		
	}
		
}
