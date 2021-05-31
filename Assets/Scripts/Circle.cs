using System;
using UnityEngine;

public class Circle : MonoBehaviour {
	private void OnTriggerEnter2D(Collider2D other){
		print("OnTriggerEnter2D");
	}

	private void OnCollisionEnter(Collision other){
		print("OnCollisionEnter");
	}
}