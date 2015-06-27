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

        public enum IconType
        {
            ActualWeapon,
            ActualItem,
            ActualSkill_1,
            ActualSkill_2,
            ActualSkill_3
        }

        public void Awake () 

        {
            //_progressionManager = this.gameObject.AddComponent<ProgressionManager>();
            _progressionManager = GameObject.Find( "ProgressionManager" ).GetComponent<ProgressionManager>();
            //ResetSave();    // TMP

        }

        public void SaveSpi ( Spi spi )
        {
            SaveSpiHealth( spi._currentHealth );
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns>true -> locked, false -> unlocked</returns>
        public static bool IsMapIsLocked ( string name )
        {
            return PlayerPrefs.GetString( name ,"1") == "1" ? true : false;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value">true -> locked, false -> unlocked</param>
        private static void SetInfoMap ( string name, bool value )
        {
            PlayerPrefs.SetString( name, value ? "1" : "0" );
        }

        public static void SetIconAttack ( IconType it, int i )
        {
            PlayerPrefs.SetInt( Enum.GetName( typeof( IconType ), it ), i );
        }
        public static void SetIconAttack ( string s, int i )
        {
            PlayerPrefs.SetInt( s, i );
        }
        public static void SetIconAttack ( IconType it, Action a)
        {
            var pm = GameObject.Find( "ProgressionManager" ).GetComponent<ProgressionManager>();
            int indice=-1;

            if(a is Skill)  indice = pm.Skills.FindIndex( (s) => {return s == a;} );
            else if(a is Item)  indice = pm.Items.FindIndex( (s) => {return s == a;} );
            else if ( a is Weapon ) indice = pm.Weapons.FindIndex( ( s ) => { return s == a; } );
            else    Debug.Log( "Error" );

            PlayerPrefs.SetInt( Enum.GetName( typeof( IconType ), it ), indice );
        }

        public static void BlockMap(string name)
        {
            SetInfoMap(name,true);
        }
        
        public static void UnlockMap ( string name )
        {
            SetInfoMap( name, false );
        }

        public static void BlockAllMap ()
        {
            List<string> listMapName = new List<string>();
            listMapName.Add( "forest_1" );
            listMapName.Add( "forest_2" );
            listMapName.Add( "forest_temple" );
            listMapName.Add( "mountain_1" );

            foreach ( string mapName in listMapName )
            {
                BlockMap( mapName );
            }
        }

        internal static void SaveSpiHealth ( int health )
        {
            PlayerPrefs.SetInt( "Spi_Health", health );
        }
        internal static int GetSpiHealth ()
        {
            return PlayerPrefs.GetInt( "Spi_Health");
        }

        public static void GetSaveSpi ( Spi spi )
        {
            if ( PlayerPrefs.HasKey( "Spi_Health" ) )
            {
                spi.CurrentHealth = PlayerPrefs.GetInt( "Spi_Health" );
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

            
        }

        // TODO : World managment
        public void ResetSave()
        {
            PlayerPrefs.SetInt( "Save", 1 );
            Debug.Log("ResetSave");
            InitializeItemList();
            InitializeSkillList();
            InitializeWeaponList();

            // Initialization Spi's Health
            PlayerPrefs.SetInt( "Spi_Health", 20 );

            UnlockWeapon( 0 );
            UnlockSkill( 0 );
            UnlockItem( 0 );

            SetIconAttack( IconType.ActualItem, _progressionManager.Items [0] );
            SetIconAttack( IconType.ActualWeapon, _progressionManager.Weapons [0] );
            SetIconAttack( IconType.ActualSkill_1, _progressionManager.Skills [0] );
            SetIconAttack( IconType.ActualSkill_2, _progressionManager.Skills [0] );
            SetIconAttack( IconType.ActualSkill_3, _progressionManager.Skills [0] );

            // Gestion des maps
            BlockAllMap();
            UnlockMap( "forest_1");

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

        internal static bool HasSave ()
        {
            return PlayerPrefs.GetInt( "Save", 0 ) == 1 ? true : false;
        }
    }
}
