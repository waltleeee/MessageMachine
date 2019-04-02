using UnityEngine;

public class MessageMachineControl : MonoBehaviour
{
    public MessageMachine.MessageMachineManager MessageMachineManager;

    public void OnConfirm()
    {
        MessageMachineManager.OnMessageConfirm();
    }

    public void OnCancel()
    {
        MessageMachineManager.OnMessageCancel();
    }
}
