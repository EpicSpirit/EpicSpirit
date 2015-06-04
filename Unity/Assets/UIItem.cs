using UnityEngine;
using System.Collections;
using UnityEngine.UI;


namespace EpicSpirit.Game
{
    public class UIItem : MonoBehaviour
    {
        Text _itemCount;
        Button _button;
        UIAction _action;
        public void Awake ()
        {
            _itemCount = gameObject.AddComponent<Text>();
            _itemCount.text = "20";
            _itemCount.fontSize = 25;
            _itemCount.fontStyle = FontStyle.Bold;
            _itemCount.font = Resources.Load<Font>("UI/BLKCHCRY");
            _itemCount.resizeTextForBestFit = true;
            _action = this.GetComponentInParent<UIAction>();

            

        }
        public void Start()
        {
            _button = this.GetComponentInParent<Button>();
            Item item = _action.target.GetAttack( _action._indice ) as Item;
            _itemCount.text = item.Quantity.ToString();
            _button.onClick.AddListener( () => _itemCount.text=item.Quantity.ToString() );
        }

    }
}