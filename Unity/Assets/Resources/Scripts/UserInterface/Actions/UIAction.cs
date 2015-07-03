using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace EpicSpirit.Game 
{
    public class UIAction : MonoBehaviour 
    {

        Button _button;
        public int _indice;
        internal Character _target;
        bool _isSkillEnabled;
        UISkill _uis;
        GameObject _itemCounter;
        bool _isActive;
        Image _image;
            
        public void Awake ()
        {
            _isActive = true;
            _target = GameObject.FindWithTag( "Player" ).GetComponent<Character>();
            _isSkillEnabled = true;
            if ( _image == null && _button == null )
            {
                _image = gameObject.AddComponent<Image>();
                _button = gameObject.AddComponent<Button>();
            }
            _uis = GetComponentInChildren<UISkill>();
            if ( _uis != null )
                _uis.enabled = false;

            Action action = _target.GetAttack( _indice );
            if ( action.Name == "" )
            {
                _button.enabled = false ;
                _image.enabled = false;
                _isActive = false;
                if ( ( _itemCounter = GameObject.Find( "ItemCount" ) ) != null )
                {
                    _itemCounter.SetActive( false );
                }
            }
        }

	    public void Start () 
        {
            Debug.Log( "Start of "+this.name );
            if ( _target.GetAttack( _indice ).GetSprite != null && _isActive )
            {
                Debug.Log( "Set Button et tout" );
                _button.enabled = true;
                _image.enabled = true;
                _button = this.GetComponent<Button>();
                _button.image.overrideSprite = _target.GetAttack( _indice ).GetSprite;
                _button.onClick.AddListener( () => {
                    if ( _isSkillEnabled )
                    {
                        Disable();
                        _target.Attack( _indice );
                        Invoke( "Enable", _target.GetAttack( _indice ).CoolDown );
                    }
                } );               
            }            
	    }
	    void Disable()
        {
            RunCoolDown();
            _isSkillEnabled = false;
        }
        void Enable ()
        {
            _isSkillEnabled = true;
        }

        void RunCoolDown ()
        {
            if ( _uis != null )
            {
                _target.GetAttack( _indice ).StartCoolDown( ( uiSkill, ccd ) =>
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