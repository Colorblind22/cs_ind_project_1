using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Director : MonoBehaviour
{
    #region vars
    public float upgradeFactor = 1f;
    public TMPro.TMP_Text waveText;
    public GameObject upgradeNotification;
    private int wave = 0;
    private int enemyCount = 0;
    public float spawnDistance;
    private Transform[] spawns;
    List<GameObject> enemies;
    public GameObject upgradeMenu;
    public UpgradeMenu upgrades;
    public GameObject gameOver;
    private GameOverMenu gameOverMenu;
    //public GameObject spawnPointContainer;
    public GameObject enemyPrefab;
    public GameObject player;
    //public Camera cam;
    public Animator anim;
    public Animator upgradeAnim;
    
    bool 
    goingToNextWave,
    cleared;
    #endregion
    #region methods
    void Start()
    {
        Debug.Log("Director.Start() called");
        //this.spawns = spawnPointContainer.GetComponentsInChildren<Transform>(false);
        this.gameOverMenu = this.gameOver.GetComponent<GameOverMenu>();
        this.gameOverMenu.director = this;
        this.upgrades = upgradeMenu.GetComponent<UpgradeMenu>();
        //Debug.Log("Calling UpgradeMenu.Start() from Director.Start()");
        upgrades.Start();
        this.enemies = new List<GameObject>();
        if(Flags.LoadOnEnter)
        {
            Debug.Log("Resuming game");
            Load();
            Flags.LoadOnEnter = false;
        }
        else StartCoroutine(SetWave(1));
        upgrades.UpdateText();
    }

    void LateUpdate()
    {
        if(enemyCount <= 0 && !PauseMenu.GamePaused && !cleared)
        {
            this.cleared = true;
            Debug.Log($"Wave {this.wave} clear\n");
            StartCoroutine(OpenUpgradeMenu());
        }
    }
    
    public void Save()
    {
        SaveSystem.Save(this.player, this.upgrades, this, this.gameOverMenu);
    }

    public void Load() // i hate this.
    {
        SaveData data = SaveSystem.Load();
        if(data is not null) // set variables to values saved
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
            this.upgrades.damageUpgradeCount = data.damageUpgradeCount;
            this.upgrades.moveSpeedUpgradeCount = data.moveSpeedUpgradeCount;
            this.upgrades.fireRateUpgradeCount = data.fireRateUpgradeCount;
            this.upgrades.healthUpgradeCount = data.healthUpgradeCount;
            this.gameOverMenu.projectiles = data.projectiles;
            this.gameOverMenu.enemiesDefeated = data.enemiesDefeated;
            this.gameOverMenu.totalCurrency = data.totalCurrency;
            //this.upgradeFactor = data.upgradeFactor;
            this.ClearEnemies();
            StartCoroutine(this.SetWave(data.wave));
            Debug.Log("Loading finished");
        } 
        else
        {
            Debug.Log("Load cancelled as save was not found");
            StartCoroutine(this.SetWave(1));
            return;
        }
    }

    IEnumerator OpenUpgradeMenu()
    {
        Debug.Log("Opening upgrade menu");
        upgradeMenu.SetActive(true);
        PauseMenu.GamePaused = true;
        upgradeAnim.SetTrigger("FadeIn");
        yield return new WaitForSeconds(.25f);
        Time.timeScale = 0f;
        upgrades.UpdateText();
    }

    public IEnumerator CloseUpgradeMenu()
    {
        if(!goingToNextWave)
        {
            this.goingToNextWave = true;
            Debug.Log("Closing upgrade menu");
            Time.timeScale = 1f;
            upgradeAnim.SetTrigger("FadeOut");
            yield return new WaitForSeconds(.25f);
            upgradeMenu.SetActive(false);
            StartCoroutine(SetWave(++this.wave));
            PauseMenu.GamePaused = false;
        }
    }


    public void NextWave()
    {
        StartCoroutine(CloseUpgradeMenu());
    }

    IEnumerator SetWave(int arg) {
        this.wave = arg;
        Debug.Log($"Wave {this.wave} starting");
        this.enemyCount = this.wave;
        if(wave % 5 == 0) StartCoroutine(EnemyUpgrade());
        this.upgradeFactor = this.CalculateUpgradeFactor();
        SetText();
        yield return new WaitForSeconds(1);
        Spawn();
        this.goingToNextWave = false;
        this.cleared = false;
    }
    IEnumerator EnemyUpgrade()
    {
        upgradeFactor += 0.1f;
        if(upgradeNotification is not null) upgradeNotification.SetActive(true);
        yield return new WaitForSeconds(1);
        if(upgradeNotification is not null) upgradeNotification.SetActive(false);
    }
    float CalculateUpgradeFactor()
    {
        return 1f + (0.1f * (int)(wave/5));
    }
    void SetEnemyCount(int arg) {this.enemyCount = arg;}
    public int GetWave() {return this.wave;}
    public int GetEnemyCount() {return this.enemyCount;}
    public int EnemyDie() 
    {
        upgrades.AddCurrency(2);
        gameOverMenu.totalCurrency += 2;
        gameOverMenu.enemiesDefeated++;
        return --this.enemyCount;
    }

    public void GameOver()
    {
        gameOverMenu.Activate();
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
            EnemyStats statPackage = new EnemyStats(upgradeFactor);
            Debug.Log(statPackage);
            unit.stats = statPackage;
            unit.player = this.player.GetComponent<Transform>();
            //unit.cam = this.cam;
            obj.GetComponent<Health>().director = this;
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
        if(upgradeNotification is not null) 
        anim.SetTrigger("NewWave");
    }
    #endregion
}
