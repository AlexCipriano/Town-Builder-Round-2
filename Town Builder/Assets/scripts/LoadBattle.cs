using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadBattle : MonoBehaviour {

	
	// Update is called once per frame
	public void startBattle () {
		GameManager.instance.StartBattle ();
	}
}
