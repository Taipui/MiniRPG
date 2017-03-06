using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{

	void Start()
	{
		this.UpdateAsObservable().Where(x => Input.GetMouseButtonDown(0))
			.Subscribe(_ => {
				SceneManager.LoadScene("PartySelect");
			})
			.AddTo(this);
	}
	
	void Update()
	{
		
	}
}
