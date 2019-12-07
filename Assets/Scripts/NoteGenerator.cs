using System.Collections;
using UnityEngine;

public enum BandType {
	Band1 = 1,
	Band2 = 2,
	Band3 = 3,
	Band4 = 4,
	Band5 = 5,
	Band6 = 6,
	Band7 = 7,
	Band8 = 8,
}

public class NoteGenerator : MonoBehaviour {

	[Header ("Note Prefabs")]
	public GameObject[] notePrefabs;
	public float speed = 1f;
	public bool useLerp = true;
	public float boostedTimeScale = 5f;
	public float boostedDuringTime = 0.5f;

	[Header ("Transform References")]
	public Transform notesParent;

	private Transform startPointsParent;
	[SerializeField] private Transform[] startPoints;
	private Transform endPointsParent;
	[SerializeField] private Transform[] endPoints;

	
	public static int totalNode = 0;
	void Awake () {
		startPointsParent = GameObject.FindGameObjectWithTag ("StartPoints").transform;
		startPoints = startPointsParent.GetComponentsInChildren<Transform> (true);
		endPointsParent = GameObject.FindGameObjectWithTag ("EndPoints").transform;
		endPoints = endPointsParent.GetComponentsInChildren<Transform> (true);

		AdvancedAudioAnalyzer.onBassTrigger += OnBassTrigger;
		AdvancedAudioAnalyzer.onBand2Trigger += OnBand2Trigger;
		AdvancedAudioAnalyzer.onBand3Trigger += OnBand3Trigger;
		AdvancedAudioAnalyzer.onBand4Trigger += OnBand4Trigger;
		AdvancedAudioAnalyzer.onBand5Trigger += OnBand5Trigger;
		AdvancedAudioAnalyzer.onBand6Trigger += OnBand6Trigger;
		AdvancedAudioAnalyzer.onBand7Trigger += OnBand7Trigger;
		AdvancedAudioAnalyzer.onBand8Trigger += OnBand8Trigger;
	}

	#region Audio Analyzer Callbacks
	public void OnBassTrigger () {
		GameObject go = Instantiate (notePrefabs[0]);
		go.name = "Band1Note";
		go.transform.SetParent (notesParent);
		go.GetComponent<FlowNote> ().InitNote (BandType.Band1, startPoints[(int) BandType.Band1], endPoints[(int) BandType.Band1], speed, useLerp);
		totalNode ++;
	}

	public void OnBand2Trigger () {
		GameObject go = Instantiate (notePrefabs[1]);
		go.name = "Band2Note";
		go.transform.SetParent (notesParent);
		go.GetComponent<FlowNote> ().InitNote (BandType.Band2, startPoints[(int) BandType.Band2], endPoints[(int) BandType.Band2], speed, useLerp);
		if (!isBoostingUp) {
			StartCoroutine (NoteBoostUp ());
		}
		totalNode ++;
	}

	public void OnBand3Trigger () {
		GameObject go = Instantiate (notePrefabs[2]);
		go.name = "Band2Note";
		go.transform.SetParent (notesParent);
		go.GetComponent<FlowNote> ().InitNote (BandType.Band3, startPoints[(int) BandType.Band3], endPoints[(int) BandType.Band3], speed, useLerp);
		totalNode ++;
	}

	public void OnBand4Trigger () {
		GameObject go = Instantiate (notePrefabs[3]);
		go.name = "Band2Note";
		go.transform.SetParent (notesParent);
		go.GetComponent<FlowNote> ().InitNote (BandType.Band4, startPoints[(int) BandType.Band4], endPoints[(int) BandType.Band4], speed, useLerp);
		totalNode ++;
	}

	public void OnBand5Trigger () {
		GameObject go = Instantiate (notePrefabs[4]);
		go.name = "Band2Note";
		go.transform.SetParent (notesParent);
		go.GetComponent<FlowNote> ().InitNote (BandType.Band5, startPoints[(int) BandType.Band5], endPoints[(int) BandType.Band5], speed, useLerp);
		totalNode ++;
	}

	public void OnBand6Trigger () {
		GameObject go = Instantiate (notePrefabs[5]);
		go.name = "Band2Note";
		go.transform.SetParent (notesParent);
		go.GetComponent<FlowNote> ().InitNote (BandType.Band6, startPoints[(int) BandType.Band6], endPoints[(int) BandType.Band6], speed, useLerp);
		totalNode ++;
	}

	public void OnBand7Trigger () {
		GameObject go = Instantiate (notePrefabs[6]);
		go.name = "Band2Note";
		go.transform.SetParent (notesParent);
		go.GetComponent<FlowNote> ().InitNote (BandType.Band7, startPoints[(int) BandType.Band7], endPoints[(int) BandType.Band7], speed, useLerp);
		totalNode ++;
	}

	public void OnBand8Trigger () {
		GameObject go = Instantiate (notePrefabs[7]);
		go.name = "Band2Note";
		go.transform.SetParent (notesParent);
		go.GetComponent<FlowNote> ().InitNote (BandType.Band8, startPoints[(int) BandType.Band8], endPoints[(int) BandType.Band8], speed, useLerp);
		totalNode ++;
	}
	#endregion

	#region Notes Boost Up
	bool isBoostingUp = false;
	IEnumerator NoteBoostUp () {
		isBoostingUp = true;
		Time.timeScale = boostedTimeScale;
		yield return new WaitForSeconds (boostedDuringTime);
		Time.timeScale = 1f;
		isBoostingUp = false;
	}
	#endregion
}
