using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace EpicSpirit.Game
{
	public abstract class TempleSwitch : Character
	{
		internal bool _isOn;
        [SerializeField]
        internal List<GameObject> _allOn;
        [SerializeField]
        internal bool _oneShot;
		[SerializeField]
		List<GameObject> _gates;

		public List<GameObject> Gates
		{
			get{ return _gates; }
			set{ _gates = value; }
		}
		
		public List<GameObject> AllOn
		{
			get{ return _allOn; }
			set{ _allOn = value; }
		}


        public override void Awake () 
		{
			_isOn = false;
            AllowMoveBack = false;
		}
        public override void Start () { }

        public override void Update () { }

        /// <summary>
        /// Call it in child class at the end of the method
        /// </summary>
        /// <param name="force">No use here</param>
        internal override void takeDamage ( int force, Action actionAttacker )
        {
            foreach ( GameObject allOn in AllOn )
                {
                    allOn.SetActive( true );
                }
            
            if ( _oneShot )
            {
                GameObject.Destroy( this );
            }
        }
	}
}