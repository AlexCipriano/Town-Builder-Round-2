using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {


	public GameObject player;

	public static GameManager instance;
	public AudioClip battleMusic;
	public AudioClip townMusic;

	[System.Serializable]
	public class RegionData {
		public string regionName;
		public int maxAmountEnemies = 4;
		public int BattleScene;
		public int Exploration;
		public List<GameObject> possibleEnemies = new List<GameObject>();

	}
	public int enemyAmount;

	public List<RegionData> regions = new List<RegionData> ();

	public List<GameObject> enemysToBattle = new List<GameObject> ();

	//Positions
	public Vector3 nextHeroPosition;
	public Vector3 lastHeroPosition;

	//Scenes
	public int SceneToLoad;
	public int lastScene;

	// Use this for initialization
	void Awake () {
		//check if the instance exists
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			//destroy it 
			Destroy(gameObject);
		}
		DontDestroyOnLoad (gameObject);
		if (!GameObject.Find ("Character")) {
			GameObject Hero = Instantiate (player, Vector3.zero, Quaternion.identity) as GameObject;
			Hero.name = "Player";
		}
	}
	public void LoadNextSceneByIndex(int SceneToLoad) {
		SceneManager.LoadScene (SceneToLoad);
	}

	public void LoadSceneAfterBattleWin() {
		regions [0].Exploration += 25;
		SceneManager.LoadScene (lastScene);
		MusicManager.Instance.MusicSource.Stop ();
		MusicManager.Instance.MusicSource.clip = townMusic;
		MusicManager.Instance.MusicSource.Play (); 
	}
	public void LoadSceneAfterBattleLose() {
		SceneManager.LoadScene (1);
		MusicManager.Instance.MusicSource.Stop ();
		MusicManager.Instance.MusicSource.clip = townMusic;
		MusicManager.Instance.MusicSource.Play ();
	}

	public void StartBattle(){
		//Amount of enemies
		enemyAmount = Random.Range(1,regions[0].maxAmountEnemies + 1);
		for (int i = 0; i < enemyAmount; i++) {
			enemysToBattle.Add (regions[0].possibleEnemies[Random.Range(0,regions[0].possibleEnemies.Count)]);
		}
		//Hero
		lastHeroPosition = GameObject.Find("Character").gameObject.transform.position;
		nextHeroPosition = lastHeroPosition;
		lastScene = SceneManager.GetActiveScene ().buildIndex;
		//Load Level
		SceneManager.LoadScene(regions[0].BattleScene);
		MusicManager.Instance.MusicSource.Stop ();
		MusicManager.Instance.MusicSource.clip = battleMusic;
		MusicManager.Instance.MusicSource.Play ();
		
	}

}
