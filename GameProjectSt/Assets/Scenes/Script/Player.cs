using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

public class Player : MonoBehaviour
{
    Player Instance = null;


    Rigidbody2D rb;
    Vector2 inputvalue;
    Vector2 dir = Vector2.right; // 총알 방향 기억
    
    [SerializeField] int speed = 3;
    [SerializeField]float delay = 1f; //총알 발사 시간 제한
    [SerializeField] int hp = 6;
    Animator anim;

    Element element;

    float timer = 0f;
    int redbulletcount = 10;
    int bluebulletcount = 10;
    int greenbulletcount = 10;
    bool nonDamage = false;
    void Start()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
            Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        OnShot();
        timer += Time.deltaTime;
    }

    
    void Initialize()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void OnMove(InputValue value)
    {
        inputvalue = value.Get<Vector2>();
        if (inputvalue != Vector2.zero)
        {
            anim.SetBool("isWalk", true);
        }
        else
        {
            anim.SetBool("isWalk", false);
        }
    }

    private void Move()
    {
        rb.linearVelocity = inputvalue.normalized * speed;
        
    }

    private void OnShot()
    {
        if (timer > delay)
        {
            //풀에서 총알 가져오기 
            if (Input.GetKeyDown(KeyCode.J) && redbulletcount > 0) // 일정시간 쏘지 않았을 경우 총알 재장전
            {
                
                element = Element.RED;
                
                redbulletcount--;
                if(redbulletcount == 0)
                {
                    StartCoroutine(RedReload());
                }
                Bullet bullet = BulletPoolManager.Instance.redBulletPool.Get();
                bullet.transform.position = this.transform.position;
                bullet.Shot();

                SelectColor();
                delay = 0f; //초기화
            }
            else if (Input.GetKeyDown(KeyCode.K) && bluebulletcount > 0)
            {
                element = Element.BLUE;
                bluebulletcount--;
                if(bluebulletcount == 0)
                {
                    StartCoroutine(BlueReload());
                }
                Bullet bullet = BulletPoolManager.Instance.blueBulletPool.Get();
                bullet.transform.position = this.transform.position;
                bullet.Shot();

                SelectColor();
                delay = 0f; //초기화
            }
            else if (Input.GetKeyDown(KeyCode.L) && greenbulletcount > 0)
            {
                element = Element.GREEN;
                greenbulletcount--;
                if(greenbulletcount == 0)
                {
                    StartCoroutine(GreenReload());
                }    
                Bullet bullet = BulletPoolManager.Instance.greenBulletPool.Get();
                bullet.transform.position = this.transform.position;
                bullet.Shot();

                SelectColor();
                delay = 0f; //초기화
            }
            
        }
    }

    public void Damaged(int damage, Element element)
    {
        if (!nonDamage)
        {
            hp -= damage;
            UIManager.Instance.RefreshHeart(hp);
            if (hp <= 0)
            {
                hp = 0;
                gameObject.SetActive(false);
            }
            StartCoroutine(invincibleTime());
        }
    }


    void SelectColor()
    {
        switch(element)
        {
            
            case Element.RED:
                
                anim.SetBool("Red", true);
                anim.SetBool("Blue", false);
                anim.SetBool("Green", false);
                break;
            case Element.GREEN:
                
                anim.SetBool("Green", true);
                anim.SetBool("Blue", false);
                anim.SetBool("Red", false);
                break;
            case Element.BLUE:
                
                anim.SetBool("Green", false);
                anim.SetBool("Blue", true);
                anim.SetBool("Red", false);
                break;
        }
    }


    IEnumerator RedReload()
    {
        yield return new WaitForSeconds(4f);
        redbulletcount = 10;
        Debug.Log("reloaded => " + redbulletcount);
    }
    IEnumerator BlueReload()
    {
        yield return new WaitForSeconds(4f);
        bluebulletcount = 10;
        Debug.Log("reloaded => " + bluebulletcount);
    }
    IEnumerator GreenReload()
    {
        yield return new WaitForSeconds(4f);
        greenbulletcount = 10;
        Debug.Log("reloaded => " + greenbulletcount);
    }

    IEnumerator invincibleTime()
    {
        nonDamage = true;
        while(timer < 0.5)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        nonDamage = false;
        timer = 0;
    }
}
