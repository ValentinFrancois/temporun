using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Text;
using System.IO;

public class Mine : MonoBehaviour {

   
    public GameObject FuséeExplosion;
	private GameObject[] tab_vie;

	public Transform vie;


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
					PersoController scripte  = 	GameObject.Find("Perso").GetComponent<PersoController>();
					scripte.doc.DocumentElement.AppendChild(scripte.partie);
					using(TextWriter sw = new StreamWriter(Application.persistentDataPath + "/sauv.xml", false, Encoding.ASCII))
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
			}
            Destroy(this.gameObject);
            Instantiate(FuséeExplosion, transform.position, Quaternion.identity);
        }
			
    }
	
}
