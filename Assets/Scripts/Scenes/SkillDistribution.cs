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
		JobNameText.text = GameManager.Instance.Party[partyNum].ActorName;
	}

	void distribute() {
		int skillPoint = GameManager.MaxSkillPoint;
		tempStatus = GameManager.Instance.Party[partyNum].StatusList;

		for (int i = 0; i < GameManager.StatusLen; ++i) {
			if (i < GameManager.StatusLen - 1) {
				int tempSkillPoint = Random.Range(0, skillPoint > tempStatus[i] ? skillPoint - tempStatus[i] : skillPoint) - 1;
				tempStatus[i] += tempSkillPoint;
				skillPoint -= tempSkillPoint;
			} else {
				tempStatus[i] += skillPoint;
			}
		}

		for (int i = 0; i < GameManager.StatusLen; ++i) {
			StatusValueTexts[i].text = tempStatus[i].ToString();
		}
	}

	void Start()
	{
		Debug.Log("SkillDistributionStart");

		if (GameManager.Instance.Party.Count == 0) {
			Brave brave = gameObject.AddComponent<Brave>();
			Warrior warrior = gameObject.AddComponent<Warrior>();
			Monk monk = gameObject.AddComponent<Monk>();

			GameManager.Instance.Party.Add(brave);
			GameManager.Instance.Party.Add(warrior);
			GameManager.Instance.Party.Add(monk);
		}

		PartyNumText.text = partyNum.ToString() + '/' + GameManager.PartyLen;
		JobNameText.text = GameManager.Instance.Party[partyNum - 1].ActorName;

		distribute();

		RetryButton.OnClickAsObservable()
			.Subscribe(_ => {
				Debug.Log("RetryButton clicked");
				distribute();
			})
			.AddTo(this);

		DecideButton.OnClickAsObservable()
			.Subscribe(_ => {
				GameManager.Instance.Party[partyNum++ - 1].StatusList = tempStatus;
				PartyNumText.text = partyNum.ToString() + '/' + GameManager.PartyLen;
				JobNameText.text = GameManager.Instance.Party[partyNum - 1].ActorName;
			})
			.AddTo(this);
	}
	
	void Update()
	{
		
	}
}
