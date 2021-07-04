using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FadeUI : MonoBehaviour {
	[SerializeField] private bool hideOnStart = true;
	[SerializeField] private TextMeshProUGUI text;
	[SerializeField] private Text text1;

	private float speed = 4;
	private List<Image> images;
	private List<TextMeshProUGUI> texts;
	private List<Text> texts1;
	
	public List<Component> renderers;

	private void Start(){
		images = GetComponentsInChildren<Image>().ToList();
		texts = GetComponentsInChildren<TextMeshProUGUI>().ToList();
		texts1 = GetComponentsInChildren<Text>().ToList();

		// renderers = new List<Component>();
		// foreach (var image in images) renderers.Add(image.GetComponent<Image>());
		// foreach (var text in texts) renderers.Add(text.GetComponent<TextMeshProUGUI>());
		// foreach (var text in texts1) renderers.Add(text.GetComponent<Text>());

		if (hideOnStart) Fade(false);
	}

	public void SetText(string data){
		if (text != null) text.text = data;   
		if (text1 != null) text1.text = data; 
	}

	public void Fade(bool fadeIn){
		if (fadeIn) FadeIn();
		else FadeOut();
	}

	public void FadeIn(){
		foreach (var image in images) Utility.FadeAlpha(this, image, speed, 1, action: () => image.raycastTarget = true);
		foreach (var text in texts) Utility.FadeAlpha(this, text, speed, 1, action: () => text.raycastTarget = true);
		foreach (var text in texts1) Utility.FadeAlpha(this, text, speed, 1, action: () => text.raycastTarget = true);

		// foreach (var renderer in renderers) Utility.FadeAlpha(this, renderer, speed, 1, action: () => renderer.SetRaycastTarget(true));
	}
	public void FadeOut(){
		foreach (var image in images) Utility.FadeAlpha(this, image, speed, 0, action: () => image.raycastTarget = false);
		foreach (var text in texts) Utility.FadeAlpha(this, text, speed, 0, action: () => text.raycastTarget = false);
		foreach (var text in texts1) Utility.FadeAlpha(this, text, speed, 0, action: () => text.raycastTarget = false);
		
		// foreach (var renderer in renderers) Utility.FadeAlpha(this, renderer, speed, 1, action: () => renderer.SetRaycastTarget(false));
	}
}