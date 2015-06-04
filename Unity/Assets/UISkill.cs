using UnityEngine;
using System.Collections;
using UnityEngine.UI;


namespace EpicSpirit.Game
{
    public class UISkill : MonoBehaviour
    {
        Text _cooldown;
        float _currentCoolDown;
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
            _currentCoolDown = _action.target.GetAttack( _action._indice ).CoolDown;
            _cooldown.text = _currentCoolDown.ToString();
            StartCoolDown();
        }
        public void StartCoolDown()
        {
            Invoke( "DecrementCoolDown", 1f );
        }
        private void DecrementCoolDown ()
        {
            _currentCoolDown--;
            _cooldown.text = _currentCoolDown.ToString();
            if ( _currentCoolDown != 0 ) Invoke( "DecrementCoolDown", 1f );
        }

    }
}