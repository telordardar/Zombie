using UnityEngine;
using UnityEngine.Events;
using System;


public class Enemy : MonoBehaviour, IDamageable
{
    public static event Action<Enemy> OnZombieMati;
    [SerializeField] protected int hp = 100;
    public float ms = 2f;
    protected Transform player;
    [Header("Pengaturan State Machine")]
    [SerializeField] protected float jarakDeteksi = 6f;
    [SerializeField] protected float jarakSerang = 1.2f;
    [SerializeField] private float jedaSerang = 1f;
    private StateZombie state = StateZombie.IDLE;
    private float waktuSerangTerakhir;
    [SerializeField] private Transform[] titikPatroli;
    private int indexPatroli = 0;
    [SerializeField] private UnityEvent onZombieMatiVisual;

    protected virtual void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
    }

    void Update()
    {
        PeriksaTransisi();

        switch (state)
        {
            case StateZombie.IDLE : PerilakuIdle(); break;
            case StateZombie.PATROL : PerilakuPatrol(); break;
            case StateZombie.CHASE : PerilakuChase(); break;
            case StateZombie.ATTACK : PerilakuAttack(); break;

        }

    }

     public float JarakPlayer()
    {
        if (player == null) return Mathf.Infinity;
        return Vector2.Distance(transform.position, player.position);
    }

    void PeriksaTransisi()
    {
        float jarak = JarakPlayer();
        if (jarak <= jarakSerang)
        state = StateZombie.ATTACK;
        else if(jarak <= jarakDeteksi)
        state = StateZombie.CHASE;
        else
        state = StateZombie.PATROL;
    }

    public void Kejar()
    {
        if (player == null) return;

        transform.position = Vector2.MoveTowards(
            transform.position,
            player.position,
            ms * Time.deltaTime
        );
    }

    public virtual void Serang()
    {
        Debug.Log("Enemy menyerang!");
    }

    public void KenaDamage(int jumlah)
    {
        hp -= jumlah;
        Debug.Log($"{gameObject.name} kena damage {jumlah}, HP sisa: {hp}");

        if (hp <= 0)
        {
            Mati();
        }
    }

    protected virtual void Mati()
    {
        Debug.Log($"{gameObject.name} mati!");
        OnZombieMati?.Invoke(this);
        onZombieMatiVisual?.Invoke();
        Destroy(gameObject);
    }

    void PerilakuIdle()
    {

    }
    void PerilakuAttack()
    {
        Debug.Log("enemy sedang menyerang");
    }
    void PerilakuChase()
    {
        Kejar();
    }
    void PerilakuPatrol()
    {
         if (titikPatroli.Length == 0) return;

    Transform target = titikPatroli[indexPatroli];

    transform.position = Vector2.MoveTowards(
        transform.position,
        target.position,
        ms * Time.deltaTime
    );

    if (Vector2.Distance(transform.position, target.position) < 0.1f)
    {
        indexPatroli++;

        if (indexPatroli >= titikPatroli.Length)
        {
            indexPatroli = 0;
        }
    }
    }
}