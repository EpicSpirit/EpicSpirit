using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

namespace EpicSpirit.Game 
{
    public class UIAction : MonoBehaviour 
    {
        Button _button;
        public int _indice;
        Action _action;
        internal Character _target;
        bool _isSkillEnabled;
        UISkill _uis;
        UIItem _itemCounter;
        bool _isActive;
        Image _image;
        UIManager _UIManager;
        Text _text;
            
        public void Awake ()
        {
            _UIManager = this.GetComponentInParent<UIManager>();
            _target = GameObject.FindWithTag( "Player" ).GetComponent<Character>();
            _image = gameObject.AddComponent<Image>();
            _button = gameObject.AddComponent<Button>();


            ActivateButton();
        }

        public void OnEnable()
        {
            ActivateButton();
        }

        private void ChargeExtension(GameObject go)
        {
            var extension = GameObject.Instantiate( go );
            extension.transform.parent = this.transform;
            _text = extension.GetComponent<Text>();
        }

        public void ActivateButton()
        {
            _isActive = true;
            _isActionEnabled = true;
            _action = _target.GetAttack( _indice );

            if ( _action.Name == "" || _action.GetSprite == null)
            {
                _UIManager.DisableAction( this );
                return;
            }

            // Si l'on a à faire à un Item, on lui rajoute ton composant de texte via un prefab
            if(_action is Item)
                ChargeExtension( _UIManager.refItemCount );
            

            if(_action is Skill)
                ChargeExtension( _UIManager.refSkillCount );
            

            _image.color = new Color( 1, 1, 1, 1 );
            _button.image.overrideSprite = _action.GetSprite;
            _button.onClick.AddListener( () =>
            {
                _target.Attack( _indice );

                if ( _action is Skill )
                {
                    StartCoolDown();
                    Invoke( "Enable", _target.GetAttack( _indice ).CoolDown );
                }
                else if(_action is Item)
                {
                    UpdateCount();
                }
            } );
            
        }

        #region Skill
        void StartCoolDown()
        {
            RunCoolDown();
            _isSkillEnabled = false;
        }

        void FinishCoolDown ()
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
        #endregion

        #region Item
        public void UpdateCount ()
        {
            if ( _action is Item )
                _text.text = ( ( Item ) _action ).Quantity.ToString();
            else
                Debug.LogException( new Exception("Can't update count of non-item"), _action );
        }

        #endregion

        public bool _isActionEnabled { get; set; }
    }
}