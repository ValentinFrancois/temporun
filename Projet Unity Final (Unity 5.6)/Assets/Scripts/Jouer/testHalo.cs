using UnityEngine;
using System.Collections;

public class testHalo : MonoBehaviour {
	float compteur = 1.5f;
	float intensite;
	
	void Start(){
		compteur = 1.5f;
	}
    void Update () 
    {
		if (compteur==1.5f){
			intensite = Random.Range(0f,0.06f);
		}
		GetComponent<Light>().intensity = intensite*compteur*Time.deltaTime*20;
		compteur-=Time.deltaTime*Random.Range(0.5f,1.5f);;
		if (compteur <= 0f){
			compteur = 1.5f;
		}
    }
}