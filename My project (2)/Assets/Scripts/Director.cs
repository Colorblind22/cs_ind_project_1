using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Director : MonoBehaviour
{
    
    public TMPro.TMP_Text waveText;
    private int wave = 0;
    private int enemyCount = 0;
    public float spawnDistance;
    private Transform[] spawns;
    public GameObject upgradeMenu;
    private UpgradeMenu upgrades;
    public GameObject spawnPointContainer;
    public GameObject enemyPrefab;
    public GameObject player;
    public Camera cam;
    public Animator anim;
    
    // Start is called before the first frame update
    void Start()
    {
        this.spawns = spawnPointContainer.GetComponentsInChildren<Transform>(false);
        this.upgrades = upgradeMenu.GetComponent<UpgradeMenu>();
        SetWave(1);
    }

    void LateUpdate()
    {
        if(enemyCount <= 0 && !PauseMenu.GamePaused)
        {
            Debug.Log($"\tWave {this.wave} clear");
            player.GetComponent<Health>().Heal(20);
            OpenUpgradeMenu();
        }
    }
    
    void OpenUpgradeMenu()
    {
        upgradeMenu.SetActive(true);
        Time.timeScale = 0f;
        PauseMenu.GamePaused = true;
    }

    public void CloseUpgradeMenu()
    {
        upgradeMenu.SetActive(false);
        SetWave(++this.wave);
        Time.timeScale = 1f;
        PauseMenu.GamePaused = false;
    }

    public void SetWave(int arg) {
        this.wave = arg;
        this.enemyCount = this.wave;
        Debug.Log($"\tWave {this.wave} starting");
        SetText();
        Spawn();
    }
    void SetEnemyCount(int arg) {this.enemyCount = arg;}
    public int GetWave() {return this.wave;}
    public int GetEnemyCount() {return this.enemyCount;}
    public void EnemyDie() 
    {
        this.enemyCount--;
        upgrades.AddCurrency(2);
    }
    
    Vector2 GenerateSpawnPosition()
    {
        float a = Random.Range(-this.spawnDistance, this.spawnDistance);
        float c = this.spawnDistance;

        Vector2 ret;
        ret.x = player.transform.position.x + a;
        ret.y = Mathf.Sqrt(Mathf.Pow(c,2)-Mathf.Pow(a,2));
        ret.y = Random.Range(-1f, 1f) > 0 ? player.transform.position.y + ret.y : player.transform.position.y + ret.y * -1;
        Debug.Log($"\tgenerated vector: ({ret.x}, {ret.y})");
        return ret;
    }
    
    void Spawn()
    {
        for(int i = 0; i < this.enemyCount; i++) // it feels wrong to have this many vars in a for loop
        {
            //Transform point = this.spawns[Random.Range(0,this.spawns.Length)];
            GameObject obj = Instantiate(enemyPrefab, GenerateSpawnPosition()/*point.position*/, Quaternion.identity);
            obj.name = $"Enemy {i+1}";
            Enemy unit = obj.GetComponentInChildren<Enemy>();
            unit.player = this.player.GetComponent<Transform>();
            unit.cam = this.cam;
            Health unitHealth = obj.GetComponent<Health>();
            unitHealth.director = this;
        }
        Debug.Log($"\t{this.enemyCount} units spawned");
    }

    void SetText()
    {
        if(waveText is not null) waveText.text = $"Wave {this.wave}";
        anim.SetTrigger("WaveStart");
    }
}
