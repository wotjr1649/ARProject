﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioVisualizer : MonoBehaviour
{

	public float maxScale = 1.0f;
	public Transform[] cubeTransform;
	
	void Update () {
		for (int i = 0; i < 8; i++) {
			cubeTransform[i].localScale = new Vector3(cubeTransform[i].localScale.x, (AdvancedAudioAnalyzer.bufferFeqs [i]) * maxScale, cubeTransform[i].localScale.z);
		}
	}
}
