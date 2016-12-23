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
	WWW XMLFile;

	private AudioSource[] tabad; 

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
	
	int timecompteur;
	
	
	void Start () {
		 tombe = false;
		 doc = new XmlDocument();
		 doc.Load(Application.persistentDataPath + "/sauv.xml");

		 partie = doc.CreateElement("partie");
		 partie.SetAttribute("name","CurrentGame");
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
	}


	void Update () {
		move = transform.position;
		if (tombe){
			GetComponent<Rigidbody>().isKinematic = false;
			//transform.position += new Vector3(10f*Time.deltaTime, -5f*Time.deltaTime, 0f);
		}
		if (DeathTime > 0){
			DeathTime+=Time.deltaTime;
		}
		if (DeathTime > 0 && DeathTime >= StopTime){
			//SceneManager.LoadScene("GameOver"); 
			GameObject.Find("Perso").GetComponent<Animator>().SetBool("mort",true);
		}

		if (Input.GetKeyDown (KeyCode.UpArrow) && TouchSol && !tombe) {
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


		if (Input.GetKeyDown (KeyCode.DownArrow) && TouchSol && !tombe) {
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


		if (Input.GetKey (KeyCode.Space) && TouchSol == true) {
			TouchSol = false; 
			Saut = true; 
			GetComponent<Animator>().SetBool("saut",true);
			GameObject.Find("Ombre").GetComponent<Animator>().SetBool("saut",true);


		} 

	}
	
	void OnTriggerEnter(Collider col)
    {
		if (col.tag == "trou" && col.transform.localPosition.x>=11){
			doc.DocumentElement.AppendChild(partie);
			
			using(TextWriter sw = new StreamWriter(Application.persistentDataPath + "/sauv.xml", false, Encoding.ASCII)) //Set encoding
			{
				doc.Save(sw);
			}
			mort = true;
			tombe = true;
			//GetComponent<Collider>().isTrigger = true;
			DeathTime = Time.time;
			StopTime = DeathTime + 0.5f;		
		}
	}


	void OnCollisionEnter(Collision c){
		//TouchSol = true;
		if (c.gameObject.tag == "TRouge" ||c.gameObject.tag == "TBleu" ||c.gameObject.tag == "TJaune" ||c.gameObject.tag == "TVert"){
			 sequence = doc.CreateElement("sequence");
			 sequence.SetAttribute("time",timecompteur.ToString());
			 			
			instru = SolManager.instru;
			if (instruCheck == instru) 
			{
				
			} 
			else 
			{
				
				instruCheck = instru; 
				if (transform.position.y == -0.4f && c.transform.position.x > -8.4f) {
					Quaternion rot = Quaternion.identity;
					rot.eulerAngles = new Vector3(-90,0,0);
					Instantiate(rays, c.transform.position, rot ,c.transform);
					switch (instru) {

					case 0:
						if (c.gameObject.tag == "TRouge") {
							batterie.color = new Color(1,0.1f,0.1f,1);
							Drum.clip = D11; 
							node = doc.CreateElement("instrument");
							node.SetAttribute("type","drum1");
							node.SetAttribute("clip","D11");
							node.SetAttribute("src","D11.wav");
							sequence.AppendChild(node);
						} else if (c.gameObject.tag == "TBleu") {
							batterie.color = new Color(0.2f,0.5f,1,1);
							Drum.clip = D12;
							node = doc.CreateElement("instrument");
							node.SetAttribute("type","drum1");
							node.SetAttribute("clip","D12");
							node.SetAttribute("src","D12.wav");
							sequence.AppendChild(node);							
						} else if (c.gameObject.tag == "TVert") {
							batterie.color = new Color(0.2f,1,0.2f,1);
							Drum.clip = D13; 
							node = doc.CreateElement("instrument");
							node.SetAttribute("type","drum1");
							node.SetAttribute("clip","D13");
							node.SetAttribute("src","D13.wav");
							sequence.AppendChild(node);
						} else if (c.gameObject.tag == "TJaune") {
							batterie.color = new Color(1,0.8f,0.2f,1);
							Drum.clip = D14;
							node = doc.CreateElement("instrument");
							node.SetAttribute("type","drum1");
							node.SetAttribute("clip","D14");
							node.SetAttribute("src","D14.wav");
							sequence.AppendChild(node);							
						}
						Drum.Play ();
						break; 

					case 1:
						if (c.gameObject.tag == "TRouge") {
							piano.color = new Color(1,0.1f,0.1f,1);
							Piano.clip = P1; 
							node = doc.CreateElement("instrument");
							node.SetAttribute("type","piano");
							node.SetAttribute("clip","P1");
							node.SetAttribute("src","P1.wav");
							sequence.AppendChild(node);
						} else if (c.gameObject.tag == "TBleu") {
							piano.color = new Color(0.1f,0.5f,1,1);
							Piano.clip = P2; 
							node = doc.CreateElement("instrument");
							node.SetAttribute("type","piano");
							node.SetAttribute("clip","P2");
							node.SetAttribute("src","P2.wav");
							sequence.AppendChild(node);
						} else if (c.gameObject.tag == "TVert") {
							piano.color = new Color(0.2f,1,0.2f,1);
							Piano.clip = P3; 
							node = doc.CreateElement("instrument");
							node.SetAttribute("type","piano");
							node.SetAttribute("clip","P3");
							node.SetAttribute("src","P3.wav");
							sequence.AppendChild(node);
						} else if (c.gameObject.tag == "TJaune") {
							piano.color = new Color(1,0.8f,0.2f,1);
							Piano.clip = P4; 
							node = doc.CreateElement("instrument");
							node.SetAttribute("type","piano");
							node.SetAttribute("clip","P4");
							node.SetAttribute("src","P4.wav");
							sequence.AppendChild(node);
						}
						Piano.Play ();
						break; 

					case 2:
						if (c.gameObject.tag == "TRouge") {
							basse.color = new Color(1,0.1f,0.1f,1);
							Basse.clip = B1; 
							node = doc.CreateElement("instrument");
							node.SetAttribute("type","basse");
							node.SetAttribute("clip","B1");
							node.SetAttribute("src","B1.wav");
							sequence.AppendChild(node);
						} else if (c.gameObject.tag == "TBleu") {
							basse.color = new Color(0.1f,0.5f,1,1);
							Basse.clip = B2; 
							node = doc.CreateElement("instrument");
							node.SetAttribute("type","basse");
							node.SetAttribute("clip","B2");
							node.SetAttribute("src","B2.wav");
							sequence.AppendChild(node);
						} else if (c.gameObject.tag == "TVert") {
							basse.color = new Color(0.2f,1,0.2f,1);
							Basse.clip = B3; 
							node = doc.CreateElement("instrument");
							node.SetAttribute("type","basse");
							node.SetAttribute("clip","B3");
							node.SetAttribute("src","B3.wav");
							sequence.AppendChild(node);
						} else if (c.gameObject.tag == "TJaune") {
							basse.color = new Color(1,0.8f,0.2f,1);
							Basse.clip = B4; 
							node = doc.CreateElement("instrument");
							node.SetAttribute("type","basse");
							node.SetAttribute("clip","B4");
							node.SetAttribute("src","B4.wav");
							sequence.AppendChild(node);
						}
						Basse.Play ();
						break; 

					case 3:
						if (c.gameObject.tag == "TRouge") {
							guitare.color = new Color(1,0.1f,0.1f,1);
							Guitare.clip = G1; 
							node = doc.CreateElement("instrument");
							node.SetAttribute("type","guitare");
							node.SetAttribute("clip","G1");
							node.SetAttribute("src","G1.wav");
							sequence.AppendChild(node);
						} else if (c.gameObject.tag == "TBleu") {
							guitare.color = new Color(0.1f,0.5f,1,1);
							Guitare.clip = G2; 
							node = doc.CreateElement("instrument");
							node.SetAttribute("type","guitare");
							node.SetAttribute("clip","G2");
							node.SetAttribute("src","G2.wav");
							sequence.AppendChild(node);
						} else if (c.gameObject.tag == "TVert") {
							guitare.color = new Color(0.2f,1,0.2f,1);
							Guitare.clip = G3; 
							node = doc.CreateElement("instrument");
							node.SetAttribute("type","guitare");
							node.SetAttribute("clip","G3");
							node.SetAttribute("src","G3.wav");
							sequence.AppendChild(node);
						} else if (c.gameObject.tag == "TJaune") {
							guitare.color = new Color(1,0.8f,0.2f,1);
							Guitare.clip = G4; 
							node = doc.CreateElement("instrument");
							node.SetAttribute("type","guitare");
							node.SetAttribute("clip","G4");
							node.SetAttribute("src","G4.wav");
							sequence.AppendChild(node);
						}
						Guitare.Play ();
						break; 

					case 4:
						if (c.gameObject.tag == "TRouge") {
							melodie.color = new Color(1,0.1f,0.1f,1);
							Melodie.clip = M1; 
							node = doc.CreateElement("instrument");
							node.SetAttribute("type","melodie");
							node.SetAttribute("clip","M1");
							node.SetAttribute("src","M1.wav");
							sequence.AppendChild(node);
						} else if (c.gameObject.tag == "TBleu") {
							melodie.color = new Color(0.1f,0.5f,1,1);
							Melodie.clip = M2; 
							node = doc.CreateElement("instrument");
							node.SetAttribute("type","melodie");
							node.SetAttribute("clip","M2");
							node.SetAttribute("src","M2.wav");
							sequence.AppendChild(node);
						} else if (c.gameObject.tag == "TVert") {
							melodie.color = new Color(0.2f,1,0.2f,1);
							Melodie.clip = M3; 
							node = doc.CreateElement("instrument");
							node.SetAttribute("type","melodie");
							node.SetAttribute("clip","M3");
							node.SetAttribute("src","M3.wav");
							sequence.AppendChild(node);
						} else if (c.gameObject.tag == "TJaune") {
							melodie.color = new Color(1,0.8f,0.2f,1);
							Melodie.clip = M4; 
							node = doc.CreateElement("instrument");
							node.SetAttribute("type","melodie");
							node.SetAttribute("clip","M4");
							node.SetAttribute("src","M4.wav");
							sequence.AppendChild(node);
						}
						Melodie.Play ();
						break; 

					}
					partie.AppendChild(sequence);
					c.gameObject.tag="Finish";
					timecompteur+=8;
				}
			}
		}
		Saut = false; 
	}

}
