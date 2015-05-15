using UnityEngine;
using System.Collections;

namespace EpicSpirit.Game
{
	public class OverworldPlayerController : MonoBehaviour 
	{
		public Character _player;
		public MapNode _currentMapNode;

		void Start () 
		{
		}
		
		void Update () 
		{
			if (Input.GetKey(KeyCode.LeftArrow))
			{
				if( _currentMapNode.PreviousNode != null )
				{
					_currentMapNode = _currentMapNode.PreviousNode;
				}
			}
			else if (Input.GetKey(KeyCode.RightArrow))
			{
				if( _currentMapNode.NextNode != null )
				{
					_currentMapNode = _currentMapNode.NextNode;
				}
			}
			Invoke("GoToNode", 1.0f);
		}

		private void GoToNode()
		{
			_player.transform.position = _currentMapNode.transform.position;
		}

	}
}