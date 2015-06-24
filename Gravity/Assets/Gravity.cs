using UnityEngine;
using System.Collections;

public class Gravity : MonoBehaviour {

	private int GrDir = 0;
	private Vector3 GrStr;

	// Use this for initialization
	void Start () {
	
	}

	void SetDown () {
		GrDir = 0;
	}

	void SetXFront () {
		GrDir = 1;
	}

	void SetXBack () {
		GrDir = 4;
	}

	void SetZFront () {
		GrDir = 2;
	}
	
	void SetZBack () {
		GrDir = 3;
	}

	void SetUp () {
		GrDir = 5;
	}

	float CheckStrike(){
		return 0;
	}

	
	// Update is called once per frame
	void Update () {
		if (GrDir == 0) {
			GrStr = new Vector3(0, -1, 0);
		}else if(GrDir == 5) {
			GrStr = new Vector3(0, 1, 0);
		}else if(GrDir == 1) {
			GrStr = new Vector3(1, 0, 0);
		}else if(GrDir == 4) {
			GrStr = new Vector3(-1, 0, 0);
		}else if(GrDir == 2) {
			GrStr = new Vector3(0, 0, 1);
		}else if(GrDir == 5) {
			GrStr = new Vector3(0, 0, -1);
		};

		CheckStrike ();

		GetComponent<Rigidbody>().AddRelativeForce (GrStr);
	}
}
