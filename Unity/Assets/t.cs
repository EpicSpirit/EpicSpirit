using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace EpicSpirit.Game {
    public class t : MonoBehaviour {
        // ouuhh le truc dégueulasse !
        Button b;
        public int _indice;
        public Character target;
        public bool a;
        
            
        void Start()
        {
            a = true;
            gameObject.AddComponent<Image>();
            b=gameObject.AddComponent<Button>();
        }

	    void Update()
        {
            if ( a )
            {
                a = false;
                if ( target.GetAttack( _indice ).GetSprite != null )
                {
                    b.image.overrideSprite = target.GetAttack( _indice ).GetSprite;
                    b.onClick.AddListener( () => {
                        Disable();
                        target.Attack( _indice ); 
                        Invoke( "Enable", target.GetAttack( _indice )._cooldown ); 
                    } );
                }
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