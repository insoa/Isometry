using System;
using System.Collections;
using System.Linq;
using Saves;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Items{
    public sealed class ItemsController : MonoBehaviour{
        public static ItemsController Instance;

        [SerializeField] private Item[] _items;
        [SerializeField] private DownButton _unlockButton;
        [SerializeField] private TextMeshProUGUI _priceText;
        [SerializeField] private TextMeshProUGUI _globalCoins;
        [SerializeField] private Button _inventoryOpenButton;
        [SerializeField] private Button _adsButton;
        [SerializeField] private Button _settingsButton;
        [SerializeField] private Button _backButton;
        [SerializeField] private Button _offerExitButton;
        [SerializeField] private GameObject _inventoryPanel;
        [SerializeField] private GameObject _settingsPanel;
        [SerializeField] private GameObject _globalMoneyObj;
        [SerializeField] private GameObject _menuButtonsPanel;
        [SerializeField] private GameObject _offerPanel;
        [SerializeField] private Animator[] _panelAnimator;
        [SerializeField] private Animator _closeButtonsPanel;
        [SerializeField] private GameData _gameData;
        [SerializeField] private AudioSource[] _clickSound;
        [SerializeField] private Color _frameCommonItemColor;
        [SerializeField] private Color _frameRareItemColor;
        [SerializeField] private Color _frameEpicItemColor;
        [SerializeField] private Color _frameLegendaryItemColor;

        private int _currentIndex = -1;
        public int GlobalCoinsValue;

        private void Awake() => Instance = this;

        public void AddBonus() => _globalCoins.text = GlobalCoinsValue.ToString();

        private void Start(){
            _gameData.Load();
            GlobalCoinsValue = _gameData.GlobalMoney;
            _globalCoins.text = GlobalCoinsValue.ToString();
            SubscriptionButtons();
            for (var i = 0; i < _items.Length; i++){
                var index = i;
                _items[i].ItemCellButton.onClick.AddListener(() => OnCellClick(index));
                if (_items[i].IsEquipped == false)
                    _items[i].SetSelected(false);
            }

            _unlockButton.Button.onClick.AddListener(BuyItem);
            InitializeData();
            _unlockButton.SetState(EDownButtonType.Unlocked);
            StartCoroutine(ShowOffer());
        }

        private IEnumerator ShowOffer(){
            yield return new WaitForSeconds(1.6f);
            var d = Random.Range(0, 4);
            if (!_gameData.AdsDisabled && d == 0){
                _offerPanel.SetActive(true);
            }
        }

        private void SubscriptionButtons(){
            _inventoryOpenButton.onClick.AddListener(OnInventoryButtonClick);
            _settingsButton.onClick.AddListener(OnSettingsClick);
            _adsButton.onClick.AddListener(OnAdsClick);
            _backButton.onClick.AddListener(OnCloseClick);
            _backButton.onClick.AddListener(OnCloseClick);
            _offerExitButton.onClick.AddListener(OnExitClick);
        }

        private void InitializeData(){
            _gameData.Load();
            var index = 0;
            for (; index < _items.Length; index++){
                var t = _items[index];
                if (t.ItemId != _gameData.SelectedItemId) 
                    continue;
                t.IsLocked = false;
                t.IsEquipped = true;
                t.SetSelected(true);
                if (t.ItemRareType == EItemRareType.Common)
                    t.Selector.GetComponent<Image>().color = _frameCommonItemColor;
                if (t.ItemRareType == EItemRareType.Rare)
                    t.Selector.GetComponent<Image>().color = _frameRareItemColor;
                if (t.ItemRareType == EItemRareType.Epic)
                    t.Selector.GetComponent<Image>().color = _frameEpicItemColor;
                if (t.ItemRareType == EItemRareType.Legendary)
                    t.Selector.GetComponent<Image>().color = _frameLegendaryItemColor;
            }

            foreach (var item in from t in _gameData.UnlockedItemsId from item in _items where item.ItemId == t select item){
                item.IsLocked = false;
                item.IsPurchased = true;
                item.SetState(EButtonType.Unlock);
            }
        }

        private void Update(){
            foreach (var item in _items){
                if (item.IsEquipped) 
                    item.SetSelected(true);
                item.SetState(item.IsLocked ? EButtonType.Lock : EButtonType.Unlock);
            }
        }

        private void BuyItem(){
            foreach (var item in _items){
                if (!item.IsLocked || !item.IsEquipped)
                    continue;
                if (item.Prise <= GlobalCoinsValue && !item.IsPurchased){
                    _clickSound[3].Play();
                    GlobalCoinsValue -= item.Prise;
                    _gameData.GlobalMoney -= item.Prise;
                    _globalCoins.text = GlobalCoinsValue.ToString();
                    item.IsEquipped = true;
                    item.IsPurchased = true; //СОХРАНИТЬ
                    OnCellClick(item.ItemId); // ПЕРЕПРОВЕРИТЬ
                    item.IsLocked = false;
                    _gameData.UnlockedItemsId.Add(item.ItemId);
                    _unlockButton.SetState(EDownButtonType.Unlocked);
                    _gameData.Save();
                    _gameData.Load();
                }
                else{
                    _clickSound[4].Play();
                }
            }
        }

        private void OnCellClick(int index){
            _currentIndex = index;
            for (var i = 0; i < _items.Length; i++){
                _items[i].SetSelected(i == _currentIndex);

                if (_items[i].ItemRareType == EItemRareType.Common)
                    _items[i].Selector.GetComponent<Image>().color = _frameCommonItemColor;

                if (_items[i].ItemRareType == EItemRareType.Rare)
                    _items[i].Selector.GetComponent<Image>().color = _frameRareItemColor;

                if (_items[i].ItemRareType == EItemRareType.Epic)
                    _items[i].Selector.GetComponent<Image>().color = _frameEpicItemColor;

                if (_items[i].ItemRareType == EItemRareType.Legendary)
                    _items[i].Selector.GetComponent<Image>().color = _frameLegendaryItemColor;

                if (i != _currentIndex)
                    continue;
                if (_items[i].IsLocked)
                    _clickSound[2].Play();
                else
                    _clickSound[5].Play();

                _items[i].ItemAnim.Play();
                if (_items[i].CurrentLocalState == EButtonType.Lock){
                    _unlockButton.SetState(EDownButtonType.Locked);
                    _priceText.text = _items[i].Prise.ToString();
                }
                else{
                    _unlockButton.SetState(EDownButtonType.Unlocked);
                    _items[i].IsEquipped = true;
                }

                if (_items[i].IsPurchased){
                    _gameData.SelectedItemId = _items[i].ItemId;
                    ItemVisualize.Instance.ChangeItemPrefab();
                }
            }
        }

        private void OnInventoryButtonClick(){
            _clickSound[0].Play();
            _gameData.Load();
            _inventoryPanel.SetActive(true);
            _backButton.gameObject.SetActive(true);
            _globalMoneyObj.SetActive(true);
            _closeButtonsPanel.Play("ClosePanel");
        }

        private void OnCloseClick(){
            _clickSound[1].Play();
            _panelAnimator[0].Play("CloseInventoryPanel");
            _panelAnimator[1].Play("CloseInventoryPanel");
            StartCoroutine(ClosePanels());
            _backButton.gameObject.SetActive(false);
            _menuButtonsPanel.SetActive(true);
            _globalMoneyObj.SetActive(false);
            _closeButtonsPanel.Play("OpenPanel");
        }

        private void OnSettingsClick(){
            _clickSound[0].Play();
            _settingsPanel.SetActive(true);
            _backButton.gameObject.SetActive(true);
            _closeButtonsPanel.Play("ClosePanel");
        }

        private void OnAdsClick(){
            _clickSound[0].Play();
            _offerPanel.SetActive(true);
        }

        private void OnExitClick(){
            _clickSound[0].Play();
            _offerPanel.SetActive(false);
        }

        IEnumerator ClosePanels(){
            yield return new WaitForSeconds(0.7f);
            _inventoryPanel.SetActive(false);
            _settingsPanel.SetActive(false);
            _gameData.Save();
        }
    }
}