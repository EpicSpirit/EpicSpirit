using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Text;

namespace EpicSpirit.Game
{
    public class SaveManager : MonoBehaviour
    {
        ProgressionManager _progressionManager;

        public void Awake () 
        {
            //_progressionManager = this.gameObject.AddComponent<ProgressionManager>();
            _progressionManager = GameObject.Find( "ProgressionManager" ).GetComponent<ProgressionManager>();
            ResetSave();    // TMP

        }

        public void SaveSpi ( Spi spi )
        {
            SaveSpiHealth( spi._health );
        }

        internal static void SaveSpiHealth ( int health )
        {
            PlayerPrefs.SetInt( "Spi_Health", health );
        }
        internal static int GetSpiHealth ()
        {
            return PlayerPrefs.GetInt( "Spi_Health");
        }

        public  void GetSaveSpi ( Spi spi )
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

        public List<Action> LoadAllSkills () 
        {
            var p = GameObject.Find( "ProgressionManager" ).GetComponent<ProgressionManager>();

            return new List<Action>();
        }

        public void InitializeSkillList () 
        {
            string message ="";
            
            for ( int i=0; i < _progressionManager.Skills.Count; i++ )
            {
                message += "0";
            }
            PlayerPrefs.SetString( "Skills", message );
        }
        public void InitializeWeaponList ()
        {
            string message ="";
            for ( int i=0; i < _progressionManager.Weapons.Count; i++ )
            {
                message += "0";
            }
            
            PlayerPrefs.SetString( "Weapons", message );
        }
        public void InitializeItemList ()
        {
            string message ="";
            for ( int i = 0; i < _progressionManager.Items.Count; i++ )
            {
                message += "0";

                PlayerPrefs.SetInt( _progressionManager.Items [i].Name, 0 );
            }
            PlayerPrefs.SetString( "Items", message );
            PlayerPrefs.SetInt( _progressionManager.Items [0].Name, 5);

            
        }
        
        // TODO : World managment
        public void ResetSave()
        {
            Debug.Log("ResetSave");
            InitializeItemList();
            InitializeSkillList();
            InitializeWeaponList();

            // Initialization Spi's Health
            PlayerPrefs.SetInt( "Spi_Health", 20 );

            UnlockWeapon( 0 );

            UnlockSkill( 0 );   // FireBall
            UnlockSkill( 1 );   // FrozenPick
            UnlockSkill( 2 );   // Dodge
            UnlockSkill( 3 );   // Dodge
            UnlockSkill( 4 );   // Dodge
            UnlockSkill( 5 );   // Dodge
            UnlockSkill( 6 );   // Dodge
            UnlockSkill( 7 );   // Dodge

        }

        public void UnlockSkill ( int index)
        {
            Char[] skillFlags = PlayerPrefs.GetString( "Skills" ).ToCharArray();
            skillFlags [index] = '1';

            StringBuilder stringbuilder = new StringBuilder();
            foreach ( Char mychar in skillFlags )
            {
                stringbuilder.Append( mychar );
            }

            PlayerPrefs.SetString( "Skills", stringbuilder.ToString() );
        }

        public static List<Action> LoadAction ()
        {
            List<Action> a = new List<Action>();
            var go=GameObject.Find( "ProgressionManager" );
            if(go != null)
            {
                var p = go.GetComponent<ProgressionManager>();

                if ( p != null )
                {
                    a.Add( p.Weapons [PlayerPrefs.GetInt( "ActualWeapon" )] );
                    a.Add( p.Items [PlayerPrefs.GetInt( "ActualItem" )] );
                    a.Add( p.Skills [PlayerPrefs.GetInt( "ActualSkill_1" )] );
                    a.Add( p.Skills [PlayerPrefs.GetInt( "ActualSkill_2" )] );
                    a.Add( p.Skills [PlayerPrefs.GetInt( "ActualSkill_3" )] );

                    return a;
                }
            }
            Debug.Log("Progression manager not found");
            return null;
        }


        public void UnlockWeapon ( int index )
        {
            Char[] WeaponFlags = PlayerPrefs.GetString( "Weapons" ).ToCharArray();
            WeaponFlags [index] = '1';

            StringBuilder stringbuilder = new StringBuilder();
            foreach(Char mychar in WeaponFlags)
            {
                stringbuilder.Append( mychar );   
            }

            PlayerPrefs.SetString( "Weapons", stringbuilder.ToString() );
            
        }
        public void UnlockItem ( int index )
        {
            Char[] itemFlags = PlayerPrefs.GetString( "Items" ).ToCharArray();
            itemFlags [index] = '1';

            StringBuilder stringbuilder = new StringBuilder();
            foreach ( Char mychar in itemFlags )
            {
                stringbuilder.Append( mychar );
            }

            PlayerPrefs.SetString( "Items", stringbuilder.ToString() );
        }

        public List<Skill> LoadAllUnlockSkills()
        {
            List<Skill> unlockSkills = new List<Skill>();
            Char[] skillsFlags = PlayerPrefs.GetString( "Skills" ).ToCharArray();


            for(int i=0; i < _progressionManager.Skills.Count; i++)
            {
                if ( i < skillsFlags.Length && skillsFlags [i] == '1' )
                    unlockSkills.Add( _progressionManager.Skills [i] );
            }

            return unlockSkills;
        }
        public List<Weapon> LoadAllUnlockWeapons ()
        {
            List<Weapon> unlockWeapons = new List<Weapon>();
            Char[] weaponsFlags = PlayerPrefs.GetString( "Weapons" ).ToCharArray();

            for ( int i = 0 ; i < _progressionManager.Weapons.Count; i++ )
            {
                if ( i < weaponsFlags.Length && weaponsFlags [i] == '1' )
                {
                    unlockWeapons.Add( _progressionManager.Weapons [i] );
                }
            }

            return unlockWeapons;
        }
        public List<Item> LoadAllUnlockItems ()
        {
            List<Item> unlockItems = new List<Item>();
            Char[] itemsFlags = PlayerPrefs.GetString( "Items" ).ToCharArray();

            for ( int i=0; i < _progressionManager.Items.Count; i++ )
            {
                if ( i < itemsFlags.Length && itemsFlags [i] == '1' )
                    unlockItems.Add( _progressionManager.Items [i] );
            }

            return unlockItems;
        }

        public static void AddItem(Item i)
        {
            PlayerPrefs.SetInt( i.Name, i.Quantity + 1 );

        }

        public static void RemoveItem(Item i)
        {
            if(i.Quantity >0)
            {
                PlayerPrefs.SetInt( i.Name, i.Quantity - 1 );
            }
            else
            {
                Debug.Log("0 item");
            }
        }
        public static int GetItemQuantity(Item i)
        {
            return PlayerPrefs.GetInt( i.Name );
        }

        
    }
}
