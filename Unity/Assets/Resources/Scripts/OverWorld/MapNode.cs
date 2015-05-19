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

        public List<MapNode> LinkedNodes
        {
            get { return _linkedNodes; }
            set { _linkedNodes = value; }
        }

		// Use this for initialization
		void Start () 
		{
		}
		
		// Update is called once per frame
		void Update () 
		{
		
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
                GameObject arrow = (GameObject)Instantiate( UnityEngine.Resources.Load<UnityEngine.Object>( "Images/Overworld/arrows" ), transform.position, Quaternion.LookRotation( node.transform.position - this.transform.position ) );

                // Arrow rotation correction
                rotateEuler = arrow.transform.localEulerAngles;
                rotateEuler.x = 90;
                rotateEuler.z = 90;
                arrow.transform.rotation = Quaternion.Euler( rotateEuler );

                // Arrow position
                arrow.transform.localPosition = new Vector3( arrow.transform.localPosition.x + 1, arrow.transform.localPosition.y, arrow.transform.localPosition.z );

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

        public void LoadLevel()
        {
            Application.LoadLevel( name );
        }

		
	}
}