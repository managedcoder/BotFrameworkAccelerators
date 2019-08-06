# Dialog Accelerator
This accelerator shows how to use the Dialog Accelerator template to create 
moderate to complex conversational bot dialogs.  This template lays out a
working roadmap of how to code the most common intereaction (conversation) 
scenarios:
* Simple prompts
* Built-in validation
* Custom validation
* How to skip asking users questions that already have answers
* How branch conversations and call independent dialog (Good for dialog reuse or 
modularizing a conversation)
* How to localize prompts for multilingual bots

For more advanced conversational flows, browse to:
* [Looping](https://docs.microsoft.com/en-us/azure/bot-service/bot-builder-dialog-manage-complex-conversation-flow?view=azure-bot-service-4.0&tabs=csharp)
* [Handling interruptions](https://docs.microsoft.com/en-us/azure/bot-service/bot-builder-howto-handle-user-interrupt?view=azure-bot-service-4.0&tabs=csharp)
* [Overview of dialogs](https://docs.microsoft.com/en-us/azure/bot-service/bot-builder-concept-dialog?view=azure-bot-service-4.0)

## To Use
1. In your Skills bot, right-click the *Dialogs* folder and choose *Add | Class...* and
name the dialog after the Intent your targeting with this dialog and then choose *Add*
2. Copy the entire GetLibraryCard.cs found [here](GetLibraryCard.cs) by clicking the 
*Raw* button and then typing Control-A and then Control-C. ![Alt text](/Images/RawButton.png)