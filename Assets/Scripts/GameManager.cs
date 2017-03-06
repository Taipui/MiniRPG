using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager
{
	/// <summary>
	/// パーティーの人数
	/// </summary>
	public const int PartyLen = 3;

	/// <summary>
	/// 職業の数
	/// </summary>
	public const int JobLen = 4;

	/// <summary>
	/// ステータスの項目数
	/// </summary>
	public const int StatusLen = 5;

	/// <summary>
	/// 分配できるスキルポイント数
	/// </summary>
	public const int MaxSkillPoint = 10;

	List<Actor> party = new List<Actor>();
	public List<Actor> Party {
		get
		{
			return party;
		}
	}

	static GameManager instance = new GameManager();
	public static GameManager Instance {
		get
		{
			if (instance == null) {
				instance = new GameManager();
			}
			return instance;
		}
	}

	void Start()
	{

	}
}
