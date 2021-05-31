using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public static class Utility {
	public static float GetWidthScreen => Vector3.Distance(ViewportToWorldPoint(0f, 0f), ViewportToWorldPoint(1f, 0f));

	public static float GetHeightScreen => Vector3.Distance(ViewportToWorldPoint(0f, 0f), ViewportToWorldPoint(0f, 1f));

	public static bool GetBool => Random.Range(0, 2) == 0;

		
	public struct WorldPositionSet
	{
		public Vector3 Position;
		public Quaternion Rotation;
		public Vector2 Size;
	}
	/*public static WorldPositionSet[] GetWorldPositionSet(int count, bool inPixels = true, bool experimental = false)
		{
			var positionSet = new WorldPositionSet[count];
			var size = MainConfig.NativeScreenSize;

			var splitY = Mathf.FloorToInt(Mathf.Sqrt(count));
			var splitX = count / splitY;

			var xDots = new float[splitX];
			var yDots = new float[splitY];

			for (var i = 0; i < splitY; i++)
				yDots[i] = (2f * i + 1f) / (splitY * 2f);

			for (var i = 0; i < splitX; i++)
				xDots[i] = (2f * i + 1f) / (splitX * 2f);

			var sign = 1;
			for (var i = 0; i < splitX; i++)
			{
				sign *= -1;
				for (var j = 0; j < splitY; j++)
				{
					var index = i * splitY + j;
					positionSet[index].Position = Camera.main.ViewportToWorldPoint(new Vector2(xDots[i], yDots[j]));
					positionSet[index].Position.z = -0.1f;
					var z = Vector2.SignedAngle(Vector2.up, -positionSet[index].Position);
					var dot = Vector2.Dot(Vector2.up, -positionSet[index].Position.normalized);

					if (dot > 0f)
						z = 0f;
					if (dot < 0f)
						z = 180f;
					if (experimental)
						z = 90f * sign;

					var x = size.x / splitX;
					var y = size.y / splitY;

					positionSet[index].Size = (int)Mathf.Abs(z) == 90 ? new Vector2(y, x) : new Vector2(x, y);
					if (!inPixels)
						positionSet[index].Size /= Screen.height / (Camera.main.orthographicSize * 2f);
					positionSet[index].Rotation = Quaternion.Euler(0f, 0f, z);
				}
			}

			return positionSet;
		}*/
		
	public static Vector3 ViewportToWorldPoint(float x, float y) {
		if (Camera.main != null)
			return Camera.main.ViewportToWorldPoint(new Vector3(x, y, Camera.main.nearClipPlane));

		return Vector3.zero;
	}

	public static float Lerp(float startValue, float endValue, float pct) {
		return startValue + (endValue - startValue) * pct;
	}

	public static float EaseIn(float t) {
		return t * t;
	}

	public static float EasingLerp(float speed, float offset = 0f, float startTime = 0f) {
		var time = Mathf.Abs(Mathf.Sin((Time.time - startTime + offset) * speed));
		return Lerp(0, 1f, EaseIn(time / 1f));
	}


	public static Vector3 GetBezierPoint(Vector3 p0, Vector3 p1, Vector3 p2, float t) {
		var part1 = Mathf.Pow(1f - t, 2f) * p0;
		var part2 = 2 * t * (1f - t) * p1;
		var part3 = Mathf.Pow(t, 2f) * p2;
		var part4 = part1 + part2 + part3;
		return part4;
	}

	public static Vector3 GetBezierPoint(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t) {
		var part1 = Mathf.Pow(1f - t, 3f) * p0;
		var part2 = 3f * t * Mathf.Pow((1f - t), 2) * p1;
		var part3 = 3f * Mathf.Pow(t, 2f) * (1f - t) * p2;
		var part4 = Mathf.Pow(t, 3f) * p3;
		var part5 = part1 + part2 + part3 + part4;
		return part5;
	}

	public static Vector3[] GetBezier(Vector3 p0, Vector3 p1, Vector3 p2, float step = 0.05f) {
		var list = new List<Vector3> {p0};
		var t = 0f;
		while (t <= 1.001f) {
			list.Add(GetBezierPoint(p0, p1, p2, t));
			t += step;
		}

		return list.ToArray();
	}

	public static Vector3[] GetBezier(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float step = 0.05f) {
		var list = new List<Vector3> {p0};
		var t = 0f;
		while (t <= 1.001f) {
			list.Add(GetBezierPoint(p0, p1, p2, p3, t));
			t += step;
		}

		return list.ToArray();
	}

	public static bool IsNullOrEmpty<T>(this List<T> array) {
		return array == null || !array.Any();
	}

	public static void Shuffle<T>(this List<T> list) {
		var n = list.Count;
		while (n > 1) {
			var k = Random.Range(0, n--);
			var temp = list[n];
			list[n] = list[k];
			list[k] = temp;
		}
	}

	public static void Shuffle<T>(this T[] array) {
		var n = array.Length;
		while (n > 1) {
			var k = Random.Range(0, n--);
			var temp = array[n];
			array[n] = array[k];
			array[k] = temp;
		}
	}

	public static void ShuffleArray<T>(T[] array) {
		var n = array.Length;
		while (n > 1) {
			var k = Random.Range(0, n--);
			var temp = array[n];
			array[n] = array[k];
			array[k] = temp;
		}
	}

	public static void ShuffleList<T>(List<T> array) {
		var n = array.Count;
		while (n > 1) {
			var k = Random.Range(0, n--);
			var temp = array[n];
			array[n] = array[k];
			array[k] = temp;
		}
	}

	public static float Distance(this Transform from, Transform to) {
		return Vector3.Distance(from.position, to.position);
	}

	public static float Distance(this Vector3[] positions) {
		var distance = 0f;
		for (var i = 0; i < positions.Length - 1; i++) {
			distance += Vector3.Distance(positions[i], positions[i + 1]);
		}

		return distance;
	}

	public static float Distance(this List<Vector3> positions) {
		var distance = 0f;
		for (var i = 0; i < positions.Count - 1; i++) {
			distance += Vector3.Distance(positions[i], positions[i + 1]);
		}

		return distance;
	}
		
	public static Vector3[] RemoveNear(List<Vector3> positions, float range = 0.01f)
	{
		return RemoveNear(positions.ToArray(), range);
	}

	public static Vector3[] RemoveNear(Vector3[] positions, float range = 0.01f)
	{
		var list = new List<Vector3>
		{
			positions[0]
		};
		var oldPosition = positions[0];
		for (var i = 1; i < positions.Length; i++)
		{
			var distance = Vector3.Distance(positions[i], oldPosition);
			if (distance >= range)
			{
				list.Add(positions[i]);
				oldPosition = positions[i];
			}
		}

		return list.ToArray();
	}

	public static void SetAlpha(this Component component, float alpha) {
		IColorAdapter adapter = null;
			
		if (component is Image image) adapter = new ImageColorAdapter(image);
		if (component is SpriteRenderer sRenderer) adapter = new SpriteRendererColorAdapter(sRenderer);
		if (component is MeshRenderer mRenderer) adapter = new MeshRendererColorAdapter(mRenderer);
		if (component is Text text) adapter = new TextColorAdapter(text);
		if (component is TextMeshProUGUI textMeshProUgui) adapter = new TextMeshProUGUIColorAdapter(textMeshProUgui);
		if (component is TextMeshPro textMeshPro) adapter = new TextMeshProColorAdapter(textMeshPro);
			
		if (adapter == null) return;

		var color = adapter.Color;
		color.a = alpha;
		adapter.Color = color;
	}
		
	public static Coroutine FadeAlpha(MonoBehaviour caller, Component component, float speed, float value, float waitSecond = 0f, Action action = null) {
		IColorAdapter adapter = null;
			
		if (component is Image image) adapter = new ImageColorAdapter(image);
		if (component is SpriteRenderer sRenderer) adapter = new SpriteRendererColorAdapter(sRenderer);
		if (component is MeshRenderer mRenderer) adapter = new MeshRendererColorAdapter(mRenderer);
		if (component is Text text) adapter = new TextColorAdapter(text);
		if (component is TextMeshProUGUI textMeshProUgui) adapter = new TextMeshProUGUIColorAdapter(textMeshProUgui);
		if (component is TextMeshPro textMeshPro) adapter = new TextMeshProColorAdapter(textMeshPro);
			
		if (adapter != null) return caller.StartCoroutine(AlphaFader(adapter, speed, value, waitSecond, action));
		else {
			Debug.LogError($"Wrong Component {component.GetType()}");
			return null;
		}
	}

	private static IEnumerator AlphaFader(IColorAdapter adapter, float speed, float value, float waitSecond = 0f,
		Action action = null) {
		if (waitSecond > 0)
			yield return new WaitForSeconds(waitSecond);

		var c = adapter.Color;
		var destinationColor = new Color(c.r, c.g, c.b, value);

		var t = 0f;

		while (t < 1f) {
			t += speed * Time.deltaTime;
			adapter.Color = Color.Lerp(c, destinationColor, t);
			yield return null;
		}

		action?.Invoke();
	}

	public static void SetSpriteSize(this SpriteRenderer spriteRenderer, float width = 1.0f, float height = 1.0f) {
		var s = spriteRenderer.sprite;

		var size = Vector3.one;
		var sizeX = s.rect.width / s.pixelsPerUnit;
		var sizeY = s.rect.height / s.pixelsPerUnit;
		size.z = 1f;

		var ratio = sizeX > sizeY ? width / sizeX : height / sizeY;

		sizeX = s.rect.width / s.pixelsPerUnit * ratio;
		sizeY = s.rect.height / s.pixelsPerUnit * ratio;

		size *= ratio;
		ratio = 1;

		if (sizeX > width) ratio = width / sizeX;
		if (sizeY > height) ratio = height / sizeY;

		size *= ratio;
		spriteRenderer.transform.localScale = size;
	}

	public static (AnimationCurve curve_x, AnimationCurve curve_y) GetAnimationCurvePath(List<Vector3> path) {
		return GetAnimationCurvePath(path.ToArray());
	}

	public static (AnimationCurve curve_x, AnimationCurve curve_y) GetAnimationCurvePath(Vector3[] path) {
		var distance = path.Distance();

		var curve_x = new AnimationCurve();
		var curve_y = new AnimationCurve();

		for (var i = 0; i < path.Length; i++) {
			var d = 0f;
			for (var y = 0; y <= i - 1; y++) {
				d += Vector3.Distance(path[y], path[y + 1]);
			}

			var t = d / distance;
			var key = new Keyframe(t, path[i].x) {
				inTangent = path[i].x,
				outTangent = path[i].x,
				inWeight = path[i].x,
				outWeight = path[i].x,
				weightedMode = WeightedMode.None
			};
			curve_x.AddKey(key);

			key = new Keyframe(t, path[i].y) {
				inTangent = path[i].y,
				outTangent = path[i].y,
				inWeight = path[i].y,
				outWeight = path[i].y,
				weightedMode = WeightedMode.None
			};

			curve_y.AddKey(key);
		}

		curve_x.postWrapMode = WrapMode.ClampForever;
		curve_y.postWrapMode = WrapMode.ClampForever;
		for (var index = 0; index < curve_x.keys.Length; index++) {
			curve_x.SmoothTangents(index, 0f);
			curve_y.SmoothTangents(index, 0f);
		}

		return (curve_x, curve_y);
	}

	public static Coroutine Scaler(MonoBehaviour caller, Transform tr, Vector3 targetScale, float speed = 1f) {
		return caller.StartCoroutine(ScaleCo(tr,  targetScale, speed));
	}
	private static IEnumerator ScaleCo(Transform tr, Vector3 targetScale, float speed) {
		var t = 0f;

		while (Math.Abs(tr.localScale.x - targetScale.x) > 0.01) {
			t += Time.deltaTime * speed;
			tr.localScale = Vector3.Lerp(tr.localScale, targetScale, t);
			yield return null;
		}
	}
		
		
}

public class RandomT<T> {
	public T[] types;

	private int index;

	public int Length => types.Length;

	public RandomT() {
		index = 2147483637;
	}

	public RandomT(IEnumerable<T> newT) {
		types = newT.ToArray();
		types.Shuffle();
		index = 2147483637;
	}

	public T Next {
		get {
			index++;
			if (index >= types.Length) {
				index = 0;
				types.Shuffle();
			}

			return types[index];
		}
	}

	public void Set(IEnumerable<T> newT) {
		types = newT.ToArray();
		types.Shuffle();
		index = 0;
	}
}

public interface IColorAdapter {
	Color Color { get; set; }
}

public class ImageColorAdapter : IColorAdapter {
	private Image image;

	public Color Color {
		get => image.color;
		set => image.color = value;
	}

	public ImageColorAdapter(Image image) {
		this.image = image;
	}
}

public class SpriteRendererColorAdapter : IColorAdapter {
	private SpriteRenderer sRenderer;

	public Color Color {
		get => sRenderer.color;
		set => sRenderer.color = value;
	}

	public SpriteRendererColorAdapter(SpriteRenderer sRenderer) {
		this.sRenderer = sRenderer;
	}
}
	
public class MeshRendererColorAdapter : IColorAdapter {
	private MeshRenderer mRenderer;

	public Color Color {
		get => mRenderer.material.color;
		set => mRenderer.material.color = value;
	}

	public MeshRendererColorAdapter(MeshRenderer mRenderer) {
		this.mRenderer = mRenderer;
	}
}

public class TextColorAdapter : IColorAdapter {
	private Text text;

	public Color Color {
		get => text.color;
		set => text.color = value;
	}

	public TextColorAdapter(Text text) {
		this.text = text;
	}
}

public class TextMeshProUGUIColorAdapter : IColorAdapter {
	private TextMeshProUGUI text;

	public Color Color {
		get => text.color;
		set => text.color = value;
	}

	public TextMeshProUGUIColorAdapter(TextMeshProUGUI text) {
		this.text = text;
	}
}

public class TextMeshProColorAdapter : IColorAdapter {
	private TextMeshPro text;

	public Color Color {
		get => text.color;
		set => text.color = value;
	}

	public TextMeshProColorAdapter(TextMeshPro text) {
		this.text = text;
	}
}