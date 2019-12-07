using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class UIBpmAnalyzer
{
    #region CONST
    
    private const int MIN_BPM = 60;
    private const int MAX_BPM = 400;
    private const int BASE_FREQUENCY = 44100;
    private const int BASE_CHANNELS = 2;
    private const int BASE_SPLIT_SAMPLE_SIZE = 2205;

    #endregion

    public struct BpmMatchData
    {
        public int bpm;
        public float match;
    }

    private static BpmMatchData[] bpmMatchDatas = new BpmMatchData[MAX_BPM - MIN_BPM + 1];
    
    public static int AnalyzeBpm(AudioClip clip)
    {
        for (int i = 0; i < bpmMatchDatas.Length; i++)
        {
            bpmMatchDatas[i].match = 0f;
        }
        if (clip == null)
        {
            return -1;
        }
        Debug.Log("AnalyzeBpm audioClipName : " + clip.name);

        int frequency = clip.frequency;
        Debug.Log("Frequency : " + frequency);

        int channels = clip.channels;
        Debug.Log("Channels : " + channels);

        int splitFrameSize = Mathf.FloorToInt(((float)frequency / (float)BASE_FREQUENCY) * ((float)channels / (float)BASE_CHANNELS) * (float)BASE_SPLIT_SAMPLE_SIZE);
        
        var allSamples = new float[clip.samples * channels];
        clip.GetData(allSamples, 0);
        
        var volumeArr = CreateVolumeArray(allSamples, frequency, channels, splitFrameSize);
        
        int bpm = SearchBpm(volumeArr, frequency, splitFrameSize);
        Debug.Log("Matched BPM : " + bpm);

        var strBuilder = new StringBuilder("BPM Match Data List\n");
        for (int i = 0; i < bpmMatchDatas.Length; i++)
        {
            strBuilder.Append("bpm : " + bpmMatchDatas[i].bpm + ", match : " + Mathf.FloorToInt(bpmMatchDatas[i].match * 10000f) + "\n");
        }
        Debug.Log(strBuilder.ToString());

        return bpm;
    }
    private static float[] CreateVolumeArray(float[] allSamples, int frequency, int channels, int splitFrameSize)
    {
        var volumeArr = new float[Mathf.CeilToInt((float)allSamples.Length / (float)splitFrameSize)];
        int powerIndex = 0;
        
        for (int sampleIndex = 0; sampleIndex < allSamples.Length; sampleIndex += splitFrameSize)
        {
            float sum = 0f;
            for (int frameIndex = sampleIndex; frameIndex < sampleIndex + splitFrameSize; frameIndex++)
            {
                if (allSamples.Length <= frameIndex)
                {
                    break;
                }
                float absValue = Mathf.Abs(allSamples[frameIndex]);
                if (absValue > 1f)
                {
                    continue;
                }
                
                sum += (absValue * absValue);
            }
            
            volumeArr[powerIndex] = Mathf.Sqrt(sum / splitFrameSize);
            powerIndex++;
        }
        
        float maxVolume = volumeArr.Max();
        for (int i = 0; i < volumeArr.Length; i++)
        {
            volumeArr[i] = volumeArr[i] / maxVolume;
        }

        return volumeArr;
    }
    
    private static int SearchBpm(float[] volumeArr, int frequency, int splitFrameSize)
    {
        var diffList = new List<float>();
        for (int i = 1; i < volumeArr.Length; i++)
        {
            diffList.Add(Mathf.Max(volumeArr[i] - volumeArr[i - 1], 0f));
        }
        
        int index = 0;
        float splitFrequency = (float)frequency / (float)splitFrameSize;
        for (int bpm = MIN_BPM; bpm <= MAX_BPM; bpm++)
        {
            float sinMatch = 0f;
            float cosMatch = 0f;
            float bps = (float)bpm / 60f;

            if (diffList.Count > 0)
            {
                for (int i = 0; i < diffList.Count; i++)
                {
                    sinMatch += (diffList[i] * Mathf.Cos(i * 2f * Mathf.PI * bps / splitFrequency));
                    cosMatch += (diffList[i] * Mathf.Sin(i * 2f * Mathf.PI * bps / splitFrequency));
                }

                sinMatch *= (1f / (float)diffList.Count);
                cosMatch *= (1f / (float)diffList.Count);
            }

            float match = Mathf.Sqrt((sinMatch * sinMatch) + (cosMatch * cosMatch));

            bpmMatchDatas[index].bpm = bpm;
            bpmMatchDatas[index].match = match;
            index++;
        }
        
        int matchIndex = Array.FindIndex(bpmMatchDatas, x => x.match == bpmMatchDatas.Max(y => y.match));

        return bpmMatchDatas[matchIndex].bpm;
    }
}