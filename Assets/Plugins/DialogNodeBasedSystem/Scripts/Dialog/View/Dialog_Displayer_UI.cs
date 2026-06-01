using UnityEngine;
using UnityEngine.UIElements;

namespace cherrydev
{
    public class Dialog_Displayer_UI : MonoBehaviour
    {
        [Header("MAIN COMPONENT")]
        [SerializeField] private DialogBehaviour _dialogBehaviour;
        [SerializeField] private UIDocument _uiDocument;

        private VisualElement _sentencePanel;
        private VisualElement _answerPanel;
        private VisualElement _buttonsContainer;
        private VisualElement _characterImage;
        private Label _characterName;
        private Label _dialogueText;

        private Button[] _answerButtons = new Button[0];
        private string _fullText;

        private void OnEnable()
        {
            var root = _uiDocument.rootVisualElement;

            _sentencePanel = root.Q<VisualElement>("sentence-panel");
            _answerPanel = root.Q<VisualElement>("answer-panel");
            _buttonsContainer = root.Q<VisualElement>("buttons-container");
            _characterImage = root.Q<VisualElement>("character-image");
            _characterName = root.Q<Label>("character-name");
            _dialogueText = root.Q<Label>("dialogue-text");

            // Debug - comprueba que encuentra los elementos
            Debug.Log($"sentence-panel: {_sentencePanel != null}");
            Debug.Log($"answer-panel: {_answerPanel != null}");
            Debug.Log($"dialogue-text: {_dialogueText != null}");

            SetPanelVisible(_sentencePanel, false);
            SetPanelVisible(_answerPanel, false);

            _dialogBehaviour.AddListenerToDialogFinishedEvent(DisableDialogPanel);
            _dialogBehaviour.DialogDisabled += DisableDialogPanel;

            _dialogBehaviour.SentenceNodeActivated += EnableSentencePanel;
            _dialogBehaviour.SentenceNodeActivated += DisableAnswerPanel;
            _dialogBehaviour.SentenceNodeActivatedWithParameter += SetupSentencePanel;
            _dialogBehaviour.DialogTextCharWrote += OnCharWrote;
            _dialogBehaviour.DialogTextSkipped += OnTextSkipped;

            _dialogBehaviour.AnswerNodeActivated += EnableAnswerPanel;
            _dialogBehaviour.AnswerNodeActivated += DisableSentencePanel;
            _dialogBehaviour.MaxAmountOfAnswerButtonsCalculated += CreateAnswerButtons;
            _dialogBehaviour.AnswerNodeActivatedWithParameter += EnableCertainAmountOfButtons;
            _dialogBehaviour.AnswerNodeSetUp += SetupAnswerButton;
            _dialogBehaviour.AnswerButtonSetUp += SetUpAnswerButtonClickEvent;

#if UNITY_LOCALIZATION
            _dialogBehaviour.LanguageChanged += HandleLanguageChanged;
#endif
        }

        private void OnDisable()
        {
            _dialogBehaviour.DialogDisabled -= DisableDialogPanel;

            _dialogBehaviour.SentenceNodeActivated -= EnableSentencePanel;
            _dialogBehaviour.SentenceNodeActivated -= DisableAnswerPanel;
            _dialogBehaviour.SentenceNodeActivatedWithParameter -= SetupSentencePanel;
            _dialogBehaviour.DialogTextCharWrote -= OnCharWrote;
            _dialogBehaviour.DialogTextSkipped -= OnTextSkipped;

            _dialogBehaviour.AnswerNodeActivated -= EnableAnswerPanel;
            _dialogBehaviour.AnswerNodeActivated -= DisableSentencePanel;
            _dialogBehaviour.MaxAmountOfAnswerButtonsCalculated -= CreateAnswerButtons;
            _dialogBehaviour.AnswerNodeActivatedWithParameter -= EnableCertainAmountOfButtons;
            _dialogBehaviour.AnswerNodeSetUp -= SetupAnswerButton;
            _dialogBehaviour.AnswerButtonSetUp -= SetUpAnswerButtonClickEvent;

#if UNITY_LOCALIZATION
            _dialogBehaviour.LanguageChanged -= HandleLanguageChanged;
#endif
        }

        // --- Sentence Panel ---

        private void SetupSentencePanel(string charName, string text, Sprite sprite)
        {
            _fullText = text;
            _characterName.text = charName;
            _dialogueText.text = "";

            if (sprite != null)
            {
                _characterImage.style.backgroundImage = new StyleBackground(sprite);
                SetPanelVisible(_characterImage, true);
            }
            else
                SetPanelVisible(_characterImage, false);
        }

        private void OnCharWrote()
        {
            if (_fullText == null) return;
            int nextLength = _dialogueText.text.Length + 1;
            if (nextLength <= _fullText.Length)
                _dialogueText.text = _fullText.Substring(0, nextLength);
        }

        private void OnTextSkipped(string fullText) => _dialogueText.text = fullText;

        // --- Answer Panel ---

        private void CreateAnswerButtons(int maxAmount)
        {
            _buttonsContainer.Clear();
            _answerButtons = new Button[maxAmount];

            for (int i = 0; i < maxAmount; i++)
            {
                var btn = new Button();
                btn.AddToClassList("answer-button");
                btn.style.display = DisplayStyle.None;
                _buttonsContainer.Add(btn);
                _answerButtons[i] = btn;
            }
        }

        private void EnableCertainAmountOfButtons(int amount)
        {
            for (int i = 0; i < _answerButtons.Length; i++)
                _answerButtons[i].style.display =
                    i < amount ? DisplayStyle.Flex : DisplayStyle.None;
        }

        private void SetupAnswerButton(int index, string text)
        {
            if (index < _answerButtons.Length)
                _answerButtons[index].text = text;
        }

        private void SetUpAnswerButtonClickEvent(int index, AnswerNode answerNode)
        {
            if (index >= _answerButtons.Length) return;
            int captured = index;
            _answerButtons[captured].clicked += () =>
                _dialogBehaviour.SetCurrentNodeAndHandleDialogGraph(captured);
        }

        // --- Visibility ---

        private void EnableSentencePanel() => SetPanelVisible(_sentencePanel, true);
        private void DisableSentencePanel() => SetPanelVisible(_sentencePanel, false);
        private void EnableAnswerPanel() => SetPanelVisible(_answerPanel, true);
        private void DisableAnswerPanel() => SetPanelVisible(_answerPanel, false);

        public void DisableDialogPanel()
        {
            SetPanelVisible(_sentencePanel, false);
            SetPanelVisible(_answerPanel, false);
        }

        private void SetPanelVisible(VisualElement el, bool visible)
        {
            if (el != null)
                el.style.display = visible ? DisplayStyle.Flex : DisplayStyle.None;
        }

#if UNITY_LOCALIZATION
        private void HandleLanguageChanged()
        {
            AnswerNode currentAnswerNode = _dialogBehaviour.CurrentAnswerNode;
            if (currentAnswerNode == null) return;

            for (int i = 0; i < currentAnswerNode.Answers.Count; i++)
            {
                if (i < _answerButtons.Length &&
                    _answerButtons[i].style.display == DisplayStyle.Flex)
                    _answerButtons[i].text = currentAnswerNode.GetAnswerText(i);
            }
        }
#endif
    }
}