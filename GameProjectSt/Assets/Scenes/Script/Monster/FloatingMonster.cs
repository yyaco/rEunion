using System.Collections;
using UnityEngine;

public class FloatingMonster : Monster
{

    [SerializeField]int route = 0;
    [SerializeField] Transform[] destinations;
    [SerializeField] float timer = 0;
    [SerializeField] float delay = 2f;
    [SerializeField] float atkDelay = 2;
    Vector3 bulletDir = Vector3.down;

    private void Start()
    {
        InitState();
    }
    public override void InitState()
    {
        state = State.MOVE;
        ChangeState(state);
    }
    public override void Idle()
    {

    }

    public override void Move()
    {
        StartCoroutine(OnMove());
    }

    public override void Attack()
    {
        StartCoroutine(OnAttack());
    }

    public override void Die()
    {

    }


    IEnumerator OnMove()
    {
        Vector3 dir;
        while(timer < delay)
        {
            timer += Time.deltaTime;
            dir = (destinations[route].position - transform.position).normalized;
            //Debug.Log(dir);
            transform.Translate(dir * speed *  Time.deltaTime);
            
            if(transform.position == destinations[route].position)
            {
                route++;
            }
            if(route == destinations.Length + 1)
            {
                route = 0;
            }
            
                yield return null;
        }
        timer = 0;
        state = State.ATTACK;
        ChangeState(state);
    }

    IEnumerator OnAttack()
    {
        Bullet bullet;
        while (timer < atkDelay)
        {
            timer += Time.deltaTime; // 0.5초 기다리기
            yield return null;
        }
        timer = 0;
        switch (element)
        {
            case Element.RED:
                bullet = BulletPoolManager.Instance.redBulletPool.Get();
                bullet.transform.position = transform.position;
                bullet.MonsterShot(bulletDir);
                break;
            case Element.GREEN:
                bullet = BulletPoolManager.Instance.greenBulletPool.Get();
                bullet.transform.position = transform.position;
                bullet.MonsterShot(bulletDir);
                break;
            case Element.BLUE:
                bullet = BulletPoolManager.Instance.blueBulletPool.Get();
                bullet.transform.position = transform.position;
                bullet.MonsterShot(bulletDir);
                break;
        }



        while (timer < atkDelay)
        {
            timer += Time.deltaTime; // 0.5초 기다리기
            yield return null;
        }

        state = State.MOVE;
        ChangeState(state);
        timer = 0;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "destination" && route < destinations.Length - 1)
        {
            route++;
        }
        else
        {
            route = 0;
        }

        Debug.Log(route);
    }

}
