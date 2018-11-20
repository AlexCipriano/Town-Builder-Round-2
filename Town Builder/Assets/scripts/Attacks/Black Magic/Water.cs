using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : BaseAttack {

    public Water()
    {
		type = Type.MAGIC;
        attackName = "Water";
        attackDescription = "Minor water elemental damage to a single target";
        attackDamage = 14f;
        manaCost = 6f;
    }
    

}
