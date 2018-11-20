using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MegaWater : BaseAttack {

    public MegaWater()
    {
		type = Type.MAGIC;
        attackName = "MegaWater";
        attackDescription = "Moderate water elemental damage to a single target";
        attackDamage = 29f;
        manaCost = 12f;
    }
    

}
