using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalScript : MonoBehaviour {

	public string goal_side;
	private bool can_score;

	// Use this for initialization
	void Start () {
		this.can_score = true;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter2D(Collision2D coll){
		//If it is a ball then score
		if (can_score == true && coll.gameObject.name == "Disc") {
//			GameObject score_object = GameObject.Find (score_to);
//			coll.gameObject.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0.5f, 0.5f);
			this.can_score = false;


			if (goal_side == "left") {
//				game_master_object.p1_score++;
//				score_object.GetComponent<TextMesh> ().text = "" + game_master_object.p1_score;
			} else {
				System.Console.WriteLine("Player 1 scored!");
//				game_master_object.p2_score++;
//				score_object.GetComponent<TextMesh> ().text = "" + game_master_object.p2_score;
			}
			Debug.Log (goal_side);
			//Reset Ball and players
			StartCoroutine (ResetPlayersAndBall());
		}
	}

	IEnumerator ResetPlayersAndBall(){
		yield return new WaitForSeconds (1.2f);
//		game_master_object.ResetBallAndPlayers();
		this.can_score = true;
	}
}
