using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Xml;
using System.IO;
using System;
using System.Text;


public class PersoController : MonoBehaviour {
	Animator animator;
	//Music variables
	AudioSource Drum; 
	AudioSource Piano; 
	AudioSource Basse; 
	AudioSource Guitare;
	AudioSource Melodie;
	AudioSource Synthe;

	private AudioSource[] tabad; 
	public AudioClip[] clips;
	public String[] portails;
	public bool[] Mute;

	private bool[] changeClips;
	//Drum
	public AudioClip D11; 
	public AudioClip D12;
	public AudioClip D13;
	public AudioClip D14;
	//Piano
	public AudioClip P1; 
	public AudioClip P2; 
	public AudioClip P3; 
	public AudioClip P4;
	//Basse 
	public AudioClip B1; 
	public AudioClip B2; 
	public AudioClip B3; 
	public AudioClip B4; 
	//Guitare
	public AudioClip G1; 
	public AudioClip G2; 
	public AudioClip G3; 
	public AudioClip G4; 
	//Melodie
	public AudioClip M1; 
	public AudioClip M2; 
	public AudioClip M3; 
	public AudioClip M4; 
	//Melodie
	public AudioClip S1; 
	public AudioClip S2; 
	public AudioClip S3; 
	public AudioClip S4; 

	public GameObject solMan; 
	public bool TouchSol = true; 
	private bool Saut = false; //To know if the player just jumped when he arrive on the middle of gate. 


	public int position;
	public int instru = 0; 
	private int instruCheck = -2; 
	Vector3 move = new Vector3();
	public Transform vie; 
	static public int perso_vie; 
	private SpriteRenderer batterie;
	private SpriteRenderer piano;
	private SpriteRenderer basse;
	private SpriteRenderer guitare;
	private SpriteRenderer melodie;
	private SpriteRenderer synthe;
	
	public Transform rays;
	
	public bool mort;
	float DeathTime;
	float StopTime;
	XmlElement root;
	XmlElement node;
	public XmlElement partie;
	XmlElement sequence;
	public XmlDocument doc;
	public bool tombe;
	
	bool isPaused;
	bool touchePortail;
	
	public static bool invincible;
	
	int timecompteur;
	public static float clignote;
	public static int isClignote;
	int compteurRecalage;
	
	void Effacer() {
		GetComponent<SpriteRenderer>().enabled = false;
    }
	void Afficher() {
		GetComponent<SpriteRenderer>().enabled = true;
    }
    public static void Degats() {
		PersoController.invincible = true;
		PersoController.isClignote = 1;
		PersoController.clignote = Time.time;
    }
	
	void Start () {
		
		 portails = new String[6];
		 portails[0]="";
		 portails[1]="";
		 portails[2]="";
		 portails[3]="";
		 portails[4]="";
		 portails[5]="";
		 
		 Mute = new bool[6];
		 Mute[0]=false;
		 Mute[1]=false;
		 Mute[2]=false;
		 Mute[3]=false;
		 Mute[4]=false;
		 Mute[5]=false;
		 
		 invincible = false;
		 tombe = false;
		 isPaused = false;
		 touchePortail = false;
		 int found = -1;
		 doc = new XmlDocument();
		 doc.Load(Application.persistentDataPath + "/sauv.xml");
		 if(doc.DocumentElement.HasChildNodes){
			 	XmlNodeList noms = doc.DocumentElement.ChildNodes;
				int NombreParties = noms.Count;
				for (int i=0; i<NombreParties; i++){
					XmlElement current = noms[i] as XmlElement;
					if (current.GetAttribute("name")=="SampleRun-Automatic-Temporary-Save"){
						found = i;
						break;
						//Debug.Log(doc.DocumentElement.ChildNodes[i].OuterXml);
					}
				}
				if (found >-1){
					doc.DocumentElement.RemoveChild(doc.DocumentElement.ChildNodes[found]);
				}
		 }

		 partie = doc.CreateElement("partie");
		 partie.SetAttribute("name","SampleRun-Automatic-Temporary-Save");
		 DateTime thisDay = DateTime.Today;
		 partie.SetAttribute("date",thisDay.ToString("dd/MM/yyyy"));
		 
		 
		mort = false;
		timecompteur = 0;
		DeathTime = 0;
		StopTime = 0;

		tabad = GetComponents<AudioSource> ();
		Drum = tabad[0]; 
		Piano = tabad [1];
		Basse = tabad[2];
		Guitare = tabad[3]; 
		Melodie = tabad[4];
		Synthe  = tabad[5];
		clips = new AudioClip[6];
		changeClips = new bool[6];
		for (int w = 0; w<6; w++){
			changeClips[w]=false;
			clips[w]=null;
		}

		perso_vie = 3;
		position = 0;
		Instantiate (vie, new Vector3 (-7f, -4.7f, -5), Quaternion.Euler(20,0,0));
		Instantiate (vie, new Vector3 (-10f, -4.7f, -5),  Quaternion.Euler(20,0,0));
		Instantiate (vie, new Vector3 (-8.5f, -4.7f, -5),  Quaternion.Euler(20,0,0));

		batterie = GameObject.FindGameObjectWithTag("batterie").GetComponent<SpriteRenderer>();
		piano = GameObject.FindGameObjectWithTag("piano").GetComponent<SpriteRenderer>();
		basse = GameObject.FindGameObjectWithTag("basse").GetComponent<SpriteRenderer>();
		guitare = GameObject.FindGameObjectWithTag("guitare").GetComponent<SpriteRenderer>();
		melodie = GameObject.FindGameObjectWithTag("melodie").GetComponent<SpriteRenderer>();
		synthe = GameObject.FindGameObjectWithTag("Synthe").GetComponent<SpriteRenderer>();
		
		compteurRecalage = 0;
	}

	public void FinPause(){
		GameObject.Find("Pause").GetComponent<CanvasGroup>().interactable = false;
		GameObject.Find("Pause").GetComponent<CanvasGroup>().alpha = 0;
		GameObject.Find("Horloge").GetComponent<CanvasGroup>().alpha = 1;
		Time.timeScale = 1;		
		Drum.UnPause(); 
		Piano.UnPause();
		Basse.UnPause();
		Guitare.UnPause();
		Melodie.UnPause();
		Synthe.UnPause();
		isPaused = false;
		Cursor.lockState = CursorLockMode.Locked;
		
	}
	
	void Update () {
		for (int c = 0; c<6; c++){
			if (changeClips[c]==true){
				if (clips[c]!=null){
					switch (c){
						case 0 : 
							if (Drum.time <= 0.03 || (Drum.time >= 7.97 && Drum.time <= 8.03) || (Drum.time >= 15.97 && Drum.time <= 16.03)){
								if (Mute[0]){
									Drum.mute=true;
								}
								else {
									Drum.mute=false;
									Drum.clip = clips[c];
									Drum.Play();
									changeClips[c] = false;
									compteurRecalage++;
								}
							}
							break;
						case 1 :
							if (Piano.time <= 0.03 || (Piano.time >= 7.97 && Piano.time <= 8.03) || (Piano.time >= 15.97 && Piano.time <= 16.03)){
								if (Mute[1]){
									Piano.mute=true;
								}
								else {
									Piano.mute=false;
									Piano.clip = clips[c];
									Piano.Play();
									changeClips[c] = false;
									compteurRecalage++;
								}
							}
							break;
						case 2 : 
							if (Basse.time <= 0.03 || (Basse.time >= 7.97  && Basse.time <= 8.03) || (Basse.time >= 15.97 && Basse.time <= 16.03)){
								if (Mute[2]){
									Basse.mute=true;
								}
								else {
									Basse.mute = false;
									Basse.clip = clips[c];
									Basse.Play();
									changeClips[c] = false;
									compteurRecalage++;
								}
							}
							break;
						case 3 : 
							if (Guitare.time <= 0.03 || (Guitare.time >= 7.97  && Guitare.time <= 8.03) || (Guitare.time >= 15.97 && Guitare.time <= 16.03)){
								if (Mute[3]){
									Guitare.mute=true;
								}
								else {
									Guitare.mute = false;
									Guitare.clip = clips[c];
									Guitare.Play();
									changeClips[c] = false;
									compteurRecalage++;
								}
							}
							break;
						case 4 : 
							if (Melodie.time <= 0.03 || (Melodie.time >= 7.97  && Melodie.time <= 8.03) || (Melodie.time >= 15.97 && Melodie.time <= 16.03)){
								if (Mute[4]){
									Melodie.mute=true;
								}
								else {
									Melodie.mute=false;
									Melodie.clip = clips[c];
									Melodie.Play();
									changeClips[c] = false;
									compteurRecalage++;
								}
							}
							break;
						case 5 : 
							if (Synthe.time <= 0.03 || (Synthe.time >= 7.97  && Synthe.time <= 8.03) || (Synthe.time >= 15.97 && Synthe.time <= 16.03)){
								if (Mute[5]){
									Synthe.mute=true;
								}
								else {
									Synthe.mute=false;
									Synthe.clip = clips[c];
									Synthe.Play();
									changeClips[c] = false;
									compteurRecalage++;
								}
							}
							break;
					}
					if (compteurRecalage==4){
						Drum.time=0;
						Piano.time=0;
						Basse.time=0;
						Guitare.time=0;
						Melodie.time=0;
						Synthe.time=0;
						compteurRecalage = 0;
					}
				}
			}
			else{
				
			}
		}
		
		
		if (invincible){
			if (Time.time>=clignote && isClignote <= 10){
				clignote+=0.1f;
				if (isClignote%2 == 0){
					Afficher();
				}
				else {
					Effacer();
				}
				if (isClignote == 10){
					invincible = false;
				}
				isClignote ++;
				
			}
		}
		move = transform.position;
		if (DeathTime > 0){
			DeathTime+=Time.deltaTime;
		}
		if (DeathTime > 0 && DeathTime >= StopTime){
			//SceneManager.LoadScene("GameOver"); 
			GameObject.Find("Perso").GetComponent<Animator>().SetBool("mort",true);
		}
		
		if (Input.GetKeyDown (KeyCode.Escape) && !isPaused){
			isPaused = true;
			Time.timeScale = 0;		
			Drum.Pause(); 
			Piano.Pause();
			Basse.Pause();
			Guitare.Pause();
			Melodie.Pause();	
			Synthe.Pause();		
			GameObject.Find("Horloge").GetComponent<CanvasGroup>().alpha = 0.3f;
			GameObject.Find("Pause").GetComponent<CanvasGroup>().alpha = 1;
			GameObject.Find("Pause").GetComponent<CanvasGroup>().interactable = true;
			Cursor.lockState = CursorLockMode.None;
		}
		else if (Input.GetKeyDown (KeyCode.Escape) && isPaused){
			FinPause();
		}

		if (Input.GetKeyDown (KeyCode.UpArrow) && TouchSol && !tombe && !isPaused) {
			switch (position) {
			case 0:
				break;

			case 1:
				move.z = -1;
				position = 0;
				break;

			case 2:
				move.z = -2;
				position = 1;
				break;

			case 3:
				move.z = -3;
				position = 2;
				break;

			default :
				break; 

			}
		}


		if (Input.GetKeyDown (KeyCode.DownArrow) && TouchSol && !tombe && !isPaused) {
			switch (position) {

			case 0:
				move.z = -2;
				position = 1; 
				break; 
			case 1:
				move.z = -3;
				position = 2;
				break; 
			case 2:
				move.z = -4;
				position = 3;

				break;
			case 3:
				break; 
			default :
				break;
			}
		}
		transform.position = move; 
		GameObject.Find("Ombre").transform.position = new Vector3(-9f,-2.84f,move.z);
		GetComponent<SpriteRenderer>().sortingOrder = 2*(int)(-move.z)-1;


		if (Input.GetKey (KeyCode.Space) && TouchSol == true && !isPaused) {
			TouchSol = false; 
			Saut = true; 
			GetComponent<Animator>().SetBool("saut",true);
			GameObject.Find("Ombre").GetComponent<Animator>().SetBool("saut",true);


		} 

	}
	
	void OnTriggerEnter(Collider col)
    {
		if (col.tag == "trou" /*&& col.transform.localPosition.x>=11*/){
			GetComponent<Rigidbody>().isKinematic = false;
			mort = true;
			tombe = true;
			/*
			if(doc.DocumentElement.HasChildNodes){
				doc.DocumentElement.InsertBefore(partie, doc.DocumentElement.FirstChild);
			}
			else {
				doc.DocumentElement.AppendChild(partie);
			}*/
			doc.DocumentElement.PrependChild(partie);
			using(TextWriter sw = new StreamWriter(Application.persistentDataPath + "/sauv.xml", false, Encoding.UTF8)) //Set encoding
			{
				doc.Save(sw);
				DeathTime = Time.time;
			  StopTime = DeathTime + 0.5f;
			}
			//GetComponent<Collider>().isTrigger = true;
		}
		if (col.tag == "sortie"){
			if (touchePortail == false){
				sequence = doc.CreateElement("sequence");
			  sequence.SetAttribute("time",timecompteur.ToString());
				node = doc.CreateElement("instrument");
				node.SetAttribute("type","none");
				node.SetAttribute("clip","");
				node.SetAttribute("src","");
				sequence.AppendChild(node);
				partie.AppendChild(sequence);
			}
			timecompteur+=8;
			touchePortail = false;
		}
	}


	void OnCollisionEnter(Collision c){
		if (c.gameObject.tag == "sol" && mort == true){
			c.gameObject.GetComponent<BoxCollider>().enabled = false;
		}
		//TouchSol = true;
		if ((c.gameObject.tag == "TRouge" ||c.gameObject.tag == "TBleu" ||c.gameObject.tag == "TJaune" ||c.gameObject.tag == "TVert") && touchePortail==false){
			 sequence = doc.CreateElement("sequence");
			 sequence.SetAttribute("time",timecompteur.ToString());
			 touchePortail = true;
			 			
			instru = SolManager.instru;
			if (instruCheck == instru) 
			{
				
			} 
			else if (transform.position.y == -0.4f)
			{
				
				instruCheck = instru; 
				Quaternion rot = Quaternion.identity;
				rot.eulerAngles = new Vector3(-90,0,0);
				Instantiate(rays, c.transform.position, rot ,c.transform);
				switch (instru) {
				case 0:
					node = doc.CreateElement("instrument");
					node.SetAttribute("type","drum1");
					if (c.gameObject.tag == portails[0]){
						node.SetAttribute("clip","");
						node.SetAttribute("src","");
						Mute[0] = true;
						batterie.color = new Color(1,1,1,1);
						portails[0]="";
					}
					else {
						Mute[0] = false;
						portails[0] = c.gameObject.tag; 
						if (c.gameObject.tag == "TRouge") {
							batterie.color = new Color(1,0.1f,0.1f,1);
							clips[0] = D11; 
							node.SetAttribute("clip","D11");
							node.SetAttribute("src","D11.wav");
						} else if (c.gameObject.tag == "TBleu") {
							batterie.color = new Color(0.2f,0.5f,1,1);
							clips[0] = D12;
							node.SetAttribute("clip","D12");
							node.SetAttribute("src","D12.wav");
						} else if (c.gameObject.tag == "TVert") {
							batterie.color = new Color(0.2f,1,0.2f,1);
							clips[0] = D13; 
							node.SetAttribute("clip","D13");
							node.SetAttribute("src","D13.wav");
						} else if (c.gameObject.tag == "TJaune") {
							batterie.color = new Color(1,0.8f,0.2f,1);
							clips[0] = D14;
							node.SetAttribute("clip","D14");
							node.SetAttribute("src","D14.wav");
						}
					}
					sequence.AppendChild(node);
					changeClips[0]=true;
					break; 

				case 1:
					node = doc.CreateElement("instrument");
					node.SetAttribute("type","piano");
					if (c.gameObject.tag == portails[1]){
						Mute[1] = true;
						node.SetAttribute("clip","");
						node.SetAttribute("src","");
						piano.color = new Color(1,1,1,1);
						portails[1]="";
					}
					else{
						Mute[1] = false;
						portails[1] = c.gameObject.tag;
						if (c.gameObject.tag == "TRouge") {
							piano.color = new Color(1,0.1f,0.1f,1);
							clips[1] = P1; 
							node.SetAttribute("clip","P1");
							node.SetAttribute("src","P1.wav");
						} else if (c.gameObject.tag == "TBleu") {
							piano.color = new Color(0.1f,0.5f,1,1);
							clips[1] = P2; 
							node.SetAttribute("clip","P2");
							node.SetAttribute("src","P2.wav");
						} else if (c.gameObject.tag == "TVert") {
							piano.color = new Color(0.2f,1,0.2f,1);
							clips[1] = P3; 
							node.SetAttribute("clip","P3");
							node.SetAttribute("src","P3.wav");
						} else if (c.gameObject.tag == "TJaune") {
							piano.color = new Color(1,0.8f,0.2f,1);
							clips[1] = P4; 
							node.SetAttribute("clip","P4");
							node.SetAttribute("src","P4.wav");
						}
					}
					sequence.AppendChild(node);
					changeClips[1]=true;
					break; 

				case 2:
					node = doc.CreateElement("instrument");
					node.SetAttribute("type","basse");
					if (c.gameObject.tag == portails[2]){
						Mute[2] = true;
						node.SetAttribute("clip","");
						node.SetAttribute("src","");
						basse.color = new Color(1,1,1,1);
						portails[2] = "";
					}
					else {
						Mute[2] = false;
						portails[2] = c.gameObject.tag;
						if (c.gameObject.tag == "TRouge") {
							basse.color = new Color(1,0.1f,0.1f,1);
							clips[2] = B1; 
							node.SetAttribute("clip","B1");
							node.SetAttribute("src","B1.wav");
						} else if (c.gameObject.tag == "TBleu") {
							basse.color = new Color(0.1f,0.5f,1,1);
							clips[2] = B2; 
							node.SetAttribute("clip","B2");
							node.SetAttribute("src","B2.wav");
						} else if (c.gameObject.tag == "TVert") {
							basse.color = new Color(0.2f,1,0.2f,1);
							clips[2] = B3; 
							node.SetAttribute("clip","B3");
							node.SetAttribute("src","B3.wav");
						} else if (c.gameObject.tag == "TJaune") {
							basse.color = new Color(1,0.8f,0.2f,1);
							clips[2] = B4; 
							node.SetAttribute("clip","B4");
							node.SetAttribute("src","B4.wav");
						}
					}
					sequence.AppendChild(node);
					changeClips[2]=true;
					break; 

				case 3:
					node = doc.CreateElement("instrument");
					node.SetAttribute("type","guitare");
					if (c.gameObject.tag == portails[3]){
						Mute[3] = true;
						node.SetAttribute("clip","");
						node.SetAttribute("src","");
						guitare.color = new Color(1,1,1,1);
						portails[3] = "";
					}	
					else {
						Mute[3] = false;
						portails[3] = c.gameObject.tag;
						if (c.gameObject.tag == "TRouge") {
							guitare.color = new Color(1,0.1f,0.1f,1);
							clips[3] = G1; 
							node.SetAttribute("clip","G1");
							node.SetAttribute("src","G1.wav");
						} else if (c.gameObject.tag == "TBleu") {
							guitare.color = new Color(0.1f,0.5f,1,1);
							clips[3] = G2; 
							node.SetAttribute("clip","G2");
							node.SetAttribute("src","G2.wav");
						} else if (c.gameObject.tag == "TVert") {
							guitare.color = new Color(0.2f,1,0.2f,1);
							clips[3] = G3; 
							node.SetAttribute("clip","G3");
							node.SetAttribute("src","G3.wav");
						} else if (c.gameObject.tag == "TJaune") {
							guitare.color = new Color(1,0.8f,0.2f,1);
							clips[3] = G4; 
							node.SetAttribute("clip","G4");
							node.SetAttribute("src","G4.wav");
						}
					}
					sequence.AppendChild(node);
					changeClips[3]=true;
					break; 

				case 4:
					node = doc.CreateElement("instrument");
					node.SetAttribute("type","melodie");
					if (c.gameObject.tag == portails[4]){
						Mute[4] = true;
						node.SetAttribute("clip","");
						node.SetAttribute("src","");
						melodie.color = new Color(1,1,1,1);
						portails[4]="";
					}	
					else {
						Mute[4] = false;
						portails[4] = c.gameObject.tag;
						if (c.gameObject.tag == "TRouge") {
							melodie.color = new Color(1,0.1f,0.1f,1);
							clips[4] = M1; 
							node.SetAttribute("clip","M1");
							node.SetAttribute("src","M1.wav");
						} else if (c.gameObject.tag == "TBleu") {
							melodie.color = new Color(0.1f,0.5f,1,1);
							clips[4] = M2; 
							node.SetAttribute("clip","M2");
							node.SetAttribute("src","M2.wav");
						} else if (c.gameObject.tag == "TVert") {
							melodie.color = new Color(0.2f,1,0.2f,1);
							clips[4] = M3; 
							node.SetAttribute("clip","M3");
							node.SetAttribute("src","M3.wav");
						} else if (c.gameObject.tag == "TJaune") {
							melodie.color = new Color(1,0.8f,0.2f,1);
							clips[4] = M4; 
							node.SetAttribute("clip","M4");
							node.SetAttribute("src","M4.wav");
						}
					}
					sequence.AppendChild(node);
					changeClips[4]=true;
					break; 
					
					case 5:
					node = doc.CreateElement("instrument");
					node.SetAttribute("type","synthe");
					if (c.gameObject.tag == portails[5]){
						Mute[5] = true;
						node.SetAttribute("clip","");
						node.SetAttribute("src","");
						synthe.color = new Color(1,1,1,1);
						portails[5]="";
					}	
					else {
						Mute[5] = false;
						portails[5] = c.gameObject.tag;
						if (c.gameObject.tag == "TRouge") {
							synthe.color = new Color(1,0.1f,0.1f,1);
							clips[5] = S1; 
							node.SetAttribute("clip","S1");
							node.SetAttribute("src","S1.wav");
						} else if (c.gameObject.tag == "TBleu") {
							synthe.color = new Color(0.1f,0.5f,1,1);
							clips[5] = S2; 
							node.SetAttribute("clip","S2");
							node.SetAttribute("src","S2.wav");
						} else if (c.gameObject.tag == "TVert") {
							synthe.color = new Color(0.2f,1,0.2f,1);
							clips[5] = S3; 
							node.SetAttribute("clip","S3");
							node.SetAttribute("src","S3.wav");
						} else if (c.gameObject.tag == "TJaune") {
							synthe.color = new Color(1,0.8f,0.2f,1);
							clips[5] = S4; 
							node.SetAttribute("clip","S4");
							node.SetAttribute("src","S4.wav");
						}
					}
					sequence.AppendChild(node);
					changeClips[5]=true;
					break; 

				}
				partie.AppendChild(sequence);
				c.gameObject.tag="Finish";
			}
		}
		Saut = false; 
	}

}
