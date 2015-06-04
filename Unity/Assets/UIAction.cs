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
        UISkill _uis;
            
        public void Awake ()
        {
            isSkillEnabled = true;
            gameObject.AddComponent<Image>();
            _button =gameObject.AddComponent<Button>();
            _uis = GetComponentInChildren<UISkill>();
            if ( _uis != null )
                _uis.enabled = false;
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
            }            
	    }
	    void Disable()
        {
            RunCoolDown();
            isSkillEnabled = false;
        }
        void Enable ()
        {
            isSkillEnabled = true;
        }

        void RunCoolDown ()
        {
            if ( _uis != null )
            {
                target.GetAttack( _indice ).StartCoolDown( ( uiSkill, ccd ) =>
                {
                    if ( ccd <=0 )
                    {
                        uiSkill._cooldown.text = "";
                    }
                    else
                    {
                        uiSkill._cooldown.text = ccd.ToString();
                    }
                    return true;
                }, _uis );
            }
        }
    }
}