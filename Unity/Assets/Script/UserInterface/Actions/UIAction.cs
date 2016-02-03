using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

namespace EpicSpirit.Game 
{
    public class UIAction : MonoBehaviour 
    {
        internal Character _target;     // Personnage que l'on contrôle
        Action _action;                 // Action que contient le bouton
        public int _indice;             // indice de l'attaque sur le personnage

        Button _button;                 // Bouton tactile
        Image _image;                   // Image du bouton (null si pas d'action)
        Text _text;                     // Text du bouton (si Skill ou Item)

        float _currentCoolDown;         // Temps restant avant la prochaine attaque (Si Skill)
        
        bool _isSkillEnabled;           // Si le skill est actif (Si Skill uniquement, sinon null)
        bool _isActive; 
        UIManager _UIManager;           // Manager principal de l'UI
            
        public void Awake ()
        {
            _UIManager = this.GetComponentInParent<UIManager>();
            _target = GameObject.FindWithTag( "Player" ).GetComponent<Character>();
            _image = gameObject.AddComponent<Image>();
            _button = gameObject.AddComponent<Button>();
            _currentCoolDown = 0f;
            _isSkillEnabled = true;
        }

        public void OnEnable()
        {
            ActivateButton();
        }

        public void ActivateButton()
        {
            // Initialisations de base, on active nos booléens et on récupère l'action associé
            _isActive = true;
            _isActionEnabled = true;
            _action = _target.GetAttack(_indice);

            // Si le nom est vide, c'est que l'action est vide, on la désactive
            if (_action.Name == "" || _action.GetSprite == null)
            {
                _UIManager.DisableAction(this);
                return;
            }

            // Si l'on a à faire à un Item, on lui rajoute ton composant de texte via un prefab
            if (_action is Item)
                ChargeExtension(_UIManager.refItemCount);

            // Si l'on a à faire à un Skill, on lui ajoute son composant CoolDown
            if (_action is Skill)
                ChargeExtension(_UIManager.refSkillCount);

            // Set image du boutton
            _image.color = new Color(1, 1, 1, 1);
            _button.image.overrideSprite = _action.GetSprite;

            // Reaction au click => on déclenche l'action + reaction spécifique si Item (consomme objet) ou Skill (cooldown)
            _button.onClick.AddListener(() =>
            {
                // Joue l'action
                _target.Attack(_indice);

                // Skill => Lancement du CoolDown
                if (_action is Skill)
                {
                    StartCoolDown();
                }
                // Item => consomme un objet
                else if (_action is Item)
                {
                    UpdateCount();
                }

            });

        }

        private void ChargeExtension(GameObject go)
        {
            // Création texte d'extension
            GameObject extension = Instantiate(go, this.transform.position, Quaternion.identity) as GameObject;
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

    #region Skill
        void StartCoolDown()
        {
            if ( _isSkillEnabled )
            {
                _currentCoolDown = _action._cooldown;
                RunCoolDown();
                _isSkillEnabled = false;
            }
        }

        void FinishCoolDown ()
        {
            _text.text = "";
            _isSkillEnabled = true;
        }

        void RunCoolDown ()
        {
            _text.text = _currentCoolDown.ToString();
            _currentCoolDown -= 1f;

            if(_currentCoolDown != -1f)
                Invoke("RunCoolDown", 1f);
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