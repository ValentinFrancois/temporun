using UnityEngine;
using System.Collections;

public class eclair : MonoBehaviour {
	// Update is called once per frame
	public Texture eclair1;
	public Texture eclair1bis;
	public Texture eclair2;
	public Texture eclair2bis;
	public Texture defaut;
	public Texture halo1;
	public Texture halo2;
	public GameObject halo;
	public GameObject titre;
	Vector2 position;
	int count = 0;
	void Update () {
		count++;
		if (count==0){
			MeshRenderer mr = GetComponent<MeshRenderer>();
			Material mat = mr.material;
			mat.mainTexture = defaut;
			position = new Vector2(0,202);
			titre.GetComponent<RectTransform>().anchoredPosition = position;
		}
		else if (count==300){
			MeshRenderer mr = GetComponent<MeshRenderer>();
			Material mat = mr.material;
			mat.mainTexture = eclair1;
			halo.GetComponent<MeshRenderer>().material.mainTexture=halo1;
			position = new Vector2(Random.Range(-15, 15),202+Random.Range(-10, 10));
			titre.GetComponent<RectTransform>().anchoredPosition = position;
		}
		else if (count==303){
			MeshRenderer mr = GetComponent<MeshRenderer>();
			Material mat = mr.material;
			mat.mainTexture = eclair1bis;	
			position = new Vector2(Random.Range(-20, 20),202+Random.Range(-10, 10));
			titre.GetComponent<RectTransform>().anchoredPosition = position;			
		}
		else if (count==306){
			MeshRenderer mr = GetComponent<MeshRenderer>();
			Material mat = mr.material;
			mat.mainTexture = defaut;
			halo.GetComponent<MeshRenderer>().material.mainTexture=defaut;
			position = new Vector2(0,202);
			titre.GetComponent<RectTransform>().anchoredPosition = position;
		}
		else if (count==600){
			MeshRenderer mr = GetComponent<MeshRenderer>();
			Material mat = mr.material;
			mat.mainTexture = eclair2;		
			halo.GetComponent<MeshRenderer>().material.mainTexture=halo2;
			position = new Vector2(Random.Range(-15, 15),202+Random.Range(-10, 10));
			titre.GetComponent<RectTransform>().anchoredPosition = position;
		}
		else if (count==603){
			MeshRenderer mr = GetComponent<MeshRenderer>();
			Material mat = mr.material;
			mat.mainTexture = eclair2bis;
			halo.GetComponent<MeshRenderer>().material.mainTexture=defaut;
			position = new Vector2(Random.Range(-20, 20),202+Random.Range(-10, 10));
			titre.GetComponent<RectTransform>().anchoredPosition = position;
			count=-3;
		}
	}
}
