using System.Collections;
using UnityEngine;

public class LaserMonster : Monster
{
    [SerializeField] float timer = 0;
    [SerializeField] float delay = 2f;
    [SerializeField] Vector3 dir;
    [SerializeField] LayerMask mask;

    
    private void Start()
    {
        InitState();
    }


    public override void InitState()
    {
        state = State.IDLE;
        ChangeState(state);
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
        StartCoroutine(OnAttack());
    }

    public override void Die()
    {

    }

    IEnumerator OnIdle()
    {
        while(timer < delay)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        timer = 0;
        state = State.ATTACK;
        ChangeState(state);
    }

    IEnumerator OnAttack()
    {
        while(timer < 1)
        {
            timer += Time.deltaTime;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, 1000f, mask);
            Debug.DrawRay(transform.position, dir, Color.red);
            if (hit.collider != null)
            {
                //Debug.Log("ÀÛµ¿µÊ");
                hit.collider.GetComponent<Player>().Damaged(attack, element);
            }

            
            yield return null;
        }

        timer = 0;
        state = State.IDLE;
        ChangeState(state);
    }

    

}
