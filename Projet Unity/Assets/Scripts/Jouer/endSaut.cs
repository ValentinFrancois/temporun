using UnityEngine;
using System.Collections;

public class endSaut : StateMachineBehaviour {
	float timer;
	int decalage;
	float pos;
	bool decalerdroite;
	bool decalergauche;
	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	 timer = Time.time + 0.4f;
	 decalage = 0;
	decalerdroite = true;
	decalergauche = true;
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		if(Input.GetKeyDown(KeyCode.UpArrow)){
			decalage+=1;
		}
		if(Input.GetKeyDown(KeyCode.DownArrow)){
			decalage-=1;
		}
		if (Time.time<=timer){
			GameObject.Find("Perso").transform.Translate(0,Time.deltaTime*10,0);
			pos = GameObject.Find("Perso").transform.position.z;
			if (decalage>0 && pos < -1 && decalergauche){
				GameObject.Find("Perso").transform.position+= new Vector3(0f,0f,1f); 
				GameObject.Find("Ombre").transform.position+= new Vector3(0f,0f,1f); 
				GameObject.Find("Perso").GetComponent<PersoController>().position-=1;
				decalage = 0;
				decalergauche = false;
			}
			else if (decalage<0 && pos > -4 && decalerdroite){
				GameObject.Find("Perso").transform.position+= new Vector3(0f,0f,-1f); 
				GameObject.Find("Ombre").transform.position+= new Vector3(0f,0f,-1f); 
				GameObject.Find("Perso").GetComponent<PersoController>().position+=1;
				decalage = 0;
				decalage = 0;
				decalerdroite = false;
			}			
		}
	}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		GameObject.Find("Perso").GetComponent<Animator>().SetBool("saut",false);
		GameObject.Find("Ombre").GetComponent<Animator>().SetBool("saut",false);
	}

	// OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
	//override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
	//override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}
}
