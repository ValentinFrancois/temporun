using UnityEngine;
using System.Collections;

public class spawn : MonoBehaviour {
    
	public GameObject Comete;
    public GameObject Fusée;
    public GameObject Mine;
	public GameObject Vie;
	public GameObject FuséeTordue;
    public float difficulte = 1f;
	public float temps = 0;
	public float compteurMine;
	public float compteurFusee;
	public float compteurComete;
	public float constante = 1f;
	int random;
	GameObject comete;
	public int pos1, pos2, pos3, pos4;
	int compt=0;
   // private GameObject FloorManager;

    void Start () {

	   compteurComete = Random.Range(6,18);
	   compteurFusee = Random.Range(2,7);
	   compteurMine = Random.Range(1,6);

	}

	void Update(){
		temps += Time.deltaTime;
		if (temps>=8*constante && difficulte > 0.2f){
			difficulte-=0.05f;
			constante+=constante*constante/10;
			if (constante>15){constante=15;}
		}
		if (temps > compteurComete){
			if (EntreeEnnemis.tableauEnnemis[0]==0 || EntreeEnnemis.tableauEnnemis[1]==0 || EntreeEnnemis.tableauEnnemis[2]==0 || EntreeEnnemis.tableauEnnemis[3]==0){
				pos1 = Random.Range(0,4);
				if (EntreeEnnemis.tableauEnnemis[pos1]==0){
					comete = Instantiate (Comete);
					comete.transform.position = new Vector3(8,10,-pos1-1);
				}
				else {
					pos2 = Random.Range(0,4);
					while (pos2 == pos1){
						pos2 = Random.Range(0,4);
					}
					if (EntreeEnnemis.tableauEnnemis[pos2]==0){
						comete = Instantiate (Comete);
						comete.transform.position = new Vector3(8,10,-pos2-1);
					}
					else{
						pos3 = Random.Range(0,4);
						while (pos3 == pos1 || pos3 == pos2){
							pos3 = Random.Range(0,4);
						}
						if (EntreeEnnemis.tableauEnnemis[pos3]==0){
							comete = Instantiate (Comete);
							comete.transform.position = new Vector3(8,10,-pos3-1);
						}
						else{
							pos4 = Random.Range(0,4);
							while (pos4 == pos1 || pos4 == pos2 || pos4 == pos3){
								pos4 = Random.Range(0,4);
							}
							comete = Instantiate (Comete);
							comete.transform.position = new Vector3(8,10,-pos4-1);
						}
					}
				}
				compteurComete = temps+Random.Range(3,10)*difficulte;
			}
			else{
				compteurComete = temps+1;
			}
			
		}
		if (temps > compteurFusee){
			random = Random.Range(0,5);
			if (random == 0){
				Instantiate (FuséeTordue);
			}
			else{
				Instantiate(Fusée);
			}
			compteurFusee = temps+Random.Range(2,7)*difficulte;
		}
		if (temps > compteurMine && SolManager.compteur > 1 && SolManager.compteur < 15){
			compt+=1;
			if (compt > 5){
				int choix = Random.Range(0,5);
				switch (choix){
					case 0 : Instantiate (Vie, new Vector3(15.75f,-1f,(float)-Random.Range(1,4)), Quaternion.identity);
					break;
					case 1 : Instantiate (Vie, new Vector3(15.75f,0.5f,(float)-Random.Range(1,4)), Quaternion.identity);
					break;
					default : Instantiate (Mine);
					break;
				}
				compt = 0;
			}
			else {
				Instantiate (Mine);
				compt++;
			}
			
			compteurMine = temps+Random.Range(1,6)*difficulte;
		}
		
	}

}
