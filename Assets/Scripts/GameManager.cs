using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager
{
	/// <summary>
	/// パーティーの人数
	/// </summary>
	public const int PartyLen = 3;

	List<Actor> party = new List<Actor>();
	public List<Actor> Party {
		get
		{
			return party;
		}
	}

	void Start()
	{

	}
}
