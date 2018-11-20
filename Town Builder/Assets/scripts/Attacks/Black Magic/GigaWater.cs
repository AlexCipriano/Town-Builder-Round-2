using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GigaWater : BaseAttack {

    public GigaWater()
    {
		type = Type.MAGIC;
        attackName = "GigaWater";
        attackDescription = "Major water elemental damage to a single target";
        attackDamage = 72f;
        manaCost = 24f;
    }
    

}
