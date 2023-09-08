using UnityEngine;
using UnityEngine.UI;
using com.soha.bridge;
using UnityEngine.Events;
using System.Collections;

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
        if (moveLeft <= 0)
        {
            onLoseGame?.Invoke();
            SetActiveOutMovePanel(true);
            StartCoroutine(DelaySetInteractSwapBtn());
            SetMoveLeftTXT(moveLeft.ToString());
            print("end game");
            return;
        }
        else
        {
            moveLeft--;
            SetMoveLeftTXT(moveLeft.ToString());
        }
    }
    public void SetActiveOutMovePanel(bool set)
    {
        outtaMovePanel.SetActive(set);
    }
    private IEnumerator DelaySetInteractSwapBtn()
    {
        yield return new WaitForSeconds(.2f);
        SetInteractSwapBtn(false);
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
