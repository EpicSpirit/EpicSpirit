using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace EpicSpirit.Game
{
    public class SaveManager : MonoBehaviour
    {
        static ProgressionManager _progressionManager;

        public void Start () 
        {
            _progressionManager = this.gameObject.AddComponent<ProgressionManager>();
        }
        public static void SavSpi ( Spi spi )
        {
            PlayerPrefs.SetInt( "Spi_Health", spi.Health );
        }

        public static void GetSavSpi ( Spi spi )
        {
            if ( PlayerPrefs.HasKey( "Spi_Health" ) )
            {
                spi.Health = PlayerPrefs.GetInt( "Spi_Health" );
            }
            else
            {
                Debug.LogError( "SaveManager, don't have Spi_Health Key on PlayerPrefs" );
            }
        }

        public static List<Action> LoadAllSkills () 
        {

            return new List<Action>();
        }

        public static void InitializeSkillList () 
        {
            string message ="";
            
            for ( int i=0; i < _progressionManager.Skills.Count; i++ )
            {
                message += "0";
            }
            PlayerPrefs.SetString( "Skills", message );
        }
        public static void InitializeWeaponList ()
        {
            string message ="";
            for ( int i=0; i < _progressionManager.Weapons.Count; i++ )
            {
                message += "0";
            }
            PlayerPrefs.SetString( "Weapons", message );
        }
        public static void InitializeItemList ()
        {
            string message ="";
            for ( int i=0; i < _progressionManager.Items.Count; i++ )
            {
                message += "0";
            }
            PlayerPrefs.SetString( "Items", message );
        }
        
        // TODO : World managment
        public static void ResetSave()
        {
            InitializeItemList();
            InitializeSkillList();
            InitializeWeaponList();

            UnlockWeapon( 0 );
        }

        public static void UnlockSkill ( int index)
        {
            Char[] skillFlags = PlayerPrefs.GetString( "Skills" ).ToCharArray();
            skillFlags [index] = '1';
            PlayerPrefs.SetString( "Weapons", skillFlags.ToString() );
        }

        public static void UnlockWeapon ( int index )
        {
            Char[] WeaponFlags = PlayerPrefs.GetString( "Skills" ).ToCharArray();
            WeaponFlags [index] = '1';
            PlayerPrefs.SetString( "Weapons", WeaponFlags.ToString() );
        }

        public static void UnlockItem ( int index )
        {
            Char[] itemFlags = PlayerPrefs.GetString( "Skills" ).ToCharArray();
            itemFlags [index] = '1';
            PlayerPrefs.SetString( "Weapons", itemFlags.ToString() );
        }


    }
}
