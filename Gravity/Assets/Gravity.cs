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

	public float StrikePower; // last step time

	private float LST; // last step time
	private float RT; // real time

	public bool Ground;

    private Transform Head, Body, ForwardTarget;


	// Use this for initialization
	void Start () {
		SetDown ();
		StepStop ();
		LST = Time.realtimeSinceStartup - 10;
		RT = Time.realtimeSinceStartup;
        Body = transform.GetChild(0);
        Head = transform.GetChild(1);
        ForwardTarget = transform.GetChild(2);
    }

	private void SetDown () {
		GrDir = 0;
		GrStr = new Vector3(0, -1, 0);
		GrStr = Vector3.RotateTowards (GrStr, -transform.up, 8, 0);
	}

	private void SetFront () {
		GrDir = 1;
		GrStr = new Vector3(1, 0, 0);
		GrStr = Vector3.RotateTowards (GrStr, Head.forward, 8, 0);
	}

	private void SetBack () {
		GrDir = 4;
		GrStr = new Vector3(-1, 0, 0);
		GrStr = Vector3.RotateTowards (GrStr, -Head.forward, 8, 0);
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
		var s = new Vector3(SL, 0, 0);
		s = Vector3.RotateTowards (s, transform.forward, 8, 0);
		Step += s;
	}
	
	private void StepBack () {
		var s = new Vector3(-SL, 0, 0);
		s = Vector3.RotateTowards (s, -transform.forward, 8, 0);
		Step += s;
	}
	
	private void StepRight () {
		var s = new Vector3(0, 0, SL);
		s = Vector3.RotateTowards (s, transform.right, 8, 0);
		Step += s;
	}
	
	private void StepLeft () {
		var s = new Vector3(0, 0, -SL);
		s = Vector3.RotateTowards (s, -transform.right, 8, 0);
		Step += s;
	}
	
	private void StepUp () {
		var s = new Vector3(0, SL, 0);
		s = Vector3.RotateTowards (s, transform.up, 8, 0);
		Step += s;
	}

	private void StepStop () {
		Step = new Vector3(0, 0, 0);
	}

	private bool IsGrounded(){
		RaycastHit help;
		if(Physics.CapsuleCast (transform.position - Vector3.Normalize(transform.up), transform.position + Vector3.Normalize(transform.up), 1/2, -transform.up, out help, 1, ~0)){return true;}
		return false;
	}

	private float CheckStrike(){ //returning -1 if step is posible, and returning StrikePower if striking an object	
        RaycastHit help;
        Rigidbody rb = this.GetComponent<Rigidbody> ();
        if (Physics.CapsuleCast(transform.position - Vector3.Normalize(transform.up), transform.position + Vector3.Normalize(transform.up), 1 / 2, rb.velocity, out help, Vector3.Magnitude(rb.velocity) * Time.fixedDeltaTime, ~0))
        {
            float v = Vector3.Magnitude(rb.velocity);
            if (v < 1)
                return 0;
            return (v - 1) * (v - 1) / 625 * 100;
        }
        return -1;
    }

    private void StandUp()
    {
        var help = Physics.CapsuleCastAll(transform.position - Vector3.Normalize(transform.up), transform.position + Vector3.Normalize(transform.up), 1 / 2, -transform.up, 1, ~0, QueryTriggerInteraction.UseGlobal);
        foreach(var g in help)
        {
            var a = g.transform.up;
            var b = transform.up;
            var ang = Vector3.Angle(a, b);
            if (ang <= 30)
            {
                var c = new Vector3(a.y * b.z - a.z * b.y, a.z * b.x - a.x * b.z, a.x * b.y - a.y * b.x);
                transform.Rotate(c, ang);
                if(Vector3.Angle(a, transform.up) > ang)
                {
                    transform.Rotate(c, -2 * ang);
                }
                break;
            }
        }
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
		Ground = IsGrounded ();
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
		} else if (Ground){//IsGrounded()) {
			if (FS)
				StepFront ();
			if (BS)
				StepBack ();
			if (LS)
				StepLeft ();
			if (RS)
				StepRight ();
			if (US)
				StepUp ();
		}

        if (Ground){
            StandUp();
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
