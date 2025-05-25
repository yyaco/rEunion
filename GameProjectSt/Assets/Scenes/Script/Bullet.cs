using UnityEngine;
using UnityEngine.Pool;


public class Bullet : MonoBehaviour
{
    [SerializeField]Transform tr;
    float speed = 5;
    int attack = 1;
    [SerializeField]Element element = Element.RED;

    bool isShot = false;
    bool monsterShot = false;
    Vector3 dir;
    public IObjectPool<Bullet> Pool { get; set; }

    

    public void Shot()
    {
        isShot = true;
    }

    public void MonsterShot(Vector3 dir)
    {
        this.dir = dir;
        monsterShot = true;
    }

    private void Update()
    {
        if (isShot)
        {
            tr.Translate(Vector3.up * speed * Time.deltaTime);
        }

        if(monsterShot)
        {
            tr.Translate(dir * speed * Time.deltaTime);
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Wall")
        {
            isShot = false;
            monsterShot = false;
            Pool.Release(this);
        }

        if(collision.tag == "Monster" && isShot)
        {
            isShot = false;
            monsterShot = false;
            collision.GetComponent<Monster>().Damaged(attack, element);
            Pool.Release(this);
        }

        if(collision.tag == "Player" && monsterShot)
        {
            isShot = false;
            monsterShot = false;
            collision.GetComponent<Player>().Damaged(attack, element);
            Pool.Release(this);
        }
    }



}

