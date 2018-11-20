using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MegaFire : BaseAttack {

    public MegaFire()
    {
		type = Type.MAGIC;
        attackName = "MegaFire";
        attackDescription = "Moderate fire elemental damage to a single target";
        attackDamage = 29f;
        manaCost = 12f;
    }
    

}
