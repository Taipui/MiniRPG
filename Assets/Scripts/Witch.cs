using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Witch : Magician
{
	Witch() {
		Debug.Log("WitchConstructor");

		actorName = "まほうつかい";
		status.Add(1);
		status.Add(3);
		status.Add(0);
		status.Add(0);
		status.Add(0);
		description = "こうげきまほうがつかえるが、\nほかのステータスがひくい。";
	}

	void Start()
	{
		
	}
	
	void Update()
	{
		
	}
}
