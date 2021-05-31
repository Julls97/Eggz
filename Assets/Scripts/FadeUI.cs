using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FadeUI : MonoBehaviour {
	[SerializeField] private bool hideOnStart = true;
	[SerializeField] private TextMeshProUGUI text;

	private float speed = 4;
	private List<Image> images;
	private List<TextMeshProUGUI> texts;

	private void Start(){
		images = GetComponentsInChildren<Image>().ToList();
		texts = GetComponentsInChildren<TextMeshProUGUI>().ToList();

		if (hideOnStart) Fade(false);
	}

	public void SetText(string data){
		if (text != null) text.text = data;
	}

	public void Fade(bool fadeIn){
		if (fadeIn) FadeIn();
		else FadeOut();
	}

	public void FadeIn(){
		foreach (var image in images) Utility.FadeAlpha(this, image, speed, 1, action: () => image.raycastTarget = true);
		foreach (var text in texts) Utility.FadeAlpha(this, text, speed, 1, action: () => text.raycastTarget = true);
	}
	public void FadeOut(){
		foreach (var image in images) Utility.FadeAlpha(this, image, speed, 0, action: () => image.raycastTarget = false);
		foreach (var text in texts) Utility.FadeAlpha(this, text, speed, 0, action: () => text.raycastTarget = false);
	}

	private IEnumerator Fade(Image image, float alpha){
		var t = 0f;

		var c = image.color;
		var targetColor = new Color(c.r, c.g, c.b, alpha);
		
		while (t < 1) {
			t += Time.deltaTime * speed;
			image.color = Color.Lerp(c, targetColor, t);
			yield return null;
		}

		image.raycastTarget = alpha != 0;
	}
	private IEnumerator Fade(TextMeshProUGUI text, float alpha){
		var t = 0f;

		var c = text.color;
		var targetColor = new Color(c.r, c.g, c.b, alpha);
		while (t < 1) {
			t += Time.deltaTime * speed;
			text.color = Color.Lerp(c, targetColor, t);
			yield return null;
		}
		
		text.raycastTarget = alpha != 0;
	}
}