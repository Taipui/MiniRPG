using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Slime : Enemy
{
	Slime() {
		Debug.Log("SlimeConstructor");

		actorName = "スライム";
		status.Add(2);
		status.Add(0);
		status.Add(1);
		status.Add(1);
		status.Add(1);
	}

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
