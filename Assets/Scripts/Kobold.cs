﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Kobold : Enemy
{
	public override void commandSelect() {
		command = Commnads.ATTACK;
	}

	void Start()
	{
		
	}
	
	void Update()
	{
		
	}
}
