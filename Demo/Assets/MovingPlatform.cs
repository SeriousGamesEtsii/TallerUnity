using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {
    public float speed;
    private bool pingPong;
    public bool moving = true;
    public new Transform platform;
    public Vector3 direction;

	// Use this for initialization
	void Start () {
        StartCoroutine(movimiento(speed));
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public static bool IsCBetweenAB(Vector3 A, Vector3 B, Vector3 C)
    {
        return Vector3.Dot((B - A).normalized, (C - B).normalized) < 0f && Vector3.Dot((A - B).normalized, (C - A).normalized) < 0f;
    }

    private IEnumerator movimiento(float speed)
    {
        direction = (transform.Find("destination").position - transform.Find("origin").position).normalized;
        while (moving)
        {
            if (!IsCBetweenAB(transform.Find("origin").position, transform.Find("destination").position, platform.position))
            {
               direction = (-platform.position + transform.Find("destination").position).normalized;
            }
            platform.Translate(direction * speed * Time.deltaTime);
            yield return null;
        }
    }
}
