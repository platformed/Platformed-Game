using UnityEngine;
using System.Collections;

public class BezierCurve2D {
	public static Vector2 Evaluate(Vector2[] points, float time) {
		Vector2 a = Vector2.Lerp(points[0], points[1],time);
		Vector3 b = Vector2.Lerp(points[1], points[2], time);
		Vector3 c = Vector2.Lerp(points[2], points[3], time);

		Vector2 d = Vector2.Lerp(a, b, time);
		Vector2 e = Vector2.Lerp(b, c, time);

		return Vector2.Lerp(d, e, time);
	}
}
