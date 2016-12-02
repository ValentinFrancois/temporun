using UnityEngine;
using System.Collections;

public class InstructionsAnimees : MonoBehaviour {
	GameObject espace, flecheH, flecheB;
	CanvasGroup espaceCanvas, flecheHCanvas, flecheBCanvas;
	int interval = 0;
	Animator animator1;
	bool bas = true;
	bool deplacer;
	GameObject perso;
	int pos = 0;

	// Use this for initialization
	void Start () {
		animator1 = transform.Find("SautPersonnage").GetComponent<Animator>();
		perso = GameObject.Find("DeplacePersonnage");
		pos = 0;
	}
	
	// Update is called once per frame
	void Update () {
		interval ++;
		espaceCanvas = transform.Find("Barre Espace").GetComponent<CanvasGroup>();		
		flecheHCanvas = transform.Find("Flèche haut").GetComponent<CanvasGroup>();	
		flecheBCanvas = transform.Find("Flèche bas").GetComponent<CanvasGroup>();			
		if ((interval >= 80 && interval <100)||(interval >= 180 && interval <200)){
			espaceCanvas.alpha = 1f;
		}
		if (interval == 100){
			espaceCanvas.alpha = 0.5f;
		}
		if (interval == 80 || interval == 180){
			animator1.SetBool("Sauter", true);
			deplacer = true;
			
		}
		if (interval==81 || interval == 181){
			animator1.SetBool("Sauter", false);
		}
		if (interval >= 80 && interval <100){
			flecheHCanvas.alpha = 1f;
		}
		if (interval == 100){
			flecheHCanvas.alpha=0.5f;
		}
		if (interval>=180 && interval < 200){
			flecheBCanvas.alpha=1f;
		}
		if (interval == 200){
			interval = 0;
			flecheBCanvas.alpha=0.5f;
			espaceCanvas.alpha = 0.5f;
		}
		if (deplacer){
			if (bas){
				perso.transform.position = new Vector3(perso.transform.position.x - 2, perso.transform.position.y + 2, perso.transform.position.z);
				pos++;
				if (pos == 18){
					pos = 0;
					bas = false;
					deplacer = false;
				}
			}
			else {
				perso.transform.position = new Vector3(perso.transform.position.x + 2, perso.transform.position.y - 2, perso.transform.position.z);
				pos++;
				if (pos == 18){
					pos = 0;
					bas = true;
					deplacer = false;
				}
			}
		}
	}
}
