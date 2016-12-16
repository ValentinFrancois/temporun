using UnityEngine;
using System.Collections;

public class EntreeEnnemis : MonoBehaviour {
	public static int[] tableauEnnemis = {0,0,0,0};
	public int[] tab = {0,0,0,0};
	int pos;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		tab[0]=tableauEnnemis[0];
		tab[1]=tableauEnnemis[1];
		tab[2]=tableauEnnemis[2];
		tab[3]=tableauEnnemis[3];
	}
	
	void OnTriggerEnter(Collider col){
		if (col.tag=="mine" || col.tag =="fusee" || col.tag=="note"){
			pos = (int)col.transform.position.z;
			pos = -pos-1;
			if (col.tag=="fusee"){pos=pos-5;}
			tableauEnnemis[pos]+=1;
		}
		if (col.tag=="TRouge" /*|| col.tag=="TBleu" || col.tag=="TVert"|| col.tag=="TJaune"*/){
			tableauEnnemis[0]+=1;
			tableauEnnemis[1]+=1;
			tableauEnnemis[2]+=1;
			tableauEnnemis[3]+=1;
		}
	}
	
}
