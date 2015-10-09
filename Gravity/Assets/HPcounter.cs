using UnityEngine;
using System.Collections;

public class HPcounter : MonoBehaviour {

    public float HP = 100;
    public short typeOb = 0;//1 if it is player

	// Use this for initialization
	void Start () {
	
	}
	
    public void GetStrike_Heall(float strikePower)
    {
        HP -= strikePower;
    }

    private void ShowDeath()
    { }// execution of players death

    private void die()
    {
        if (typeOb == 0)
            Destroy(gameObject);
        else
            ShowDeath();
    }

	// Update is called once per frame
	void Update () {
	    if(HP < 0)
        {
            die();
        }
	}
}
