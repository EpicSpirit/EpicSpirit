using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FPSDisplay : MonoBehaviour 
{
    Text _text;
    int _count;
    void Awake()
    {
        _text = gameObject.AddComponent<Text>();
        _text.fontSize = 56;
        _text.fontStyle = FontStyle.Bold;
        _text.font = (Font)Resources.Load( "UI/BLKCHCRY" );
        _text.color = Color.black;

        _count = 0;
    }
	void Update () 
    {
        _count++;
        if ( _count > 10 )
            DisplayFPS();
	}
    private void DisplayFPS()
    {
        _text.text = ((int)( 1 / Time.deltaTime )).ToString();
        _count = 0;
    }
}
