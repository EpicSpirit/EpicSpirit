﻿using System;
using UnityEngine;
using System.Collections;


public class Spi : Character
{

	
	void Start()
	{
		Initialisation();
		
		_life = 20;
		_lookaroundcount = 300;
	}
	
	void Update()
	{
		Gravity();
	}
	
	
}

