using UnityEngine;
using System.Collections;

public class Shooting_Controll : MonoBehaviour {

    private Vector3 tGr;
    private bool changing;
    private bool FS;//front changing
    private bool BS;//back changing
    private bool RS;//right changing
    private bool LS;//left changing
    private bool DS;//down changing
    private bool US;//up changing
    private Transform player;

	// Use this for initialization
	void Start () {
        tGr = new Vector3(0, -1, 0);
        player = transform.parent;
    }

    private void SetDown()
    {
        tGr = new Vector3(0, -1, 0);
        tGr = Vector3.RotateTowards(tGr, -player.up, 8, 0);
    }

    private void SetFront()
    {
        tGr = new Vector3(1, 0, 0);
        tGr = Vector3.RotateTowards(tGr, transform.forward, 8, 0);
    }

    private void SetBack()
    {
        tGr = new Vector3(-1, 0, 0);
        tGr = Vector3.RotateTowards(tGr, -transform.forward, 8, 0);
    }

    private void SetRight()
    {
        tGr = new Vector3(0, 0, 1);
        tGr = Vector3.RotateTowards(tGr, player.right, 8, 0);
    }

    private void SetLeft()
    {
        tGr = new Vector3(0, 0, -1);
        tGr = Vector3.RotateTowards(tGr, -player.right, 8, 0);
    }

    private void SetUp()
    {
        tGr = new Vector3(0, 1, 0);
        tGr = Vector3.RotateTowards(tGr, player.up, 8, 0);
    }

    // Update is called once per frame
    void Update () {
        changing = Input.GetKey(KeyCode.LeftControl);
        FS = Input.GetKey(KeyCode.W);
        BS = Input.GetKey(KeyCode.S);
        LS = Input.GetKey(KeyCode.A);
        RS = Input.GetKey(KeyCode.D);
        DS = Input.GetKey(KeyCode.X);
        US = Input.GetKey(KeyCode.Space);
        if (changing)
        {
            if (FS)
                SetFront();
            else if (BS)
                SetBack();
            else if (LS)
                SetLeft();
            else if (RS)
                SetRight();
            else if (US)
                SetUp();
            else if (DS)
                SetDown();
        }

        Ray myray = new Ray(transform.position, transform.forward);
        RaycastHit help;
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            if (Physics.Raycast(myray, out help))
            {
                Debug.Log(help.point);
                help.collider.gameObject.GetComponent<HPcounter>().GetStrike_Heall(10);
            }
        }
        if (Input.GetKeyUp(KeyCode.Mouse2))
        {
            if (Physics.Raycast(myray, out help))
            {
                Debug.Log(help.point);
                help.collider.gameObject.GetComponent<GravityForTargets>().SetGr(tGr);
            }
        }
    }
}
