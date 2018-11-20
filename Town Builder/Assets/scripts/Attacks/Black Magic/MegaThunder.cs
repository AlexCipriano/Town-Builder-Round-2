using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MegaThunder : BaseAttack {

    public MegaThunder()
    {
		type = Type.MAGIC;
        attackName = "MegaThunder";
        attackDescription = "Moderate thunder elemental damage to a single target";
        attackDamage = 29f;
        manaCost = 12f;
    }
    

}
