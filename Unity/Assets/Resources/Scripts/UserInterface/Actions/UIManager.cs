using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


namespace EpicSpirit.Game
{
    /// <summary>
    /// Cette classe nous permet de gérer correctement les différents éléments d'UI sur le GameMenu
    /// </summary>
    public class UIManager : MonoBehaviour
    {

        UIAction[] _gamepad;
        public GameObject refItemCount;
        public GameObject refSkillCount;

        /// <summary>
        /// On récupère tout nos éléments d'UI pour les garder en références et en avoir le contrôle
        /// </summary>
        void Awake ()
        {
            _gamepad = this.GetComponentsInChildren<UIAction>();
            refItemCount = Resources.Load<GameObject>( "Prefab/ItemCount" );
            refSkillCount = Resources.Load<GameObject>( "Prefab/CoolDown" );

        }

        // Use this for initialization
        void Start ()
        {

        }

        // Update is called once per frame
        void Update ()
        {

        }

        internal void DisableAction ( UIAction UIAction )
        {
            UIAction.GetComponent<Button>().onClick.RemoveAllListeners();
            UIAction.gameObject.SetActive( false );
        }
        internal void EnableAction(UIAction UIAction)
        {
            UIAction.enabled = true;
        }
    }
}