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
        bool _isActive;
        Image _image;
        UIManager _UIManager;
        Text _text;
        float _currentCoolDown;
            
        public void Awake ()
        {
            _UIManager = this.GetComponentInParent<UIManager>();
            _target = GameObject.FindWithTag( "Player" ).GetComponent<Character>();
            _image = gameObject.AddComponent<Image>();
            _button = gameObject.AddComponent<Button>();
        }

        public void OnEnable()
        {
            ActivateButton();
        }

        private void ChargeExtension(GameObject go)
        {
            // Création texte d'extension
            GameObject extension = GameObject.Instantiate( go, this.transform.position, Quaternion.identity ) as GameObject;
            extension.transform.parent = this.transform;
            _text = extension.GetComponent<Text>();

            // Si c'est un Item, on décale le texte et on met à jour le nb d'objet
            if(_action is Item)
            {
                extension.transform.Translate( new Vector3( 30, -30, 0 ) );
                UpdateCount();
            }
            else if(_action is Skill)
            {
                _text.fontSize = 25;
                _text.fontStyle = FontStyle.Bold;
                _text.font = Resources.Load<Font>( "UI/BLKCHCRY" );
                _text.resizeTextForBestFit = true;
                _text.alignment = TextAnchor.MiddleCenter;
            }

        }

        public void ActivateButton()
        {
            // Initialisations de base, on active nos booléens et on récupère l'action associé
            _isActive = true;
            _isActionEnabled = true;
            _action = _target.GetAttack( _indice );

            // Si le nom est vide, c'est que l'action est vide, on la désactive
            if ( _action.Name == "" || _action.GetSprite == null)
            {
                _UIManager.DisableAction( this );
                return;
            }

            // Si l'on a à faire à un Item, on lui rajoute ton composant de texte via un prefab
            if(_action is Item)
                ChargeExtension( _UIManager.refItemCount );
            
            // Si l'on a à faire à un Skill, on lui ajoute son composant CoolDown
            if(_action is Skill)
                ChargeExtension( _UIManager.refSkillCount );
            
            // Set image du boutton
            _image.color = new Color( 1, 1, 1, 1 );
            _button.image.overrideSprite = _action.GetSprite;

            // Reaction au click => on déclenche l'action + reaction spécifique si Item (consomme objet) ou Skill(cooldown)
            _button.onClick.AddListener( () =>
            {
                // Joue l'action
                _target.Attack( _indice );

                // Skill => COol, et prépare la fin du cooldown
                if ( _action is Skill )
                {
                    StartCoolDown();
                    Invoke( "Enable", _target.GetAttack( _indice ).CoolDown );
                }
                // Item => consomme un objet
                else if(_action is Item)
                {
                    UpdateCount();
                }

            } );
            
        }

    #region Skill
        void StartCoolDown()
        {
            _currentCoolDown = _action._cooldown;
            RunCoolDown();
            _isSkillEnabled = false;
        }

        void FinishCoolDown ()
        {
            _isSkillEnabled = true;
        }

        void RunCoolDown ()
        {
            _currentCoolDown --;
            if(_currentCoolDown != 0)
                Invoke("RunCoolDown", Time.deltaTime * 1f);
            else 
                FinishCoolDown();
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