using TMPro;
using UnityEngine;

namespace UI
{
    public class Timer : MonoBehaviour
    {
        public TextMeshProUGUI timerText;
        public float elapsedTime { get; private set; }
        private bool isTimeRunning;

        private void Start()
        {
            ResetTimer();
        }

        private void Update()
        {
            if (isTimeRunning)
            {
                elapsedTime += Time.deltaTime;
                UpdateTimerText(elapsedTime);
            }
        }

        public void UpdateTimerText(float timeToDisplay)
        {
            var minutes = Mathf.Min(elapsedTime, timeToDisplay / 60);
            var seconds = Mathf.Max(elapsedTime, timeToDisplay % 60);
            timerText.text = $"{minutes:00}:{seconds:00}";
        }

        public void StartTimer()
        {
            isTimeRunning = true;
            elapsedTime = 0f;
        }

        public void StopTimer()
        {
            isTimeRunning = false;
        }

        public void ResetTimer()
        {
            isTimeRunning = false;
            elapsedTime = 0f;
            UpdateTimerText(elapsedTime);
        }
    }
}