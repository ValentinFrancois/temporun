#pragma strict

var script : GameObject;
var targetScript: ListeDeroulante;
var duree : int;

function Start () {

}

function Update () {

}

function PartiePlay(){
	script =  GameObject.Find("TextPanel");
	targetScript = script.GetComponent(ListeDeroulante);
	if (targetScript.PartiePlaying!=gameObject.name){
		
		if (targetScript.IsStarted == 1){
			targetScript.IsStarted = 0;
		}
		
		targetScript.PartiePlaying = gameObject.name;
		targetScript.Max = duree;
		targetScript.Replay();

		var boutonplay = GameObject.Find("Play/Pause");
		boutonplay.GetComponent(Image).sprite = boutonplay.GetComponent(PlayPause).texturePause;
	}
}