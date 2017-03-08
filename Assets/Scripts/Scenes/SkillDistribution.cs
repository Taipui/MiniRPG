using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class SkillDistribution : MonoBehaviour
{
	GameManager gm;

	int partyNum = 0;

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
		PartyNumText.text = (partyNum + 1).ToString() + '/' + GameManager.PartyLen;
	}

	void distribute() {
		int skillPoint = GameManager.MaxSkillPoint;
		tempStatus = new List<int>(GameManager.Instance.Party[partyNum].StatusList);
		showTempStatus(tempStatus);

		for (int i = 0; i < GameManager.StatusLen; ++i) {
			int tempSkillPoint = 0;
			if (i < GameManager.StatusLen - 1) {
				tempSkillPoint = Random.Range(-1, skillPoint + tempStatus[i] > GameManager.MaxSkillPoint ? skillPoint - tempStatus[i] : skillPoint) + 1;
			} else {
				tempSkillPoint = skillPoint;
			}
//			Debug.Log("tempSkillPoint:" + tempSkillPoint);
			tempStatus[i] += tempSkillPoint;
//			Debug.Log("tempStatus[" + i + "]:" + tempStatus[i]);
			skillPoint -= tempSkillPoint;
//			Debug.Log("skillPoint:" + skillPoint);

			StatusValueTexts[i].text = tempStatus[i].ToString();
//			Debug.Log("StatusValueTexts[" + i + "].text:" + StatusValueTexts[i].text);

		}
	}

	void showTempStatus(List<int> tempStatus) {
		for (int i = 0; i < GameManager.StatusLen; ++i) {
			Debug.Log("tempStatus[" + i + "]:" + tempStatus[i]);
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
			
		jobInfoUpdate();

		distribute();

		RetryButton.OnClickAsObservable()
			.Subscribe(_ => {
				Debug.Log("RetryButton clicked");
				distribute();
			})
			.AddTo(this);

		DecideButton.OnClickAsObservable()
			.Subscribe(_ => {
				GameManager.Instance.Party[partyNum++].StatusList = tempStatus;
				jobInfoUpdate();
				distribute();
			})
			.AddTo(this);
	}
	
	void Update()
	{
		
	}
}
