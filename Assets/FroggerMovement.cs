using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class FroggerMovement : MonoBehaviour {
    private bool isDead;
	private Vector3 startPosition;
	private Tilemap tilemap;
	private bool isOnLog;
	private float logSpeed;
	private bool isWin;
	private int goalCount;
	private int lifecount;
	public Image life1;
	public Image life2;
	public Image life3;
	private GameObject[] goals;
	public Text resultsText;

	void Start() {
		tilemap = Component.FindObjectOfType<Tilemap>();
		startPosition = transform.position;
		goals = GameObject.FindGameObjectsWithTag("Goal");
	}

	void OnEnable() {
		goalCount = 0;
		isWin = false;
		lifecount = 3;
		isOnLog = false;

		if (goals != null) {
			foreach (GameObject goal in goals) {
				goal.SetActive(true);
			}
		}
		
		life1.gameObject.SetActive(true);
		life2.gameObject.SetActive(true);
		life3.gameObject.SetActive(true);
	}

	private void Move(Vector3 direction) {
		transform.Translate(direction);
		StartCoroutine(this.CheckTileCollision());
	}

	IEnumerator CheckTileCollision() {
		yield return new WaitForSeconds(0.1f);
		Vector3Int pos = tilemap.WorldToCell(transform.position);
		TileBase tileBase = tilemap.GetTile(pos);
		if (tileBase) {
			string tileName = tilemap.GetTile(pos).name;
			if (tileName == "water" || tileName== "grass") {
				if (!isOnLog) {
					Die();
				}
			}
		}
	}

	void Update () {
		if (!isDead && !isWin) {
			if (Input.GetKeyDown(KeyCode.UpArrow)) {
				Move(new Vector3(0,1,0));
			} 
			if (Input.GetKeyDown(KeyCode.DownArrow)) {
				Move(new Vector3(0,-1,0));
			}
			if (Input.GetKeyDown(KeyCode.RightArrow)) {
				Move(new Vector3(1,0,0));
			}
			if (Input.GetKeyDown(KeyCode.LeftArrow)) {
				Move(new Vector3(-1,0,0));
			}

			if (isOnLog) {
				transform.Translate(logSpeed * Time.deltaTime,0,0);
			}
		}
	}

	void OnTriggerEnter2D(Collider2D collider) {
		if (!isDead && !isWin) {
			if (collider.tag == "Goal") {
				collider.gameObject.SetActive(false);
				transform.position = startPosition;
				goalCount++;
				if (goalCount > 3) {
					Win();
				}
			} else if (collider.tag == "vehicle") {
				Die();
			} else if (collider.tag == "Log") {
				isOnLog = true;
				logSpeed = collider.GetComponent<Move>().speed;
			}
		}
    }
	void OnTriggerExit2D(Collider2D collider) {
		if (!isDead && !isWin) {
			if (collider.tag == "Log") {
				isOnLog = false;
			}
		}
	}
	private void Win() {
		resultsText.text = "You Win!";
		isWin = true;
		Invoke("DieCompleted", 2);
	}
	private void Die() {
		lifecount--;

		if (lifecount == 2) {
			life1.gameObject.SetActive(false);
		} else if (lifecount == 1) {
			life2.gameObject.SetActive(false);
		}

		if (lifecount == 0) {
			life3.gameObject.SetActive(false);
			isDead = true;
			resultsText.text = "You Lose!";
			Invoke("DieCompleted", 2);
		} else {
			transform.position = startPosition;	
		}
	}
	private void DieCompleted() {
		isDead = false;	
		transform.position = startPosition;
		Component.FindObjectOfType<GameUi>().ShowResults();
	}
}
