using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace EpicSpirit.Game 
{
    public class UIAction : MonoBehaviour 
    {

        Button _button;
        public int _indice;
        public Character target;
        public bool a;
        
            
        public void Awake ()
        {
            a = true;
            gameObject.AddComponent<Image>();
            b=gameObject.AddComponent<Button>();
        }

	    void Start () 
        {
            if ( target.GetAttack( _indice ).GetSprite != null )
            {
                _button = this.GetComponent<Button>();
                _button.image.overrideSprite = target.GetAttack( _indice ).GetSprite;
                _button.onClick.AddListener( () => target.Attack( _indice ) );

                this.enabled = false;
               
            }            
	    }
	    void Disable()
        {
            Debug.Log( "Disable :D" );
        }
        void Enable ()
        {
            Debug.Log( "Enable :D" );
        }
    }
}