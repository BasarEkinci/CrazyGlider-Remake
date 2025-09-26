using TMPro;
using UnityEngine;
using static _GameFolders.Scripts.Managers.GameEventManager;

namespace _GameFolders.Scripts.UI.Panels
{
    public class GameOverPanel : MonoBehaviour
    {
        [SerializeField] private TMP_Text distanceText;
        [SerializeField] private TMP_Text maxHeightText;
        [SerializeField] private TMP_Text earnedCoinText;
        [SerializeField] private TMP_Text skillText;

        private void OnEnable()
        {
            OnLevelFailed += HandleLevelFailed;
        }
        private void OnDisable()
        {
            OnLevelFailed -= HandleLevelFailed;
        }
        private void HandleLevelFailed(float arg1, float arg2)
        {
            distanceText.SetText($" {arg2:F}");
            maxHeightText.SetText($" {arg1:F}");
            earnedCoinText.SetText($" {0f}");
            skillText.SetText($" {0f}");
        }
    }
}