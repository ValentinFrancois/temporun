using UnityEngine;
using System.Collections;

public class Vie : MonoBehaviour {
	private GameObject[] tab_vie;
	public Transform vie;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter(Collider col)
    {

        if (col.tag == "perso"){
			if (PersoController.perso_vie < 3){
				PersoController.perso_vie ++;
			}
			tab_vie = GameObject.FindGameObjectsWithTag ("vie");
			for (int i = 0; i < tab_vie.Length; i++) {
				Destroy (tab_vie[i]);
			}
			switch (PersoController.perso_vie) {
				case 2: 	
					Instantiate (vie, new Vector3 (-8.5f, -4.7f, -5),  Quaternion.Euler(20,0,0));
					Instantiate (vie, new Vector3 (-10, -4.7f, -5),  Quaternion.Euler(20,0,0));
					break; 
				case 3: 	
					Instantiate (vie, new Vector3 (-8.5f, -4.7f, -5),  Quaternion.Euler(20,0,0));
					Instantiate (vie, new Vector3 (-10, -4.7f, -5),  Quaternion.Euler(20,0,0));
					Instantiate (vie, new Vector3 (-7, -4.7f, -5),  Quaternion.Euler(20,0,0));
					break; 
				default:break;
			}
            Destroy(this.gameObject);
        }
	}
}
