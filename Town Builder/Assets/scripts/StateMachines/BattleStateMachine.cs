using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleStateMachine : MonoBehaviour {

	public enum PerformAction {
		WAIT,
		TAKEACTION,
		PERFORMACTION,
		CHECKALIVE,
		WIN,
		LOSE
	}
	public PerformAction battleState;

	public List<HandleTurns> turnOrder = new List<HandleTurns> ();
	public List<GameObject> herosInBattle = new List<GameObject> ();
	public List<GameObject> enemiesInBattle = new List<GameObject> ();


	public enum HeroGUI {
		ACTIVATE,
		IDLE,
		INPUT1,
		INPUT2,
		DONE
	}

	public HeroGUI HeroInput;
	private HandleTurns HeroChoice;
	public List<GameObject> HeroesToManage = new List<GameObject> ();

	public GameObject enemyButton;
	public Transform spacer;
	public Transform ActionSpacer;
	public Transform MagicSpacer;
	public Transform AbilitySpacer;
	public Transform ItemsSpacer;


	public GameObject ActionPanel;
	public GameObject SelectEnemyPanel;
	public GameObject MagicPanel;
	public GameObject AbilitiesPanel;
	public GameObject ItemsPanel;
	public GameObject ActionButton;
    public GameObject SpellButton;
	private List<GameObject> DeleteButtons = new List<GameObject> ();

	//enemy buttons
	private List<GameObject> enemybtns = new List<GameObject>();




	// Use this for initialization
	void Start () {
		battleState = PerformAction.WAIT;
		enemiesInBattle.AddRange(GameObject.FindGameObjectsWithTag ("Enemy"));
		Debug.Log (enemiesInBattle [0]);
		herosInBattle.AddRange(GameObject.FindGameObjectsWithTag ("Hero"));
		HeroInput = HeroGUI.ACTIVATE;

		ActionPanel.SetActive (false);
		MagicPanel.SetActive (false);
		AbilitiesPanel.SetActive (false);
		ItemsPanel.SetActive (false);
		SelectEnemyPanel.SetActive (false);

		EnemyButtons ();
	}
	
	// Update is called once per frame
	void Update () {

		switch (battleState) {
		case(PerformAction.WAIT):
			if (turnOrder.Count > 0) {
				battleState = PerformAction.TAKEACTION;
			}
		break;
		case(PerformAction.TAKEACTION):
			GameObject character = GameObject.Find (turnOrder [0].Attacker);
			if (turnOrder [0].Type == "Enemy") {
				EnemyStateMachine ESM = character.GetComponent<EnemyStateMachine> ();
				for (int i = 0; i < herosInBattle.Count; i++) {
					if (turnOrder [0].targetOfAttack == herosInBattle [i]) {
						ESM.HeroToAttack = turnOrder [0].targetOfAttack;
						ESM.currentState = EnemyStateMachine.Turnstate.ACTION;
						break;
					} else {
						turnOrder [0].targetOfAttack = herosInBattle [Random.Range (0, herosInBattle.Count)];
						ESM.HeroToAttack = turnOrder [0].targetOfAttack;
						ESM.currentState = EnemyStateMachine.Turnstate.ACTION;
					}
				}

			}
			if (turnOrder [0].Type == "Hero") {
				HeroStateMachine HSM = character.GetComponent<HeroStateMachine> ();
				HSM.enemyToAttack = turnOrder [0].targetOfAttack;
				HSM.currentState = HeroStateMachine.Turnstate.ACTION;
			}
			battleState = PerformAction.PERFORMACTION;
		break;
		case(PerformAction.PERFORMACTION):

		break;

		case(PerformAction.CHECKALIVE):
			if (herosInBattle.Count < 1) {
				battleState = PerformAction.LOSE;
				//lose the battle
			} 
			else if (enemiesInBattle.Count < 1) {
				battleState = PerformAction.WIN;
				//win the battle
			}
			else {
				//call function
				clearActionPanel();
				HeroInput = HeroGUI.ACTIVATE;
			}
		break;

		case(PerformAction.WIN):
			for (int i = 0; i < herosInBattle.Count; i++) {
				herosInBattle [i].GetComponent<HeroStateMachine> ().currentState = HeroStateMachine.Turnstate.IDLE;
			}
		break;

		case(PerformAction.LOSE):

		break;
		}


		switch (HeroInput) {
		case(HeroGUI.ACTIVATE):
			if (HeroesToManage.Count > 0) {
				HeroesToManage [0].transform.Find ("Selector").gameObject.SetActive (true);
				HeroChoice = new HandleTurns ();

				ActionPanel.SetActive (true);
				//populate buttons
				CreateActionButtons();
				HeroInput = HeroGUI.IDLE;
			}
			
			break;
		case(HeroGUI.IDLE):
			
			break;
		case(HeroGUI.INPUT1):
			
			break;
		case(HeroGUI.INPUT2):
			
			break;
		case(HeroGUI.DONE):
			HeroInputDone ();
			break;

		}
	}

	public void GetTurn (HandleTurns turn) {
		turnOrder.Add (turn);
	}

	public void EnemyButtons() {

		//clean up 
		foreach (GameObject enemyBtn in enemybtns) {
			Destroy (enemyBtn);
		}
		enemybtns.Clear();

		//create buttons
		foreach (GameObject enemy in enemiesInBattle) {
			GameObject newButton = Instantiate (enemyButton) as GameObject;
			EnemySelectedButton button = newButton.GetComponent<EnemySelectedButton> ();

			EnemyStateMachine curEnemy = enemy.GetComponent<EnemyStateMachine> ();

			Text buttonText = newButton.transform.Find ("Text").gameObject.GetComponent<Text> ();
			buttonText.text = curEnemy.Enemy.theName;

			button.EnemyPrefab = enemy;

			newButton.transform.SetParent(spacer);
			enemybtns.Add (newButton);

		}
		foreach (GameObject hero in herosInBattle) {
			GameObject newButton = Instantiate (enemyButton) as GameObject;
			EnemySelectedButton button = newButton.GetComponent<EnemySelectedButton> ();

			HeroStateMachine curHero = hero.GetComponent<HeroStateMachine> ();

			Text buttonText = newButton.transform.Find ("Text").gameObject.GetComponent<Text> ();
			buttonText.text = curHero.hero.theName;
			button.EnemyPrefab = hero;
			newButton.transform.SetParent(spacer);
			enemybtns.Add (newButton);
		}
	}


	//attack button
	public void HeroAttack() {
		HeroChoice.Attacker = HeroesToManage [0].name;
		HeroChoice.attackersObject = HeroesToManage [0];
		HeroChoice.Type = "Hero";
		HeroChoice.chosenAttack = HeroesToManage [0].GetComponent<HeroStateMachine> ().hero.AttackList [0];
		ActionPanel.SetActive (false);
		SelectEnemyPanel.SetActive (true);
	}

	public void EnemySelect(GameObject enemy) {
		HeroChoice.targetOfAttack = enemy;
		HeroInput = HeroGUI.DONE;
	}

    public void ChooseMagic (BaseAttack chosenMagic)//Choose magic to cast
    {
        HeroChoice.Attacker = HeroesToManage[0].name;
        HeroChoice.attackersObject = HeroesToManage[0];
        HeroChoice.Type = "Hero";

        HeroChoice.chosenAttack = chosenMagic;
        MagicPanel.SetActive(false);
        SelectEnemyPanel.SetActive(true);

    }
    public void MagicPanelSwitch()
    {
        ActionPanel.SetActive(false);
        MagicPanel.SetActive(true);
    }

	public void HeroInputDone() {
		turnOrder.Add (HeroChoice);
		//clean attackpanel
		clearActionPanel ();

		HeroesToManage [0].transform.Find ("Selector").gameObject.SetActive (false);
		HeroesToManage.RemoveAt (0);
		HeroInput = HeroGUI.ACTIVATE;
	}

	void clearActionPanel() {
		SelectEnemyPanel.SetActive (false);
		ActionPanel.SetActive (false);
		MagicPanel.SetActive (false);
		ItemsPanel.SetActive (false);
		AbilitiesPanel.SetActive (false);

		foreach (GameObject DeleteButton in DeleteButtons) {
			Destroy (DeleteButton);
		}
		DeleteButtons.Clear();


	}

	void CreateActionButtons () {
		GameObject MenuButton = Instantiate (ActionButton) as GameObject;
		Text MenuButtonText = MenuButton.transform.Find ("Text").gameObject.GetComponent<Text> ();
		MenuButtonText.text = "Attack";
		MenuButton.GetComponent<Button> ().onClick.AddListener (() => HeroAttack ());
		MenuButton.transform.SetParent (ActionSpacer, false);
		DeleteButtons.Add (MenuButton);

		GameObject MagicAttackButton = Instantiate (ActionButton) as GameObject;
		Text MagicAttackButtonText = MagicAttackButton.transform.Find ("Text").gameObject.GetComponent<Text> ();
        MagicAttackButtonText.text = "Magic";
        MagicAttackButton.GetComponent<Button>().onClick.AddListener(() => MagicPanelSwitch());
        MagicAttackButton.transform.SetParent (ActionSpacer, false);
		DeleteButtons.Add (MagicAttackButton);

        //vairable for getcomponent
        List<BaseAttack> heroMagicList = HeroesToManage[0].GetComponent<HeroStateMachine>().hero.MagicAttacks;
	
        if (heroMagicList.Count > 0)
        {
            foreach(BaseAttack magAttack in heroMagicList)
            {
                GameObject spellButton = Instantiate(SpellButton) as GameObject;
                Text spellButtonText = SpellButton.transform.Find("Text").gameObject.GetComponent<Text>();
                spellButtonText.text = magAttack.attackName;
                MagicButton magic = spellButton.GetComponent<MagicButton>();
                magic.magicAttackToDo = magAttack;
                magic.transform.SetParent(MagicSpacer, false);
                DeleteButtons.Add(spellButton);

            }
        }
        else
        {
            MagicAttackButton.GetComponent<Button>().interactable = false;
        }
	}

}
