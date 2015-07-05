using UnityEngine;
using System.Collections;
using UnityEngine.UI;


namespace EpicSpirit.Game
{
    public class UISkill : MonoBehaviour
    {
        internal Text _cooldown;
        Button _button;
        UIAction _action;

        public void Awake ()
        {
            _cooldown = gameObject.AddComponent<Text>();
            _cooldown.fontSize = 25;
            _cooldown.fontStyle = FontStyle.Bold;
            _cooldown.font = Resources.Load<Font>( "UI/BLKCHCRY" );
            _cooldown.resizeTextForBestFit = true;
            _cooldown.alignment = TextAnchor.MiddleCenter;
            _action = this.GetComponentInParent<UIAction>();
        }

        public void Start ()
        {
            _button = this.GetComponentInParent<Button>();

            _action._target.GetAttack( _action._indice ).StartCoolDown( ( uiSkill, ccd ) => {
                if ( ccd == 0 ) { 
                    uiSkill._cooldown.text = ""; 
                }
                else 
                { 
                    uiSkill._cooldown.text = ccd.ToString(); 
                }
                return true; 
            }, this );

        }

    }
}