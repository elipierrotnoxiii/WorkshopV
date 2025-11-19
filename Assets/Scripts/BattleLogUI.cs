using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Text;

public class BattleLogUI : MonoBehaviour
{
    public static BattleLogUI Instance;

    [Header("UI")]
    public TextMeshProUGUI logText;

    [Header("Settings")]
    public int maxLines = 10;
    public float fadeDuration = 3f;   // Time spent fading out
    public float fadeStartDelay = 1f; // Time before fade starts

    private class LogEntry
    {
        public string text;
        public float timeAdded;
    }

    private readonly List<LogEntry> entries = new List<LogEntry>();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        ClearLog();
    }

    public void AddMessage(string message)
    {
        entries.Add(new LogEntry { text = message, timeAdded = Time.time });

        // Limit the number of active lines
        if (entries.Count > maxLines)
            entries.RemoveAt(0);

        UpdateText();
    }

    public void ClearLog()
    {
        entries.Clear();
        if (logText != null)
            logText.text = "";
    }

    private void Update()
    {
        UpdateText();
    }

    private void UpdateText()
    {
        if (logText == null) return;

        StringBuilder sb = new StringBuilder();

        // We’ll track which messages are done fading
        List<LogEntry> toRemove = new List<LogEntry>();

        foreach (var entry in entries)
        {
            float age = Time.time - entry.timeAdded;
            float alpha = 1f;

            if (age > fadeStartDelay)
            {
                float t = Mathf.Clamp01((age - fadeStartDelay) / fadeDuration);
                alpha = Mathf.Lerp(1f, 0f, t);
            }

            // If completely faded, mark for removal
            if (alpha <= 0.01f)
            {
                toRemove.Add(entry);
                continue;
            }

            int alphaInt = Mathf.RoundToInt(alpha * 255);
            string alphaHex = alphaInt.ToString("X2");

            sb.AppendLine($"<alpha=#{alphaHex}>{entry.text}");
        }

        // Remove fully faded messages so newer ones move up
        foreach (var e in toRemove)
            entries.Remove(e);

        logText.text = sb.ToString();
    }
}
