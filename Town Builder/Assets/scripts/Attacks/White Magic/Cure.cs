using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cure : BaseAttack {

	public Cure() {
		type = Type.HEALING;
		attackName = "Cure";
		attackDescription = "Restores minor HP to the target";
		attackDamage = 16f;
		manaCost = 6f;
	}

}
