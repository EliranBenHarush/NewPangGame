using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PanelScore : MonoBehaviour
{   
    [SerializeField]
    private TMP_Text scoreText,highScoreText;
    
    public void RunAnim(int score)
    {
        if (scoreText != null)
        {
            scoreText.text = "SCORE" + "\n" + score;
        }
        if (highScoreText != null)
        {
            highScoreText.text = PlayerPrefs.GetInt("Score").ToString();
        }
        transform.DOLocalMove(Vector3.zero, 0.5f);
    }

    public void AnimClose()
    {
        
        transform.DOScale(Vector3.zero, 0.5f).OnComplete(() =>
        {
            Destroy(gameObject);
        });
    }
    
    public void ResetGame()
    {
        SceneManager.LoadScene("Game");
    }
    
}
