using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slash : BaseAttack {

	public Slash () {
		attackName = "Slash";
		attackDescription = "Fast weapon attack with extra damage";
		attackDamage = 20f;
		manaCost = 2f;
	}
}
