using UnityEngine;
using System.Collections;

namespace EpicSpirit.Game
{
	public class MapNode : MonoBehaviour 
	{
		bool _isLocked;
		string _name;
		string _levelName;

		[SerializeField]
		MapNode _previousNode;
		[SerializeField]
		MapNode _nextNode;
		[SerializeField]
		MapNode _templeNode;

		public MapNode PreviousNode
		{
			get{ return _previousNode; }
			set{ _previousNode = value; }
		}

		public MapNode NextNode
		{
			get{ return _nextNode; }
			set{ _nextNode = value; }
		}

		public MapNode TempleNode
		{
			get{ return _templeNode; }
			set{ _templeNode = value; }
		}

		// Use this for initialization
		void Start () 
		{
			_name = this.name;
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
			Instantiate (UnityEngine.Resources.Load<UnityEngine.Object>( "Images/Overworld/arrows" ), transform.position, transform.rotation);

		}
		
	}
}