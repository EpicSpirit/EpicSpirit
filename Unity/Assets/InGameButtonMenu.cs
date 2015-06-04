using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace EpicSpirit.Game
{
    public class InGameButtonMenu : MonoBehaviour
    {
        Button _button;
        GameObject _menu;
        void Awake()
        {
            gameObject.AddComponent<Image>();
            gameObject.AddComponent<Button>();

           _button = GetComponent<Button>();
           _button.image.overrideSprite = Resources.Load<Sprite>( "Images/InGame/ButtonReturnToOverworld" );

           _menu = GameObject.FindWithTag( "Menu" );

           _menu.SetActive( false );
           _button.onClick.AddListener( () => _menu.SetActive(true) );

        }
        void Update()
        {
        }
    }
}