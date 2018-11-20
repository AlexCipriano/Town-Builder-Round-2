using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyStateMachine : MonoBehaviour {

	[SerializeField] private BattleStateMachine battleStateMachine;
	public BaseEnemy Enemy;

	public enum Turnstate
	{
		WAITING,
		CHOOSEACTION,
		IDLE,
		ACTION,
		DEAD
	}
	public Turnstate currentState;


	//variables for ActionBar
	private float curCooldown = 0f;
	private float maxCooldown;
	public GameObject Selector;
	public bool alive = true;

	private Vector3 startPosition;

	private bool actionStarted = false;
	public GameObject HeroToAttack;
	private float animSpeed = 5f;


	// Use this for initialization
	void Start () {
		maxCooldown = 50f / Enemy.speed;
		currentState = Turnstate.WAITING;
		battleStateMachine = GameObject.Find ("BattleManager").GetComponent<BattleStateMachine> ();
		startPosition = transform.position;
		Selector.SetActive (false);

	}

	// Update is called once per frame
	void Update () {

		switch (currentState) 
		{
		case(Turnstate.WAITING): 
			FillActionBar ();
			break;

		case(Turnstate.CHOOSEACTION): 
			ChooseAction ();
			currentState = Turnstate.IDLE;
			break;

		case(Turnstate.IDLE): 

			break;

		case(Turnstate.ACTION): 
			StartCoroutine (TimeForAction());
			break;

		case(Turnstate.DEAD): 
			if (!alive) {
				return;
			} else {
				//change tag of the enemy
				this.gameObject.tag = "DeadEnemy";
				//not attackable by a hero
				battleStateMachine.enemiesInBattle.Remove(this.gameObject);
				//deactivate the selector
				Selector.SetActive(false);
				//remove the enemy from the turnOrder
				if (battleStateMachine.enemiesInBattle.Count > 0) {
					for (int i = 0; i < battleStateMachine.turnOrder.Count; i++) {
						if (battleStateMachine.turnOrder [i].attackersObject == this.gameObject) {
							battleStateMachine.turnOrder.Remove (battleStateMachine.turnOrder [i]);
						}
						if (battleStateMachine.turnOrder [i].targetOfAttack == this.gameObject) {
							battleStateMachine.turnOrder [i].targetOfAttack = battleStateMachine.enemiesInBattle [Random.Range (0, battleStateMachine.enemiesInBattle.Count)];
						}
					}
				}
				//change color (or death animation)
				this.gameObject.transform.rotation = Quaternion.Euler(-90f, -90f, 0f);
				//reset heroInput
				battleStateMachine.battleState = BattleStateMachine.PerformAction.CHECKALIVE;
				alive = false;
				//reset buttons
				battleStateMachine.EnemyButtons ();
				//check alive
				battleStateMachine.battleState = BattleStateMachine.PerformAction.CHECKALIVE;

			}
			break;
		}
	}

	void FillActionBar () {
		curCooldown = curCooldown + Time.deltaTime;
		if (curCooldown >= maxCooldown) {
			currentState = Turnstate.CHOOSEACTION;
		}
	}

	void ChooseAction() {
		HandleTurns myAttack = new HandleTurns ();
		myAttack.Attacker = Enemy.theName;
		myAttack.Type = "Enemy";
		myAttack.attackersObject = this.gameObject;
		myAttack.targetOfAttack = battleStateMachine.herosInBattle[Random.Range(0, battleStateMachine.herosInBattle.Count)];

		int randAttack = Random.Range (0, Enemy.AttackList.Count);
		myAttack.chosenAttack = Enemy.AttackList [randAttack];

		battleStateMachine.GetTurn (myAttack);
	}

	private IEnumerator TimeForAction () {
		if (actionStarted) {
			yield break;
		}

		actionStarted = true;

		//animate the enemy that will be attacking
		Vector3 heroPosition = new Vector3(HeroToAttack.transform.position.x + 1f, HeroToAttack.transform.position.y, HeroToAttack.transform.position.z);
		while (MoveToHero (heroPosition)) {yield return null;}

		//wait for event
		yield return new WaitForSeconds(0.5f);
		//do damge
		DoDamage();

		//animate back to start
		Vector3 firstPosition = startPosition;
		while (MoveToStart (firstPosition)) {yield return null;}

		//remove this action from the list in the turnOrder
		battleStateMachine.turnOrder.RemoveAt(0);
		//reset the Battlestate to wait
		battleStateMachine.battleState = BattleStateMachine.PerformAction.WAIT;

		//end coroutine
		actionStarted = false;
		//reset the enemy state 
		curCooldown = 0f;
		currentState = Turnstate.WAITING;
	}

	private bool MoveToHero(Vector3 target) {
		return target != (transform.position = Vector3.MoveTowards (transform.position, target, animSpeed * Time.deltaTime));
	}
	private bool MoveToStart(Vector3 target) {
		return target != (transform.position = Vector3.MoveTowards (transform.position, target, animSpeed * Time.deltaTime));
	}

	void DoDamage() {
		float baseDamage = (Enemy.curATK - HeroToAttack.GetComponent<HeroStateMachine> ().hero.curDEF) * Enemy.strength;
		float bonus = Random.Range (Enemy.strength, ((Enemy.level + Enemy.strength) / 8) + Enemy.strength);
		float damage = baseDamage * bonus;
		HeroToAttack.GetComponent<HeroStateMachine> ().TakeDamage(damage);
	}

	public void TakeDamage(float getDamage) {
		Enemy.curHP -= getDamage;
		if (Enemy.curHP <= 0) {
			Enemy.curHP = 0;
			currentState = Turnstate.DEAD;
		}
	}
}
