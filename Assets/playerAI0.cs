using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAI0 : player {
	SortedList<string,int> toScore;
	// Use this for initialization
	void Start () {
		toScore=new SortedList<string, int>();
		toScore.Add ("_*_", 10);
		toScore.Add ("_**_", 100);
		toScore.Add ("***_", 500);
		toScore.Add ("_***", 500);
		toScore.Add ("_***_", 1000);
		toScore.Add ("_****", 5000);
		toScore.Add ("****_", 5000);
		toScore.Add ("_****_", 10000);
		toScore.Add ("_*****_", int.MaxValue);
	}
	//AI评分对每一个点进行评分转换为一个字符串进行匹配
	int CheckOneline(int[] pos,int[] offset,int chess){
		string temp = "*";
		//int score=0;
		for (int x = pos [0]+offset[0],y=pos[1]+offset[1]; x < 19 && x >= 0&&y<19&&y>=0; x += offset [0],y+=offset[1]) {
			if (board.grid[x,y] == (int)chess) {
				temp = temp + "*";
			} else if (board.grid [x, y] == 0) {
				temp = temp + "_";
				break;
			} else {
				break;
			}
		}
		for (int x = pos [0]-offset[0],y=pos[1]-offset[1]; x < 19 && x >= 0&&y<19&&y>=0; x -= offset [0],y-=offset[1]) {
			if (board.grid [x, y] == (int)chess) {
				temp = "*" + temp;
			} else if (board.grid [x, y] == 0) {
				temp = "_" + temp;
				break;
			} else {
				break;
			}
		}
		int maxscore = 0;
		foreach (var item in toScore) {
			if (temp.Contains (item.Key) && toScore [item.Key] > maxscore) {
				maxscore = toScore [item.Key];
			}
		}
		return maxscore;
	}
	//遍历四个方向进行评分
	int SetScore(int[] pos){
		int score = 0;
		score += CheckOneline (pos, new int[]{1,1},1);
		score += CheckOneline (pos, new int[]{0,1},1);
		score += CheckOneline (pos, new int[]{1,0},1);
		score += CheckOneline (pos, new int[]{1,-1},1);
		score += CheckOneline (pos, new int[]{1,1},2);
		score += CheckOneline (pos, new int[]{0,1},2);
		score += CheckOneline (pos, new int[]{1,0},2);
		score += CheckOneline (pos, new int[]{1,-1},2);
		return score;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public override void play(){
//		Debug.Log (SetScore (new int[]{ 9, 9 }));
		int maxX = 7, maxY = 7,maxScore=40;
		if (board.grid [7, 7] != 0)
			maxScore = 0;
		for (int x = 0; x < 17; x++) {
			for (int y = 0; y < 17; y++) {
				if (board.grid [x, y] != 0)
					continue;
				int score = SetScore (new int[]{ x, y });
				if (score > maxScore) {
					maxX = x;maxY = y;
					maxScore = score;
				}
			}
		}
		board.PlayChess (new int[]{ maxX, maxY });
	}
}
