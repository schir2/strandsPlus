using TMPro;
using UnityEngine;

namespace UI
{
    public class Timer : MonoBehaviour
    {
        public static Timer Instance { get; private set; }
        public TextMeshProUGUI timerText;
        public float ElapsedTime { get; set; }
        private bool isTimeRunning;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            ResetTimer();
        }

        private void Update()
        {
            if (!isTimeRunning) return;
            ElapsedTime += Time.deltaTime;
            UpdateTimerText(ElapsedTime);
        }

        private void UpdateTimerText(float timeToDisplay)
        {
            var minutes = Mathf.Min(ElapsedTime, timeToDisplay / 60);
            var seconds = Mathf.Max(ElapsedTime, timeToDisplay % 60);
            timerText.text = $"{minutes:00}:{seconds:00}";
        }

        public void ResumeTimer()
        {
            isTimeRunning = true;
        }

        public void PauseTimer()
        {
            isTimeRunning = false;
        }

        private void ResetTimer()
        {
            isTimeRunning = false;
            ElapsedTime = 0f;
            UpdateTimerText(ElapsedTime);
        }
    }
}