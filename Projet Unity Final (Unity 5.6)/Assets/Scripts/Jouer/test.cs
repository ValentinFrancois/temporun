using UnityEngine;
using System.Collections;

public class test : MonoBehaviour {
    public LineRenderer lineRend1;
    public int quality = 100;
    public float len = 5;
    public float amplitude = 10;
	private AudioSource[] tabad;
	int i, j, k;
	float[] soundBit;
	float[] tab;	
	
	void Start(){
		soundBit = new float[quality];
		tab = new float[quality];
		tabad = GetComponents<AudioSource> ();
		for (k = 0; k<quality; k++){
			tab[k]=0;
		}
        lineRend1.SetVertexCount(quality);
	}
    
    void Update () 
    {
		for (i = -quality / 2; i < quality / 2; i++)
		{
			for(j =0; j<6; j++){
				tabad[j].GetOutputData(soundBit, 0);
					for (k = 0; k<quality; k++){
						tab[k]+=soundBit[k];
					}
			}
			lineRend1.SetPosition(i + quality / 2, new Vector3(i * (float)((float)len / (float)quality), tab[i + quality / 2] * amplitude, 9));
			for (k = 0; k<quality; k++){
				tab[k]=0;
			}
		}
    }
}