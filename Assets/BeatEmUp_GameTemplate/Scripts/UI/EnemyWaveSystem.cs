using UnityEngine;
using System.Collections;

public class EnemyWaveSystem : MonoBehaviour {

	[Header ("List of enemy Waves")]
	public Transform positionMarkerLeft;
	public EnemyWave[] EnemyWaves;


	[SerializeField]
	public int currentWave;
	public delegate void OnLevelEvent();
	public static event OnLevelEvent onLevelComplete;
	public static event OnLevelEvent onLevelStart;


	public delegate void TimerResetEvent();
	public static event TimerResetEvent onTimerReset;
	void OnEnable(){
		Enemy.OnUnitDestroy += onUnitDestroy;
		DeliveryCustomer.onDeliveryMade += onDeliveryMade;
	}

	void OnDisable(){
		Enemy.OnUnitDestroy -= onUnitDestroy;
        DeliveryCustomer.onDeliveryMade -= onDeliveryMade;
    }

	void Start(){
		currentWave = 0;
		//DisableEnemiesAtStart();
		CameraFollow cam = Camera.main.GetComponent<CameraFollow>();
		if(cam != null) cam.SetLeftClampedPosition(positionMarkerLeft.position);
	}

	public void OnLevelStart(){
		if(onLevelStart != null) onLevelStart();
		StartWave();
	}

	void onUnitDestroy(	GameObject g){
		if(EnemyWaves.Length > currentWave){
			EnemyWaves[currentWave].RemoveEnemyFromWave(g);
			if(EnemyWaves[currentWave].waveComplete() && EnemyWaves[currentWave].deliveriesMade()) {
				currentWave += 1;
				if(!allWavesCompleted()){ 
					StartWave();
					Debug.Log("Wave " + currentWave + "Starting");
				} else{
					if(onLevelComplete != null) onLevelComplete();
				}
			}
		}
	}

	void onDeliveryMade(GameObject g)
	{
		if (EnemyWaves[currentWave].CustomerList != null)
		{
			EnemyWaves[currentWave].RemoveCustomerFromList(g);
			if (EnemyWaves[currentWave].waveComplete())
			{
                currentWave += 1;
                if (!allWavesCompleted())
                {
                    StartWave();
                }
                //else
                //{
                //    if (onLevelComplete != null) onLevelComplete();
                //}
            }
       
       
        }
	} 

	public void StartWave(){
		CameraFollow cam = Camera.main.GetComponent<CameraFollow>();

		if (onTimerReset!= null) { onTimerReset(); }


		if(cam != null){
			if(EnemyWaves[currentWave].PositionMarker != null){

				//set camera X clamp position
				if(currentWave == 0){ 
					cam.SetNewClampPosition(EnemyWaves[currentWave].PositionMarker.position, 0f); //don't lerp at first start
				} else {
					cam.SetNewClampPosition(EnemyWaves[currentWave].PositionMarker.position, 1.5f);
				}

				//enable the enemies of this wave
				foreach(GameObject g in EnemyWaves[currentWave].EnemyList){
					if (g.activeInHierarchy == false)
					{
						g.SetActive(true);
						//g.GetComponent<EnemyAIExtension>().targetSpotted = true;
						//g.GetComponent<EnemyAIExtension>().enableAI = true;
					}
                }

			} else {
				Debug.Log("no position marker found in this wave");
			}
		} else {
			Debug.Log("no camera Follow component found on " + Camera.main.gameObject.name);
		}
		Invoke("SetEnemyTactics", .1f);
		

	}

	void SetEnemyTactics(){
		EnemyManager.SetEnemyTactics();
	}

	void DisableEnemiesAtStart(){
		foreach(EnemyWave wave in EnemyWaves){
			foreach(GameObject g in wave.EnemyList){
				g.SetActive(false);
			}
		}
	}

	//returns true if all the waves are completed
	bool allWavesCompleted(){
		int waveCount = EnemyWaves.Length;
		int waveFinished = 0;
		for(int i=0; i<waveCount; i++){
			if(EnemyWaves[i].waveComplete()) waveFinished += 1;
		}
		if(waveCount == waveFinished) 
			return true;
		else 
			return false;
	}
}
