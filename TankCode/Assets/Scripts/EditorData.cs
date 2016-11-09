using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EditorData : MonoBehaviour {

	public bool debugClear;
	[HideInInspector]
	public List<string> ScriptNames = new List<string>();
	[HideInInspector]
	public string currentScript;
	[HideInInspector]
	public int lastSteps = 0;

	void Start(){
		if (GameObject.FindGameObjectsWithTag ("Data").Length != 1) {
			Destroy (this.gameObject);
		}
		DontDestroyOnLoad (this);
		currentScript = null;
		lastSteps = 0;
		if (debugClear) {
			PlayerPrefs.DeleteAll ();
		} else {
			string ss = PlayerPrefs.GetString ("SavedScripts");
			string[] tmp = ss.Split ("#".ToCharArray ());
			if(tmp[0] != ""){
				for (int x = 0; x < tmp.Length; x++) {
					ScriptNames.Add (tmp [x]);
				}
			}
		}
	}

	public void SaveData(){
		string names = "";
		for(int x = 0; x < ScriptNames.Count; x++){
			if (x != ScriptNames.Count - 1) {
				names += ScriptNames [x] + "#";
			} else {
				names += ScriptNames [x];
			}
		}
		PlayerPrefs.SetString ("SavedScripts", names);
		Destroy (this.gameObject);
	}

	void onApplicationQuit(){
		SaveData ();
	}
}
