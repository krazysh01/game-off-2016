using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class ScriptEditor : MonoBehaviour {

	private EditorData ED;

	public InputField scriptText;
	public GameObject Toolbar;
	public GameObject Editor;
	public InputField scriptName;
	public GameObject Loader;
	public GameObject HelpScreen;
	public GameObject fileOptions;
	private bool saving = true;

	void Start(){
		ED = GameObject.FindGameObjectWithTag ("Data").GetComponent<EditorData>();
		if (ED.currentScript != null) {
			string name = ED.currentScript;
			scriptName.text = name;
			string script = PlayerPrefs.GetString (name);
			scriptText.text = script;
		}
	}


	public void saveScript(){
		string name = scriptName.text;
		if (name == null) {
			name = "Script";
		}
		string script = scriptText.text;
		PlayerPrefs.SetString(name, script);
		ED.ScriptNames.Add (name);
	}

	public void loaderShow(){
		Loader.GetComponentInChildren<Dropdown>().ClearOptions ();
		if(ED.ScriptNames.Count != 0)
			Loader.GetComponentInChildren<Dropdown>().AddOptions (ED.ScriptNames);
		Loader.SetActive (true);
	}

	public void loaderHide(){
		Loader.GetComponentInChildren<Dropdown>().ClearOptions ();
		Loader.SetActive (false);
	}

	public void loadScript(){
		string name = Loader.GetComponentInChildren<Dropdown>().captionText.text;
		scriptName.text = name;
		if (name == null) {
			name = "Script";
		}
		string script = PlayerPrefs.GetString (name);
		scriptText.text = script;
		loaderHide ();
	}

	public void showToolbar(Toggle s){
		if (s.isOn) {
			Debug.Log ("Showing Toolbar");
			Toolbar.SetActive (true);
			Editor.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(40.0f, 0f, 0f);
		} else {
			Debug.Log ("Hiding Toolbar");
			Toolbar.SetActive (false);
			Editor.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(-20.0f, 0f, 0f);
		}
	}

	public void BeginTest(){
		ED.currentScript = scriptName.text;
		SceneManager.LoadScene ("MazeNavigate");
	}

	public void ReturnToMenu(){
		ED.SaveData ();
		SceneManager.LoadScene (0);
	}

	public void ShowHelp(){
		HelpScreen.SetActive (true);
	}

	public void hideHelp(){
		HelpScreen.SetActive (false);
	}

	public void showFile(){
		fileOptions.SetActive (true);
	}

	public void hideFile(){
		fileOptions.SetActive (false);
	}

	public void loadTemplate(){
		string template = @"
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
		scriptText.text = template;
		scriptName.text = "Template";
		
	}

	public void loadFile(){
		saving = false;
	}

	public void saveFile(){
		saving = true;
	}

}
