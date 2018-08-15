using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Move : MonoBehaviour {
	public float speed;
	public float minXPosition;
	public float maxXPosition;	

	void Update () {
		Vector3 temp;

		transform.Translate(speed * Time.deltaTime,0,0);
		temp = transform.position;

		if (speed < 0) {
			if (temp.x < minXPosition) {
				temp.x = maxXPosition;
			}
		} else {
			if (temp.x > maxXPosition) {
				temp.x = minXPosition;
			}
		}

		
		transform.position = temp;	
	}
}
