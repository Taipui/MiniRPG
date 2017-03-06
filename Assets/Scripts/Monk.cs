using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monk : Magician
{
	Monk() {
		Debug.Log("MonkConstructor");

		actorName = "そうりょ";
		status.Add(1);
		status.Add(3);
		status.Add(0);
		status.Add(0);
		status.Add(0);
		description = "かいふくまほうがつかえるが、\nほかのステータスがひくい。";
	}

	void Start()
	{
		
	}
	
	void Update()
	{
		
	}
}
