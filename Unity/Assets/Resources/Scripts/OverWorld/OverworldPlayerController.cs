using UnityEngine;
using System.Collections;

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

		void Start () 
		{
            _currentMapNode.Enter();
		}
		
		void Update () 
		{
            int i = 0;
            foreach(GameObject arrow in _currentMapNode.Arrows)
            {
                if ( arrow.GetComponent<ArrowGesture>().Move )
                {
                    _currentMapNode.Exit();
                    _currentMapNode = _currentMapNode.LinkedNodes[i];
                    _currentMapNode.Enter();
                }
                    i++;
            }

            _player.MoveTo( _currentMapNode.transform.position ) ;
		}

        public void LoadLevel()
        {
            Application.LoadLevel( _currentMapNode.name );
        }
    }
}