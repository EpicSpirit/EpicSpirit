using UnityEngine;
using System.Collections;
using UnityEngine.UI;


namespace EpicSpirit.Game
{
    public class UISkill : MonoBehaviour
    {
        Text _cooldown;
        Button _button;
        UIAction _action;

        public void Awake ()
        {
            _cooldown = gameObject.AddComponent<Text>();
            _cooldown.fontSize = 25;
            _cooldown.fontStyle = FontStyle.Bold;
            _cooldown.font = Resources.Load<Font>( "UI/BLKCHCRY" );
            _cooldown.resizeTextForBestFit = true;
            _action = this.GetComponentInParent<UIAction>();
        }
        public void Start ()
        {
            _button = this.GetComponentInParent<Button>();
            _action.target.GetAttack( _action._indice ).StartCoolDown( (uiSkill) => uiSkill.Timer(), this );
            Timer();
        }
 
        bool Timer ()
        {
            _cooldown.text = _action.target.GetAttack( _action._indice ).CurrentCoolDown.ToString();
            return true;
        }
        


    }
}