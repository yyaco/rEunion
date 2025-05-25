using System.Collections;
using System.Threading;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class ExplosionMonster : Monster
{

    bool explosion = false;
    [SerializeField] float timer = 0f;
    [SerializeField] float delay = 1f;

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
        gameObject.SetActive(false);
    }

    IEnumerator OnMove()
    {
        while (!explosion)
        {
            Vector3 target = GameObject.FindWithTag("Player").transform.position;
            Vector3 dir = (target - transform.position).normalized;
            transform.Translate(dir * speed * Time.deltaTime);

            yield return null;
        }
        state = State.ATTACK;
        ChangeState(state);
    }

    IEnumerator OnAttack()
    {
        Vector2 dir;
        Bullet bullet;
        
        
        while(timer < delay)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        for (int i = 0; i <= 360; i += 45)
        {

            dir.x = Mathf.Cos(Mathf.PI / 180 * i);
            dir.y = Mathf.Sin(Mathf.PI / 180 * i);
            switch (element)
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
        }

        state = State.DIE;
        ChangeState(state);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            explosion = true;
            Debug.Log(explosion); //공격 만들기
        }
    }
}
