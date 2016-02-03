using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

namespace EpicSpirit.Game
{
    public class GameMenuManager : MonoBehaviour
    {
        SaveManager _saveManager;

        List<Action> _actions;

        List<Weapon> _weapons;
        List<Skill> _skills;
        List<Item> _items;
        actionicone _selectedAction;

        // TODO : Rendre ça propre ensuite
        public GameObject description;
        public List<GameObject> _slots;
        public GameObject target;
        public GameObject origin;

        public enum TypeOfContent
        {
            Weapons,
            Skills,
            Items
        }
        TypeOfContent _typeOfContent;
        int _x, _y;
        float _xOffset;
        float _yOffset;

        void Awake ()
        {
            _selectedAction = null;
            _saveManager = GameObject.FindWithTag( "SaveManager" ).GetComponent<SaveManager>();
            _weapons = _saveManager.LoadAllUnlockWeapons();
            _items = _saveManager.LoadAllUnlockItems();
            _skills = _saveManager.LoadAllUnlockSkills();
            _typeOfContent = TypeOfContent.Weapons;

            _x = 0;
            _y = 0;
            _xOffset = Screen.width / 5f;
            _yOffset = Screen.height / 3.8f;
        }

        void Start ()
        {
            WeaponMenu();
        }

        public void RefreshMenu()
        {
            _x = 0;
            _y = 0;
            // Destroy all ActionIcon
            foreach ( GameObject actionIcon in GameObject.FindGameObjectsWithTag( "ActionIcon" ))
            {
                GameObject.Destroy(actionIcon);
            }

            // Refresh
            List<Action> _actions = SaveManager.LoadAction();


            switch ( _typeOfContent )
            {
                case TypeOfContent.Weapons:
                    foreach ( Weapon weapon in _weapons )
                    {
                        AddIcon( weapon );
                    }
                    _slots[1].GetComponent<Image>().sprite = _actions[0].GetSprite;
                    break;
                case TypeOfContent.Skills:
                    foreach ( Skill skill in _skills )
                    {
                        AddIcon( skill );
                    }
                    _slots[0].GetComponent<Image>().sprite = _actions[2].GetSprite;
                    _slots[1].GetComponent<Image>().sprite = _actions[3].GetSprite;
                    _slots[2].GetComponent<Image>().sprite = _actions[4].GetSprite;
                    break;
                case TypeOfContent.Items:
                    foreach ( Item item in _items )
                    {
                        AddIcon( item );
                    }
                    _slots[1].GetComponent<Image>().sprite = _actions[1].GetSprite;

                    break;
                default:
                    Debug.Log("Probleme TypeOfContent");
                    break;
            }            
        }

        public void AddIcon(Action action)
        {
            GameObject gameObject = (GameObject)Instantiate( Resources.Load<GameObject>( "UI/GameMenu/ActionIcon" ), new Vector3( target.transform.position.x +( _x * _xOffset), target.transform.position.y - (_y * _yOffset), 0 ), new Quaternion() );
            gameObject.transform.parent = target.transform;

            gameObject.transform.localScale = new Vector3(5,5,5);
            var ai = gameObject.GetComponent<actionicone>();
            ai.Action = action;
            var b = gameObject.GetComponent<Button>();
            b.onClick.AddListener( () => GetDescription(ai));
            
            if( ++_x >= 3)
            {
                _x = 0;
                _y++;
            }
        }
        
        private void GetDescription( actionicone ai)
        {
            _selectedAction = ai;
            GameObject.Find( "DescriptionTitle" ).GetComponent<Text>().text = ai.Action.Name;
            GameObject.Find( "DescriptionText" ).GetComponent<Text>().text = ai.Action.Description;
        }
        
        private bool AlreadyExist(Action action)
        {
            bool flag = false;
            List<Action> listAction = SaveManager.LoadAction();

            foreach ( Action a in listAction )
            {
                if ( action == a ) flag = true;
            }

            return flag;
        }

        public void AddToSlot(int index)
        {
            ProgressionManager progressionManager = GameObject.Find( "ProgressionManager" ).GetComponent<ProgressionManager>();

            if ( AlreadyExist( _selectedAction.Action ) && _selectedAction.Action != progressionManager.Skills [0] ) return;

            string key;
            int value;

            if ( _selectedAction.Action is Weapon )
            {
                key = "ActualWeapon";
                value = progressionManager.Weapons.IndexOf( (Weapon)_selectedAction.Action );
            }
            else if ( _selectedAction.Action is Item )
            {
                key = "ActualItem";
                value = progressionManager.Items.IndexOf( (Item)_selectedAction.Action );
            }
            else if ( _selectedAction.Action is Skill )
            {
                key = "ActualSkill_" + index;
                value = progressionManager.Skills.IndexOf( (Skill)_selectedAction.Action );
            }
            else
            {
                Debug.LogException(new Exception("action must be Weapon,Item or Skill"));
                key = "";
                value = -1;
            }

            PlayerPrefs.SetInt(key,value);
            Debug.Log( key + " => " + value );
            RefreshMenu();
        }

        public void WeaponMenu()
        {
            _typeOfContent = TypeOfContent.Weapons;
            _slots[0].SetActive( false );
            _slots[2].SetActive( false );

            EnableButton( GameObject.Find( "WeaponButton" ).GetComponent<Button>() );
            DisableButton( GameObject.Find( "SkillButton" ).GetComponent<Button>() );
            DisableButton( GameObject.Find( "ObjectButton" ).GetComponent<Button>() );

            GameObject.Find( "DescriptionTitle" ).GetComponent<Text>().text = "";
            GameObject.Find( "DescriptionText" ).GetComponent<Text>().text = "";


            RefreshMenu();
        }
        public void ItemMenu ()
        {
            _typeOfContent = TypeOfContent.Items;

            _slots[0].SetActive( false );
            _slots[2].SetActive( false );
            GameObject.Find( "DescriptionTitle" ).GetComponent<Text>().text = "";
            GameObject.Find( "DescriptionText" ).GetComponent<Text>().text = "";

            EnableButton( GameObject.Find( "ObjectButton" ).GetComponent<Button>() );
            DisableButton( GameObject.Find( "SkillButton" ).GetComponent<Button>() );
            DisableButton( GameObject.Find( "WeaponButton" ).GetComponent<Button>() );


            RefreshMenu();
        }
        public void SkillMenu ()
        {
            _typeOfContent = TypeOfContent.Skills;
            _slots[0].SetActive( true );
            _slots[2].SetActive( true );
            GameObject.Find( "DescriptionTitle" ).GetComponent<Text>().text = "";
            GameObject.Find( "DescriptionText" ).GetComponent<Text>().text = "";

            EnableButton( GameObject.Find( "SkillButton" ).GetComponent<Button>() );
            DisableButton( GameObject.Find( "ObjectButton" ).GetComponent<Button>() );
            DisableButton( GameObject.Find( "WeaponButton" ).GetComponent<Button>() );



            RefreshMenu();
        }

        public void ExitMenu ()
        {
            Application.LoadLevel( "overworld" );
        }

        void EnableButton ( Button b )
        {
            b.image.color = Color.white;

        }
        void DisableButton ( Button b )
        {
            b.image.color = Color.clear;
        }

    }
}