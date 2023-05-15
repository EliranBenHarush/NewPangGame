using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lobby : MonoBehaviour
{
    [SerializeField] public GameObject lobbyImg,highScorePanel;
    
    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void HighScorePanel()
    {
        var obj=Instantiate(highScorePanel, lobbyImg.transform);
        obj.GetComponent<PanelScore>().RunAnim(0);
    }

   
}
