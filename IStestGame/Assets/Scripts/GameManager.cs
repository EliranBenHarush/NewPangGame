using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class Mobile
{
    public static bool CheckPlatform()
    {
        bool isMobile;
        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        {
            isMobile = true;
        }
        else
        {
            isMobile = false;
        }
        return isMobile;
    }
}
public class GameManager : MonoBehaviour
{   
    [SerializeField]
    private GameObject panelScore ,bubblesPrefab, bubblesEmpty,joystick,arrowBtn;
    [SerializeField]
    private Canvas canvas;
    [Space]
    [SerializeField]
    private TMP_Text timerText,liveText;
    [SerializeField]
    private Image heartLive;
    private GameObject player;
    [Space]
    private const string SCORE_KEY = "Score";
    public float damage;
    private int score;
    private int live = 4;
    private int levelup = 1;
    
    void Start()
    {
        Arrow.bubbleExplos += BubbleExploded;
        Balls.PlayerDamage += PlayerDamage;
        StartGame(levelup);
        LoadScore();
        if (!Mobile.CheckPlatform())
        {
            joystick.SetActive(false);
            arrowBtn.SetActive(false);
        }
    }

    private void StartGame(int bubblesCount)
    {
        liveText.text = "X" + live;
        StartCoroutine("TimerGame",0);
        for (int i = 0; i < bubblesCount; i++)
        {
            Instantiate(bubblesPrefab, bubblesEmpty.transform);
        }
    }

    private void PlayerDamage()
    {
        var result = heartLive.fillAmount - damage;
        heartLive.DOFillAmount(result, 1);
        live--;
        liveText.text = "X" + live;
        if (live == 0)
        {
            GameOver();
        }
    }


    private void BubbleExploded()
    {
        AddPoints(100);
        Debug.Log(bubblesEmpty.transform.childCount);
        if (bubblesEmpty.transform.childCount <= 1  && live >0)
        {
            levelup++;
            LevelUp();
        }
    }

    private void LevelUp()
    {
        StartGame(levelup);
    }

    

    private IEnumerator TimerGame()
    {
        int timer = 99;
        for (int i = 0; i < timer; i++)
        {
            yield return new WaitForSeconds(1);
            timer--;
            timerText.text = "TIME: " + timer;
        }
        GameOver();
    }

    private void GameOver()
    {
        StopAllCoroutines();
       var obj= Instantiate(panelScore, canvas.transform);
       for (int i = 0; i < bubblesEmpty.transform.childCount; i++)
       {
           Destroy(bubblesEmpty.transform.GetChild(i).gameObject);
       }
       obj.GetComponent<PanelScore>().RunAnim(score);
    }
    
    
    private void AddPoints(int points)
    {
        score += points;
        SaveScore();
    }

    

    private void SaveScore()
    {
        PlayerPrefs.SetInt(SCORE_KEY, score);
        PlayerPrefs.Save();
    }

    private void LoadScore()
    {
        if (PlayerPrefs.HasKey(SCORE_KEY))
        {
            score = PlayerPrefs.GetInt(SCORE_KEY);
        }
        else
        {
            score = 0;
        }
    }
}
