using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace EpicSpirit.Game
{
	public class TempleSwitch : Character
	{
		bool _isOn;
		[SerializeField]
		List<GameObject> _gates;

		public List<GameObject> Gates
		{
			get{ return _gates; }
			set{ _gates = value; }
		}


		void Start () 
		{
			_isOn = false;
		}

		void Update()
		{
		}

		internal override void takeDamage (int force)
		{
			if (_isOn == false) 
			{
				_isOn = true;
				transform.Rotate( new Vector3 ( 90 ,0 ,0  ) );
			} 
			else
			{
				_isOn = false;
				transform.Rotate (new Vector3 (-90, 0, 0));
			}

			foreach( GameObject gate in _gates )
			{
				if( gate.activeSelf )
				{
					gate.SetActive(false);
				}
				else
				{
					gate.SetActive(true);
				}
			}

		}
	}
}