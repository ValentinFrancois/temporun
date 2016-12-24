using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour {
	public void level(string name){
		Time.timeScale = 1;	
		SceneManager.LoadScene(name);
	}
}
