using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
  public AbilityManager skillManager;
  public static GameManager instance;

  [Header("# Game Control")]
  public bool isLive;
  public float gameTime;
  public float maxGameTime = 2 * 10f;

   [Header("# Player Info")]
  public int playerId;
 // public int level;
 // public float health;
 // public float maxHealth = 100;
  public int kill;
  public int exp;
  public int[] nextExp ={ 3, 5, 10 ,100, 150, 210, 280, 360, 450, 600};

   [Header("# Game Object")]
  public PoolManager pool;
  public Player player;
  public LevelUp uiLevelUp;
  public Result uiResult;
//  public GameObject enemyCleaner;

  public List<GameObject> numberOfenemy;

  void Awake()
  {
    instance = this;
  }

  public void GameStart(int id)
  {
     Resume();
    player.gameObject.SetActive(true);
    Debug.Log("active");
  

   
    playerId = id;
    //health=maxHealth;
    //임시 스크립트
    //uiLevelUp.Select(playerId % 2);//무기지급
   

    AudioManager.instance.PlayBgm(true);

    AudioManager.instance.PlaySfx(AudioManager.Sfx.Select);
  }

  public void GameOver()
  {
    StartCoroutine(GameOverRoutine());
  }

  IEnumerator GameOverRoutine()
  {
    isLive = false;

    yield return new WaitForSeconds(0.5f);

    uiResult.gameObject.SetActive(true);
    uiResult.Lose();

    Stop();

    AudioManager.instance.PlayBgm(false);
    AudioManager.instance.PlaySfx(AudioManager.Sfx.Lose);
  }

   public void GameVictory()
  {
    StartCoroutine(GameVictoryRoutine());
  }

  IEnumerator GameVictoryRoutine()
  {
    isLive = false;
   //enemyCleaner.SetActive(true);

    yield return new WaitForSeconds(0.5f);

    uiResult.gameObject.SetActive(true);
    uiResult.Win();
    Stop();

    AudioManager.instance.PlayBgm(false);
    AudioManager.instance.PlaySfx(AudioManager.Sfx.Win);
  }

  public void GameRetry()
  {
    SceneManager.LoadScene(0);
  }

    void Update()
    {
      if(!isLive)
      return;


        gameTime += Time.deltaTime;

        if(gameTime > maxGameTime)
        {
          gameTime = maxGameTime;
          GameVictory();
        }

        if(Input.GetKeyDown(KeyCode.P))
        {
          PlayerPrefs.DeleteAll();
        }
    }

    public void GetExp()
    {
      if(!isLive)
      return;


      exp++;

     // if(exp == nextExp[Mathf.Min(level,nextExp.Length-1)])
      {
     //   level++;
        exp = 0;
       // uiLevelUp.Show();
      }
    }


    public void Stop()
    {
      isLive = false;
      Time.timeScale = 0;
    }
     public void Resume()
    {
      isLive = true;
      Time.timeScale = 1;
    }

    public void Init()
    {
      PlayerPrefs.SetInt("SaveData", 1);
      GameManager.instance.player.level = 1;
      GameManager.instance.player.hp = 30;
      GameManager.instance.player.maxhp = GameManager.instance.player.hp;
      GameManager.instance.player.speed = 4;
      GameManager.instance.player.exp = 0;
      GameManager.instance.player.expToNextLevel = 10;
       Debug.Log("Init");
    }

    public void Load()
    {
       Debug.Log("Load");
      GameManager.instance.player.level  = PlayerPrefs.GetInt("Level",1);
      GameManager.instance.player.hp = PlayerPrefs.GetFloat("HP", 30);
      GameManager.instance.player.maxhp = PlayerPrefs.GetFloat("MaxHP", 30);
      GameManager.instance.player.speed = PlayerPrefs.GetFloat("Speed", 4);
      GameManager.instance.player.exp = PlayerPrefs.GetFloat("Exp", 0);
      GameManager.instance.player.expToNextLevel = PlayerPrefs.GetFloat("NextExp", 10);
      kill = PlayerPrefs.GetInt("Kill", 0);
    }

    public void Save()
    {
       Debug.Log("Save");
      PlayerPrefs.SetInt("Level", GameManager.instance.player.level);
      PlayerPrefs.GetFloat("HP", GameManager.instance.player.hp);
      PlayerPrefs.GetFloat("MaxHP", GameManager.instance.player.maxhp);
      PlayerPrefs.GetFloat("Speed", GameManager.instance.player.speed);
      PlayerPrefs.GetFloat("Exp", GameManager.instance.player.exp);
      PlayerPrefs.GetFloat("NextExp", GameManager.instance.player.expToNextLevel);
      PlayerPrefs.SetInt("Kill", kill);
    }

    private void OnApplicationQuit() {
      Save();
      PlayerPrefs.Save();
    }

   
}
