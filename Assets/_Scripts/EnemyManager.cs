using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager main;

    public Transform spawnpoint;
    public Transform[] checkpoints;

    [SerializeField] private GameObject zombie;
    [SerializeField] private GameObject fastZombie;
    [SerializeField] private GameObject tankZombie;

    [SerializeField] private int enemyCount = 6;

    [SerializeField] private float zombieRate = 0.5f;
    [SerializeField] private float fastZombieRate = 0.4f;
    [SerializeField] private float tankZombieRate = 0.1f;

    private List<GameObject> waveset = new List<GameObject>();
    private int enemyLeft;

    private int zombieCount;
    private int fastZombieCount;
    private int tankZombieCount;
    void Awake()
    {
        main = this;
    }

    void Start()
    {
        SetWave();
    }

    void Update()
    {

    }

    private void SetWave()
    {
        zombieCount = Mathf.RoundToInt(enemyCount * zombieRate);
        fastZombieCount = Mathf.RoundToInt(enemyCount * fastZombieRate);
        tankZombieCount = Mathf.RoundToInt(enemyCount * tankZombieRate);

        waveset = new List<GameObject>();

        for (int i = 0; i < zombieCount; i++)
        {
            waveset.Add(zombie);
        }

        for (int i = 0; i < fastZombieCount; i++)
        {
            waveset.Add(fastZombie);
        }

        for (int i = 0; i < tankZombieCount; i++)
        {
            waveset.Add(tankZombie);
        }

        StartCoroutine(spawn());
    }

    IEnumerator spawn()
    {
        for (int i = 0; i < waveset.Count; i++)
        {
            Instantiate(waveset[i], spawnpoint.position, Quaternion.identity);
            yield return new WaitForSeconds(0.5f);
        }
    }

}
