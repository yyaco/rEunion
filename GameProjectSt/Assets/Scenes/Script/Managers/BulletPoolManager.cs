using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class BulletPoolManager : MonoBehaviour
{
    public static BulletPoolManager Instance = null;

    public IObjectPool<Bullet> redBulletPool;
    public IObjectPool<Bullet> blueBulletPool;
    public IObjectPool<Bullet> greenBulletPool;

    [SerializeField] Bullet redBullet;
    [SerializeField] Bullet blueBullet;
    [SerializeField] Bullet greenBullet;
    

    private void Awake()
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
    }

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        redBulletPool = new ObjectPool<Bullet>(CreateRedBullet, GetBullet, ReleaseBullet, DestroyBullet);
        blueBulletPool = new ObjectPool<Bullet>(CreateBlueBullet, GetBullet, ReleaseBullet, DestroyBullet);
        greenBulletPool = new ObjectPool<Bullet>(CreateGreenBullet, GetBullet, ReleaseBullet, DestroyBullet);

        for (int i = 1; i <= 10; i++)
        {
            Bullet bullet = CreateRedBullet().GetComponent<Bullet>();
            bullet.GetComponent<Bullet>().Pool = redBulletPool;
            bullet.Pool.Release(bullet); //持失
        }

        for (int i = 1; i <= 10; i++)
        {
            Bullet bullet = CreateBlueBullet().GetComponent<Bullet>();
            bullet.GetComponent<Bullet>().Pool = blueBulletPool;
            bullet.Pool.Release(bullet); //持失
        }

        for (int i = 1; i <= 10; i++)
        {
            Bullet bullet = CreateGreenBullet().GetComponent<Bullet>();
            bullet.GetComponent<Bullet>().Pool = greenBulletPool;
            bullet.Pool.Release(bullet); //持失
        }

    }

    private Bullet CreateRedBullet()
    {
        Bullet bulletGo = Instantiate(redBullet);
        bulletGo.GetComponent<Bullet>().Pool = redBulletPool;
        return bulletGo;
    }

    private Bullet CreateBlueBullet()
    {
        Bullet bullet = Instantiate(blueBullet);
        bullet.GetComponent<Bullet>().Pool = blueBulletPool;
        return bullet;
    }

    private Bullet CreateGreenBullet()
    {
        Bullet bullet = Instantiate(greenBullet);
        bullet.GetComponent<Bullet>().Pool = greenBulletPool;
        return bullet;
    }

    private void GetBullet(Bullet bullet)
    {
        bullet.gameObject.SetActive(true);
        
    }

    private void ReleaseBullet(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
    }

    private void DestroyBullet(Bullet bullet)
    {
        Destroy(bullet.gameObject);
    }
    
}
