using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform Target;
    public Vector3 Offset;
    public float SmoothSpeed = 1000f;

    private void Update()
    {
        if (Target != null)
        {
            Vector3 Position = Target.transform.position - this.transform.position;
            Vector3 moveVector = new Vector3(Position.x * SmoothSpeed * Time.deltaTime, Position.y * SmoothSpeed * Time.deltaTime , 0.0f);
            this.transform.Translate(moveVector);
        } 
       // transform.LookAt(Target);

    }
}
