using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blizzard : BaseAttack {

    public Blizzard()
    {
		type = Type.MAGIC;
        attackName = "Blizzard";
        attackDescription = "Minor ice elemental damage to a single target";
        attackDamage = 14f;
        manaCost = 6f;
    }
    

}
