#pragma strict
import System.Xml;
import System.IO;
import System.Text;
var XMLFile2 : TextAsset;
var liste : Transform;
var bouton : GameObject;
var bouton2 : GameObject;
var avancement : GameObject;

var Temps : float;
var Sequence = 0;
var PartiePlaying = "";
var IsPlaying = 0;
var IsStarted = 0;
var seqtime = 0;
var Max = 0;

var CurrentTime : GameObject;
var EndTime : GameObject;
var flr : PartiePlaying;
//Music variables
var Drum1 : AudioSource;
var Synthe : AudioSource;
var Piano : AudioSource;
var Basse : AudioSource;
var Guitare : AudioSource;
var Melodie : AudioSource;
var tabad : Component[]; 
//Drum1
var D11 : AudioClip; 
var D12 : AudioClip; 
var D13 : AudioClip; 
var D14 : AudioClip; 

//Synthe
var S1 : AudioClip; 
var S2 : AudioClip; 
var S3 : AudioClip; 
var S4 : AudioClip;
//Piano
var P1 : AudioClip; 
var P2 : AudioClip; 
var P3 : AudioClip; 
var P4 : AudioClip;
//Basse 
var B1 : AudioClip; 
var B2 : AudioClip; 
var B3 : AudioClip; 
var B4 : AudioClip; 
//Guitare
var G1 : AudioClip; 
var G2 : AudioClip; 
var G3 : AudioClip; 
var G4 : AudioClip; 
//Melodie
var M1 : AudioClip; 
var M2 : AudioClip; 
var M3 : AudioClip; 
var M4 : AudioClip; 

var compteur = 0;
var i : int = 0;
var length : int = 0;
var min1 : int;
var min2 : int;
var sec1 : int;
var sec2 : int;
var textclock1 : Text;
var textclock2 : Text;
var division : float;

var D1playing;
var Splaying;
var Bplaying;
var Gplaying;
var Mplaying;
var Pplaying;

function Start () {
	Temps = 0;
	tabad = GetComponents(AudioSource);
	Drum1 = tabad[0] as AudioSource; 
	Synthe = tabad[1] as AudioSource; 
	Piano = tabad [2] as AudioSource;
	Basse = tabad[3] as AudioSource;
	Guitare = tabad[4] as AudioSource; 
	Melodie = tabad[5] as AudioSource;
	D1playing = false;
	Splaying = false;
	Gplaying = false;
	Bplaying = false;
	Pplaying = false;
	Mplaying = false;
	
	/*
	var found = 0;
	
	var doc = new XmlDocument();
	doc.Load(Application.persistentDataPath + "/sauv.xml");
	if(doc.DocumentElement.HasChildNodes){
		
		 if(doc.DocumentElement.HasChildNodes){
			 	var noms : XmlNodeList = doc.DocumentElement.ChildNodes;
				var NombreParties : int = noms.Count;
				for (var i=0; i<NombreParties && !found; i++){
					var current : XmlElement = noms[i] as XmlElement;
					if (current.GetAttribute("name")=="SampleRun-Automatic-Temporary-Save"){
						found = i;
					}
				}
		 }
		 if (found>0){
			doc.DocumentElement.RemoveChild(doc.DocumentElement.ChildNodes[found]);
			var sw : TextWriter = new StreamWriter(Application.persistentDataPath + "/sauv.xml", false, Encoding.UTF8); //Set encoding
			doc.Save(sw);
		 }
	}	
	*/
	 var reader:XmlTextReader = new XmlTextReader(new StreamReader(Application.persistentDataPath + "/sauv.xml", Encoding.UTF8));

	 var y = 0;
	 while(reader.Read())
	 {

		 if(reader.Name == "partie" && reader.NodeType == XmlNodeType.Element)
		 {   
			 var yoffset = bouton2.transform.position.y - bouton.transform.position.y;
			 var newbouton = Instantiate(bouton, liste) as GameObject;
			 newbouton.transform.position = bouton.transform.position + Vector3(0f,-y-yoffset-2f,0f);
			 newbouton.GetComponentInChildren(UI.Text).text = " "+reader.GetAttribute("name");
			 
			 length = 40-reader.GetAttribute("name").Length;
			 for(i=0; i<length; i++){
				 newbouton.GetComponentInChildren(UI.Text).text+=" ";
			 }
			 
			 newbouton.GetComponentInChildren(UI.Text).text+= reader.GetAttribute("date");
			 newbouton.name = reader.GetAttribute("name");
			 
			 y += yoffset;
			 
			 if (y>480.0f*yoffset/40.0f){
				 compteur++;
				 liste.GetComponent(RectTransform).sizeDelta.y+=40.0f;
			 }
			 
			 var count = 0;
			 while(reader.Read() && reader.Name!= "partie"){
				 if (reader.Name == "sequence" && reader.NodeType == XmlNodeType.Element){
					 count += 8;
				 }
			 }
			 
			var minutes = Mathf.FloorToInt(count / 60F);
			var seconds = Mathf.FloorToInt(count - minutes * 60);
			var time = String.Format("{0:00}:{1:00}", minutes, seconds);
			newbouton.GetComponentInChildren(UI.Text).text += " - " + time;
			
			flr = newbouton.GetComponent("PartiePlaying") as PartiePlaying;
			flr.duree = count;
		 } 
	 }
	liste.GetComponent(RectTransform).sizeDelta.y+=40;
}

function Update () {
	if (IsStarted && IsPlaying){
		Temps += Time.deltaTime;
		avancement.GetComponent(RectTransform).sizeDelta.x = 1f*500/Max*Temps;
		textclock2 = CurrentTime.GetComponent(UnityEngine.UI.Text);
		 min2 = (parseInt(Temps))/60;
		 sec2 = (parseInt(Temps))%60;
		 if (min2 > 9){
			 if (sec2> 9){
				 textclock2.text = min2+":"+sec2;
			 }
			 else {
				 textclock2.text = min2+":0"+sec2;
			 }
		 }
		 else {
			 if (sec2 > 9){
				 textclock2.text = "0"+min2+":"+sec2;
			 }
			 else {
				 textclock2.text = "0"+min2+":0"+sec2;
			 }
		 }
		if (Temps >= Sequence + 8){
			Sequence += 8;
			var reader3:XmlTextReader = new XmlTextReader(new StreamReader(Application.persistentDataPath + "/sauv.xml", Encoding.UTF8));
			 while(reader3.Read())
			 {
				 if(reader3.Name == "partie" && reader3.NodeType == XmlNodeType.Element)
				 {   	
					var nomPartie = reader3.GetAttribute("name");
					if (nomPartie == PartiePlaying){
						while(reader3.Read()){
							if(reader3.NodeType == XmlNodeType.Element){
							if (reader3.Name == "sequence"){
							if (int.Parse(reader3.GetAttribute("time")) == Sequence){
								while (reader3.Read() && reader3.Name!= "sequence" ){
									if (reader3.NodeType == XmlNodeType.Element){
										var instru = reader3.GetAttribute("type");
										var code = reader3.GetAttribute("clip");
										switch(instru){
											case "drum1" : D1playing = true;
											if (code != ""){
												Drum1.mute = false;
											}
											else {
												Drum1.mute = true;
											}
											break;
											case "synthe" : Splaying = true;
											if (code != ""){
												Synthe.mute = false;
											}
											else {
												Synthe.mute = true;
											}
											break;
											case "guitare" : Gplaying = true;
											if (code != ""){
												Guitare.mute = false;
											}
											else {
												Guitare.mute = true;
											}
											break;
											case "basse" : Bplaying = true;
											if (code != ""){
												Basse.mute = false;
											}
											else {
												Basse.mute = true;
											}
											break;
											case "piano" : Pplaying = true;
											if (code != ""){
												Piano.mute = false;
											}
											else {
												Piano.mute = true;
											}
											break;
											case "melodie" : Mplaying = true;
											if (code != ""){
												Piano.mute = false;
											}
											else {
												Piano.mute = true;
											}
											break;
											default : break;
										}
										switch(code){
											case "D11" : Drum1.clip = D11;
											Drum1.Play();
											break;
											case "D12" : Drum1.clip = D12;
											Drum1.Play();
											break;
											case "D13" : Drum1.clip = D13;
											Drum1.Play();
											break;
											case "D14" : Drum1.clip = D14;
											Drum1.Play();
											break;
											case "S1" : Synthe.clip = S1;
											Synthe.Play();
											break;
											case "S2" : Synthe.clip = S2;
											Synthe.Play();
											break;
											case "S3" : Synthe.clip = S3;
											Synthe.Play();
											break;
											case "S4" : Synthe.clip = S4;
											Synthe.Play();
											break;
											case "P1" : Piano.clip = P1;
											Piano.Play();
											break;
											case "P2" : Piano.clip = P2;
											Piano.Play();
											break;
											case "P3" : Piano.clip = P3;
											Piano.Play();
											break;
											case "P4" : Piano.clip = P4;
											Piano.Play();
											break;
											case "G1" : Guitare.clip = G1;
											Guitare.Play();
											break;
											case "G2" : Guitare.clip = G2;
											Guitare.Play();
											break;
											case "G3" : Guitare.clip = G3;
											Guitare.Play();
											break;
											case "G4" : Guitare.clip = G4;
											Guitare.Play();
											break;
											case "M1" : Melodie.clip = M1;
											Melodie.Play();
											break;
											case "M2" : Melodie.clip = M2;
											Melodie.Play();
											break;
											case "M3" : Melodie.clip = M3;
											Melodie.Play();
											break;
											case "M4" : Melodie.clip = M4;
											Melodie.Play();
											break;
											case "B1" : Basse.clip = B1;
											Basse.Play();
											break;
											case "B2" : Basse.clip = B2;
											Basse.Play();
											break;
											case "B3" : Basse.clip = B3;
											Basse.Play();
											break;
											case "B4" : Basse.clip = B4;
											Basse.Play();
											break;
											default : break;
										}
									}
								}
								break;
							}}}
						}
					}
				 }
			 }
		}
		if (Temps >= Max){
			Pause();
			avancement.GetComponent(RectTransform).sizeDelta.x=500;
			IsStarted = 0;
		}
	}
}

function Pause (){
	if (IsPlaying){
		Drum1.Pause();
		Synthe.Pause();
		Basse.Pause();
		Guitare.Pause();
		Melodie.Pause();
		Piano.Pause();
		IsPlaying = 0;
	}
}

function Play() {
	var reader2:XmlTextReader = new XmlTextReader(new StreamReader(Application.persistentDataPath + "/sauv.xml", Encoding.UTF8));
	if (IsPlaying==0 && IsStarted ==1){
		if (D1playing) Drum1.UnPause();
		if (Splaying)Synthe.UnPause();
		if (Bplaying)Basse.UnPause();
		if (Gplaying)Guitare.UnPause();
		if (Mplaying)Melodie.UnPause();
		if (Pplaying)Piano.UnPause();
		IsPlaying = 1;		
	}
	else {
		Drum1.Pause();
		Synthe.Pause();
		Basse.Pause();
		Guitare.Pause();
		Melodie.Pause();
		Piano.Pause();
		Drum1.time=0f;
		Synthe.time=0f;
		Basse.time=0f;
		Guitare.time=0f;
		Melodie.time=0f;
		Piano.time=0f;
		D1playing = false;
		Splaying = false;
		Gplaying = false;
		Bplaying = false;
		Pplaying = false;
		Mplaying = false;
		Temps = 0;
		Sequence = 0;
		IsStarted = 1;
		IsPlaying = 1;
		avancement.GetComponent(RectTransform).sizeDelta.x = 0f;
		 textclock1 = EndTime.GetComponent(UnityEngine.UI.Text);
		 min1 = Max/60;
		 sec1 = Max%60;
		 if (min1 > 9){
			 if (sec1 > 9){
				 textclock1.text = min1+":"+sec1;
			 }
			 else {
				 textclock1.text = min1+":0"+sec1;
			 }
		 }
		 else {
			 if (sec1 > 9){
				 textclock1.text = "0"+min1+":"+sec1;
			 }
			 else {
				 textclock1.text = "0"+min1+":0"+sec1;
			 }
		 }
		 while(reader2.Read())
		 {
			 var NomPartie = reader2.GetAttribute("name");
			 if(reader2.Name == "partie" && reader2.NodeType == XmlNodeType.Element && NomPartie == PartiePlaying)
			 {  
				reader2.Read();
				reader2.Read();
				while (reader2.Read() && reader2.Name!= "sequence"){
					var instru = reader2.GetAttribute("type");
					var code = reader2.GetAttribute("clip");
					switch(instru){
						case "drum1" : D1playing = true;
						if (code != ""){
							Drum1.mute = false;
						}
						else {
							Drum1.mute = true;
						}
						break;
						case "synthe" : Splaying = true;
						if (code != ""){
							Synthe.mute = false;
						}
						else {
							Synthe.mute = true;
						}
						break;
						case "guitare" : Gplaying = true;
						if (code != ""){
							Guitare.mute = false;
						}
						else {
							Guitare.mute = true;
						}
						break;
						case "basse" : Bplaying = true;
						if (code != ""){
							Basse.mute = false;
						}
						else {
							Basse.mute = true;
						}
						break;
						case "piano" : Pplaying = true;
						if (code != ""){
							Piano.mute = false;
						}
						else {
							Piano.mute = true;
						}
						break;
						case "melodie" : Mplaying = true;
						if (code != ""){
							Piano.mute = false;
						}
						else {
							Piano.mute = true;
						}
						break;
						default : break;
					}
					switch(code){
						case "D11" : Drum1.clip = D11;
						Drum1.Play();
						break;
						case "D12" : Drum1.clip = D12;
						Drum1.Play();
						break;
						case "D13" : Drum1.clip = D13;
						Drum1.Play();
						break;
						case "D14" : Drum1.clip = D14;
						Drum1.Play();
						break;
						case "S1" : Synthe.clip = S1;
						Synthe.Play();
						break;
						case "S2" : Synthe.clip = S2;
						Synthe.Play();
						break;
						case "S3" : Synthe.clip = S3;
						Synthe.Play();
						break;
						case "S4" : Synthe.clip = S4;
						Synthe.Play();
						break;
						case "P1" : Piano.clip = P1;
						Piano.Play();
						break;
						case "P2" : Piano.clip = P2;
						Piano.Play();
						break;
						case "P3" : Piano.clip = P3;
						Piano.Play();
						break;
						case "P4" : Piano.clip = P4;
						Piano.Play();
						break;
						case "G1" : Guitare.clip = G1;
						Guitare.Play();
						break;
						case "G2" : Guitare.clip = G2;
						Guitare.Play();
						break;
						case "G3" : Guitare.clip = G3;
						Guitare.Play();
						break;
						case "G4" : Guitare.clip = G4;
						Guitare.Play();
						break;
						case "M1" : Melodie.clip = M1;
						Melodie.Play();
						break;
						case "M2" : Melodie.clip = M2;
						Melodie.Play();
						break;
						case "M3" : Melodie.clip = M3;
						Melodie.Play();
						break;
						case "M4" : Melodie.clip = M4;
						Melodie.Play();
						break;
						case "B1" : Basse.clip = B1;
						Basse.Play();
						break;
						case "B2" : Basse.clip = B2;
						Basse.Play();
						break;
						case "B3" : Basse.clip = B3;
						Basse.Play();
						break;
						case "B4" : Basse.clip = B4;
						Basse.Play();
						break;
						default : break;
					}
				}
			 } 
		 } 
	}
}

function Replay(){
	IsStarted = 0;
	var boutonplay = GameObject.Find("Play/Pause");
	boutonplay.GetComponent(Image).sprite = boutonplay.GetComponent(PlayPause).texturePause;
	Play();
}