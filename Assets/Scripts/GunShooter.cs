using System.Collections;
using UnityEngine;

public class GunShooter : MonoBehaviour {

	[Header ("Particle System References")]
	public ParticleSystem normalParticle;
	public ParticleSystem shotGunParticle;
	public ParticleSystem machineGunParticle;

	[Header ("Gun Settings")]
	[Range (5f, 30f)]
	public float normalMax = 20f;
	[Range (5f, 200f)]
	public float normalSpeed = 5f;
	[Range (1f, 5f)]
	public float normalSpeedScale = 2f;
	
	void Start () {
		AdvancedAudioAnalyzer.onBand2Trigger += OnShotGunTrigger;
		AdvancedAudioAnalyzer.onBand4Trigger += OnNormalGunTrigger;
		AdvancedAudioAnalyzer.onBand6Trigger += OnMachineGunTrigger;
	}

	#region Controller
	public void StartShooting () {
		normalParticle.Play ();
	}
	#endregion

	#region Band Trigger
	public void OnNormalGunTrigger () {
		StartCoroutine (TriggerNormalGun ());
	}

	public void OnShotGunTrigger () {
		shotGunParticle.Play ();
	}
	
	public void OnMachineGunTrigger () {
		machineGunParticle.Play ();
	}
	#endregion

	#region Corotine Methods
	IEnumerator TriggerNormalGun () {
		var emission = normalParticle.emission;
		var rate = emission.rateOverTime;
		float orgRate = rate.constant;
		rate.constant = normalMax;
		normalParticle.startSpeed *= normalSpeedScale;
		yield return new WaitForSeconds (0.5f);
		rate.constant = orgRate;
		normalParticle.startSpeed = normalSpeed;
	}
	#endregion
}
