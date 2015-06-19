using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace EpicSpirit.Game
{
	public class OverworldPlayerController : MonoBehaviour 
	{
		public Character _player;
		public MapNode _currentMapNode;
        bool _isMoving;

        public MapNode CurrentMapNode
        {
            get { return _currentMapNode; }
        }

        void SetActualLevel()
        {
            try
            {
                _currentMapNode = GameObject.Find( ( string ) LevelManager.GetParameter( "level" ) ).GetComponent<MapNode>();
            }
            catch ( Exception e )
            {
                _currentMapNode = GameObject.Find( "forest_1.3" ).GetComponent<MapNode>();
            }

            GameObject.FindWithTag( "Player" ).transform.position = _currentMapNode.transform.position;
            GameObject.FindWithTag( "MainCamera" ).transform.position = _currentMapNode.transform.position;

        }

        void UnlockLevels()
        {
            if ( LevelManager.GetParameter( "ended" ) != null && ( bool ) LevelManager.GetParameter( "ended" ) == true )
            {
                List<MapNode> linkedMapNodes = new List<MapNode>();
                foreach ( MapNode mapNode in _currentMapNode.LinkedNodes )
                {
                    mapNode.Unlock();
                }
            }
        }

		void Awake () 
		{
            // On récupère le bon niveau
            SetActualLevel();

            // On débloque les maps si on vient de finir un level
            UnlockLevels();

            //On entre dans le node où l'on est actuellement
            _currentMapNode.Enter();
		}

		
		void Update () 
		{
            // On se déplace si le mec a appuyé sur une flèche
            int i = 0;
            foreach(GameObject arrow in _currentMapNode.Arrows)
            {
                if ( arrow.GetComponent<ArrowGesture>().Move )
                {
                    _currentMapNode.Exit();
                    _currentMapNode = _currentMapNode.LinkedNodes[i];
                    LevelManager.SetParameter( "level", _currentMapNode.name );
                    _currentMapNode.Enter();
                }
                i++;
            }
            _player.MoveTo( _currentMapNode.transform.position ) ;
		}

        public void LoadLevel()
        {
            LevelManager.SetParameter( "ended", false );
            LevelManager.SetParameter( "level", _currentMapNode.name );

            if ( _currentMapNode.IsLocked == false)
                Application.LoadLevel( _currentMapNode.name );
            else Debug.Log("Can't load the level");
        }

        public void LoadMenu ()
        {
            LevelManager.SetParameter( "ended", false );
            Application.LoadLevel( "game_menu" );
        }
    }
}