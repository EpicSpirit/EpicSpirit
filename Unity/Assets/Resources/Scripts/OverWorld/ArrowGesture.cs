using UnityEngine;
using System.Collections;

namespace EpicSpirit.Game
{
    public class ArrowGesture : MonoBehaviour
    {
        MapNode _linkedNode;
        bool _move;

        public bool Move
        {
            get { return _move; }
        }

        public MapNode LinkedNode
        {
            get { return _linkedNode; }
            set { _linkedNode = value; }
        }

        void Start()
        {
            _move = false;
        }

        public void OnMouseUp()
        {
            _move = true;
        }
    }
}