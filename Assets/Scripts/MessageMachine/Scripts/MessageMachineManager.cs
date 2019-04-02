using System;
using System.Collections.Generic;
using UnityEngine;

namespace MessageMachine
{
    public class MessageData
    {
        ////0:no confirm button and counter time to close   1:have confirm button and no counter  2:have confirm and cancel button,no counter
        private int messageType = 0;
        public int MessageType 
        {
            get
            {
                return messageType; 
            }
            set 
            {
                if (value < 0 || value > 2)
                    messageType = 0;
                else
                    messageType = value;
             }
        }
        public string Content = "";
        public Delegate ConfirmCallback=null; 
    }

    public class MessageMachineManager : MonoBehaviour
    {
        public MessageMachineDisplay MessageMachineDisplay;

        private bool isConfirmWaiting = false;
        private bool isShowLog=true;
        private bool isConfirmFinish = false;
        private List<MessageData> messageDataList0 = new List<MessageData>();
        private List<MessageData> messageDataList1 = new List<MessageData>();
        private List<MessageData> mainMessageDataList=null;
        private List<MessageData> subMessageDataList = null;

        private void Start()
        {
            mainMessageDataList = messageDataList0;
            subMessageDataList = messageDataList1;
        }

        #region Public

        public void OnMessageConfirm()
        {
            isConfirmFinish = true;
            MessageMachineDisplay.OffMessage();

        }

        public void OnMessageCancel()
        {
            isConfirmFinish = false;
            MessageMachineDisplay.OffMessage();

        }

        public void MessageCounterTimeUp()
        {
            isConfirmFinish = true;
            MessageMachineDisplay.OffMessage();
           
        }

        public void OffMessageFinish()
        {
            messageFinshWork(isConfirmFinish);
        }

        public void ReceiveMessageData(MessageData inData)
        {
            if (inData == null)
                return;

            if (inData.MessageType < 0 || inData.MessageType > 3)
                return;

            if (inData.Content == "")
                return;

            if (!isConfirmWaiting)
            {
                isConfirmWaiting = true;
                mainMessageDataList.Add(inData);
                messageDataWork(inData);
            }
            else
                subMessageDataList.Add(inData);
        }
        #endregion

        #region Private

        private void messageDataWork(MessageData inData)
        {
            switch (inData.MessageType)
            {
                case 0:
                    MessageMachineDisplay.ShowCounterMessage(inData.Content);
                    break;
                case 1:
                    MessageMachineDisplay.ShowConfirmMessage(inData.Content);
                    break;
                case 2:
                    MessageMachineDisplay.ShowConfirmCancelMessage(inData.Content);
                    break;
                default:
                    break;
            }
        }

        private void messageFinshWork(bool isConfirm)
        {
            if (mainMessageDataList[0].ConfirmCallback != null)
            {
                if(isConfirm)
                    mainMessageDataList[0].ConfirmCallback.DynamicInvoke();
            }

            mainMessageDataList.RemoveAt(0);

            if (mainMessageDataList.Count != 0)
                messageDataWork(mainMessageDataList[0]);
            else
            {
                if (subMessageDataList.Count != 0)
                {
                    mainSubChange();
                    messageDataWork(mainMessageDataList[0]);
                }
                else
                    isConfirmWaiting = false;
            }
        }

        private void mainSubChange()
        {
            List<MessageData> list = mainMessageDataList;
            mainMessageDataList = subMessageDataList;
            subMessageDataList = list;
        }

        private void showLog(string inContent)
        {
            if(isShowLog)
                Debug.Log(inContent+ " MessageMachine");
        }
        private void showLog<T>(T inContent)
        {
            if (isShowLog)
                Debug.Log(inContent+" MessageMachine");
        }
        #endregion
    }
}
