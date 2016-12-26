#pragma strict
import UnityEngine.UI;
var script : GameObject;
var targetScript: ListeDeroulante;
var texturePlay : Sprite;
var texturePause : Sprite;
var bouton : GameObject;
var im : Image;

function Start () {
	im = bouton.GetComponent(Image);
}

function Update () {

}

function PlayPause(always : int){
	targetScript = script.GetComponent(ListeDeroulante);
	if (always == 1){
			targetScript.Pause();
			im.sprite = texturePlay;
	}
	else{
		if (targetScript.IsStarted && targetScript.IsPlaying == 0){
			targetScript.Play();	
			im.sprite = texturePause;
		}
		else if (targetScript.IsStarted && targetScript.IsPlaying == 1) {
			targetScript.Pause();
			im.sprite = texturePlay;
			
		}
	}
}