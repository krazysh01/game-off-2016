using UnityEngine;
using System.Collections;

public class ForwardCheck : MonoBehaviour {

	public string wallTag;
	private bool forwardSafe = true;

	void Start(){
		forwardSafe = true;
	}

	public bool CheckForward(){
		return forwardSafe;
	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == wallTag)
			forwardSafe = false;
	}

	void OnTriggerExit(Collider other){
		if (other.gameObject.tag == wallTag) {
			forwardSafe = true;
		}
	}

}
