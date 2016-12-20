using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class clock : MonoBehaviour {
	int min=0, sec=0;
	float timer = 0f;
	string minute;
	string seconde;
	Text textClock;

	string LeadingZero(int n){
		return n.ToString().PadLeft(2,'0');
	}
	
	void Start () {
		textClock = GetComponent<Text>();
		textClock.text = "00:00";
		min = 0;
		sec = 0;
		timer = 0f;
	}
	
	void Update () {
		timer+=Time.deltaTime;
		if (timer>1){
			sec++;
			timer=0f;
		}
		if (sec>59){
			sec = 0;
			min++;
		}
		minute = LeadingZero(min);	
		seconde = LeadingZero(sec);	
		textClock.text = minute + ":" + seconde;
	}
}
