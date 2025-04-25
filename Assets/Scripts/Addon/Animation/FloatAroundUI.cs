using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatAroundUI : MonoBehaviour
{
    public Vector2 rangeX, rangeY;
    public Vector3 origin;

    public Vector3 target;


    public float chaserSpeed;
    public Vector3 chaser;
    private void Start()
    {
        origin = transform.position;
        Randomize();
        chaser = target;
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, chaser, Time.deltaTime / 2f);

        chaser = Vector3.MoveTowards(chaser, target, chaserSpeed);
        if (Vector3.Distance(chaser, target) <= 0.001f) Randomize();
    }

    private void Randomize()
    {
        target = origin + new Vector3(Random.Range(rangeX.x, rangeX.y), Random.Range(rangeY.x, rangeY.y));
    }
}
