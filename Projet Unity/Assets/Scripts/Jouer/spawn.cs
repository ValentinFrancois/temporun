using UnityEngine;
using System.Collections;

public class spawn : MonoBehaviour {
    
	public GameObject Comete;
	
	public GameObject VieBleue;
	public GameObject VieRouge;
	public GameObject VieVerte;
	public GameObject VieJaune;
	
	public GameObject MineBleue;
	public GameObject MineRouge;
	public GameObject MineVerte;
	public GameObject MineJaune;
	
	public GameObject FuséeBleue;
	public GameObject FuséeTordueBleue;
	public GameObject FuséeRouge;
	public GameObject FuséeTordueRouge;
	public GameObject FuséeVerte;
	public GameObject FuséeTordueVerte;
	public GameObject FuséeJaune;
	public GameObject FuséeTordueJaune;
	
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
	int compare;
	public static int randomNumber;
	GameObject fusee;
   // private GameObject FloorManager;

    void Start () {

	   compteurComete = Random.Range(6,18);
	   compteurFusee = Random.Range(2,7);
	   compteurMine = Random.Range(1,6);

	}

	void Update(){
		temps += Time.deltaTime;
		if (temps>=1.3f*constante && difficulte > 0.1f){
			difficulte-=0.05f;
			if (constante<2){
				constante+=2;
			} 
			else if (constante>15){
				constante+=15;
			}
			else{
				constante+=constante*constante/11;
			}
			
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

			randomNumber = -1*Random.Range(1, 5);
			if (SolManager.compteur < 0 || SolManager.compteur > 6){
				compare = SolManager.posAleat;
			}
			else{
				compare = SolManager.oldPosAleat;
			}
			if (randomNumber == -(1+compare)%4-1){
				if (random == 0){
					Instantiate (FuséeTordueRouge);
				}
				else{
					fusee = Instantiate(FuséeRouge,new Vector3(15.5f,-1f,randomNumber-5f),Quaternion.identity) as GameObject;
				}				
			}
			else if (randomNumber == -(2+compare)%4-1){
				if (random == 0){
					Instantiate (FuséeTordueBleue);
				}
				else{
					fusee = Instantiate(FuséeBleue,new Vector3(15.5f,-1f,randomNumber-5f),Quaternion.identity) as GameObject;
				}				
			}
			else if (randomNumber == -(3+compare)%4-1){
				if (random == 0){
					Instantiate (FuséeTordueVerte);
				}
				else{
					fusee = Instantiate(FuséeVerte,new Vector3(15.5f,-1f,randomNumber-5f),Quaternion.identity) as GameObject;
				}				
			}
			else if (randomNumber == -(4+compare)%4-1){
				if (random == 0){
					Instantiate (FuséeTordueJaune);
				}
				else{
					fusee = Instantiate(FuséeJaune,new Vector3(15.5f,-1f,randomNumber-5f),Quaternion.identity) as GameObject;
				}				
			}
			
			compteurFusee = temps+Random.Range(2,7)*difficulte;
			//Debug.Log(SolManager.compteur);
		}
		if (temps > compteurMine && SolManager.compteur > 1 && SolManager.compteur < 15){
			compt+=1;
			if (compt > 5 && PersoController.perso_vie<3){
				int choix = Random.Range(0,6);
				randomNumber = -1*Random.Range(1, 5);
				
				switch (choix){
					case 0 : 
						if (randomNumber == -(1+SolManager.posAleat)%4-1){
							Instantiate (VieRouge, new Vector3(15.75f,-1f,randomNumber), Quaternion.identity);
						}
						else if (randomNumber == -(2+SolManager.posAleat)%4-1){
							Instantiate (VieBleue, new Vector3(15.75f,-1f,randomNumber), Quaternion.identity);
						}
						else if (randomNumber == -(3+SolManager.posAleat)%4-1){
							Instantiate (VieVerte, new Vector3(15.75f,-1f,randomNumber), Quaternion.identity);
						}
						else if (randomNumber == -(4+SolManager.posAleat)%4-1){
							Instantiate (VieJaune, new Vector3(15.75f,-1f,randomNumber), Quaternion.identity);
						}
					break;
					case 1 :
						if (randomNumber == -(1+SolManager.posAleat)%4-1){
							Instantiate (VieRouge, new Vector3(15.75f,0.5f,randomNumber), Quaternion.identity);
						}
						else if (randomNumber == -(2+SolManager.posAleat)%4-1){
							Instantiate (VieBleue, new Vector3(15.75f,0.5f,randomNumber), Quaternion.identity);
						}
						else if (randomNumber == -(3+SolManager.posAleat)%4-1){
							Instantiate (VieVerte, new Vector3(15.75f,0.5f,randomNumber), Quaternion.identity);
						}
						else if (randomNumber == -(4+SolManager.posAleat)%4-1){
							Instantiate (VieJaune, new Vector3(15.75f,0.5f,randomNumber), Quaternion.identity);
						}
					break;
					default :
						if (randomNumber == -(1+SolManager.posAleat)%4-1){
							Instantiate (MineRouge, new Vector3(15.75f,-2.85f,randomNumber), Quaternion.identity);
						}
						else if (randomNumber == -(2+SolManager.posAleat)%4-1){
							Instantiate (MineBleue, new Vector3(15.75f,-2.85f,randomNumber), Quaternion.identity);
						}
						else if (randomNumber == -(3+SolManager.posAleat)%4-1){
							Instantiate (MineVerte, new Vector3(15.75f,-2.85f,randomNumber), Quaternion.identity);
						}
						else if (randomNumber == -(4+SolManager.posAleat)%4-1){
							Instantiate (MineJaune, new Vector3(15.75f,-2.85f,randomNumber), Quaternion.identity);
						}
					break;
				}
				compt = 0;
			}
			else {
				if (randomNumber == -(1+SolManager.posAleat)%4-1){
					Instantiate (MineRouge, new Vector3(15.75f,-2.85f,randomNumber), Quaternion.identity);
				}
				else if (randomNumber == -(2+SolManager.posAleat)%4-1){
					Instantiate (MineBleue, new Vector3(15.75f,-2.85f,randomNumber), Quaternion.identity);
				}
				else if (randomNumber == -(3+SolManager.posAleat)%4-1){
					Instantiate (MineVerte, new Vector3(15.75f,-2.85f,randomNumber), Quaternion.identity);
				}
				else if (randomNumber == -(4+SolManager.posAleat)%4-1){
					Instantiate (MineJaune, new Vector3(15.75f,-2.85f,randomNumber), Quaternion.identity);
				}
				compt++;
			}
			
			compteurMine = temps+Random.Range(1,6)*difficulte;
		}
		
	}

}
