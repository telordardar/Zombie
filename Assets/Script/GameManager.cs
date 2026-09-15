using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int totalKoin;
    private int koinTerkumpul = 0;

    [SerializeField] private int skor = 0;

    void Start()
    {
        totalKoin = GameObject.FindGameObjectsWithTag("coin").Length;
    }

    void OnEnable()
    {
        Enemy.OnZombieMati += TambahSkorSaatZombieMati;
    }

    void OnDisable()
    {
        Enemy.OnZombieMati -= TambahSkorSaatZombieMati;
    }

    void TambahSkorSaatZombieMati(Enemy zombieYangMati)
    {
        skor += 10;
        Debug.Log("Skor: " + skor);
    }

    public void AmbilKoin()
    {
        koinTerkumpul++;
        if (koinTerkumpul == totalKoin) Menang();
    }

    void Menang()
    {
        Debug.Log("WOI KAMU MENANG!");
    }
}