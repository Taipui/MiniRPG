using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
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
