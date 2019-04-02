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

        //if want to show log,set true
        private bool isShowLog = true;

        private bool isConfirmWaiting = false;
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

        //when confirm button press
        public void OnMessageConfirm()
        {
            showLog("OnMessageConfirm");
            isConfirmFinish = true;
            MessageMachineDisplay.OffMessage();

        }

        //when cancel button press
        public void OnMessageCancel()
        {
            showLog("OnMessageCancel");
            isConfirmFinish = false;
            MessageMachineDisplay.OffMessage();

        }

        //when counter type message timeup
        public void MessageCounterTimeUp()
        {
            showLog("MessageCounterTimeUp");
            isConfirmFinish = true;
            MessageMachineDisplay.OffMessage();
           
        }

        //when off message process all finish
        public void OffMessageFinish()
        {
            showLog("OffMessageFinish");
            messageFinshWork(isConfirmFinish);
        }

        //receive messageData whitch want to show
        public void ReceiveMessageData(MessageData inData)
        {
            showLog("ReceiveMessageData");
            if (inData == null)
            {
                showLog("MessageData is null");
                return;
            }

            if (inData.MessageType < 0 || inData.MessageType > 3)
            {
                showLog("MessageData Type Error");
                return;
            }

            if (inData.Content == "")
            {
                showLog("MessageData no content");
                return;
            }

            if (!isConfirmWaiting)
            {
                showLog("MessageData check ok,do messageDataWrok");
                isConfirmWaiting = true;
                mainMessageDataList.Add(inData);
                messageDataWork(inData);
            }
            else
            {
                showLog("ReceiveMessageData but other message is working,add to sub list");
                subMessageDataList.Add(inData);
            }
        }
        #endregion

        #region Private

        //check message type and do work
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

        //when message work finish,check callback and do next message if have other messageData
        private void messageFinshWork(bool isConfirm)
        {
            showLog("messageFinshWork");
            if (mainMessageDataList[0].ConfirmCallback != null)
            {
                if (isConfirm)
                {
                    showLog("Do confirm callback");
                    mainMessageDataList[0].ConfirmCallback.DynamicInvoke();
                }
            }

            mainMessageDataList.RemoveAt(0);

            if (mainMessageDataList.Count != 0)
                messageDataWork(mainMessageDataList[0]);
            else
            {
                if (subMessageDataList.Count != 0)
                {
                    showLog("Do next messageData work");
                    mainSubChange();
                    messageDataWork(mainMessageDataList[0]);
                }
                else
                {
                    showLog("MessageData list is empty");
                    isConfirmWaiting = false;
                }
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
                Debug.Log(inContent+ "  MessageMachine");
        }
        private void showLog<T>(T inContent)
        {
            if (isShowLog)
                Debug.Log(inContent+"  MessageMachine");
        }
        #endregion
    }
}
