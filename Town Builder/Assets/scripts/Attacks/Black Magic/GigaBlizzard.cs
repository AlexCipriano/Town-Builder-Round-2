using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GigaBlizzard : BaseAttack {

    public GigaBlizzard()
    {
		type = Type.MAGIC;
        attackName = "GigaBlizzard";
        attackDescription = "Major ice elemental damage to a single target";
        attackDamage = 72f;
        manaCost = 24f;
    }
    

}
