using UnityEngine;
using UnityEngine.UI;
using com.soha.bridge;
using UnityEngine.Events;

public class GameCOntroller : MonoBehaviourSingleton<GameCOntroller>
{
    [SerializeField] private Text moveLeftTXT;
    [SerializeField] private GameObject outtaMovePanel;
    [SerializeField] private int moveLeft;
    [SerializeField] private UnityEvent onLoseGame;
    [SerializeField] private GameObject interactPanel;
    [SerializeField] private Button swapButton;


    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void Init()
    {
        SetMoveLeftTXT(moveLeft.ToString());
    }
    private void SetMoveLeftTXT(string set)
    {
        moveLeftTXT.text = set;
    }
    public void DecreaseMovesLeft()
    {
        moveLeft--;
        SetMoveLeftTXT(moveLeft.ToString());
        if (moveLeft <= 0)
        {
            onLoseGame?.Invoke();
            SetActiveOutMovePanel(true);
            print("end game");
        }
    }
    public void SetActiveOutMovePanel(bool set)
    {
        outtaMovePanel.SetActive(set);
    }
    public void SetActiveInteract(bool set)
    {
        interactPanel.SetActive(set);
    }
    public void SetInteractSwapBtn(bool set)
    {
        swapButton.interactable = set;
    }
}
