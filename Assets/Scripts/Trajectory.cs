using System.Collections.Generic;
using UnityEngine;

public class Trajectory : MonoBehaviour {
	private LineRenderer lineRendererComponent;

	private List<Vector3> points = new List<Vector3>();

	private void Start() {
		lineRendererComponent = GetComponent<LineRenderer>();
	}

	public void SetPoints(Vector3 origin, Vector3 speed) {
		points = GeneratePoints(origin, speed);
	}

	public List<Vector3> GetPoints() => points;

	public void ShowTrajectory() {
		// var points = GetPositions(origin, speed);

		if (points.Count == 0) return;

		lineRendererComponent.positionCount = points.Count;
		lineRendererComponent.SetPositions(points.ToArray());


		// var points = new Vector3[300];
		// lineRendererComponent.positionCount = points.Length;
		//
		// for (var i = 0; i < points.Length; i++) {
		// 	var time = i * 0.1f;
		//
		// 	points[i] = origin + speed * time + Physics.gravity * time * time / 2f;
		//
		// 	if (points[i].y < -5) {
		// 		lineRendererComponent.positionCount = i + 1;
		// 		break;
		// 	}
		// }
		// lineRendererComponent.SetPositions(points);
	}

	private List<Vector3> GeneratePoints(Vector3 origin, Vector3 speed) {
		var newPoints = new List<Vector3>();

		for (var i = 0; i < 100; i++) {
			var time = i * 0.1f;

			var point = origin + speed * time + Physics.gravity * time * time / 2f;

			newPoints.Add(point);

			if (point.y < -5) {
				// lineRendererComponent.positionCount = i + 1;
				break;
			}
		}

		return newPoints;
	}
}