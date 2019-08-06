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
Leveraging the Dialog Accelerator is not an exact science, but rather, it's more of
what you might call a "*muddle*" technique that's a mix of prescribed and improvised
steps.  

Incorporating this template into your solution involves two phases:
* Get template sample code running (this becomes your working roadmap)
* Repurpose template sample code for your particular scenario

Visual Studio **ToDo** Tasks have been added to the code to provide step-by-step
instructions for incorporating the template into your soluton and getting it running.In the end though, this process is a lot faster than having to start from
first principals.  Here are the steps:

1. In Visual Studio, open your Skills bot project, right-click the **Dialogs** folder
and choose **Add | Class...** and name the class after the Intent you're targeting with
this dialog and then choose **Add**
2. Copy the entire source code of GetLibraryCard.cs found [here](GetLibraryCard.cs) into
the paste buffer by clicking the **Raw** button and then Control-A and then Control-C 
> <img src="/Images/RawButton.png" width="200">
3. Switch back Visual Studio, and delete the entire contents of the file you created
in Step 1 and replace it with the current contents of your paste buffer (Control-V)
4. To expedite "*muddling*", Visual Studio ToDo tasks have been added to the code