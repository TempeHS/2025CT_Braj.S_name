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

    [SerializeField] private int wave = 1;
    [SerializeField] private int enemyCount = 6;
    [SerializeField] private float enemyCountRate = 0.2f;
    [SerializeField] private float spawnDelayMax = 1f;
    [SerializeField] private float spawnDelayMin = 0.75f;

    [SerializeField] private float zombieRate = 0.5f;
    [SerializeField] private float fastZombieRate = 0.4f;
    [SerializeField] private float tankZombieRate = 0.1f;

    private bool wavedone = false;
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
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (Input.GetKeyDown(KeyCode.Return) && wavedone && enemies.Length == 0)
        {
            wave++;
            wavedone = false;
            enemyCount += Mathf.RoundToInt(enemyCount * enemyCountRate);
            SetWave();
        }

        if (Input.GetKeyDown(KeyCode.D) && wavedone)
        {
            for (int i = 0; i < enemies.Length; i++)
            {
                Destroy(enemies[i]);
            }
        }
    }

    private void SetWave()
    {
        zombieCount = Mathf.RoundToInt(enemyCount * (zombieRate+ tankZombieRate));
        fastZombieCount = Mathf.RoundToInt(enemyCount * fastZombieRate);
        tankZombieCount = 0;

        if (wave % 5 == 0)
        {
            zombieCount = Mathf.RoundToInt(enemyCount * zombieRate);
            tankZombieCount = Mathf.RoundToInt(enemyCount * tankZombieRate);
        }

        enemyLeft = zombieCount + fastZombieCount + tankZombieCount;
        enemyCount = enemyLeft; 

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

        waveset = Shuffle(waveset);

        StartCoroutine(spawn());
    }


    public List<GameObject> Shuffle(List<GameObject> waveSet)
    {
        List<GameObject> temp = new List<GameObject>();
        List<GameObject> result = new List<GameObject>();
        temp.AddRange(waveSet);

        for (int i = 0; i < waveSet.Count; i++) ;
        {
            int index = Random.Range(0, temp.Count - 1);
            result.Add(temp[index]);
            temp.RemoveAt(index);
        }
    }
    IEnumerator spawn()
    {
        for (int i = 0; i < waveset.Count; i++) ;
        {
            Instantiate(waveset[i], spawnpoint.position, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(spawnDelayMin, spawnDelayMax));
        }
        wavedone = true;
    }

}
