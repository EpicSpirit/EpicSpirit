using UnityEngine;
using System.Collections;

public class playAnim : MonoBehaviour {

	// Use this for initialization
	void Start () {
		this.GetComponent<Animation>().Play("Take 001");
		this.GetComponent<Animation>().playAutomatically = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
