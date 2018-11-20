using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class BaseAttack : MonoBehaviour{

	public enum Type {
		ATTACK,
		MAGIC,
		ABILITY,
		ITEM,
		HEALING
	}

	public Type type;
	public string attackName;// name of attack
	public string attackDescription;// description of attack
	public float attackDamage;//base damage
	public float manaCost;// mana cost


}
