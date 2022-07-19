using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [SerializeField] private GameObject failBackground;
    [SerializeField] private GameObject successBackground;
    [SerializeField] private GameObject tapToPlayArea;
    [SerializeField] private GameObject inGameArea;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private GameObject successArea;
    [SerializeField] private GameObject failArea;

    [SerializeField] private TextMeshPro plasticBagText;
    private int tempJellyAmount=0;
    
    private void Awake()
    {
        Instance = this;
    }

    public void ShowUI()
    {
        failBackground.SetActive(JellyManager.Instance.GetJellyGameStateMachine().GetCurrentState()
                == JellyManager.Instance.GetJellyGameStateMachine().failState);

        successBackground.SetActive(false);

        tapToPlayArea.SetActive((JellyManager.Instance.GetJellyGameStateMachine().GetCurrentState()
                 == JellyManager.Instance.GetJellyGameStateMachine().tapToPlayState) &&(GameManager.Instance.level<2));

        inGameArea.SetActive(JellyManager.Instance.GetJellyGameStateMachine().GetCurrentState()
                 == JellyManager.Instance.GetJellyGameStateMachine().playState);

        successArea.SetActive(false);

        failArea.SetActive(JellyManager.Instance.GetJellyGameStateMachine().GetCurrentState()
                 == JellyManager.Instance.GetJellyGameStateMachine().failState);

    }

    public void NextLevelOnUI(bool _isRestart)
    {
        if (!_isRestart)
        {
            GameManager.Instance.level = GameManager.Instance.level + 1;
        }

        GameManager.Instance.StartLevelStartAction();
    }
    public void ShowSuccessPanel()
    {

        successBackground.SetActive(true);

        successArea.SetActive(true);
    }

    public void ShowLevel()
    {
        levelText.text = "Level " + GameManager.Instance.level;
    }
    public void IncreasePlasticBagJellyAmount(ref int _jellyAmount)
    {
        DOTween.To(() => tempJellyAmount, x => tempJellyAmount = x, _jellyAmount, 0.3f)
            .OnUpdate(() =>
            {
                plasticBagText.text = tempJellyAmount.ToString();
            });

        //plasticBagText.text = _jellyAmount.ToString();
    }
}
