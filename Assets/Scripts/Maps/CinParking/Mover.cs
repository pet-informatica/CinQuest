using UnityEngine;
using System.Collections;

/// <summary>
/// Automatically moves a Move script to it's deadend in start method
/// </summary>
public class Mover : MonoBehaviour
{
    [HideInInspector]
    public MoverSpawner spawner;

    protected Move move;

	protected virtual void Start ()
    {
        move = GetComponent<Move>();
        move.StartMoving();
	}

    protected virtual void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "DeadEnd")
        {
            spawner.spawnedAmount--;
            Destroy(this.gameObject);
        }
    }

    protected virtual void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "DeadEnd")
        {
            spawner.spawnedAmount--;
            Destroy(this.gameObject);
        }
    }
}
