using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicButton : MonoBehaviour {

    public BaseAttack magicAttackToDo;

    public void CastMagic ()
    {
        GameObject.Find("BattleManager").GetComponent<BattleStateMachine>().ChooseMagic(magicAttackToDo);

    }
}
