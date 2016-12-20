using UnityEngine;
using System.Collections;

public class TextFadeIn : MonoBehaviour {
	// Use this for initialization
	void Start () {
		CanvasGroup canvas = GetComponent<CanvasGroup>();
		canvas.alpha = 1;
	}
	
	// Update is called once per frame
	void Update () {
		CanvasGroup canvas = GetComponent<CanvasGroup>();
		if (canvas.alpha > 0){
			canvas.alpha -= Time.deltaTime / 2;
		}
	}
}
