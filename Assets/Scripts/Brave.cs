using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brave : Fighter
{
	Brave() {
		Debug.Log("BraveConstructor");
		
		actorName = "ゆうしゃ";
		status.Add(2);
		status.Add(0);
		status.Add(1);
		status.Add(1);
		status.Add(1);
		description = "まんべんなくスキルがある。\nオールマイティーではあるがきようびんぼう。";
	}

	void Start()
	{
		Debug.Log("BraveStart");


	}
	
	void Update()
	{
		
	}
}
