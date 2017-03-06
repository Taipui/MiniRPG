using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UniRx;

public class PartySelect : MonoBehaviour
{
	GameManager gm;

	List<Actor> jobs = new List<Actor>();

	int partyNum = 1;

	/// <summary>
	/// 現在選択している職業
	/// </summary>
	int currentJob = 0;

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
	/// 職業の説明文を表示するテキスト
	/// </summary>
	[SerializeField]
	Text DescriptionText;

	/// <summary>
	/// 次の職業へ移るボタン
	/// </summary>
	[SerializeField]
	Button NextButton;

	/// <summary>
	/// 前の職業へ移るボタン
	/// </summary>
	[SerializeField]
	Button PrevButton;

	/// <summary>
	/// 職業を決定するボタン
	/// </summary>
	[SerializeField]
	Button DecideButton;

	void jobInfoUpdate() {
		JobNameText.text = jobs[currentJob].ActorName;
		StatusValueTexts[(int)Actor.Status.HP].text = jobs[currentJob].StatusList[(int)Actor.Status.HP].ToString();
		StatusValueTexts[(int)Actor.Status.MP].text = jobs[currentJob].StatusList[(int)Actor.Status.MP].ToString();
		StatusValueTexts[(int)Actor.Status.ATTACK].text = jobs[currentJob].StatusList[(int)Actor.Status.ATTACK].ToString();
		StatusValueTexts[(int)Actor.Status.DEFENCE].text = jobs[currentJob].StatusList[(int)Actor.Status.DEFENCE].ToString();
		StatusValueTexts[(int)Actor.Status.SPEED].text = jobs[currentJob].StatusList[(int)Actor.Status.SPEED].ToString();			
		DescriptionText.text = jobs[currentJob].Description;
	}

	void Awake()
	{
		Debug.Log("PartySelectAwake");

		Brave brave = gameObject.AddComponent<Brave>();
		Warrior warrior = gameObject.AddComponent<Warrior>();
		Monk monk = gameObject.AddComponent<Monk>();
		Witch witch = gameObject.AddComponent<Witch>();
		
		jobs.Add(brave);
		jobs.Add(warrior);
		jobs.Add(monk);
		jobs.Add(witch);	
	}

	void Start()
	{
		Debug.Log("PartySelectStart");

		if (GameObject.Find("GameManager") == null) {
			GameObject go = new GameObject("GameManager");
			gm = go.AddComponent<GameManager>();
		} else {
			gm = GameObject.Find("GameManager").GetComponent<GameManager>();
		}

		PartyNumText.text = partyNum.ToString() + '/' + GameManager.PartyLen;
		jobInfoUpdate();
	
		NextButton.OnClickAsObservable()
			.Subscribe(_ => {
				currentJob = (++currentJob + GameManager.JobLen) % GameManager.JobLen;
				jobInfoUpdate();
			})
			.AddTo(this);

		PrevButton.OnClickAsObservable()
			.Subscribe(_ => {
				currentJob = (--currentJob + GameManager.JobLen) % GameManager.JobLen;
				jobInfoUpdate();
			})
			.AddTo(this);
	
		DecideButton.OnClickAsObservable()
			.Subscribe(_ => {
				gm.Party.Add(jobs[currentJob]);
				++partyNum;
				if (partyNum == GameManager.PartyLen + 1) {
					SceneManager.LoadScene("SkillDistribution");
				} else {
					PartyNumText.text = partyNum.ToString() + '/' + GameManager.PartyLen;
					currentJob = 0;
					jobInfoUpdate();
				}
			})
			.AddTo(this);
	}
	
	void Update()
	{
		
	}
}
