using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BaseEnemy: BaseClass {


	public enum Type {
		NONE,
		ICE,
		FIRE,
		WATER,
		ELECTRIC,
		RESISTALL
	}

	public Type enemyType;




}
