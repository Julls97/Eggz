using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class Controller : MonoBehaviour {
	[SerializeField] private List<GameObject> panels;
	[SerializeField] private Egg egg;
	[SerializeField] private FadeUI gameOverPanel;
	[SerializeField] private TextMeshProUGUI scoreText;
	[SerializeField] private TextMeshProUGUI addScore;

	private int score = 0;

	public static Controller Instance;

	private void Awake(){
		if (Instance == null) Instance = this;
		
		foreach (var panel in panels) panel.SetActive(true);
	}

	private void Start(){
		Pause();
		ResetScore();
	}

	public void ResetScore(){
		score = 0;
		scoreText.text = "00000";
	}

	public void AddScore(int count){
		score += count;
		scoreText.text = score.ToString();

		addScore.text = $"+{count}";
		StartCoroutine(ScaleScore());
	}
	
	private IEnumerator ScaleScore(float speed = 3.5f){
		var t = 0f;

		while (t < 0.75f) {
			t += Time.deltaTime * speed;
			addScore.transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, t);
			yield return null;
		}
		
		t = 0f;
		while (t <  0.75f) {
			t += Time.deltaTime * speed * 1.5f;
			addScore.transform.localScale = Vector3.Lerp(Vector3.one, Vector3.zero, t);
			yield return null;
		}
		
		addScore.transform.localScale = Vector3.zero;
	}

	public void GameOver(){
		Pause();
		gameOverPanel.SetText(score.ToString());
		gameOverPanel.FadeIn();
	}

	public void Play(){
		egg.Play();
	}	
	public void Pause(){
		egg.Stop();
	}
}