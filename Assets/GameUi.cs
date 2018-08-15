using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUi : MonoBehaviour {
	public GameObject startButton;
	public GameObject resultsPanel;
	public GameObject world;	

	void Start() {
		world.SetActive(false);
		resultsPanel.SetActive(false);
	}
	public void OnClickStart () {
		resultsPanel.SetActive(false);
		startButton.SetActive(false);
		Invoke("ShowResults", 60);
		world.SetActive(true);
	}

	public void ShowResults (){
		CancelInvoke ("ShowResults");
		resultsPanel.SetActive(true);
		world.SetActive(false);
	}
}
