﻿using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {
	public Transform player1;

	// Update is called once per frame
	void Update () {
		this.transform.position = new Vector3 (player1.position.x, 0, -10);
	}
}
