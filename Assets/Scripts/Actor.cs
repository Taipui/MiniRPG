using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour
{
	/// <summary>
	/// 最下階
	/// </summary>
	public const int MaxFloor = 10;

	/// <summary>
	/// 次のメッセージが表示されるまでの待ち時間
	/// </summary>
	public const float TextWaitTime = 1.0f;

	public enum Jobs {
		BRAVE,
		WARRIOR,
		MONK,
		WITCH
	};
	
	public enum Status {
		HP,
		MP,
		ATTACK,
		DEFENCE,
		SPEED
	};
	
	public enum Commnads {
		ATTACK,
		MAGIC,
		ESCAPE,
		BACK,
		MAGIC1,
		MAGIC2,
		NONE = -1
	};

	/// <summary>
	/// 職業及び敵の名前
	/// </summary>
	protected string actorName;
	public string ActorName {
		get
		{
			return actorName;
		}
	}

	/// <summary>
	/// ステータス
	/// </summary>
	protected List<int> status = new List<int>();
	public List<int> StatusList {
		get
		{
			return status;
		}
		set
		{
			status = value;
		}
	}

	/// <summary>
	/// 命令
	/// </summary>
	protected Commnads command;

	/// <summary>
	/// 累積経験値及び得られる経験値
	/// </summary>
	protected int exp;

	/// <summary>
	/// レベル
	/// </summary>
	protected int lv;

	/// <summary>
	/// 逃げられるかどうか
	/// </summary>
	protected bool canEscape;

	/// <summary>
	/// 職業の説明
	/// </summary>
	protected string description;
	public string Description {
		get
		{
			return description;
		}
	}

	protected Actor() {
		Debug.Log("ActorConstructor");
	}

	void Awake()
	{
		Debug.Log("ActorAwake");
	}
	
	void Update()
	{
		
	}
}
