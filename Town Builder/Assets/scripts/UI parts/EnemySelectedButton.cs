using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySelectedButton : MonoBehaviour {

	public GameObject EnemyPrefab;

	public void SelectEnemy (){
		GameObject.Find ("BattleManager").GetComponent<BattleStateMachine> ().EnemySelect(EnemyPrefab);
	}

	public void ShowSelector () {
		EnemyPrefab.transform.Find ("Selector").gameObject.SetActive(true);
	}
	public void HideSelector () {
		EnemyPrefab.transform.Find ("Selector").gameObject.SetActive(false);
	}
}
