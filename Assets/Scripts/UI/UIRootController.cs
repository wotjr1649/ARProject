using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIRootController : MonoBehaviour {

	[Header ("GameObject Controller References")]
	[SerializeField] private AudioSource  audioPlayer;
	[SerializeField] private GunShooter gunShooter;
	[SerializeField] private GunShooter gunShooter92;
	[SerializeField] private GunShooter gunShooter87;
	[SerializeField] private GameObject visualizer;
	[SerializeField] private GameObject gunVisual;

	[Header ("UI References")]
	public GameObject mainPanel;
	public GameObject ingamePanel;
	public UIAutoCounter autoCounter;

	public static UIRootController instance;

	void Awake() {
		instance = this;
	}

	#region uGUI Callbacks
	public void OnStartClicked ()
	{
		gunVisual.SetActive(true);
		mainPanel.SetActive (false);
		ingamePanel.SetActive (true);
		if (audioPlayer != null && gunShooter != null && gunShooter92 != null && gunShooter87 != null && visualizer != null) {
			visualizer.SetActive (true);
			gunShooter.StartShooting ();
			gunShooter92.StartShooting ();
			gunShooter87.StartShooting ();
			audioPlayer.Stop();
			audioPlayer.Play ();
		}
	}
	#endregion
	
	#region Incode UI Update Methods
	public void UpdateScoreText () {
		autoCounter.UpdateText ();
	}
	#endregion
}
