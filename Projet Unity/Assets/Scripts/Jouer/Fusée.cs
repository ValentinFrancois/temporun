﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Text;
using System.IO;

public class Fusée : MonoBehaviour {

    public Vector3[] positions;
    public GameObject FuséeExplosion;
    public GameObject FloorManager ;
	private GameObject[] tab_vie;
	public Transform vie; 
	int random;
    void Start()

    {
        FloorManager = GameObject.FindGameObjectWithTag("SolMan");

        int randomNumber = Random.Range(0, positions.Length);
        transform.position = positions[randomNumber];
		random = UnityEngine.Random.Range(0,2);
		if (random==0){
			transform.position += new Vector3(0,0.75f,0);
		}

    }


    void OnTriggerEnter(Collider col)
    {

        if (col.tag == "perso")
        {
			
			PersoController.perso_vie--;
			tab_vie = GameObject.FindGameObjectsWithTag ("vie");
			for (int i = 0; i < tab_vie.Length; i++) {
				Destroy (tab_vie [i]);
			}
			switch (PersoController.perso_vie) {

			case 0:
				PersoController scripte  = 	GameObject.Find("Perso").GetComponent<PersoController>();
				scripte.doc.DocumentElement.AppendChild(scripte.partie);
				using(TextWriter sw = new StreamWriter(Application.persistentDataPath + "/sauv.xml", false, Encoding.ASCII)) //Set encoding
				{
					scripte.doc.Save(sw);
				}
				GameObject.Find("Perso").GetComponent<Animator>().SetBool("mort",true);
				
				break; 
			case 1: 
				Instantiate (vie, new Vector3 (-10, -4.7f, -5),  Quaternion.Euler(20,0,0));
				break; 
			case 2: 	
				Instantiate (vie, new Vector3 (-8.5f, -4.7f, -5),  Quaternion.Euler(20,0,0));
				Instantiate (vie, new Vector3 (-10, -4.7f, -5),  Quaternion.Euler(20,0,0));

			
				break; 
			}
            Instantiate(FuséeExplosion, transform.position + new Vector3 (0,0,5), Quaternion.identity, FloorManager.transform);
            Destroy(this.gameObject);
            
        }

    }
    
    void Update () {
        transform.position = transform.position + new Vector3(-10f*Time.deltaTime, 0, 0);
        Vector3 move = new Vector3();
        transform.position += move;
		if (transform.position.x < -20) {
			Destroy (this.gameObject);
		}
    }
}
