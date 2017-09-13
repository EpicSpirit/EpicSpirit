using UnityEngine;
using System.Collections;

namespace EpicSpirit.Game
{
    public class SplitLastShape : Split
    {
        BadBoy _badBoy1;
        BadBoy _badBoy2;
        bool _started;

        public override void Awake()
        {
            base.Awake();
            _child = "Characters/Prefab/BadBoySplitterAsBadBoy";
        }
    }
}
