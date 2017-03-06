using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior : Fighter
{
	Warrior() {
		Debug.Log("WarriorConstructor");

		actorName = "せんし";
		status.Add(3);
		status.Add(0);
		status.Add(2);
		status.Add(1);
		status.Add(0);
		description = "たくましいが、すばやさはない。のうきん。";
	}


	void Start()
	{
		Debug.Log("WarriorStart");
	}
	
	void Update()
	{
		
	}
}
