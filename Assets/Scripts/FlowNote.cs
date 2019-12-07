using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowNote : MonoBehaviour {

	[Header ("Settings")]
	[Range (1f, 10f)]
	public float speed = 1f;

	[Header ("Debug")]
	[SerializeField] private BandType noteType = BandType.Band1;
	[SerializeField] private bool isInit = false;
	[SerializeField] private Transform target;
	[SerializeField] private bool useLerp = true;

	public void InitNote (BandType type, Transform start, Transform target) {
		this.target = target;
		transform.position = start.position;
		noteType = type;
		isInit = true;
	}
	public void InitNote (BandType type, Transform start, Transform target, float speed, bool useLerp = true) {
		this.speed = speed;
		this.target = target;
		this.useLerp = useLerp;
		transform.position = start.position;
		noteType = type;
		isInit = true;
	}
	
	void Update () {
		if (isInit) {
			if (useLerp) {
				transform.position = Vector3.Lerp (transform.position, target.position, speed * Time.deltaTime);
			} else {
				transform.Translate (-Vector3.forward * Time.deltaTime * speed, Space.Self);
			}
		}
	}
	
	void OnTriggerEnter(Collider other) {
		if (other.gameObject.name == "Destroyer") {
			Destroy (gameObject);
		}
	}
	
	void OnParticleCollision(GameObject other) {
		UIRootController.instance.UpdateScoreText ();
		Destroy (gameObject);
	}
}
