using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BaseEnemy: BaseClass {

	public float EXP;
	public float Money;

	public enum Type {
		NONE,
		ICE,
		FIRE,
		WATER,
		ELECTRIC,
		RESISTALL,
		UNDEAD
	}

	public Type enemyType;




}
