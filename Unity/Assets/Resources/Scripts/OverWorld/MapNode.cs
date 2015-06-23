using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

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

        public bool IsLocked
        {
            get { return _isLocked; }
        }

        public List<MapNode> LinkedNodes
        {
            get { return _linkedNodes; }
            set { _linkedNodes = value; }
        }

        public void Start()
        {
            _isLocked = SaveManager.IsMapIsLocked( this.name );
            if(!_isLocked)   GetComponentInChildren<SphereCollider>().gameObject.SetActive( false );
        }

		/// <summary>
		/// Call it when the player enter in this node
		/// </summary>
		public void Enter()
		{
            Vector3 rotateEuler;
            _arrows = new List<GameObject>();

            foreach ( MapNode node in LinkedNodes.Where( ( n ) => !SaveManager.IsMapIsLocked( n.name ) ) )
            {
                // Directionnal arrows managment
                GameObject arrow = (GameObject)Instantiate( UnityEngine.Resources.Load<UnityEngine.Object>( "Images/Overworld/arrows" ), transform.position, Quaternion.LookRotation( node.transform.position - this.transform.position ) );

                // Arrow rotation correction
                rotateEuler = arrow.transform.localEulerAngles;
                rotateEuler.x = 90;
                rotateEuler.z = 90;
                arrow.transform.rotation = Quaternion.Euler( rotateEuler );

                // Arrow position
                arrow.transform.Translate( Vector3.right * 3.0f, Space.Self );
                arrow.GetComponent<ArrowGesture>().BaseNode = this;
                arrow.GetComponent<ArrowGesture>().LinkedNode = node;
                _arrows.Add(arrow);

            }

		}

        public bool Unlock()
        {
            if ( SaveManager.IsMapIsLocked( this.name ) )
            {
                SaveManager.UnlockMap( this.name);
                _isLocked = SaveManager.IsMapIsLocked( this.name );
                this.GetComponentInChildren<ParticleSystem>().Play();
                return true;
            }

            return false;
        }

        public void Exit()
        {
            foreach(GameObject arrow in _arrows)
            {
                GameObject.Destroy( arrow );
            }
        }

        public void OnMouseUp()
        {
            var opc = GameObject.Find( "Controller" ).GetComponent<OverworldPlayerController>();
            if( opc.CurrentMapNode.LinkedNodes.Contains(this))
            {
                foreach(var arrow in opc.CurrentMapNode.Arrows)
                {

                    var a = arrow.GetComponent<ArrowGesture>();
                    Debug.Log(a.LinkedNode.name);

                    if( a.LinkedNode == this)
                    {
                        Debug.Log("WIN");
                        a.Move = true;
                        return;
                    }
                }
            }
        }
	}
}