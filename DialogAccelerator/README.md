# Dialog Accelerator
This accelerator shows how to use the Dialog Accelerator template to create 
moderate to complex conversational bot dialogs.  This template lays out a
working roadmap of how to code the most common interaction (conversation) 
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
Leveraging the Dialog Accelerator is not an exact science but rather more of what
you might call a "*muddle*" technique that's a mix of prescribed and improvised
steps.  In the end though, this process is a lot faster than having to start from
first principals.  Here are the steps:

1. In Visual Studio, open your Skills bot project, right-click the **Dialogs** folder
and choose **Add | Class...** and name the class after the Intent you're targeting with
this dialog and then choose **Add**
2. Copy the entire GetLibraryCard.cs found [here](GetLibraryCard.cs) into the paste buffer
by clicking the **Raw** button and then typing Control-A and then Control-C.
<img src="/Images/RawButton.png" width="200">
3. In Visual Studio, open the new class you created 