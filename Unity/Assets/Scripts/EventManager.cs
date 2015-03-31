using UnityEngine;
using System.Collections;

public class EventManager : MonoBehaviour
{
    bool mouseDown;

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {

	}

    public void OnMouseDown()
    {
        Debug.Log( "aaaa" );
        mouseDown = true;
    }

}
