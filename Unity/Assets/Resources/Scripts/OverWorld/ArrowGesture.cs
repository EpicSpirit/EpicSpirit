using UnityEngine;
using System.Collections;

namespace EpicSpirit.Game
{
    public class ArrowGesture : MonoBehaviour
    {
        MapNode _baseNode;
        MapNode _linkedNode;
        bool _move;

        public bool Move
        {
            get { return _move; }
            set { _move = value; }
        }

        public MapNode BaseNode
        {
            get { return _baseNode; }
            set { _baseNode = value; }
        }
        public MapNode LinkedNode
        {
            get { return _linkedNode; }
            set { _linkedNode = value; }
        }

        void Awake ()
        {
            _move = false;
        }

        public void OnMouseUp()
        {
            //_move = _linkedNode.IsLocked ? false : true;
            _move = true;
        }
    }
}