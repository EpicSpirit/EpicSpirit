using UnityEngine;
using System.Collections;

public class MovePerso : MonoBehaviour {

	public GameObject perso;
	private CharacterController controller;
	public float vitesse;

	// Use this for initialization
	void Start () {
	
		controller = perso.GetComponents<CharacterController>()[0];

	}
	
	// Update is called once per frame
	void Update () {

		Vector3 mouvement;
		mouvement.x = 0;
		mouvement.y = 0;
		mouvement.z = 0;

		// Code mouvement pour les tests
		if(Input.GetKey(KeyCode.RightArrow)) {
			mouvement.x += vitesse;

		} 
		if(Input.GetKey(KeyCode.LeftArrow)) {
			
			mouvement.x -= vitesse;
		} 
		if(Input.GetKey(KeyCode.UpArrow)) {
			
			mouvement.z += vitesse;
		}
		if(Input.GetKey(KeyCode.DownArrow)) {
			
			mouvement.z -= vitesse;
		} 

		//perso.transform.Translate(mouvement);
		controller.Move (controller.transform.TransformDirection(mouvement*vitesse*Time.deltaTime));



	
	}

}