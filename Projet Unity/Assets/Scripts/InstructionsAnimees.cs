using UnityEngine;
using System.Collections;

public class InstructionsAnimees : MonoBehaviour {
	GameObject espace, flecheH, flecheB;
	CanvasGroup espaceCanvas, flecheHCanvas, flecheBCanvas;
	float interval = 0f;
	Animator animator1;
	bool bas = true;
	bool deplacer;
	GameObject perso;
	float pos = 0;

	// Use this for initialization
	void Start () {
		animator1 = transform.Find("SautPersonnage").GetComponent<Animator>();
		perso = GameObject.Find("DeplacePersonnage");
		pos = 0;
		interval = 0f;
	}
	
	// Update is called once per frame
	void Update () {
		interval +=60*Time.deltaTime;
		espaceCanvas = transform.Find("Barre Espace").GetComponent<CanvasGroup>();		
		flecheHCanvas = transform.Find("Flèche haut").GetComponent<CanvasGroup>();	
		flecheBCanvas = transform.Find("Flèche bas").GetComponent<CanvasGroup>();		
	
		if ((interval >= 80 && interval <100)||(interval >= 180 && interval <200)){
			espaceCanvas.alpha = 1f;
		}
		if (interval >= 100){
			espaceCanvas.alpha = 0.5f;
		}
		if ((interval >= 80 && interval <85) || (interval >= 180 && interval <185)){
			animator1.SetBool("Sauter", true);
			deplacer = true;
			
		}
		if (interval>=85 || interval >= 185){
			animator1.SetBool("Sauter", false);
		}
		if (interval >= 80 && interval <100){
			flecheHCanvas.alpha = 1f;
		}
		if (interval >= 100){
			flecheHCanvas.alpha=0.5f;
		}
		if (interval>=180 && interval < 200){
			flecheBCanvas.alpha=1f;
		}
		if (interval >= 200){
			interval = 0;
			flecheBCanvas.alpha=0.5f;
			espaceCanvas.alpha = 0.5f;
		}
		if (deplacer){
			if (bas){
				perso.transform.position = new Vector3(perso.transform.position.x - 120*Time.deltaTime, perso.transform.position.y + 120*Time.deltaTime, perso.transform.position.z);
				pos+=60*Time.deltaTime;
				if (pos >= 18){
					pos = 0;
					bas = false;
					deplacer = false;
				}
			}
			else {
				perso.transform.position = new Vector3(perso.transform.position.x + 120*Time.deltaTime, perso.transform.position.y - 120*Time.deltaTime, perso.transform.position.z);
				pos+=60*Time.deltaTime;
				if (pos >= 18){
					pos = 0;
					bas = true;
					deplacer = false;
				}
			}
		}
	}
}
