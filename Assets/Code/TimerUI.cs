using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimerUI : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public float timer = 120f;

    void Update()
    {
        timer -= Time.deltaTime;


        int displayTime = Mathf.CeilToInt(timer);
        timerText.text = "Time: " + displayTime;

        if (timer <= 0)
        {
            // SceneManager.LoadScene("Oh no");
        }
    }
}