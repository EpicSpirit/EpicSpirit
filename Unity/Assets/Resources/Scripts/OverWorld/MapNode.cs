using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace EpicSpirit.Game
{
	public class MapNode : MonoBehaviour 
	{
		bool _isLocked;

        List<GameObject> _arrows;

        [SerializeField]
        List<MapNode> _linkedNodes;

        public List<GameObject> Arrows
        {
            get { return _arrows; }
        }

        public List<MapNode> LinkedNodes
        {
            get { return _linkedNodes; }
            set { _linkedNodes = value; }
        }

		/// <summary>
		/// Call it when the player enter in this node
		/// </summary>
		public void Enter()
		{
            Vector3 rotateEuler;
            _arrows = new List<GameObject>();

            foreach(MapNode node in LinkedNodes)
            {
                // Directionnal arrows managment
                GameObject arrow = (GameObject)Instantiate( UnityEngine.Resources.Load<UnityEngine.Object>( "Images/Overworld/arrows" ), transform.position, Quaternion.LookRotation( node.transform.position - this.transform.position ) );

                // Arrow rotation correction
                rotateEuler = arrow.transform.localEulerAngles;
                rotateEuler.x = 90;
                rotateEuler.z = 90;
                arrow.transform.rotation = Quaternion.Euler( rotateEuler );

                // Arrow position
                arrow.transform.Translate( Vector3.right * 1.7f, Space.Self );

                arrow.GetComponent<ArrowGesture>().LinkedNode = this;

                _arrows.Add(arrow);
            }
		}

        public void Exit()
        {
            foreach(GameObject arrow in _arrows)
            {
                GameObject.Destroy( arrow );
            }
        }
	}
}