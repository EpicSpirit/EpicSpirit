using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace EpicSpirit.Game
{
	public abstract class TempleSwitch : Character
	{
		internal bool _isOn;
        [SerializeField]
        internal bool _allOn;
        [SerializeField]
        internal bool _oneShot;
		[SerializeField]
		List<GameObject> _gates;

		public List<GameObject> Gates
		{
			get{ return _gates; }
			set{ _gates = value; }
		}


		void Awake () 
		{
			_isOn = false;
		}
        public override void Start () { }

        public override void Update () { }

        /// <summary>
        /// Call it in child class at the end of the method
        /// </summary>
        /// <param name="force">No use here</param>
        internal override void takeDamage( int force )
        {
            if ( _allOn )
            {
                foreach ( GameObject gate in Gates )
                {
                    gate.SetActive( true );
                }
            }
            if ( _oneShot )
            {
                GameObject.Destroy( this );
            }
        }

		public override void MoveBack(GameObject c, float strengh){}
	}
}