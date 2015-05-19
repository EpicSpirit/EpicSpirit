using UnityEngine;
using System.Collections;

namespace EpicSpirit.Game
{
	public class OverworldPlayerController : MonoBehaviour 
	{
		public Character _player;
		public MapNode _currentMapNode;
        bool _isMoving;

		void Start () 
		{
          //  _currentMapNode.LinkedNodes[0].LinkedNodes[0].Enter();
            _currentMapNode.Enter();
		}
		
		void Update () 
		{
			if (Input.GetKeyDown(KeyCode.LeftArrow))
			{
				if( _currentMapNode.LinkedNodes[0] != null )
				{
                    _currentMapNode.Exit();
                    _currentMapNode = _currentMapNode.LinkedNodes[0];
                    _currentMapNode.Enter();
				}
			}
			else if (Input.GetKeyDown(KeyCode.RightArrow))
			{
                if ( _currentMapNode.LinkedNodes[1] != null )
				{
                    _currentMapNode.Exit();
					_currentMapNode = _currentMapNode.LinkedNodes[1];
                    _currentMapNode.Enter();
				}
			}
            _player.MoveTo( _currentMapNode.transform.position ) ;
		}
	}
}