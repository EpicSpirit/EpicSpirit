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
        public bool isSkillEnabled;
        
            
        public void Awake ()
        {
            isSkillEnabled = true;
            gameObject.AddComponent<Image>();
            _button =gameObject.AddComponent<Button>();
        }

	    void Start () 
        {
            if ( target.GetAttack( _indice ).GetSprite != null )
            {
                _button = this.GetComponent<Button>();
                _button.image.overrideSprite = target.GetAttack( _indice ).GetSprite;
                _button.onClick.AddListener( () => {
                    if ( isSkillEnabled )
                    {
                        Disable();
                        target.Attack( _indice );
                        Invoke( "Enable", target.GetAttack( _indice ).CoolDown );
                    }
                } );

                this.enabled = false;
               
            }            
	    }
	    void Disable()
        {
            isSkillEnabled = false;
        }
        void Enable ()
        {

            isSkillEnabled = true;
        }
    }
}