using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GetQuality : MonoBehaviour {

	// Use this for initialization
	void Start () {
		int quality = QualitySettings.GetQualityLevel();
		switch(quality){
			case 0 : GameObject.Find("Minimale").GetComponent<Toggle>().isOn = true;
			break;
			case 1 : GameObject.Find("Basse").GetComponent<Toggle>().isOn = true;
			break;
			case 2 : GameObject.Find("Moyenne").GetComponent<Toggle>().isOn = true;
			break;
			case 3 : GameObject.Find("Bonne").GetComponent<Toggle>().isOn = true;
			break;
			case 4 : GameObject.Find("Très bonne").GetComponent<Toggle>().isOn = true;
			break;
			case 5 : GameObject.Find("Maximale").GetComponent<Toggle>().isOn = true;
			break;
			
		}
	}
	
}
