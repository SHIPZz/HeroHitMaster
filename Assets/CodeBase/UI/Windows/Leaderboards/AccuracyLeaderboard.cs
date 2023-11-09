using System;
using System.Collections.Generic;
using Agava.YandexGames;
using CodeBase.Enums;
using I2.Loc;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Windows.Leaderboards
{
    public class AccuracyLeaderboard : SerializedMonoBehaviour
    {
        private const string Name = "Leaderboard";

        [SerializeField] private List<TextMeshProUGUI> _nameTexts;
        [SerializeField] private List<TextMeshProUGUI> _scoreTexts;
        [SerializeField] private List<TextMeshProUGUI> _rankTexts;
        [SerializeField] private TMP_Text _nameText;
        [SerializeField] private Button _closeLeaderboardButton;
        [OdinSerialize] private Dictionary<WeaponTypeId, Image> _icons;

        private readonly Dictionary<string, string> _anonimTexts = new()
        {
            { "en", "Anonymous" },
            { "ru", "Анонимный" },
            { "tr", "Anonim" },
        };

        public event Action Closed;

        private void OnEnable() =>
            _closeLeaderboardButton.onClick.AddListener(OnCloseClicked);

        private void OnDisable() =>
            _closeLeaderboardButton.onClick.RemoveListener(OnCloseClicked);

        private void OnCloseClicked() =>
            Closed?.Invoke();

        public void SetScore(int score)
        {
            if (!PlayerAccount.IsAuthorized)
                return;

            Leaderboard.GetPlayerEntry(Name,
                _ => { Leaderboard.SetScore(Name, score, () => Debug.Log("SUCCCESSSSS")); });
        }

        public void SetInfo(string weaponName, WeaponTypeId targetWeaponType)
        {
            foreach (Image icon in _icons.Values)
            {
                icon.gameObject.SetActive(false);
            }

            _nameText.text = weaponName;
            _icons[targetWeaponType].gameObject.SetActive(true);

            Fill();
        }

        private void Fill()
        {
            if (!PlayerAccount.IsAuthorized)
                return;

            PlayerAccount.RequestPersonalProfileDataPermission();

            if (!PlayerAccount.HasPersonalProfileDataPermission)
            {
                foreach (TextMeshProUGUI text in _nameTexts)
                {
                    text.text = _anonimTexts[LocalizationManager.CurrentLanguageCode];
                }

                return;
            }
            
            Leaderboard.GetEntries(Name, result =>
            {
                for (int i = 0; i < result.entries.Length; i++)
                {
                    _scoreTexts[i].text = result.entries[i].score.ToString();
                    _nameTexts[i].text = result.entries[i].player.publicName;
                    _rankTexts[i].text = result.entries[i].rank.ToString();

                    if (String.IsNullOrEmpty(_nameTexts[i].text))
                        _nameTexts[i].text = _anonimTexts[LocalizationManager.CurrentLanguageCode];
                    
                    _nameTexts[i].transform.parent.gameObject.SetActive(true);
                }
            });
        }
    }
}