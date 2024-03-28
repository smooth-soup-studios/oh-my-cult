using UnityEngine;
using UnityEngine.UIElements;

namespace Managers {
    public class UIManager : MonoBehaviour {
        public static UIManager Instance { get; private set; }

        public StateMachine PlayerStateMachine;

        private VisualElement _root;
        private VisualElement _healthBarValue;
        private VisualElement _keyIndicator;

        private float _debugHealth = .5f;

        private void Awake() {
            if (Instance == null) {
                if (PlayerStateMachine == null) {
                    throw new System.Exception("PlayerStateMachine is not set in UIManager");
                }

                Instance = this;
                _root = GetComponent<UIDocument>().rootVisualElement;
                _healthBarValue = _root.Q<VisualElement>("health-bar-value");
                _keyIndicator = _root.Q<VisualElement>("key-indicator");
            }
            else {
                Destroy(gameObject);
            }
        }

        private void Update() {
            _healthBarValue.style.width = new StyleLength(new Length(_debugHealth * 100, LengthUnit.Percent));
            _keyIndicator.style.visibility = PlayerStateMachine.HasPlaytestKey ? Visibility.Visible : Visibility.Hidden;
        }

        public void ToggleDialogBox() {
            if (_root.Q<VisualElement>("dialog-box").ClassListContains("closed")) {
                ShowDialogBox();
            }
            else {
                HideDialogBox();
            }
        }

        public void ShowDialogBox() {
            _root.Q<VisualElement>("dialog-box").RemoveFromClassList("closed");
        }

        public void HideDialogBox() {
            _root.Q<VisualElement>("dialog-box").AddToClassList("closed");
        }

        public void DebugCycleHealth() {
            _debugHealth = 1 - ((1 - _debugHealth + .1f) % 1);
        }
    }
}