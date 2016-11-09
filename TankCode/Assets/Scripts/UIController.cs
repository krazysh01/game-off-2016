using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class UIController : MonoBehaviour {

	public Text current;
	public Interpreter interp;
	public Text counter;
	public GameObject mainUi;
	public GameObject EndUI;
	public Text EndName;
	public Text EndSteps;
	public Button[] speeds;
	private GameObject data;
	private bool running = false;
	private string script;
	// Use this for initialization
	void Start () {
		data = GameObject.FindGameObjectWithTag ("Data");
		if (data != null) {
			current.text = data.GetComponent<EditorData> ().currentScript;
			script = PlayerPrefs.GetString (current.text);
		} else {
			current.text = "Test Script";
			script = @"
if(tank.forwardClear()) then
	tank.moveForward()
elseif(tank.leftClear()) then
	tank.turnLeft()
elseif(tank.rightClear()) then
	tank.turnRight()
else
	tank.turnAround()
end
";
		}
		running = false;
		speeds[3].GetComponentInChildren<Text>().text = "\u25A0";
	}

	public void StartScript(){
		if (!running) {
			interp.RunScript (script);
			running = true;
			
		} else {
			Time.timeScale = 1;
		}
		speeds [0].interactable = false;
		speeds [1].interactable = true;
		speeds [2].interactable = true;
		speeds [3].interactable = true;
		speeds [4].interactable = true;
	}

	public void speed2(){
		Time.timeScale = 2;
		speeds [0].interactable = true;
		speeds [1].interactable = false;
		speeds [2].interactable = true;
		speeds [4].interactable = true;
	}

	public void speed3(){
		Time.timeScale = 3;
		speeds [0].interactable = true;
		speeds [1].interactable = true;
		speeds [2].interactable = false;
		speeds [4].interactable = true;
	}

	public void Stop(){
		running = false;
		interp.StopScript ();
		speeds [0].interactable = true;
		speeds [1].interactable = false;
		speeds [2].interactable = false;
		speeds [3].interactable = false;
		speeds [4].interactable = false;
	}

	public void Pause(){
		Time.timeScale = 0;
		speeds [0].interactable = true;
		speeds [1].interactable = true;
		speeds [2].interactable = true;
		speeds [4].interactable = false;
	}
	// Update is called once per frame
	void Update () {
		counter.text = interp.Steps + "";
	}

	public void Return(){
		data.GetComponent<EditorData> ().lastSteps = interp.Steps;
		SceneManager.LoadScene ("ScriptEditor");
	}

	public void ShowEnd(){
		Stop ();
		EndUI.SetActive (true);
		mainUi.SetActive (false);
		EndName.text = current.text;
		EndSteps.text = counter.text;
	}

	public void ShowNormal(){
		EndUI.SetActive (false);
		mainUi.SetActive (true);
	}
}
