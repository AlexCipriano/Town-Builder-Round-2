using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poison : BaseAttack {

    public Poison()
    {
        attackName = "Poison";
        attackDescription = "Inflict poison status to single target";
        attackDamage = 42f;
        manaCost = 18f;
    }
    

}
