using UnityEngine;
using System.Collections;

public class MovePerso : MonoBehaviour {

	public GameObject perso;
	private CharacterController controller;
	public float vitesse;
	public float vitesseRotation;

	// Use this for initialization
	void Start () {

		if(vitesse == 0) {vitesse=10;Debug.Log ("Utilisation valeur par défaut");}
		if(vitesseRotation == 0) {vitesseRotation=10;Debug.Log ("Utilisation valeur par défaut");}

		controller = perso.GetComponents<CharacterController>()[0];

	}
	
	// Update is called once per frame
	void Update () {

		Vector3 mouvement;
		mouvement.x = 0;
		mouvement.y = 0;
		mouvement.z = 0;



		// Code mouvement pour les tests
		if(Input.GetKey(KeyCode.RightArrow) || Input.GetKey (KeyCode.D)) {
			mouvement.x += vitesse;
			controller.transform.rotation = Quaternion.Slerp(controller.transform.rotation, Quaternion.LookRotation(Vector3.right), vitesseRotation*Time.deltaTime);
			 
		} 
		if(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey (KeyCode.Q)) {
			
			mouvement.x -= vitesse;
			controller.transform.rotation = Quaternion.Slerp(controller.transform.rotation, Quaternion.LookRotation(Vector3.left), vitesseRotation*Time.deltaTime);


		} 
		if(Input.GetKey(KeyCode.UpArrow) || Input.GetKey (KeyCode.Z)) {
			
			mouvement.z += vitesse;
			controller.transform.rotation = Quaternion.Slerp(controller.transform.rotation, Quaternion.LookRotation(Vector3.forward), vitesseRotation*Time.deltaTime);

		}
		if(Input.GetKey(KeyCode.DownArrow) || Input.GetKey (KeyCode.S)) {
			
			mouvement.z -= vitesse;
			controller.transform.rotation = Quaternion.Slerp(controller.transform.rotation, Quaternion.LookRotation(Vector3.back), vitesseRotation*Time.deltaTime);

		} 

		controller.Move(mouvement*vitesse*Time.deltaTime);
	


	
	}

}