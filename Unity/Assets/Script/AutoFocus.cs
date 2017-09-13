using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace EpicSpirit.Game
{
    public class AutoFocus : MonoBehaviour {

        HashSet<Character> _nearTargets;
        Character _me;

        void Awake()
        {
            _nearTargets = new HashSet<Character>();
            _me = this.GetComponentInParent<Character>();
            if(_me == null)
            {
                Debug.LogError("Character Component not found");
            }
        }

        void OnTriggerEnter(Collider other)
        {
            var character = other.GetComponent<Character>();
            if(character != null && character != _me)
            {
                _nearTargets.Add(character);
            }
            else
            {
                Debug.LogWarning("Character not found");
            }
            
        }

        void OnTriggerExit(Collider other)
        {

            var character = other.GetComponent<Character>();
            if (character != null)
            {
                _nearTargets.Remove(character);
            }
            else
            {
                Debug.LogWarning("Character not found");
            }
        }
        
        public Character FindNearestCharacter()
        {
            List<Character> nextToRemove = new List<Character>();
            var enumerator = _nearTargets.GetEnumerator();
            Character c = null;
            float range = float.MaxValue;

            while(enumerator.MoveNext())
            {
                var actualCharacter = enumerator.Current;

                if (actualCharacter == null)
                {
                    nextToRemove.Add(actualCharacter);
                    continue;
                }

                var actualRange = Vector3.Distance(_me.transform.position, actualCharacter.transform.position);

                if( actualRange < range)
                {
                    range = actualRange;
                    c = actualCharacter;
                }
            }
            enumerator.Dispose();
            foreach(var t in nextToRemove)
            {
                _nearTargets.Remove(t);
            }

            return c;
        }


    }
}
