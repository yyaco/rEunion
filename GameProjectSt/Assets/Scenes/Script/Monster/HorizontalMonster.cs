using System.Collections;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UIElements;

public class HorizontalMonster : Monster
{

    [SerializeField] Vector3 dir;
    Vector3 bulletDir = Vector3.down;
    public IObjectPool<Bullet> Pool { get; set; }
    [SerializeField]float timer = 0;
    [SerializeField]float moveDelay = 3f;
    [SerializeField] float atkDelay = 1f;
    private void Start()
    {
        InitState();
    }
    public override void InitState()
    {
        ChangeState(State.MOVE);
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
        gameObject.SetActive(false);
    }

    IEnumerator OnMove()
    {
        
        while(moveDelay > timer)
        {
            timer += Time.deltaTime;
            transform.Translate(dir * speed * Time.deltaTime);
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
        if(collision.tag == "Wall" || collision.tag == "Player")
        {
            if(dir == Vector3.left)
            {
                dir = Vector3.right;
            }
            else
            {
                dir = Vector3.left;
            }
        }
    }
}
