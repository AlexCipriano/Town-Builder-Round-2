using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegularAttack : BaseAttack {

	public RegularAttack () {
		attackName = "Attack";
		attackDescription = "Attack with equipped weapon";
		attackDamage = 5f;
		manaCost = 0f;
	}
}
