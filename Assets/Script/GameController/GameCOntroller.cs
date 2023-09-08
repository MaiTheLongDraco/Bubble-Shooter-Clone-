using UnityEngine;
using UnityEngine.UI;
using com.soha.bridge;

public class GameCOntroller : MonoBehaviourSingleton<GameCOntroller>
{
    [SerializeField] private Text moveLeftTXT;
    [SerializeField] private GameObject outtaMovePanel;
    [SerializeField] private int moveLeft;
    [SerializeField] private bool canPlay;


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
        canPlay = true;
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
            canPlay = false;
            SetActiveOutMovePanel(true);
            print("end game");
        }
    }
    public void SetActiveOutMovePanel(bool set)
    {
        outtaMovePanel.SetActive(set);
    }
}
