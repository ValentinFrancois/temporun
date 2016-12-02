using UnityEngine;
using System.Collections;

public class TextFadeInterval : MonoBehaviour {
	int fade = 0;
	// Use this for initialization
	void Start () {
		CanvasGroup canvas = GetComponent<CanvasGroup>();
		canvas.alpha = 0;
	}
	
	// Update is called once per frame
	void Update () {
		CanvasGroup canvas = GetComponent<CanvasGroup>();
		if (canvas.alpha < 1 && fade == 0){
			canvas.alpha += Time.deltaTime / 1;
			if (canvas.alpha >= 1){
				canvas.alpha = 1;
				fade = 1;
			}
			
		}
		if (canvas.alpha > 0 && fade == 1){
			canvas.alpha -= Time.deltaTime / 1;
			if (canvas.alpha <= 0){
				canvas.alpha = 0;
				fade = 0;
			}
		}
	}
}
