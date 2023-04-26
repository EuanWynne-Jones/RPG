using RPG.Control;
using RPG.Quests;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkerAnimation : MonoBehaviour
{
    public Transform movingObject;
    public Vector3 localStartPoint;
    public Vector3 localEndPoint;
    public float speed = 1f;
    public bool useEase = false;
    public AnimationCurve easeCurve = AnimationCurve.Linear(0, 0, 1, 1);

    private Vector3 startPoint;
    private Vector3 endPoint;
    private float lerpValue = 0f;

    private void Awake()
    {
        movingObject = GetComponentInParent<AIController>().transform;
    }
    private void Start()
    {
        startPoint = movingObject.TransformPoint(localStartPoint);
        endPoint = movingObject.TransformPoint(localEndPoint);
    }

    private void Update()
    {

        startPoint = movingObject.TransformPoint(localStartPoint);
        endPoint = movingObject.TransformPoint(localEndPoint);

        if (useEase)
        {
            lerpValue = easeCurve.Evaluate(lerpValue);
        }

        lerpValue += Time.deltaTime * speed;
        lerpValue = Mathf.Clamp01(lerpValue);

        transform.position = Vector3.Lerp(startPoint, endPoint, lerpValue);

        transform.LookAt(Camera.main.transform.position, Vector3.up);

        if (lerpValue == 1f)
        {
            lerpValue = 0f;
            Vector3 tempPoint = localStartPoint;
            localStartPoint = localEndPoint;
            localEndPoint = tempPoint;
        }
    }

    public void SetLocalStartPoint(Vector3 point)
    {
        localStartPoint = point;
        startPoint = movingObject.TransformPoint(localStartPoint);
        lerpValue = 0f;
    }

    public void SetLocalEndPoint(Vector3 point)
    {
        localEndPoint = point;
        endPoint = movingObject.TransformPoint(localEndPoint);
        lerpValue = 0f;
    }
}
