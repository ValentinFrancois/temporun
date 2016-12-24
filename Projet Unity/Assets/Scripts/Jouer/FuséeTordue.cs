using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class FuséeTordue : MonoBehaviour {

    public GameObject FuséeExplosion;
    public GameObject FloorManager ;
	private GameObject[] tab_vie;
	public Transform vie; 
    void Start()

    {
        FloorManager = GameObject.FindGameObjectWithTag("SolMan");
		transform.position = new Vector3(15.5f, -1f, spawn.randomNumber-5f);
    }


    void OnTriggerEnter(Collider col)
    {

        if (col.tag == "perso" && !PersoController.invincible)
        {
			if (!PersoController.invincible){
				PersoController.perso_vie--;
				if (PersoController.perso_vie>0){
					PersoController.Degats();
				}
				tab_vie = GameObject.FindGameObjectsWithTag ("vie");
				for (int i = 0; i < tab_vie.Length; i++) {
					Destroy (tab_vie [i]);
				}
				switch (PersoController.perso_vie) {

				case 0:
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
