using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour {
	RaycastHit hit;
	public Camera cam;
	public ChessBroad board;
	public ChessType chess;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (board.turn != chess)
			return;
		play ();
	}
	public virtual void play(){
		if(Input.GetKeyDown(KeyCode.Mouse0))
		{
			if(Physics.Raycast (cam.ScreenPointToRay (Input.mousePosition), out hit))
			{
				//print (hit.point.x + "," + hit.point.y);
				board.PlayChess(new int[]{(int)(hit.point.x+0.5f),(int)(hit.point.y+0.5f)});
			}
		}
	}
}