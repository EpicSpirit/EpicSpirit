using UnityEngine;
using System.Collections;

public class ButtonManager : MonoBehaviour 
{
    bool mouseDown;

	// Use this for initialization
	void Start ()
    {
        mouseDown = false;
	}
	
	// Update is called once per frame
	void Update () 
    {
        if ( mouseDown == true )
        {
            mouseDown = false;
            Debug.Log( "aaaa" );
            SendMessage( "ToAttack" );
        }
	}
    public void OnClick()
    {
        Debug.Log( "fnefhrefne" );
    }
    public void Test()
    {
        Debug.Log( "Test" );
    }
    public void PointerClick()
    {
        Debug.Log( "LA" );
    }

}
