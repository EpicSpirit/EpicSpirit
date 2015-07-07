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
        Item _item;

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
            _item = _action._target.GetAttack( _action._indice ) as Item;
            _itemCount.text = _item.Quantity.ToString();
            _button.onClick.AddListener( () => { UpdateCount(); } );
            if ( _item.Name == "" ) _itemCount.text = "";

        }

        public void UpdateCount ()
        {
            Item item = _action._target.GetAttack( _action._indice ) as Item;
            _itemCount.text = item.Quantity.ToString();
            if ( item.Name == "" ) _itemCount.text = "";
            
        }

    }
}