using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {
	
	public enum CONTROLLER { KEYBOARD, JOYSTICK, TOUCHSCREEN }
	[Header("Current Controller")]
	public CONTROLLER controller = CONTROLLER.KEYBOARD;
	private GameObject TSC; // touch screen controls
	private bool levelInProgress;

	[Header("Keyboard keys")]
	public KeyCode Left = KeyCode.LeftArrow;
	public KeyCode Right = KeyCode.RightArrow;
	public KeyCode Up = KeyCode.UpArrow;
	public KeyCode Down = KeyCode.DownArrow;
	public KeyCode PunchKey = KeyCode.Z;
	public KeyCode KickKey = KeyCode.X;
	public KeyCode HealKey = KeyCode.C;
	public KeyCode JumpKey = KeyCode.Space;
	public KeyCode PauseKey = KeyCode.Escape;

	[Header("Joypad keys")]
	public KeyCode JoypadPunch = KeyCode.JoystickButton2;
	public KeyCode JoypadKick = KeyCode.JoystickButton3;
	public KeyCode JoypadHeal = KeyCode.JoystickButton1;
	public KeyCode JoypadJump = KeyCode.JoystickButton0;
	public KeyCode JoypadPause = KeyCode.JoystickButton7;

	//delegates
	public delegate void InputEventHandler(Vector2 dir);
	public static event InputEventHandler onInputEvent;
	public delegate void CombatInputEventHandler(string action);
	public static event CombatInputEventHandler onCombatInputEvent;

	//Custom Delegates
	public delegate void PauseInputHandler(string action);
	public static event PauseInputHandler onPauseInput;

	private GameSettings settings;

	void OnEnable(){
		EnemyWaveSystem.onLevelStart += OnLevelStart;
		EnemyWaveSystem.onLevelComplete += OnLevelEnd;
	}

	void OnDisable(){
		EnemyWaveSystem.onLevelStart -= OnLevelStart;
		EnemyWaveSystem.onLevelComplete -= OnLevelEnd;
	}

	public static void InputEvent(Vector2 dir){
		if( onInputEvent != null) onInputEvent(dir);
	}

	public static void CombatInputEvent(string action){
		if( onCombatInputEvent != null) onCombatInputEvent(action);
	}

	public static void PauseInputEvent(string action)
	{
		if (onPauseInput != null) onPauseInput(action);
	}

	void Update(){

		//Use keyboard
		if(controller == CONTROLLER.KEYBOARD) KeyboardControls();

		//use joypad
		if(controller == CONTROLLER.JOYSTICK) JoyPadControls();

		//use touchscreen
		if(controller == CONTROLLER.TOUCHSCREEN && !TSC) CreateTouchScreenControls();
		if(TSC) TSC.SetActive((controller == CONTROLLER.TOUCHSCREEN));
		if(TSC && !levelInProgress) TSC.SetActive(false);
	}

	void KeyboardControls(){
		
		//movement
		float x = 0f;
	 	float y = 0f;

		if(Input.GetKey(Left)) x = -1f;
		if(Input.GetKey(Right)) x = 1f;
		if(Input.GetKey(Up)) y = 1f;
		if(Input.GetKey(Down)) y = -1f;

		Vector2 dir = new Vector2(x,y);
		InputEvent(dir);


		//Combat input -- TO DO Make this only applicable on Game Active
		if(Input.GetKeyDown(PunchKey)){
			CombatInputEvent("Punch");
		}

		if(Input.GetKeyDown(KickKey)){
			CombatInputEvent("Kick");
		}

		if(Input.GetKeyDown(HealKey)){
			CombatInputEvent("Heal"); 
		}

		if(Input.GetKeyDown(JumpKey)){
			CombatInputEvent("Jump");
		}

		if (Input.GetKeyDown(PauseKey))
		{
			PauseInputEvent("Pause");
		}


        //TO DO --- Create Game Paused / Option Input

    }

    void JoyPadControls(){
	 	float x = Input.GetAxis("Horizontal");
	 	float y = Input.GetAxis("Vertical");
		Vector2 dir = new Vector2(x,y);
		InputEvent(dir.normalized);

        //Combat input -- TO DO Make this only applicable on Game Active

        if (Input.GetKeyDown(JoypadPunch)){
			CombatInputEvent("Punch");
		}

		if(Input.GetKeyDown(JoypadKick)){
			CombatInputEvent("Kick");
		}

		if(Input.GetKey(JoypadHeal)){
			CombatInputEvent("Heal");
		}

		if(Input.GetKey(JoypadJump)){
			CombatInputEvent("Jump");
		}

        if (Input.GetKeyDown(JoypadPause))
        {
            PauseInputEvent("Pause");
        }


		//TO DO --- Create Game Paused / Option Input
    }

	void CreateTouchScreenControls(){
		GameObject canvas = GameObject.FindObjectOfType<Canvas>().gameObject;
		if(canvas != null) {
			TSC = GameObject.Instantiate(Resources.Load("UI_TouchScreenControls")) as GameObject;
			TSC.transform.SetParent(canvas.transform, false);
		}
	}

	void OnLevelStart(){
		levelInProgress = true;
	}

	void OnLevelEnd(){
		levelInProgress = false;
	}
}

public enum INPUTTYPE {	
	KEYBOARD = 0,	
	JOYPAD = 2,	
	TOUCHSCREEN = 4, 
}
