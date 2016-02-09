using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour
{
    [HideInInspector]
    public MoverSpawner spawner;

    protected Move move;

	protected virtual void Start ()
    {
        move = GetComponent<Move>();
	}
	
	protected virtual void Update ()
    {
        if (move.hasCompleted)
            move.moveToPoints();
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
