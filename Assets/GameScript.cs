using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CarPosition {
	public float x;
	public float y;
	public float rotation;

	public CarPosition(float x, float y, float rotation) {
		this.x = x;
		this.y = y;
		this.rotation = rotation;
	}
}

public class GameScript : MonoBehaviour {

	public GameObject[] players_objects;
	public GameObject disc;
	public Rigidbody2D[] players_rigid_bodies;
	public Rigidbody2D disc_rigid_body;
	public AudioSource goal_sound;
	public TextMesh score_text;

	public int[] scores;

	const float court_top = 5;
	const float court_bottom = -5;
	const float court_left = 0;
	const float court_right = 14;

	public CarPosition pos;

	// Use this for initialization
	void Start () {
		players_objects = new GameObject[2];
		players_rigid_bodies = new Rigidbody2D[2];
		scores = new int[2];
		scores [0] = 0;
		scores [1] = 0;
		players_objects[0] = GameObject.Find ("Player1");
		players_objects[1] = GameObject.Find ("Player2");
		disc = GameObject.Find ("Disc");
		players_rigid_bodies[0] = players_objects[0].GetComponent<Rigidbody2D>();
		players_rigid_bodies[1] = players_objects[1].GetComponent<Rigidbody2D>();
		disc_rigid_body = GameObject.Find ("Disc").GetComponent<Rigidbody2D>();
		goal_sound = GameObject.Find ("GoalSound").GetComponent<AudioSource>();
		score_text = GameObject.Find ("ScoreText").GetComponent<TextMesh>();
		Application.targetFrameRate = 100;
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.UpArrow)) {
			setCarVelocity (0, 5);
		}
	}

	public void setCarPosition(int car_id, float x, float y) {
		float actual_x = x * (court_right - court_left) - (court_right - court_left) / 2;
		float actual_y = (1-y) * (court_top - court_bottom) - (court_top - court_bottom) / 2;
		players_rigid_bodies[car_id].transform.position = new Vector3 (actual_x, actual_y, 0);
	}

	public void setCarRotation(int car_id, float rotation) {
		players_rigid_bodies[car_id].rotation = rotation;
	}

	public void setCarVelocity(int car_id, float velocity) {
		var velocity_vec = new Vector2 (velocity, 0);
		players_rigid_bodies [car_id].AddRelativeForce (velocity_vec, ForceMode2D.Force);
	}

	public void setCarVisibility(int car_id, bool is_hidden) {
		players_objects [car_id].SetActive (!is_hidden);
	}

	public void goal(int car_id) {
		scores [car_id] += 1;
		disc.GetComponentInChildren<SpriteRenderer> ().color = Color.red;
		goal_sound.Play ();
		updateScore ();
		StartCoroutine( resetGame ());
	}

	IEnumerator resetGame(){
		yield return new WaitForSeconds (1.2f);
		disc.transform.position = new Vector3 (0, 0, 0);
		disc.GetComponentInChildren<SpriteRenderer> ().color = new Color((float)86/255, (float)218/255, (float)98/255, (float)255/255);
	}

	void updateScore() {
		score_text.text = scores [0].ToString() + " : " + scores [1].ToString();
	}
}
