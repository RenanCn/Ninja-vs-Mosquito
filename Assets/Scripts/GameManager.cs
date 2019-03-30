using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static int score;
    public static int record;

    [SerializeField]
    private Text txtPlacar;
    [SerializeField]
    private Text txtRecord;

    private bool paused;

    public bool Paused
    {
        get { return paused; }
        set { paused = value; }
    }

    // Use this for initialization
    void Start()
    {
        Paused = false;
    }

    void Update()
    {
        txtPlacar.text = score.ToString();
        txtRecord.text = PlayerPrefs.GetInt("record", record).ToString();
    }


    public void ChangeScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public void ExitGame()
    {
        Application.Quit();
    }


    public void Pause()
    {
        Paused = !Paused;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Paused = !Paused;
        }

        if (Paused == true)
        {
            Time.timeScale = 0;
        }
        else if (Paused == false)
        {
            Time.timeScale = 1;
        }
    }




}
