using System.Collections;
using Unity.XR.OpenVR;
using UnityEngine;
using UnityEngine.Pool;

public class StandMonster :  Monster
{
    
    public IObjectPool<Bullet> Pool { get; set; }
    private void Update()
    {
        
    }

    private void Start()
    {
        InitState();
    }
    public override void InitState()
    {
        ChangeState(State.IDLE);
        
    }

    public override void Idle()
    {
        StartCoroutine(OnIdle());
    }

    public override void Move()
    {
        
    }

    public override void Attack()
    {
        Bullet bullet;

        Vector3 target = GameObject.FindWithTag("Player").transform.position;
        Vector3 dir = (target - transform.position).normalized;

        switch(element)
        {
            case Element.RED:
                bullet = BulletPoolManager.Instance.redBulletPool.Get();
                bullet.transform.position = transform.position;
                bullet.MonsterShot(dir);
                break;
            case Element.GREEN:
                bullet = BulletPoolManager.Instance.greenBulletPool.Get();
                bullet.transform.position = transform.position;
                bullet.MonsterShot(dir);
                break;
            case Element.BLUE:
                bullet = BulletPoolManager.Instance.blueBulletPool.Get();
                bullet.transform.position = transform.position;
                bullet.MonsterShot(dir);
                break;
        }

        
        

        ChangeState(State.IDLE);

    }

    public override void Die()
    {
        gameObject.SetActive(false);
    }


    IEnumerator OnIdle()
    {
        float timer = 0;
        float delay = 2f;
        while (timer < delay)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        ChangeState(State.ATTACK);
    }
}
