using UnityEngine;
using UnityEngine.XR;


public enum Element
{
    RED, GREEN, BLUE
}

public enum State
{
    IDLE, MOVE, ATTACK, DIE
}
public class Monster : MonoBehaviour
{
    [SerializeField] protected Element element;
    protected State state;
    [SerializeField]protected int hp;
    [SerializeField] protected float speed = 2;
    [SerializeField] protected int attack;

    public virtual void InitState()
    {

    }
    public virtual void Idle()
    {

    }

    public virtual void Move()
    {

    }

    public virtual void Attack()
    {

    }

    public virtual void Die()
    {

    }

    public void Damaged(int damage, Element element)
    {
        switch(element)
        {
            case Element.RED:
                if(this.element == Element.RED)
                {
                    damage -= 1;
                }
                else if(this.element == Element.BLUE)
                {
                    damage *= 2;
                }
                    break;
            case Element.GREEN:
                if (this.element == Element.RED || this.element == Element.BLUE)
                {
                    damage -= 1;
                }
                else if (this.element == Element.RED)
                {
                    damage *= 2;
                }
                break;
            case Element.BLUE:
                if (this.element == Element.RED || this.element == Element.BLUE)
                {
                    damage -= 1;
                }
                else if (this.element == Element.GREEN)
                {
                    damage *= 2;
                }
                break;
        }


        hp -= damage;
        if(hp < 0)
        {
            hp = 0;
            state = State.DIE;
            ChangeState(state);
        }
    }

    public void ChangeState(State state)
    {
        this.state = state;

        switch (state)
        {
            case State.IDLE:
                
                Idle();
                break;
            case State.MOVE:
                
                Move();
                break;
            case State.ATTACK:
                
                Attack();
                break;
            case State.DIE:
                
                Die();
                break;
        }
    }
}
