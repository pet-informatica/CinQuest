using UnityEngine;
using System.Collections;

public class Follower : MonoBehaviour {

	public Move following = new Move(), followed = new Move();
	private bool setted = false;

	public void setStartEnd(Point followingStart, Point followedEnd, bool horizontalFirstFollowed){
		following.addPoint (followingStart);
		followed.addPoint (followedEnd);
		setted = true;
	}

	public void addPointPath(Point point){
		following.addPoint (point);
		followed.addPoint (point);
	}

	public void startPath(){
		if (!setted)
			throw new UnityException ("Following start point or Followed end point not setted, use setStartEnd method to avoid this");

		followed.moveToPoints ();
		following.moveToPoints ();

	}



	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
