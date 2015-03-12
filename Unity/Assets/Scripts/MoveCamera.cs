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
		Vector3 mouvement = cible.transform.position;
		mouvement.y += 5;
		mouvement.z -= 10;

		this.transform.position = mouvement;

	}
}
