using UnityEngine;
using System.Collections;
using MoonSharp.Interpreter;
public class TankControlProxy{

	[MoonSharpHidden]
	TankControl tankController;

	[MoonSharpHidden]
	public TankControlProxy(TankControl t){
		this.tankController = t;
	}

	public void turnRight(){
		tankController.TurnRight ();
	}

	public void turnLeft(){
		tankController.TurnLeft ();
	}

	public void turnAround(){
		tankController.TurnAround();
	}

	public bool forwardClear(){
		return tankController.CheckForward ();
	}

	public bool rightClear(){
		return tankController.CheckRight ();
	}

	public bool leftClear(){
		return tankController.CheckLeft ();
	}

	public void moveForward(){
		tankController.MoveForward ();
	}

}
