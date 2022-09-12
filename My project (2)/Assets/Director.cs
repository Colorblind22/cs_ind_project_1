using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Director : MonoBehaviour
{
    
    public TMPro.TMP_Text waveText;
    private int wave;
    private int enemyCount;
    private List<Transform> spawns;
    public GameObject spawnPointContainer;
    public GameObject enemyPrefab;
    public Transform player;
    
    
    // Start is called before the first frame update
    void Start()
    {
        SetWave(1);
        GetComponentsInChildren<Transform>(false, spawns);
        Debug.Log(spawns);
    }

    public void SetWave(int arg)
    {
        this.wave = arg;
        SetText();
    }
    public void SetEnemyCount(int arg)
    {
        this.enemyCount = arg;
    }
    public int GetWave() {return this.wave;}
    public int GetEnemyCount() {return this.enemyCount;}
    
    void Spawn()
    {
        
        for(int i = 0; i < this.enemyCount; i++)
        {
            
        }
    }
    
    void SetText()
    {
        if(waveText is not null) waveText.text = $"Wave {this.wave}";
    }
}
