#pragma strict
import System.Xml;
import System.IO;
var XMLFile2 : TextAsset;
var liste : Transform;
var bouton : GameObject;
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
var Drum2 : AudioSource;
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

//Drum2
var D21 : AudioClip; 
var D22 : AudioClip; 
var D23 : AudioClip; 
var D24 : AudioClip;
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

var XMLFile : WWW;

function Start () {
	Temps = 0;
	tabad = GetComponents(AudioSource);
	Drum1 = tabad[0] as AudioSource; 
	Drum2 = tabad[1] as AudioSource; 
	Piano = tabad [2] as AudioSource;
	Basse = tabad[3] as AudioSource;
	Guitare = tabad[4] as AudioSource; 
	Melodie = tabad[5] as AudioSource;
	
     //if(XMLFile != null)
     //{
		 System.IO.File.WriteAllText(Application.persistentDataPath + "/sauv.xml", XMLFile2.text);
		 XMLFile = new WWW("file:///" + Application.persistentDataPath + "/sauv.xml");
		 yield XMLFile;
         var reader:XmlTextReader = new XmlTextReader(new StringReader(XMLFile.text));

		 var y = 0;
         while(reader.Read())
         {
	
             if(reader.Name == "partie" && reader.NodeType == XmlNodeType.Element)
             {   
				 var xposition = 20;
				 if ((Screen.width/Screen.height)>=1.333333333f){
					 xposition = (Screen.width-Screen.height*1.333333f)/2+30*Screen.height/1024f;
				 }
				 
				 if ((Screen.width/Screen.height)<1.333333333f){
					 xposition = Screen.width/1024*30;
				 }
				 var newbouton = Instantiate(bouton,/* Vector3 (0, -y+420, 0), Quaternion.identity,*/ liste) as GameObject;
				 //newbouton.transform.SetParent(liste, false);
				 //newbouton.transform.position= liste.transform.position + Vector3(-liste.GetComponent(RectTransform).sizeDelta.x+20,-y);
				 newbouton.transform.position = bouton.transform.position + Vector3(0f,-y-40f,0f);
				 //newbouton.transform.position = Vector3(xposition,y,0f);
				 //Debug.Log(liste.transform.position.x);
				 //newbouton.GetComponent(RectTransform).anchoredPosition = Vector2(0f,y);
				 newbouton.GetComponentInChildren(UI.Text).text = " "+reader.GetAttribute("name");
				 length = 40-reader.GetAttribute("name").Length;
				 for(i=0; i<length; i++){
					 newbouton.GetComponentInChildren(UI.Text).text+=" ";
				 }
				 newbouton.GetComponentInChildren(UI.Text).text+= reader.GetAttribute("date");
				 newbouton.name = reader.GetAttribute("name");
				 y += 40;
				 if (y>418){
					 //y -= 20;
					 compteur++;
					 liste.GetComponent(RectTransform).sizeDelta.y+=40;
					 //liste.position.y = liste.position.y-20;
					 //newbouton.position.y = compteur;
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
		//liste.position.y = liste.position.y-compteur*20;
     //}
}

function Update () {
	if (IsStarted && IsPlaying){
		Temps += Time.deltaTime;
		avancement.GetComponent(RectTransform).sizeDelta.x = 500/(Max+8)*Temps*1.04f;
		textclock2 = CurrentTime.GetComponent(Text);
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
			var reader3:XmlTextReader = new XmlTextReader(new StringReader(XMLFile.text));
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
										var NomInstr = reader3.Name;
										var code = reader3.GetAttribute("clip");
										switch (NomInstr){
											case "Drum1" :
											switch(code){
												case "D11" : Drum1.clip = D11;
												Drum1.Play();
												break;
												case "D12" : Drum1.clip = D12;
												Drum1.Play();
												break;
												case "D13" : Drum1.clip = D14;
												Drum1.Play();
												break;
												case "D14" : Drum1.clip = D14;
												Drum1.Play();
												break;
											}
											break;
											case "Drum2" :
											switch(code){
												case "D21" : Drum2.clip = D21;
												Drum2.Play();
												break;
												case "D22" : Drum2.clip = D22;
												Drum2.Play();
												break;
												case "D23" : Drum2.clip = D24;
												Drum2.Play();
												break;
												case "D24" : Drum2.clip = D24;
												Drum2.Play();
												break;
											}
											break;
											case "Piano" :
											switch(code){
												case "P1" : Piano.clip = P1;
												Piano.Play();
												break;
												case "P2" : Piano.clip = P2;
												Piano.Play();
												break;
												case "P3" : Piano.clip = P4;
												Piano.Play();
												break;
												case "P4" : Piano.clip = P4;
												Piano.Play();
												break;
											}
											break;
											case "Guitare" :
											switch(code){
												case "G1" : Guitare.clip = G1;
												Guitare.Play();
												break;
												case "G2" : Guitare.clip = G2;
												Guitare.Play();
												break;
												case "G3" : Guitare.clip = G4;
												Guitare.Play();
												break;
												case "G4" : Guitare.clip = G4;
												Guitare.Play();
												break;
											}
											break;
											case "Melodie" :
											switch(code){
												case "M1" : Melodie.clip = M1;
												Melodie.Play();
												break;
												case "M2" : Melodie.clip = M2;
												Melodie.Play();
												break;
												case "M3" : Melodie.clip = M4;
												Melodie.Play();
												break;
												case "M4" : Melodie.clip = M4;
												Melodie.Play();
												break;
											}
											break;
											case "Basse" :
											switch(code){
												case "B1" : Basse.clip = B1;
												Basse.Play();
												break;
												case "B2" : Basse.clip = B2;
												Basse.Play();
												break;
												case "B3" : Basse.clip = B4;
												Basse.Play();
												break;
												case "B4" : Basse.clip = B4;
												Basse.Play();
												break;
											}
											break;
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
		if (Temps >= Max+8){
			Pause();
			avancement.GetComponent(RectTransform).sizeDelta.x=500;
			IsStarted = 0;
		}
	}
}

function Pause (){
	if (IsPlaying){
		Drum1.Pause();
		Drum2.Pause();
		Basse.Pause();
		Guitare.Pause();
		Melodie.Pause();
		Piano.Pause();
		IsPlaying = 0;
	}
}

function Play() {
	var reader2:XmlTextReader = new XmlTextReader(new StringReader(XMLFile.text));
	if (IsPlaying==0 && IsStarted ==1){
		Drum1.UnPause();
		Drum2.UnPause();
		Basse.UnPause();
		Guitare.UnPause();
		Melodie.UnPause();
		Piano.UnPause();
		IsPlaying = 1;		
	}
	else {
		if(XMLFile != null)
		 {	 //PartiePlaying = name;
			Temps = 0;
			Sequence = 0;
			IsStarted = 1;
			IsPlaying = 1;
			avancement.GetComponent(RectTransform).sizeDelta.x = 0f;
			 textclock1 = EndTime.GetComponent(Text);
			 min1 = (Max+8)/60;
			 sec1 = (Max+8)%60;
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
						var NomInstr = reader2.Name;
						var code = reader2.GetAttribute("clip");
						switch (NomInstr){
							case "Drum1" :
							switch(code){
								case "D11" : Drum1.clip = D11;
								Drum1.Play();
								break;
								case "D12" : Drum1.clip = D12;
								Drum1.Play();
								break;
								case "D13" : Drum1.clip = D14;
								Drum1.Play();
								break;
								case "D14" : Drum1.clip = D14;
								Drum1.Play();
								break;
								default: break;
							}
							break;
							case "Drum2" :
							switch(code){
								case "D21" : Drum2.clip = D21;
								Drum2.Play();
								break;
								case "D22" : Drum2.clip = D22;
								Drum2.Play();
								break;
								case "D23" : Drum2.clip = D24;
								Drum2.Play();
								break;
								case "D24" : Drum2.clip = D24;
								Drum2.Play();
								break;
								default: break;
							}
							break;
							case "Piano" :
							switch(code){
								case "P1" : Piano.clip = P1;
								Piano.Play();
								break;
								case "P2" : Piano.clip = P2;
								Piano.Play();
								break;
								case "P3" : Piano.clip = P4;
								Piano.Play();
								break;
								case "P4" : Piano.clip = P4;
								Piano.Play();
								break;
								default: break;
							}
							break;
							case "Guitare" :
							switch(code){
								case "G1" : Guitare.clip = G1;
								Guitare.Play();
								break;
								case "G2" : Guitare.clip = G2;
								Guitare.Play();
								break;
								case "G3" : Guitare.clip = G4;
								Guitare.Play();
								break;
								case "G4" : Guitare.clip = G4;
								Guitare.Play();
								break;
								default: break;
							}
							break;
							case "Melodie" :
							switch(code){
								case "M1" : Melodie.clip = M1;
								Melodie.Play();
								break;
								case "M2" : Melodie.clip = M2;
								Melodie.Play();
								break;
								case "M3" : Melodie.clip = M4;
								Melodie.Play();
								break;
								case "M4" : Melodie.clip = M4;
								Melodie.Play();
								break;
								default: break;
							}
							break;
							case "Basse" :
							switch(code){
								case "B1" : Basse.clip = B1;
								Basse.Play();
								break;
								case "B2" : Basse.clip = B2;
								Basse.Play();
								break;
								case "B3" : Basse.clip = B4;
								Basse.Play();
								break;
								case "B4" : Basse.clip = B4;
								Basse.Play();
								break;
								default: break;
							}
							break;
						}
					}
				 } 
			 } 
		 }
	}
}

function Replay(){
	Drum1.UnPause();
	Drum2.UnPause();
	Basse.UnPause();
	Guitare.UnPause();
	Melodie.UnPause();
	Piano.UnPause();
	IsStarted = 0;
	var boutonplay = GameObject.Find("Play/Pause");
	boutonplay.GetComponent(Image).sprite = boutonplay.GetComponent(PlayPause).texturePause;
	Play();
}