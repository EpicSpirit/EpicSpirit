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
        if ( cible != null )
        {
		    Vector3 mouvement = this.transform.position;
            mouvement.x = cible.transform.position.x;
            mouvement.z = cible.transform.position.z - 8;

            this.transform.position = mouvement;
        }
	}
}
