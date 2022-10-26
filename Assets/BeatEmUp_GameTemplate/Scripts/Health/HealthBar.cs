using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class HealthBar : MonoBehaviour {

	public Text nameField;
	public Slider HpSlider;
	public bool isPlayer;

	public void OnEnable() {
		HealthSystem.onHealthChange += UpdateHealth;
	
	}

	public void OnDisable() {
		HealthSystem.onHealthChange -= UpdateHealth;
	}

	void Start(){
		HpSlider.gameObject.SetActive(isPlayer);
	}

	private void Update()
	{
		if (GameManager.instance.currentState == GameManager.GameStates.GameOver)
		{
			{
				HpSlider.gameObject.SetActive(false);
				nameField.gameObject.SetActive(false);
			}
		}
    }

	public void UpdateHealth(float percentage, GameObject go){
		if(isPlayer && go.CompareTag("Player")){
			HpSlider.value = percentage;
		} 	

		if(!isPlayer && go.CompareTag("Enemy")){
			HpSlider.gameObject.SetActive(true);
			HpSlider.value = percentage;
			nameField.text = go.GetComponent<Enemy>().enemyName;
			if (percentage == 0)
			{
              
                Invoke("HideOnDestroy", 2);

			}
		}
		}

	void HideOnDestroy(){
		HpSlider.gameObject.SetActive(false);
		nameField.text = "";
	}
}
