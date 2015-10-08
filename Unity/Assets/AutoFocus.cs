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
                Debug.Log("New Ennemis " + other.name);
                _nearTargets.Add(character);
            }
            else
            {
                Debug.LogWarning("Character not found");
            }
            
        }

        void OnTriggerExit(Collider other)
        {
            Debug.Log("Exit Ennemis " + other.name);

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
            var enumerator = _nearTargets.GetEnumerator();
            Character c = null;
            float range = float.MaxValue;

            while(enumerator.MoveNext())
            {
                var actualCharacter = enumerator.Current;
                var actualRange = Vector3.Distance(_me.transform.position, actualCharacter.transform.position);

                if( actualRange < range)
                {
                    range = actualRange;
                    c = actualCharacter;
                }
            }

            Debug.Log(c);
            return c;
        }


    }
}