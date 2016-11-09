using UnityEngine;
using System.Collections;
using MoonSharp.Interpreter;

public class Interpreter : MonoBehaviour {

	private Script script;
	[HideInInspector]
	public string ControlScript;
	public GameObject endPoint;
	public UIController UI;
	private TankControl Controller;

	public int Steps{
		get{
			return Controller.stepCounter;
		}
	}

	// Use this for initialization
	void Start () {
		/*
		script.Globals ["turnRight"] = (Action)Controller.TurnRight;
		script.Globals["turnLeft"] = (Action)this.TurnLeft;
		script.Globals["turnAround"] = (Action)this.TurnAround;
		script.Globals["checkRight"] = (Func<bool>)this.CheckRight;
		script.Globals["checkLeft"] = (Func<bool>)this.CheckLeft;
		script.Globals["checkForward"] = (Func<bool>)this.CheckForward;
		script.Globals["moveForward"] = (Action)this.MoveForward;
		*/
		Controller = this.gameObject.GetComponent<TankControl> ();
		script = new Script (CoreModules.Preset_SoftSandbox);
		UserData.RegisterProxyType<TankControlProxy,TankControl> (r => new TankControlProxy (r));
		script.Globals["tank"] = (TankControl)Controller;
/*
		ControlScript = 
@"
if(tank.forwardClear() and tank.leftClear() == false and tank.rightClear() == false) then
	tank.moveForward()
elseif(tank.leftClear()) then
	tank.turnLeft()
elseif(tank.rightClear()) then
		tank.turnRight()
else
		tank.turnAround()
end
";
		StartCoroutine("TankAction");
*/
	}


	public void RunScript(string s){
		ControlScript = s;
		StartCoroutine ("TankAction");
	}

	public void StopScript(){
		Controller.ResetPosition ();
		StopCoroutine ("TankAction");
	}

	IEnumerator TankAction(){
		while (!Controller.isFinished(endPoint)) {
			try{
				script.DoString(ControlScript);
			}catch(ScriptRuntimeException ex){
				Debug.Log ("An Exception Occured: "+ ex.DecoratedMessage + "\nStack trace: " +ex.StackTrace);
			}
			yield return new WaitForSeconds (1);
		}
		Debug.Log ("Finished");
		UI.ShowEnd ();
		yield return null;
	}
}
