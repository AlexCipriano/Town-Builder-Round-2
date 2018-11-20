using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GigaThunder : BaseAttack {

    public GigaThunder()
    {
		type = Type.MAGIC;
        attackName = "GigaThunder";
        attackDescription = "Major thunder elemental damage to a single target";
        attackDamage = 72f;
        manaCost = 24f;
    }
    

}
