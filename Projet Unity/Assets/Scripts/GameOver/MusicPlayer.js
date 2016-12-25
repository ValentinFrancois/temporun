#pragma strict
import System.Xml;
import System.IO;
import System.Text;
var XMLFile2 : TextAsset;
var avancement : GameObject;

var EclairGauche : Sprite;
var EclairDroit : Sprite;
var EclairCentre : Sprite;
var ImageGameOver : Sprite;
var ImageGameOver1 : Sprite;
var ImageGameOver2 : Sprite;
var ImageGameOver3 : Sprite;
var ImageGameOver4 : Sprite;

var GameOver : GameObject;
var GameOverPere : GameObject;
var Titre : GameObject;
var TitrePere : GameObject;
var Eclair : GameObject;
var EclairPere : GameObject;
var TextesBoutons : GameObject;


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

var D1playing;
var D2playing;
var Bplaying;
var Gplaying;
var Mplaying;
var Pplaying;

var initTime : float = 0;

function Start () {
	Temps = 0;
	tabad = GetComponents(AudioSource);
	Drum1 = tabad[0] as AudioSource; 
	Drum2 = tabad[1] as AudioSource; 
	Piano = tabad [2] as AudioSource;
	Basse = tabad[3] as AudioSource;
	Guitare = tabad[4] as AudioSource; 
	Melodie = tabad[5] as AudioSource;
	
	D1playing = false;
	D2playing = false;
	Gplaying = false;
	Bplaying = false;
	Pplaying = false;
	Mplaying = false;
	
	 TextesBoutons.GetComponent(CanvasGroup).alpha=0;
	 GameOverPere.GetComponent(CanvasGroup).alpha=0;
	 EclairPere.GetComponent(CanvasGroup).alpha=0;
	 TitrePere.GetComponent(CanvasGroup).alpha=0;
	 TitrePere.GetComponent(AudioSource).Play();
 
	 var reader:XmlTextReader = new XmlTextReader(new StreamReader(Application.persistentDataPath + "/sauv.xml", Encoding.UTF8));

	 var y = 0;
	 while(reader.Read())
	 {

		 if(reader.Name == "partie" && reader.NodeType == XmlNodeType.Element)
		 {   
			PartiePlaying = reader.GetAttribute("name");
			 var count = 0;
			 while(reader.Read() && reader.Name!= "partie"){
				 if (reader.Name == "sequence" && reader.NodeType == XmlNodeType.Element){
					 count += 8;
				 }
			 }
			Max = count;
			var minutes = Mathf.FloorToInt(Max / 60F);
			var seconds = Mathf.FloorToInt(Max - minutes * 60);
			var time = String.Format("{0:00}:{1:00}", minutes, seconds);
			EndTime.GetComponent(Text).text = time;
			break;
			
		 } 
	 }
		 
		 
}

function Update () {
	
	if (initTime<4){
		initTime+=Time.deltaTime;
		if (initTime>=0.3f && initTime<0.4f){
			EclairPere.GetComponent(CanvasGroup).alpha=1;
			Eclair.GetComponent(Image).sprite = EclairGauche;
		}
		if (initTime>=0.4f && initTime<0.5f){
			Eclair.GetComponent(Image).sprite = EclairDroit;
		}
		if (initTime>=0.5f && initTime<0.6f){
			Eclair.GetComponent(Image).sprite = EclairCentre;
		}
		if (initTime >= 0.6f){
			EclairPere.GetComponent(CanvasGroup).alpha=0;
		}
		if (initTime >= 0.6f && initTime < 0.7f){
			GameOverPere.GetComponent(CanvasGroup).alpha=1;
		}
		if (initTime>=0.6f && initTime<0.625f){
			GameOver.GetComponent(Image).sprite = ImageGameOver1;
		}
		if (initTime>=0.625f && initTime<0.65f){
			GameOver.GetComponent(Image).sprite = ImageGameOver2;
		}
		if (initTime>=0.65f && initTime<0.675f){
			GameOver.GetComponent(Image).sprite = ImageGameOver3;
		}
		if (initTime>=0.675f && initTime<0.7f){
			GameOver.GetComponent(Image).sprite = ImageGameOver4;
		}
		if (initTime >= 0.7f && initTime < 1.7f){
			GameOverPere.GetComponent(CanvasGroup).alpha-=Time.deltaTime;
			TitrePere.GetComponent(CanvasGroup).alpha+=Time.deltaTime;
		}
		if (initTime >= 1.7f && initTime < 2.7f){
			GameOverPere.GetComponent(CanvasGroup).alpha=0;
			TitrePere.GetComponent(CanvasGroup).alpha=1;
			Titre.transform.position.y+=Time.deltaTime*200;
		}
		if (initTime>=2.7f && initTime < 3.7f){
			TextesBoutons.GetComponent(CanvasGroup).alpha+=Time.deltaTime;
		}
		if (initTime>=3.7f){
			TextesBoutons.GetComponent(CanvasGroup).alpha=1;
		}
	}

	if (IsStarted && IsPlaying){
		Temps += Time.deltaTime;
		avancement.GetComponent(RectTransform).sizeDelta.x += 1f*500/Max*Time.deltaTime;
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
										switch(instru){
											case "drum1" : D1playing = true;
											break;
											case "drum2" : D2playing = true;
											break;
											case "guitare" : Gplaying = true;
											break;
											case "basse" : Bplaying = true;
											break;
											case "piano" : Pplaying = true;
											break;
											case "melodie" : Mplaying = true;
											break;
											default : break;
										}
										var code = reader3.GetAttribute("clip");
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
		Drum2.Pause();
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
		if (D2playing)Drum2.UnPause();
		if (Bplaying)Basse.UnPause();
		if (Gplaying)Guitare.UnPause();
		if (Mplaying)Melodie.UnPause();
		if (Pplaying)Piano.UnPause();
		IsPlaying = 1;		
	}
	else {
		Drum1.Pause();
		Drum2.Pause();
		Basse.Pause();
		Guitare.Pause();
		Melodie.Pause();
		Piano.Pause();
		Drum1.time=0f;
		Drum2.time=0f;
		Basse.time=0f;
		Guitare.time=0f;
		Melodie.time=0f;
		Piano.time=0f;
		D1playing = false;
		D2playing = false;
		Gplaying = false;
		Bplaying = false;
		Pplaying = false;
		Mplaying = false;
		Temps = 0;
		Sequence = 0;
		IsStarted = 1;
		IsPlaying = 1;
		avancement.GetComponent(RectTransform).sizeDelta.x = 0f;
		 textclock1 = EndTime.GetComponent(Text);
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
					switch(instru){
						case "drum1" : D1playing = true;
						break;
						case "drum2" : D2playing = true;
						break;
						case "guitare" : Gplaying = true;
						break;
						case "basse" : Bplaying = true;
						break;
						case "piano" : Pplaying = true;
						break;
						case "melodie" : Mplaying = true;
						break;
						default : break;
					}
					var code = reader2.GetAttribute("clip");
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
	boutonplay.GetComponent(Image).sprite = boutonplay.GetComponent(PlayPause2).texturePause;
	Play();
}