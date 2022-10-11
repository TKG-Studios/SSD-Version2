using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Enemy : MonoBehaviour {

	//public int HP;
	public float attackRange = 1.5f;
	public float closeRangeDistance = 2.5f;
	public float midRangeDistance = 3f;
	public float farRangeDistance = 4.5f;
	public float walkSpeed = 2.5f;
	public string enemyName = "";
	public float sightDistance = 50f;
	public int attackDamage = 2;
	public float attackInterval = 1f;
	public bool targetSpotted;


	//Random Number That Will Determine The Amount Dropped or if dropped
	private int diceRoll;
	//Types Of Currency Available To Drop
	public GameObject[] currencyArray;
	    
	//global event handler for enemies
	public delegate void UnitEventHandler(GameObject Unit);
	public static event UnitEventHandler OnUnitSpawn;

	//global event Handler for destroying units
	public static event UnitEventHandler OnUnitDestroy;

	//destroy event
	public void DestroyUnit(){
		if(OnUnitDestroy != null) OnUnitDestroy(gameObject);
		Destroy(gameObject);

		diceRoll = Random.Range(0, 3);
		if (diceRoll < 3)
		{
			dropCurrency();
		}
	}

	//create event
	public void CreateUnit(GameObject g){
		OnUnitSpawn(g);
	}

	//void Awake()
	//{
	//	enemyName = GetName();
	//}

	//returns a random name from this list
	//string GetName(){
	//	List<string> nameList = new List<string> {
	//		"Robert",
	//		"John",
	//		"Jason",
	//		"Anthony",
	//		"Jeff",
	//		"James",
	//		"Daniel",
	//		"George",
	//		"Steven",
	//		"Brian",
	//		"Chris",
	//		"Mark",
	//		"David",
	//		"Thomas",
	//	};
	//	return nameList [Random.Range(0, nameList.Count)];
	//}


	private void dropCurrency()
	{
		GameObject currency = Instantiate(currencyArray[diceRoll], transform.position, transform.rotation);
	}
}