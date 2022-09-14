using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Director : MonoBehaviour
{
    
    public TMPro.TMP_Text waveText;
    private int wave = 0;
    private int enemyCount = 0;
    private Transform[] spawns;
    public GameObject spawnPointContainer;
    public GameObject enemyPrefab;
    public Transform player;
    public Camera cam;
    
    
    // Start is called before the first frame update
    void Start()
    {
        this.spawns = spawnPointContainer.GetComponentsInChildren<Transform>(false);
        SetWave(1);
    }

    void LateUpdate()
    {
        if(enemyCount <= 0)
        {
            Debug.Log($"\tWave {this.wave} clear");

            SetWave(this.wave+=1);
        }
    }

    public void SetWave(int arg) {
        this.wave = arg;
        this.enemyCount = this.wave;
        Debug.Log($"\tWave {this.wave} starting");
        SetText();
        Spawn();
    }
    public void SetEnemyCount(int arg) {this.enemyCount = arg;}
    public int GetWave() {return this.wave;}
    public int GetEnemyCount() {return this.enemyCount;}
    public void EnemyDie() {this.enemyCount--;}
    
    void Spawn()
    {
        for(int i = 0; i < this.enemyCount; i++) // it feels wrong to have this many vars in a for loop
        {
            Transform point = this.spawns[Random.Range(0,this.spawns.Length)];
            GameObject obj = Instantiate(enemyPrefab, point.position, Quaternion.identity);
            obj.name = $"Enemy {i+1}";
            Enemy unit = obj.GetComponentInChildren<Enemy>();
            unit.player = this.player;
            unit.cam = this.cam;
            Health unitHealth = obj.GetComponent<Health>();
            unitHealth.director = this;
        }
        Debug.Log($"\t{this.enemyCount} units spawned");
    }
    
    void SetText()
    {
        if(waveText is not null) waveText.text = $"Wave {this.wave}";
    }
}
