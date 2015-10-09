using UnityEngine;
using System.Collections;

public class GravityForTargets : MonoBehaviour {

    private Vector3 GrStr;

    // Use this for initialization
    void Start () {
        GrStr = new Vector3(0, -1, 0);
	}
	
    public void SetGr(Vector3 newGr)
    {
        GrStr = newGr;
    }

	// Update is called once per frame
	void Update () {
        GetComponent<Rigidbody>().AddForce(GrStr);
    }
}
