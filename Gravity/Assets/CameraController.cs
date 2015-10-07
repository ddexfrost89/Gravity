using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public float rotationX;
    public float rotationY;
    private GameObject Head;
    private GameObject Body;

    // Use this for initialization
    void Start () {
        Head = transform.parent.gameObject;
        Body = Head.transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update () {
        rotationX = Input.GetAxis("Mouse X") * 10F;
        rotationY = Input.GetAxis("Mouse Y") * 10F;
        if (((Mathf.Abs(Vector3.Angle(Head.transform.forward, Body.transform.forward) - rotationY) < 50)&&(Vector3.Angle(Head.transform.forward, Body.transform.up) > 90))||((Mathf.Abs(Vector3.Angle(Head.transform.forward, Body.transform.forward) + rotationY) < 70) && (Vector3.Angle(Head.transform.forward, Body.transform.up) <= 90)))
            Head.transform.Rotate(new Vector3(-rotationY, 0, 0));
        Body.transform.Rotate(new Vector3(0, rotationX, 0));
    }
}
