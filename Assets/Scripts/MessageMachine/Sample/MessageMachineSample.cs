
using UnityEngine;
using UnityEngine.UI;

//using MessageMachine
using MessageMachine;

public class MessageMachineSample : MonoBehaviour
{

    public MessageMachineManager MessageMachineManager;
    public Text callbackButtonText;

    private bool isCallback = true;
    private delegate void functionType();


    public void SendSingleType0()
    {
        MessageData data = makeMessageData(0);
        MessageMachineManager.ReceiveMessageData(data);
    }

    public void SendSingleType1()
    {
        MessageData data = makeMessageData(1);
        MessageMachineManager.ReceiveMessageData(data);
    }

    public void SendSingleType2()
    {
        MessageData data = makeMessageData(2);
        MessageMachineManager.ReceiveMessageData(data);
    }

    public void Send3Type0()
    {
        for (int i = 0; i < 3; i++)
        {
            MessageData data = makeMessageData(0);
            MessageMachineManager.ReceiveMessageData(data);
        }
    }

    public void Send3Type1()
    {
        for (int i = 0; i < 3; i++)
        {
            MessageData data = makeMessageData(1);
            MessageMachineManager.ReceiveMessageData(data);
        }
    }

    public void Send3Type2()
    {
        for (int i = 0; i < 3; i++)
        {
            MessageData data = makeMessageData(2);
            MessageMachineManager.ReceiveMessageData(data);
        }
    }

    public void ChangeCallbackMode()
    {
        isCallback = !isCallback;
        if (isCallback)
            callbackButtonText.text = "Console Callback:ON";
        else
            callbackButtonText.text = "Console Callback:OFF";

    }

    private MessageData makeMessageData(int inType)
    {
        MessageData data = new MessageData();
        data.MessageType = inType;
        data.Content = "SAMPLE OK";

        if (isCallback)
        {
            functionType func = delegate ()
              {
                  Debug.Log("CALLBACK OK");
              };

            data.ConfirmCallback = func;
        }

        return data;
     }
}
