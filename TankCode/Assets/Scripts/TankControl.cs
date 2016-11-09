using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using MoonSharp.Interpreter;

public class TankControl : MonoBehaviour {

	[MoonSharpHidden]
	public GameObject ForwardPointer;
	[MoonSharpHidden]
	public GameObject RightPointer;
	[MoonSharpHidden]
	public GameObject LeftPointer;
	[MoonSharpHidden]
	public GameObject Tank;
	[MoonSharpHidden]
	public float Speed;
	[MoonSharpHidden]
	public Color tankColor;

	public int stepCounter = 0;

	public bool isFinished(GameObject endPoint){
		Vector3 posThis = new Vector3 (Mathf.Round (Tank.transform.position.x), 0f, Mathf.Round (Tank.transform.position.z));
		Vector3 posEnd = new Vector3(Mathf.Round (endPoint.transform.position.x),0f,Mathf.Round (endPoint.transform.position.z));
		return posThis == posEnd;	
	}
	private Vector3 startingPos;
	private Vector3 startingRotation;

	// Use this for initialization
	void Start () {
		Tank.GetComponent<MeshRenderer> ().material.color = tankColor;
		startingPos = Tank.transform.position;
		startingRotation = Tank.transform.eulerAngles;
		stepCounter = 0;
	}

	public void ResetPosition(){
		Tank.transform.position = startingPos;
		Tank.transform.eulerAngles = startingRotation;
		stepCounter = 0;
	}

	//Turns The Tank 90degrees right
	public void TurnRight(){
		Tank.transform.Rotate (new Vector3(0,90,0));
	}

	//Turns The Tank 90degrees left
	public void TurnLeft(){
		Tank.transform.Rotate (new Vector3(0,-90,0));
	}

	//Moves The Tank forward 1 unit
	public void MoveForward(){
		Vector3 newPos;
		if (CheckForward ()) {
			stepCounter++;
			newPos = Tank.transform.position + Tank.transform.rotation * Vector3.forward*4;
			//newPos = newPos + Tank.transform.rotation * Vector3.forward;
			while (Tank.transform.position != newPos) {
				Tank.transform.position = Vector3.MoveTowards (Tank.transform.position, newPos, Speed);
			}
		}
	}

	//Checks if the Tank is able to move forward
	public bool CheckForward(){
		bool check = ForwardPointer.GetComponent<ForwardCheck> ().CheckForward ();
		//Debug.Log ("Forward Check: " + check);
		return check;
	}

	//Checks if the Tank is able to move left
	public bool CheckLeft(){
		bool check = LeftPointer.GetComponent<ForwardCheck> ().CheckForward ();
		//Debug.Log ("Left Check: " + check);
		return check;
	}

	//Checks if the Tank is able to move right
	public bool CheckRight(){
		bool check = RightPointer.GetComponent<ForwardCheck> ().CheckForward ();
		//Debug.Log ("Right Check: " + check);
		return check;
	}

	public void TurnAround(){
		Tank.transform.Rotate (new Vector3(0,180,0));
	}

}
