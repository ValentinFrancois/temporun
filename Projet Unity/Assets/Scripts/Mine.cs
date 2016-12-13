using UnityEngine;
using System.Collections;

public class Mine : MonoBehaviour {

   
    public Vector3[] positions;
    public GameObject FuséeExplosion;
	private GameObject[] tab_vie;
	public Transform vie; 
    
    void Start()

    {

        int randomNumber = UnityEngine.Random.Range(0, positions.Length);
        transform.position = positions[randomNumber];

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
				Application.LoadLevel ("ReplayTest");
				break; 
			case 1: 
				Instantiate (vie, new Vector3 (-10, -5, -5),  Quaternion.Euler(20,0,0));
				break; 
			case 2: 	
				Instantiate (vie, new Vector3 (-8, -5, -5),  Quaternion.Euler(20,0,0));
				Instantiate (vie, new Vector3 (-10, -5, -5),  Quaternion.Euler(20,0,0));


				break; 
			}
            Destroy(this.gameObject);
            Instantiate(FuséeExplosion, transform.position, Quaternion.identity);
        }
			
    }
   

    void Update()
    {
        transform.position = transform.position + new Vector3(-0.1f, 0, 0);
        Vector3 move = new Vector3();
        transform.position += move;
		if (transform.position.x < -20) {
			Destroy (this.gameObject);
		}

    }
}
