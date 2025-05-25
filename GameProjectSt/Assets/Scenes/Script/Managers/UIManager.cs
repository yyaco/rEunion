using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance = null;

    [SerializeField] List<GameObject> playerHearts;

    private void Start()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
    }

    public void RefreshHeart(int hp)
    {
        for(int i = 0; i < 6; i++)
        {
            if(hp > i)
            {
                playerHearts[i].SetActive(true);
                
            }
            else
            {
                playerHearts[i].SetActive(false);
            }
        }
    }
}
