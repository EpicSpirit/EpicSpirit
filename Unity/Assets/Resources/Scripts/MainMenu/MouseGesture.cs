using UnityEngine;
using System.Collections;

public class MouseGesture : MonoBehaviour 
{
    //MeshRenderer text;
	void Start () 
    {
       // text = GetComponent<MeshRenderer>();
	}
	
	void Update () 
    {
	   //text.material.color = Color.green;
	}
    public void OnMouseEnter()
    {
        Debug.Log("aaaaaaa");
    }
    public void OnMouseUp()
    {
        Debug.Log( "bbbbb" );
        Application.LoadLevel( "Scene1" );
    }
}
