using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class Egg : MonoBehaviour {
	[SerializeField] private float powerForce = 5f; // сила толчка 
	[SerializeField] private SpriteRenderer sprite;
	[SerializeField] private Rigidbody2D myRigidbody2D;
	[SerializeField] private Collider2D myCollider2D;
	[SerializeField] private Trajectory trajectory;

	private float bigSize = 2f;
	private float maxAngle = 6;
	private float posUp = 0.6f;
	private float posRight = 0.4f;
	private Vector3 startPos;
	private Coroutine bigSizeCo, moveCo;
	
	public bool hasClicked = false;
	
	private void Awake(){
		startPos = transform.position;
		
		// Time.timeScale = 0.5f;
	}

	private void AddForce(float mult = 1) {
		AddVelocity();
		return;
		
		trajectory.SetPoints(transform.position, transform.up * powerForce * mult);
		trajectory.ShowTrajectory();
		
		if (moveCo != null) StopCoroutine(moveCo);
		moveCo = StartCoroutine(Move());
		
		// myRigidbody2D.AddForce(transform.up * powerForce * mult, ForceMode2D.Impulse);
	}

	private void AddVelocity() {
		var targetPos = new Vector3(2, 16, 0);
		var fromTo = targetPos - transform.position;
		var fromToXZ = new Vector3(fromTo.x, 0, fromTo.z);

		var x = fromToXZ.magnitude;
		var y = fromTo.y;

		var angleDeg = 45;
		var angleRad = angleDeg * Math.PI / 180;
		var g = Physics.gravity.y;

		var v2 = (g * x * x) / (2 * (y - Math.Tan(angleRad) * x) * Math.Pow(Math.Cos(angleRad), 2));
		var v = (float)Math.Sqrt(Math.Abs(v2));

		myRigidbody2D.velocity = transform.forward * v;
	}

	private IEnumerator Move() {
		var points = trajectory.GetPoints();
		// Utility.RemoveNear(points);
		
		var speedMove = 0.4f;
		
		foreach (var point in points) {
			var t = 0f;

			while (t < 0.3f) {
				t += Time.deltaTime * speedMove;
				transform.position = Vector3.Lerp(transform.position, point, t);
				yield return null;
			}
		}
	}

	public void SetBigSize(){
		bigSizeCo = StartCoroutine(BigSizeCo());
	}

	private IEnumerator BigSizeCo(){
		var t = 0f;
		while (t < 0.65f) {
			t += Time.deltaTime * 2;
			transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one * bigSize, t);
			yield return null;
		}
		
		transform.localScale = Vector3.one * 2;
		sprite.color = new Color(1f, 0.82f, 0.25f);
		
		yield return new WaitForSeconds(5);
		
		t = 0f;
		while (t < 0.85f) {
			t += Time.deltaTime * 2;
			transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one, t);
			yield return null;
		}
		
		transform.localScale = Vector3.one;
		sprite.color = Color.white;
	}

	public void Restart(){
		if (bigSizeCo != null) StopCoroutine(bigSizeCo);
		sprite.color = Color.white;
		
		transform.position = startPos;
		transform.localScale = Vector3.one;

		// Quaternion rot = new Quaternion {eulerAngles = new Vector3(0, 0, 0)};
		// transform.rotation = rot;
		transform.eulerAngles = new Vector3(0, 0, 0);

		hasClicked = false;
		
		Play();
		AddForce(1);
	}

	public void Play(){
		 myRigidbody2D.simulated = true;
	}

	public void Stop(){
		 myRigidbody2D.simulated = false;
	}

	private void OnTriggerEnter2D(Collider2D other){
		if (other.CompareTag($"circle1")) Controller.Instance.AddScore(1);
		else if (other.CompareTag($"circle2")) Controller.Instance.AddScore(2);
		else if (other.CompareTag($"circle4")) Controller.Instance.AddScore(4);
		else if (other.CompareTag($"dead zone")) {
			print("dead Zone");
			Controller.Instance.GameOver();
		}
	}

	private void Update(){
		if (transform.position.y > -1.0f) hasClicked = false;

		if (transform.position.y < -6.0f) Controller.Instance.GameOver();
	}

	private void OnMouseDown(){
		print("OnMouseDown");
		
		if (hasClicked) {
			print("return. hasClicked = " + hasClicked);
			return;
		}
		
		if (transform.position.y < -2.0f) { // -1
			Stop();
			hasClicked = true;
			var mouse = Input.mousePosition;
			
			var screenPosition = new Vector2(mouse.x, mouse.y);
			Vector2 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);

			var point = new GameObject("point");
			var egg = new GameObject("egg");
			point.transform.position = worldPosition;
			egg.transform.position = transform.position;
			point.transform.SetParent(egg.transform);

			
			var locPos = point.transform.localPosition;
			if (bigSizeCo != null) locPos /= bigSize;

			print("point pos = " + point.transform.position);
			print("point loc pos = " + point.transform.localPosition);

			
			var angle = 0f;
			var x = Math.Abs(locPos.x) / posRight;
			
			if (locPos.x > 0 && locPos.y > 0) { // 1
				angle = x * maxAngle * -1;
			}
			else if (locPos.x > 0 && locPos.y < 0 ) { // 4
				angle = x * maxAngle;
			}
			else if (locPos.x < 0 && locPos.y > 0 ) { // 2
				angle = x * maxAngle;
			}
			else if (locPos.x < 0 && locPos.y < 0 ) { // 3
				angle = x * maxAngle * -1;
			}
			
			transform.Rotate(new Vector3(0, 0, angle));
			print(angle);
			
			AddForce(1f);
			Play();
		}
	}
}