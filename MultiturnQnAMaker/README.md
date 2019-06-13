# Multiturn QnA Maker
This accelerator shows how to add the ability to show the new follow-on prompts you can now add to
QnA Maker knowledgebases.

You can find an overview of this new multiturn QnA capability
[here](https://docs.microsoft.com/en-us/azure/cognitive-services/qnamaker/how-to/multiturn-conversation) 
along with how to add follow-on prompts in the portal (see **Add new QnA pair as follow-up prompt** section
for quick way to manually add 1 or 2 to get started)

## To Use
1. Create, build and deploy a Virtual Assistant bot as described [here](https://github.com/microsoft/botframework-solutions/blob/master/docs/tutorials/csharp/virtualassistant.md)
2. Copy the **Helpers** folder to root project folder of your assistant (i.e. where the .csproj file is)
3. Copy the files from the **Models** folder into the **Models** folder of your assistant
4. Copy **Services\MultiturnQnAMaker.cs** to the **Services** folder of your assistant
5. Edit your assistant's **Dialogs\MainDialog.cs** as outlined in 
[Dialogs\Changes to MainDialog.cs.pdf](Changes to MainDialog.cs.pdf)
6. Edit your assistant's **Services\BotServices.cs** as outlined in 
[Services\Changes to BotServices.cs.pdf](MultiturnQnAMaker\Services\Changes to BotServices.cs.pdf)
7. Edit your assistant's **Startup.cs** as outlined in 
[Changes to Startup.cs.pdf](MultiturnQnAMaker\Changes to Startup.cs.pdf)
8. Browse to [https://www.qnamaker.ai/](https://www.qnamaker.ai/) and find the **faq** for the assistant
you deployed in step 1 and add a few QnAs with follow-on prompts as descrived [here](https://docs.microsoft.com/en-us/azure/cognitive-services/qnamaker/how-to/multiturn-conversation) 
(see **Add new QnA pair as follow-up prompt** section specifically)
9. Sync the new QnA changes with your assistant as described [here](https://github.com/microsoft/botframework-solutions/blob/master/docs/tutorials/csharp/customizeassistant.md#update-your-local-lu-files-for-luis-and-qnamaker)
10. Run the bot and test it with the bot emulator by typing in the multiturn QnA questions you added