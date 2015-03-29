using UnityEngine;
using System.Collections;



public class MoveCamera : MonoBehaviour 
{

	public GameObject cible;


	void Start () 
    {

	}
	

	void Update () 
    {
    	if(cible != null) {
    	
			Vector3 mouvement = this.transform.position;
			mouvement.x = cible.transform.position.x;
			mouvement.z = cible.transform.position.z - 10;
			mouvement.y = cible.transform.position.y + 15;
			
			this.transform.position = mouvement;
    	
    	}
		

	}
}
