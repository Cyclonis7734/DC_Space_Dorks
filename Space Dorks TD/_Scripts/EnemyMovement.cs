using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {
    public float speed;

    private Transform target;
    private int wavePointIndex = 0;

    public Animator anim;

    void Start()
    {
        float floWaveIndex = EnemySpawner.Instance.waveIndex;
        target = WayPoints.wayPoints[0];
        speed = Random.Range(1.5f, Mathf.Clamp((2f + floWaveIndex/10), 2f, 5f));
    }

    void Update()
    {
        Vector2 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        if (Vector2.Distance(transform.position, target.position) <= 0.1f)
        {
            GetNextWayPoint();
        }

        anim.SetInteger("Right", (int)dir.x);
        anim.SetInteger("Up", (int)dir.y);
        

    }

    void GetNextWayPoint()
    {
        if (wavePointIndex >= WayPoints.wayPoints.Length - 1) //Enemy gets to end of path, playerbase, do damage to player
        {
            Enemy.Instance.DestroyEnemy(gameObject,false);
            PlayerData.Instance.TakeDamage(10f);
            return;
        }
        wavePointIndex++;
        target = WayPoints.wayPoints[wavePointIndex];
    }
}
