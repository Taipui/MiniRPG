using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class Dungeon : MonoBehaviour
{
	GameManager gm;

	List<Enemy> enemyList = new List<Enemy>();

	/// <summary>
	/// 現在のフロア
	/// </summary>
	int floor = 0;

	[SerializeField]
	GameObject MessageWindow;
	/// <summary>
	/// メッセージウインドウのテキスト
	/// </summary>
	[SerializeField]
	Text Message;

	[SerializeField]
	GameObject StatusWindow;

	/// <summary>
	/// 各ステータスの値のテキスト
	/// </summary>
	[SerializeField]
	List<Text> StatusValueTexts = new List<Text>();

	[SerializeField]
	GameObject JobNameWindow;

	[SerializeField]
	Text JobNameText;

	[SerializeField]
	GameObject CommandWindow;

	/// <summary>
	/// 「こうげき」ボタン
	/// </summary>
	[SerializeField]
	Button AttackButton;

	/// <summary>
	/// 「まほう」ボタン
	/// </summary>
	[SerializeField]
	Button MagicButton;

	/// <summary>
	/// 「にげる」ボタン
	/// </summary>
	[SerializeField]
	Button EscapeButton;

	[SerializeField]
	GameObject Arrow;

	[SerializeField]
	GameObject DecideButtonObject;

	[SerializeField]
	Button DecideButton;

	/// <summary>
	/// attack関数の引数に使用
	/// </summary>
	const bool PLAYER_TURN = true;
	const bool ENEMY_TURN = false;

	/// <summary>
	/// 現在選択している命令
	/// </summary>
	Actor.Commnads command;

	bool isAlive() { return gm.Party.Count > 0; }

	void battle() {
		while (true) {
			int enemySpeed = enemyList[0].StatusList[(int)Actor.Status.SPEED];
			gm.Party.Sort((a, b) => b.StatusList[(int)Actor.Status.SPEED] - a.StatusList[(int)Actor.Status.SPEED]);
			if (enemySpeed > gm.Party[0].StatusList[(int)Actor.Status.SPEED]) {
				enemyTurn();
				if (!isAlive()) {
					break;
				}
				playerTurn();
				if (!isAlive()) {
					break;
				}
			} else {
				playerTurn();
				if (!isAlive()) {
					break;
				}
				enemyTurn();
				if (!isAlive()) {
					break;
				}
			}
			break;
		}
	}

	string convertToFullWidth(int value) {
		const int ConvertionConstant = 65248;
		string fullWidthStr = null;
		string halfWidthStr = value.ToString();
		for (int i = 0; i < halfWidthStr.Length; ++i) {
			fullWidthStr += (char)(halfWidthStr[i] + ConvertionConstant);
		}
		return fullWidthStr;
	}

	void attack(bool isPlayerTurn, int index = 0) {
		if (!!isPlayerTurn) {
			
		} else {
			Message.text = enemyList[0].ActorName + "のこうげき！";
			int enemyAttack = enemyList[0].StatusList[(int)Actor.Status.ATTACK];
			int partyDefence = gm.Party[index].StatusList[(int)Actor.Status.DEFENCE];
			if (enemyAttack - partyDefence > 0) {
				int damage = enemyAttack - partyDefence;
				Observable.Return(0)
					.Delay(System.TimeSpan.FromSeconds(1))
					.Subscribe(_ => {
						Message.text += "\n" + gm.Party[index].ActorName + "に" + convertToFullWidth(damage) + "のダメージ！";
					})
					.AddTo(this);
			} else {
				Message.text += "\nダメージをあたえられない！";
			}
		}
	}

	void enemyTurn() {
		/// <summary>
		/// 攻撃対象
		/// </summary>
		int attackIndex = Random.Range(0, gm.Party.Count - 1);
		enemyList[0].commandSelect();
		attack(ENEMY_TURN, attackIndex);
	}

	void playerTurn() {
	}


	void Start()
	{
		if (GameObject.Find("GameManager") == null) {
			GameObject go = new GameObject("GameManager");
			gm = go.AddComponent<GameManager>();
			Brave brave = go.AddComponent<Brave>();
			Warrior warrior = go.AddComponent<Warrior>();
			Monk monk = go.AddComponent<Monk>();
			gm.Party.Add(brave);
			gm.Party.Add(warrior);
			gm.Party.Add(monk);
		} else {
			gm = GameObject.Find("GameManager").GetComponent<GameManager>();
		}

		Slime slime = gameObject.AddComponent<Slime>();
		enemyList.Add(slime);

		while (floor++ < Actor.MaxFloor) {
			Message.text = enemyList[0].ActorName + "があらわれた！";
			Observable.Return(0)
				.Delay(System.TimeSpan.FromSeconds(Actor.TextWaitTime))
				.Subscribe(_ => {
					MessageWindow.SetActive(false);

					StatusValueTexts[(int)Actor.Status.HP].text = gm.Party[0].StatusList[(int)Actor.Status.HP].ToString();
					StatusValueTexts[(int)Actor.Status.MP].text = gm.Party[0].StatusList[(int)Actor.Status.MP].ToString();
					StatusValueTexts[(int)Actor.Status.ATTACK].text = gm.Party[0].StatusList[(int)Actor.Status.ATTACK].ToString();
					StatusValueTexts[(int)Actor.Status.DEFENCE].text = gm.Party[0].StatusList[(int)Actor.Status.DEFENCE].ToString();
					StatusValueTexts[(int)Actor.Status.SPEED].text = gm.Party[0].StatusList[(int)Actor.Status.SPEED].ToString();
					StatusWindow.SetActive(true);

					JobNameText.text = gm.Party[0].ActorName;
					JobNameWindow.SetActive(true);

					CommandWindow.SetActive(true);
					command = Actor.Commnads.ATTACK;

					DecideButtonObject.SetActive(true);

					battle();
				})
				.AddTo(this);
					
			if (!isAlive()) {
				return;
			}
//			Message.text = "オーブをみつけた！";
		}

		AttackButton.OnClickAsObservable()
			.Subscribe(_ => {
				Arrow.transform.position = new Vector3(Arrow.transform.position.x, AttackButton.transform.position.y - 5, 0);
			})
			.AddTo(this);

		MagicButton.OnClickAsObservable()
			.Subscribe(_ => {
				Arrow.transform.position = new Vector3(Arrow.transform.position.x, MagicButton.transform.position.y - 5, 0);
			})
			.AddTo(this);

		EscapeButton.OnClickAsObservable()
			.Subscribe(_ => {
				Arrow.transform.position = new Vector3(Arrow.transform.position.x, EscapeButton.transform.position.y - 5, 0);
			})
			.AddTo(this);

		DecideButton.OnClickAsObservable()
			.Subscribe(_ => {
			})
			.AddTo(this);
	}
	
	void Update()
	{
		
	}

	void partyShow() {
		for (int i = 0; i < Actor.partyLen; ++i) {
			Debug.Log(gm.Party[i].ActorName);
		}
	}
}
