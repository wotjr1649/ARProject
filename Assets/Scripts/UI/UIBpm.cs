using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBpm : MonoBehaviour
{
    [SerializeField] private AudioClip targetClip;
    [SerializeField] private Text text;

    private void Start()
    {
        int bpm = UIBpmAnalyzer.AnalyzeBpm(targetClip);
        text.text = "Music is " + targetClip.name + "\nBPM is " + bpm;
    }
}
