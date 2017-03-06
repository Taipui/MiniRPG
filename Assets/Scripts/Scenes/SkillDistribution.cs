using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class SkillDistribution : MonoBehaviour
{
	GameManager gm;

	int partyNum = 1;

	List<int> tempStatus = new List<int>();

	/// <summary>
	/// partyNumを表示するテキスト
	/// </summary>
	[SerializeField]
	Text PartyNumText;

	/// <summary>
	/// 現在選択している職業名
	/// </summary>
	[SerializeField]
	Text JobNameText;

	/// <summary>
	/// 各ステータスの値のテキスト
	/// </summary>
	[SerializeField]
	List<Text> StatusValueTexts = new List<Text>();

	/// <summary>
	/// スキルを振り直すボタン
	/// </summary>
	[SerializeField]
	Button RetryButton;

	/// <summary>
	/// 職業を決定するボタン
	/// </summary>
	[SerializeField]
	Button DecideButton;

	void jobInfoUpdate() {
		JobNameText.text = gm.Party[partyNum].ActorName;
	}

	void distribute() {
		int skillPoint = Actor.MaxSkillPoint;
		tempStatus = gm.Party[partyNum].StatusList;

		for (int i = 0; i < Actor.statusLen; ++i) {
			if (i < Actor.statusLen - 1) {
				int tempSkillPoint = Random.Range(0, skillPoint > tempStatus[i] ? skillPoint - tempStatus[i] : skillPoint) - 1;
				tempStatus[i] += tempSkillPoint;
				skillPoint -= tempSkillPoint;
			} else {
				tempStatus[i] += skillPoint;
			}
		}

		for (int i = 0; i < Actor.statusLen; ++i) {
			StatusValueTexts[i].text = tempStatus[i].ToString();
		}
	}

	void Start()
	{
		Debug.Log("SkillDistributionStart");

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

		PartyNumText.text = partyNum.ToString() + '/' + Actor.partyLen;
		JobNameText.text = gm.Party[partyNum - 1].ActorName;

		distribute();

		RetryButton.OnClickAsObservable()
			.Subscribe(_ => {
				Debug.Log("RetryButton clicked");
				distribute();
			})
			.AddTo(this);

		DecideButton.OnClickAsObservable()
			.Subscribe(_ => {
				gm.Party[partyNum++ - 1].StatusList = tempStatus;
				PartyNumText.text = partyNum.ToString() + '/' + Actor.partyLen;
				JobNameText.text = gm.Party[partyNum - 1].ActorName;
			})
			.AddTo(this);
	}
	
	void Update()
	{
		
	}
}
