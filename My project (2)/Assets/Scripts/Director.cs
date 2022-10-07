using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Director : MonoBehaviour
{
    #region vars
    public TMPro.TMP_Text waveText;
    private int wave = 0;
    private int enemyCount = 0;
    public float spawnDistance;
    private Transform[] spawns;
    List<GameObject> enemies;
    public GameObject upgradeMenu;
    private UpgradeMenu upgrades;
    //public GameObject spawnPointContainer;
    public GameObject enemyPrefab;
    public GameObject player;
    public Camera cam;
    public Animator anim;
    #endregion
    #region methods
    void Start()
    {
        Debug.Log("Director.Start() called");
        //this.spawns = spawnPointContainer.GetComponentsInChildren<Transform>(false);
        this.upgrades = upgradeMenu.GetComponent<UpgradeMenu>();
        Debug.Log("Calling UpgradeMenu.Start() from Director.Start()");
        upgrades.Start();
        this.enemies = new List<GameObject>();
        if(Flags.LoadOnEnter)
        {
            Debug.Log("Resuming game");
            Load();
            Flags.LoadOnEnter = false;
        }
        else SetWave(0);
    }

    void LateUpdate()
    {
        if(enemyCount <= 0 && !PauseMenu.GamePaused)
        {
            Debug.Log($"Wave {this.wave} clear\n");
            OpenUpgradeMenu();
        }
    }
    
    public void Save()
    {
        SaveSystem.Save(this.player, this.upgrades, this);
    }

    public void Load() // i hate this.
    {
        SaveData data = SaveSystem.Load();
        if(data is not null)
        {
            Debug.Log("Data loading");
            Debug.Log(data);
            Vector2 position = new Vector2();
            position.x = data.playerPosition[0];
            position.y = data.playerPosition[1];
            this.player.transform.position = position;
            this.upgrades.currency = data.currency;
            this.upgrades.hp.maxHealth = data.maxHealth;
            this.upgrades.hp.SetHealth(data.health);
            this.upgrades.movement.moveSpeed = data.moveSpeed;
            this.upgrades.wep.damage = data.damage;
            this.upgrades.wep.SetFireRate(data.fireRate);
            this.upgrades.damageUpgradeCost = data.damageUpgradeCost;
            this.upgrades.moveSpeedUpgradeCost = data.moveSpeedUpgradeCost;
            this.upgrades.fireRateUpgradeCost = data.fireRateUpgradeCost;
            this.upgrades.healthUpgradeCost = data.healthUpgradeCost;
            this.ClearEnemies();
            this.SetWave(data.wave);
            Debug.Log("Loading finished");
        }
        else
        {
            Debug.Log("Load cancelled");
            return;
        }
    }

    void OpenUpgradeMenu()
    {
        Debug.Log("Opening upgrade menu");
        upgradeMenu.SetActive(true);
        Time.timeScale = 0f;
        PauseMenu.GamePaused = true;
        upgrades.UpdateText();
        Debug.Log("Opened upgrade menu");
    }

    public void CloseUpgradeMenu()
    {
        Debug.Log("Closing upgrade menu");
        upgradeMenu.SetActive(false);
        SetWave(++this.wave);
        Time.timeScale = 1f;
        PauseMenu.GamePaused = false;
        Debug.Log("Closed upgrade menu");
    }

    public void SetWave(int arg) {
        this.wave = arg;
        this.enemyCount = this.wave;
        Debug.Log($"Wave {this.wave} starting");
        SetText();
        Spawn();
    }
    void SetEnemyCount(int arg) {this.enemyCount = arg;}
    public int GetWave() {return this.wave;}
    public int GetEnemyCount() {return this.enemyCount;}
    public int EnemyDie() 
    {
        upgrades.AddCurrency(2);
        return --this.enemyCount;
    }

    public void GameOver()
    {
       // bool newHighScore = false;
        if(this.wave > PlayerPrefs.GetInt("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", this.wave);
            //newHighScore = true;
        }
        //GameOverMenu.Activate(); or something
        
    }
    
    Vector2 GenerateSpawnPosition()
    {
        float a = Random.Range(-this.spawnDistance, this.spawnDistance);
        float c = this.spawnDistance;

        Vector2 ret;
        ret.x = player.transform.position.x + a;
        ret.y = Mathf.Sqrt(Mathf.Pow(c,2)-Mathf.Pow(a,2));
        ret.y = Random.Range(-1f, 1f) > 0 ? player.transform.position.y + ret.y : player.transform.position.y + ret.y * -1;
        //Debug.Log($"\tgenerated vector: ({ret.x}, {ret.y})");
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
            enemies.Add(obj);
        }
        Debug.Log($"\t{this.enemyCount} units spawned");
    }

    void ClearEnemies()
    {
        for(int i = enemies.Count - 1; i >= 0; i--)
        {
            Destroy(enemies[i].gameObject);
        }
        Debug.Log("Enemies removed");
        enemies.Clear();
    }

    void SetText()
    {
        if(waveText is not null) waveText.text = $"Wave {this.wave}";
        anim.SetTrigger("NewWave");
    }
    #endregion
}
