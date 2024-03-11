using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    GameManager gM;

    public GameObject dieObj;

    [SerializeField] int spawnMonId;
    [SerializeField] float hp;
    [SerializeField] float maxHp;

    public float Hp { get => hp; set => hp = value; }
    public float MaxHp { get => maxHp; set => maxHp = value; }
    
    // Start is called before the first frame update
    void Start()
    {
        gM = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void HPDown(float playerAtk)
    {
        hp -= playerAtk;
        SetHp();
    }
    public void SetHp()
    {
        if(hp > 0)
            gM.SetEnemyHpBar();
        else
        {
            Instantiate(dieObj);
            gM.MonsterDie();
            Destroy(gameObject);
        }
    }
}
