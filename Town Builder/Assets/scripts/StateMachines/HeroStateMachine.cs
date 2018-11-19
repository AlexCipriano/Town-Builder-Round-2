using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroStateMachine : MonoBehaviour {

	[SerializeField] private BattleStateMachine battleStateMachine;
	public BaseHero hero;
	private Image actionBar;
	public GameObject Selector;

	public enum Turnstate
	{
		WAITING,
		ADDTOLIST,
		IDLE,
		ACTION,
		DEAD
	}


	//Variables for UI
	private HeroPanelStats stats;
	public GameObject heroPanel;
	private Transform HeroPanelSpacer;

	//variables for ActionBar
	private float curCooldown = 0f;
	private float maxCooldown;

	public Turnstate currentState;

	//Ienumerator variables
	public GameObject enemyToAttack;
	private bool actionStarted = false;
	private Vector3 startPosition;
	private float animSpeed = 5f;

	//dead
	private bool alive = true;




	// Use this for initialization
	void Start () {
		//find spacer
		HeroPanelSpacer = GameObject.Find("BattleCanvas").transform.Find("HeroPanel").transform.Find("HeroSpacer");
		//create panel, fill info
		CreatePanel();


		startPosition = transform.position;
		curCooldown = Random.Range (0f, 4f);
		maxCooldown = 50f / hero.speed;
		currentState = Turnstate.WAITING;
		battleStateMachine = GameObject.Find ("BattleManager").GetComponent<BattleStateMachine> ();
		Selector.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {

		switch (currentState) 
		{
		case(Turnstate.WAITING): 
			FillActionBar ();
		break;

		case(Turnstate.ADDTOLIST): 
			battleStateMachine.HeroesToManage.Add (this.gameObject);
			currentState = Turnstate.IDLE;
		break;

		case(Turnstate.IDLE): 
			//idle
		break;

		case(Turnstate.ACTION): 
			StartCoroutine(TimeForAction ());
		break;

		case(Turnstate.DEAD): 
			if (!alive) {
				return;
			} else {
				//change tag of the hero
				this.gameObject.tag = "DeadHero";
				//not attackable by an enemy
				battleStateMachine.herosInBattle.Remove(this.gameObject);
				//not able to manage this hero anymore
				battleStateMachine.HeroesToManage.Remove(this.gameObject);
				//deactivate the selector
				Selector.SetActive(false);
				//reset gui
				battleStateMachine.ActionPanel.SetActive(false);
				battleStateMachine.SelectEnemyPanel.SetActive (false);

				//remove the hero from the turnOrder
				for (int i = 0; i < battleStateMachine.turnOrder.Count; i++) {
					if (battleStateMachine.turnOrder [i].targetOfAttack == this.gameObject) {
						battleStateMachine.turnOrder.Remove (battleStateMachine.turnOrder [i]);
					}
				}
				//change color (or death animation)
				this.gameObject.transform.rotation = Quaternion.Euler(90f, 90f, 0f);
				//reset heroInput
				battleStateMachine.HeroInput = BattleStateMachine.HeroGUI.ACTIVATE;
				alive = false;

			}
		break;
		}
	}

	void FillActionBar () {
		curCooldown = curCooldown + Time.deltaTime;
		float calcCooldown = curCooldown / maxCooldown;
		actionBar.transform.localScale = new Vector3 (Mathf.Clamp (calcCooldown, 0, 1), actionBar.transform.localScale.y, actionBar.transform.localScale.z);
		if (curCooldown >= maxCooldown) {
			actionBar.color = new Color32(0xCB, 0xB1, 0x00, 0xFF);
			currentState = Turnstate.ADDTOLIST;
		}
	}

	private IEnumerator TimeForAction () {
		if (actionStarted) {
			yield break;
		}

		actionStarted = true;

		//animate the enemy that will be attacking
		Vector3 enemyPosition = new Vector3(enemyToAttack.transform.position.x - 1f, enemyToAttack.transform.position.y, enemyToAttack.transform.position.z);
		while (MoveToEnemy (enemyPosition)) {yield return null;}

		//wait for event
		yield return new WaitForSeconds(0.5f);
		//do damge

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
		actionBar.color = new Color32(0xD6, 0xD6, 0xD3, 0xFF);
		currentState = Turnstate.WAITING;
	}

	private bool MoveToEnemy(Vector3 target) {
		return target != (transform.position = Vector3.MoveTowards (transform.position, target, animSpeed * Time.deltaTime));
	}
	private bool MoveToStart(Vector3 target) {
		return target != (transform.position = Vector3.MoveTowards (transform.position, target, animSpeed * Time.deltaTime));
	}

	public void takeDamage (float getDamage) {
		hero.curHP -= getDamage;
		if (hero.curHP <= 0) {
			hero.curHP = 0;
			currentState = Turnstate.DEAD;
		}
		UpdateHeroPanel ();
	}
	void CreatePanel () {
		heroPanel = Instantiate (heroPanel) as GameObject;
		stats = heroPanel.GetComponent<HeroPanelStats> ();
		stats.HeroName.text = hero.theName;
		stats.HeroHP.text = "HP: " + hero.curHP + "/" + hero.baseHP;
		stats.HeroMP.text = "MP: " + hero.curMP + "/" + hero.baseMP;
		actionBar = stats.ActionBar;

		heroPanel.transform.SetParent (HeroPanelSpacer, false);
	}
	void UpdateHeroPanel () {
		stats.HeroHP.text = "HP: " + hero.curHP;	
	}


}
