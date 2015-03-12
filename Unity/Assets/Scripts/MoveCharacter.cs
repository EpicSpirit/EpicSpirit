using UnityEngine;
using System.Collections;

public class MoveCharacter : MonoBehaviour
{

	public GameObject perso;
	private CharacterController controller;
	public float speed;
	public float speedRotation;

	// Use this for initialization
	void Start () 
    {

		if(speed == 0)
        {
            speed = 10; Debug.Log( "Utilisation valeur par défaut" );
        }
		if(speedRotation == 0) 
        {
            speedRotation = 10; Debug.Log( "Utilisation valeur par défaut" );
        }

        controller = perso.GetComponents<CharacterController>()[0];

	}
	
	// Update is called once per frame
	void Update () 
    {

		Vector3 motion;
		motion.x = 0;
		motion.y = 0;
		motion.z = 0;



		// Code motion pour les tests
		if(Input.GetKey(KeyCode.RightArrow) || Input.GetKey (KeyCode.D))
        {
			motion.x += speed;
            controller.transform.rotation = Quaternion.Slerp( controller.transform.rotation, Quaternion.LookRotation( Vector3.right ), speedRotation * Time.deltaTime );
			 
		} 
		if(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey (KeyCode.Q))
        {
			
			motion.x -= speed;
            controller.transform.rotation = Quaternion.Slerp( controller.transform.rotation, Quaternion.LookRotation( Vector3.left ), speedRotation * Time.deltaTime );


		}
        if ( Input.GetKey( KeyCode.UpArrow ) || Input.GetKey( KeyCode.Z ) ) 
        {
			
			motion.z += speed;
            controller.transform.rotation = Quaternion.Slerp( controller.transform.rotation, Quaternion.LookRotation( Vector3.forward ), speedRotation * Time.deltaTime );

		}
        if ( Input.GetKey( KeyCode.DownArrow ) || Input.GetKey( KeyCode.S ) ) 
        {
			
			motion.z -= speed;
            controller.transform.rotation = Quaternion.Slerp( controller.transform.rotation, Quaternion.LookRotation( Vector3.back ), speedRotation * Time.deltaTime );

		}
		if(Input.GetKey (KeyCode.Space)) {

			Debug.Log ("Attaque !");
			Vector3 cone_centre;
			cone_centre.z = 2;
			cone_centre.x = 0;
			cone_centre.y = 0;
			Debug.DrawRay(controller.transform.position , controller.transform.TransformDirection(cone_centre));

			Vector3 cone_droite = cone_centre;
			cone_droite.x += 1;
			Debug.DrawRay(controller.transform.position , controller.transform.TransformDirection(cone_droite));

			Vector3 cone_gauche = cone_centre;
			cone_gauche.x -= 1;
			Debug.DrawRay(controller.transform.position , controller.transform.TransformDirection(cone_gauche));




		}

        controller.Move( motion * speed * Time.deltaTime );
	


	
	}

}