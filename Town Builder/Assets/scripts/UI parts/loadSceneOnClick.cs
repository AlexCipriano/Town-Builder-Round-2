using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class loadSceneOnClick : MonoBehaviour {
	

	public void LoadSceneByIndex(int sceneIndex)
	{
		SceneManager.LoadScene (sceneIndex);
	}

	public void SetMusic(AudioClip music){
		MusicManager.Instance.MusicSource.Stop ();
		MusicManager.Instance.MusicSource.clip = music;
		MusicManager.Instance.MusicSource.Play();
	}
}
