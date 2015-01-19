using UnityEngine;
using System.Collections;

public class Follower : MonoBehaviour {

	public Mover following = new Mover(), followed = new Mover();
	private bool setted = false;

	public void setStartEnd(Point followingStart, bool horizontalFirstFollwing, Point followedEnd, bool horizontalFirstFollowed){
		following.addPoint (followingStart, horizontalFirstFollwing);
		followed.addPoint (followedEnd, horizontalFirstFollowed);
		setted = true;
	}

	public void addPointPath(Point point, bool horizontalFirst){
		following.addPoint (point, horizontalFirst);
		followed.addPoint (point, horizontalFirst);
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
