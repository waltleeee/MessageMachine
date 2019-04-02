using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MessageMachineDisplay : MonoBehaviour
{
    //if want to change counter time,change this
    private const int counterTime = 2;


    public GameObject MessagePanel;
    public GameObject MessageObject;
    public Text MessageText;
    public Button ConfirmButton;
    public Button CancelButton;
    public MessageMachine.MessageMachineManager MessageMachineManager;


    private Vector3 cancelButtonPosition;
    private Vector3 confirmButtonPosition;
    private Vector3 centerButtonPosition;
    private Vector3 openPosition = new Vector3(0, 0,0);
    private Vector3 standbyPosition = new Vector3(8000,-8000,0);
    private Vector3 messageTextUpPosition = new Vector3(0, 30, 0);
    private Vector3 messageTextCenterPosition = new Vector3(0,0,0);
    public Animator animator;

    private void Start()
    {
        cancelButtonPosition = CancelButton.transform.localPosition;
        confirmButtonPosition = ConfirmButton.transform.localPosition;
        centerButtonPosition = new Vector2(0, confirmButtonPosition.y);
    }

    #region Public

    public void ShowCounterMessage(string inText)
    {
        MessageText.text = inText;
        ConfirmButton.transform.localPosition = standbyPosition;
        CancelButton.transform.localPosition = standbyPosition;
        MessageText.transform.localPosition = messageTextCenterPosition;
        showMessageAnimation();
        StartCoroutine(counterFinish());
    }

    public void ShowConfirmMessage(string inText)
    {
        MessageText.text = inText;
        ConfirmButton.transform.localPosition = centerButtonPosition;
        CancelButton.transform.localPosition = standbyPosition;
        MessageText.transform.localPosition = messageTextUpPosition;
        showMessageAnimation();
    }

    public void ShowConfirmCancelMessage(string inText)
    {
        MessageText.text = inText;
        ConfirmButton.transform.localPosition = confirmButtonPosition;
        CancelButton.transform.localPosition = cancelButtonPosition;
        MessageText.transform.localPosition = messageTextUpPosition;
        showMessageAnimation();
    }

    public void OffMessage()
    {
        offMessageAnimation();
    }

    public void OffMessageFinish()
    {
        MessagePanel.transform.localPosition = standbyPosition;
        MessageMachineManager.OffMessageFinish();
    }
    #endregion

    #region Private

    private void showMessageAnimation()
    {
        MessagePanel.transform.localPosition = openPosition;
        
        animator.Play("openMessage");
    }

    private void offMessageAnimation()
    {
       animator.Play("offMessage");
    }

    private IEnumerator counterFinish()
    {
        yield return new WaitForSeconds(counterTime);
        MessageMachineManager.MessageCounterTimeUp();
    }
    #endregion



}
