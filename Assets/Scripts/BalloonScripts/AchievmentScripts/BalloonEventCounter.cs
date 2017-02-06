using UnityEngine;
using System.Collections;

public class BalloonEventCounter {
	private int poppedBalloons;
	public int PoppedBalloons {
		get {
			return poppedBalloons;
		}
	}

	private int rightAnswerCounter;
	public int RightAnswerCounter {
		get {
			return rightAnswerCounter;
		}
	}

	private int wrongAnswerCounter;
	public int WrongAnswerCounter {
		get {
			return rightAnswerCounter;
		}
	}

	private int[] playedLevelCounter;
	public int[] PlayedLevelCounter {
		get {
			return playedLevelCounter;
		}
	}


	private static BalloonEventCounter instance;
	public static BalloonEventCounter Instance {
		get
		{
			if (instance == null) {
				instance = new BalloonEventCounter ();
			}
			return instance;
		}
	}


	public BalloonEventCounter () {
		Load ();
	}

	public void setLevelCount(int levelCount) {
		playedLevelCounter = new int[levelCount];
		for (int i = 0; i < playedLevelCounter.Length; i++) {
			playedLevelCounter [i] = 0;
		}
	}

	public void increaseLevelCounter(int level) {
		playedLevelCounter [level]++;
	}

	public void increaseBalloonCounter () {
		poppedBalloons++;
	}

	public void increaseRightAnswerCounter () {
		rightAnswerCounter++;
	}

	public void Load() {

	}

	public void Save() {

	}

	public void CheckForAchievements() {
		Debug.Log ("Check for achievements");
		AchievementManager mng = AchievementManager.Instance;
		mng.EarnAchievement ("Knallfrosch");
		if (poppedBalloons >= 1) {
			Debug.Log ("Knallfrosch");
			mng.EarnAchievement ("Knallfrosch");
		}
	}

	/*
	 * Reaction time 
	 * Right answer
	 * Wrong answer
	 * Total answers
	 * Games played per difficulty
	 * above what score
	 * score?
	 * played so and so many times
	 * all answers right in a game
	 * all anseres right  3 times in a row (hattrick)
	 */
}

