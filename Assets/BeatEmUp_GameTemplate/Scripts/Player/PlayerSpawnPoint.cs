using UnityEngine;
using System.Collections;

public class PlayerSpawnPoint : MonoBehaviour {

	void OnEnable(){
		EnemyWaveSystem.onLevelStart += spawnPlayer;
	}

	void OnDisable(){
		EnemyWaveSystem.onLevelStart -= spawnPlayer;
	}

	void spawnPlayer() {
		GameObject player = GameObject.Instantiate(Resources.Load("Emiko"), transform.position, Quaternion.identity) as GameObject;
		player.name = "Emiko";
		//HealthBarExtension.instance.UpdatePlayerName(player.name);
	}
}
