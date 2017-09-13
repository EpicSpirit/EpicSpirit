using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.SceneManagement;

namespace EpicSpirit.Game
{
	public class OverworldPlayerController : MonoBehaviour 
	{
		public Character _player;
		public MapNode _currentMapNode;
        bool _isMoving;

        public MapNode CurrentMapNode {
			get { return _currentMapNode; }
			set { _currentMapNode = value; }
		}

		void Awake () 
		{
            if ( _player == null )
                _player = GameObject.FindGameObjectWithTag( "Player" ).GetComponent<Character>();

            // Modifie notre Spi pour qu'il ne puisse pas gêner le clique du joueur
            _player.GetComponent<CharacterController>().radius = 0;
            _player.GetComponentInChildren<AutoFocus>().gameObject.SetActive(false);

            // On récupère le bon niveau
            LoadActualLevel();
                //_currentMapNode = GameObject.Find( "forest_1" ).GetComponent<MapNode>();

            // On débloque les maps si on vient de finir un level // Plutot dans le start ça non ?
            UnlockLevels();

            //On entre dans le node où l'on est actuellement // pareil ?
            CurrentMapNode.Enter();
		}

		
		void Update () 
		{
            // On se déplace si le mec a appuyé sur une flèche
            int i = 0;
            foreach(GameObject arrow in CurrentMapNode.Arrows)
            {
                if ( arrow.GetComponent<ArrowGesture>().Move )
                {
                    CurrentMapNode.Exit();
                    CurrentMapNode = CurrentMapNode.LinkedNodes[i];
                    LevelManager.SetParameter( "level", CurrentMapNode.name );
                    CurrentMapNode.Enter();
                }
                i++;
            }
            _player.MoveTo( CurrentMapNode.transform.position ) ;
		}

		void LoadActualLevel()
		{
			try
			{
				CurrentMapNode = GameObject.Find( ( string ) LevelManager.GetParameter( "level" ) ).GetComponent<MapNode>();
			}
			catch
			{
				CurrentMapNode = GameObject.Find( "forest_1" ).GetComponent<MapNode>();
			}
			
			GameObject.FindWithTag( "Player" ).transform.position = CurrentMapNode.transform.position;
			GameObject.FindWithTag( "MainCamera" ).transform.position = CurrentMapNode.transform.position;
			
		}
		
		void UnlockLevels()
		{
			if ( LevelManager.GetParameter( "ended" ) != null && ( bool ) LevelManager.GetParameter( "ended" ) == true )
			{
				foreach ( MapNode mapNode in CurrentMapNode.LinkedNodes )
				{
					mapNode.Unlock();
				}
			}
		}

        public void LoadLevel()
        {
            LevelManager.SetParameter( "ended", false );
            LevelManager.SetParameter( "level", CurrentMapNode.name );

            if ( CurrentMapNode.IsLocked == false)
                SceneManager.LoadScene( CurrentMapNode.name );
            else Debug.Log("Can't load the level");
        }

        public void LoadMenu ()
        {
            GameObject.Find( "GameMenu 2" ).GetComponent<Animator>().SetBool( "open", true );
            Invoke( "StopAnimation", 0.2f );
            Invoke( "LoadMenu_Step2", 0.3f );
        }
        private void StopAnimation()
        {
            GameObject.Find( "GameMenu 2" ).GetComponent<Animator>().SetBool( "open", false );
        }
        public void LoadMenu_Step2()
        {
            LevelManager.SetParameter( "ended", false );
            UnityEngine.SceneManagement.SceneManager.LoadScene( "game_menu" );
        }
    }
}