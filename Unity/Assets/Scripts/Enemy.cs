using UnityEngine;
using System.Collections;


public class Enemy : Character
{
    public int _aggroArea;

    void Start()
    {
        if ( _aggroArea == 0 )
        {
            _aggroArea = 8;
        }
        Health = 3;
        Speed = 2;
    }
    void Update()
    {

    }
}

