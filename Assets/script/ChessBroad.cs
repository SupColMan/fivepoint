using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessBroad : MonoBehaviour {
	public int[,] grid{ get; private set;}
	public ChessType turn{ get; private set;}
	public GameObject[] chessPrefabs;
	public Stack<GameObject> chessPath;
	// Use this for initialization
	void Start () {
		grid = new int[18, 18];
		turn = ChessType.black;
		chessPath=new Stack<GameObject>();
	}
	public void PlayChess(int[] pos){
		if (grid [pos [0], pos [1]] != 0 || pos [0] < 0 || pos [0] > 20 || pos [1] < 0 || pos [1] > 20)
			return;
		chessPath.Push(Instantiate (chessPrefabs [(int)turn - 1],new Vector3(pos[0],pos[1],-1),Quaternion.identity));
		grid [pos [0], pos [1]] = (int)turn;
		if (CheckWiner (pos)) {
			Debug.Log (turn + "胜");
			Time.timeScale = 0;
		}
		if (turn == ChessType.black) {
			turn = ChessType.white;
		} else {
			turn = ChessType.black;
		}
	}
	bool CheckWiner(int[] pos){
		if (CheckOneline (pos, new int[]{ 1, 0 }))
			return true;
		if (CheckOneline (pos, new int[]{ 1, 1 }))
			return true;
		if (CheckOneline (pos, new int[]{ 1, -1 }))
			return true;
		if (CheckOneline (pos, new int[]{ 0, 1 }))
			return true;
		return false;
	}
	bool CheckOneline(int[] pos,int[] offset){
		int sum = 1;
		for (int x = pos [0]+offset[0],y=pos[1]+offset[1]; x < 19 && x >= 0&&y<19&&y>=0; x += offset [0],y+=offset[1]) {
			if (grid [x,y] == (int)turn) {
				sum++;
			} else {
				break;
			}
		}
		for (int x = pos [0]-offset[0],y=pos[1]-offset[1]; x < 19 && x >= 0&&y<19&&y>=0; x -= offset [0],y-=offset[1]) {
			if (grid [x,y] == (int)turn) {
				sum++;
			} else {
				break;
			}
		}
		if (sum >= 5) {
			return true;
		}
		return false;
	}
	public void ReSetChess(){
		if (chessPath.Count > 0) {
			GameObject temp=chessPath.Pop();
			grid [(int)(temp.transform.position.x), (int)(temp.transform.position.y)]=0;
			Destroy (temp);
		}
		if (chessPath.Count > 0) {
			GameObject temp=chessPath.Pop();
			grid [(int)(temp.transform.position.x), (int)(temp.transform.position.y)]=0;
			Destroy (temp);
		}

	}
	// Update is called once per frame
	void Update () {
		
	}
}
public enum ChessType{
	旁观,
	black,
	white
}