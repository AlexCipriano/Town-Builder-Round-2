using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HandleTurns{

	public string Attacker; //name of attacker
	public string Type; //Enemy or Hero
	public GameObject attackersObject;// object that is attacking
	public GameObject targetOfAttack;// target being attacked

	//which attack is being performed
	public BaseAttack chosenAttack;

}
