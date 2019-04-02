# MessageMachine

ATTENTION:

if you want UI object which should be click block when showing message,you should 
put your UI object like MessageMachineSamplePanel in project



INTRODUCTION:

Script:

1.MessageMechineManager:
    manage all messageData and do callback if messageData's callback isn't null

2.MessageMechineDisplay:
    do message view work all

3.MessageMechineControl:
    when confirm or cancel button click,report to MessageMachineManager

4.MessageMachineAnimationControl
    when offMessage animation play finish,report to MessageMechineDisplay    


Animation:

1.idle
    MessageObject initial state

2.openMessage
    play when show message

3.offMessage
    play when off message        


Prefab:

1.MessageMachinePanel
    main message prefab 
