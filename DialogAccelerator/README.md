# Dialog Accelerator
This accelerator shows how to repurpose a dialog template that can be used to create 
moderate and complex conversational bot dialogs.  This template is a working roadmap
of how to code the most common intereaction (conversation) scenarios:

* Simple prompts
* Built-in validation
* Custom validation
* How to skip asking users questions that already have answers
* How branch conversations and call independent dialog (Good for dialog reuse or 
modularizing a conversation)
* How to localize prompts for multilingual bots

For more advanced conversation, browse to anyone of the following:
* [Looping](https://docs.microsoft.com/en-us/azure/bot-service/bot-builder-dialog-manage-complex-conversation-flow?view=azure-bot-service-4.0&tabs=csharp)
* [Handling interruptions](https://docs.microsoft.com/en-us/azure/bot-service/bot-builder-howto-handle-user-interrupt?view=azure-bot-service-4.0&tabs=csharp)
* [Overview of dialogs](https://docs.microsoft.com/en-us/azure/bot-service/bot-builder-concept-dialog?view=azure-bot-service-4.0)

## To Use
1. Copy wwwroot\default.htm to the wwwroot folder and replace the existing default.htm file
2. Copy Controllers\TokenController.cs to your Controllers folder in your Virtual Assistant and follow the instructions at the top
of that file