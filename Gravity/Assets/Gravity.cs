using UnityEngine;
using System.Collections;
public class Gravity : MonoBehaviour {

	private int GrDir = 0;
	private Vector3 GrStr;
	private Vector3 Step;
	private bool changing;
	private bool FS;//front step or front changing
	private bool BS;//back step or back changing
	private bool RS;//right step or right changing
	private bool LS;//left step or left changing
	private bool DS;// or down changing
	private bool US;//jump or up changing
	private const float SL = 1;// step length

	private float StrikePower; // last step time

	private float LST; // last step time
	private float RT; // real time

	private int TestStepCount;


	// Use this for initialization
	void Start () {
		SetDown ();
		StepStop ();
		LST = Time.realtimeSinceStartup - 10;
		RT = Time.realtimeSinceStartup;
		TestStepCount = 0;
	}

	private void SetDown () {
		GrDir = 0;
		GrStr = new Vector3(0, -1, 0);
		GrStr = Vector3.RotateTowards (GrStr, -transform.up, 8, 0);
	}

	private void SetFront () {
		GrDir = 1;
		GrStr = new Vector3(1, 0, 0);
		GrStr = Vector3.RotateTowards (GrStr, transform.forward, 8, 0);
	}

	private void SetBack () {
		GrDir = 4;
		GrStr = new Vector3(-1, 0, 0);
		GrStr = Vector3.RotateTowards (GrStr, -transform.forward, 8, 0);
	}

	private void SetRight () {
		GrDir = 2;
		GrStr = new Vector3(0, 0, 1);
		GrStr = Vector3.RotateTowards (GrStr, transform.right, 8, 0);
	}
	
	private void SetLeft () {
		GrDir = 3;
		GrStr = new Vector3(0, 0, -1);
		GrStr = Vector3.RotateTowards (GrStr, -transform.right, 8, 0);
	}

	private void SetUp () {
		GrDir = 5;
		GrStr = new Vector3(0, 1, 0);
		GrStr = Vector3.RotateTowards (GrStr, transform.up, 8, 0);
	}

	private void StepFront () {
		Step = new Vector3(SL, 0, 0);
		Step = Vector3.RotateTowards (Step, transform.forward, 8, 0);
		TestStepCount += 1;
	}
	
	private void StepBack () {
		Step = new Vector3(-SL, 0, 0);
		Step = Vector3.RotateTowards (Step, -transform.forward, 8, 0);
		TestStepCount += 1;
	}
	
	private void StepRight () {
		Step = new Vector3(0, 0, SL);
		Step = Vector3.RotateTowards (Step, transform.right, 8, 0);
		TestStepCount += 1;
	}
	
	private void StepLeft () {
		Step = new Vector3(0, 0, -SL);
		Step = Vector3.RotateTowards (Step, -transform.right, 8, 0);
		TestStepCount += 1;
	}
	
	private void StepUp () {
		Step = new Vector3(0, SL, 0);
		Step = Vector3.RotateTowards (Step, transform.up, 8, 0);
	}

	private void StepStop () {
		Step = new Vector3(0, 0, 0);
	}

	private bool IsGrounded(){
		return true;
	}

	private float CheckStrike(){ //returning -1 if step is posible, and returning StrikePower if striking an object
		if (TestStepCount < 10)
			return -1;
		return 0;
	}

	
	// Update is called once per frame
	void Update () {
		StrikePower = CheckStrike ();

		changing = Input.GetKey(KeyCode.LeftShift);
		FS = Input.GetKey(KeyCode.W);
		BS = Input.GetKey(KeyCode.S);
		LS = Input.GetKey(KeyCode.A);
		RS = Input.GetKey(KeyCode.D);
		DS = Input.GetKey(KeyCode.X);
		US = Input.GetKey(KeyCode.Space);
		if (changing) {
			if (FS)
				SetFront ();
			else if (BS)
				SetBack ();
			else if (LS)
				SetLeft ();
			else if (RS)
				SetRight ();
			else if (US)
				SetUp ();
			else if (DS)
				SetDown ();
		} else if (IsGrounded()) {
			if (FS)
				StepFront ();
			else if (BS)
				StepBack ();
			else if (LS)
				StepLeft ();
			else if (RS)
				StepRight ();
			else if (US)
				StepUp ();
		}

		GetComponent<Rigidbody>().AddForce (GrStr);

		RT = Time.realtimeSinceStartup;
		if (RT - LST > Time.fixedDeltaTime) {
			LST = RT;
			if(StrikePower < 0){
				transform.position = transform.position + Step*Time.fixedDeltaTime;
			}
		}


		StepStop ();
	}
}
