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
			_button = gameObject.AddComponent<Button>();
			_button.image.overrideSprite = Resources.Load<Sprite>( "Images/PauseMenu/ButtonReturnToOverworld" );

           _menu = GameObject.FindWithTag( "Menu" );
        }
        void Start()
        {
			_menu.SetActive( false );
			_button.onClick.AddListener( () => 
			{
				_menu.SetActive(true) ;
				Debug.Log("Ici");
				PauseManager.BlockEveryCharacter(true);
			});

        }
    }
}