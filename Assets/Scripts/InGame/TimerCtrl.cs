using TMPro;
using UnityEngine;

public class TimerCtrl : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _timeText;
    [SerializeField] private float _gameTime = 30.0f;

    public bool IsCountdown {  get; set; }

    public float CurrentTime { get; private set; }

    private void Start()
    {
        SetTime(_gameTime);
    }

    // Update is called once per frame
    private void Update()
    {
        if(IsCountdown && CurrentTime > 0.0f)
        {
            CurrentTime -= Time.deltaTime;
            if (CurrentTime <= 0.0f)
            {
                CurrentTime = 0.0f;
            }
            UpdateTimeText();
        }
    }

    public void SetTime(float time)
    {
        CurrentTime = time;

        UpdateTimeText();
    }

    private void UpdateTimeText()
    {
        _timeText.text = CurrentTime.ToString("0");
    }
}
