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
Leveraging the Dialog Accelerator is not an exact science, but rather it's more of
what you might call a "*muddle*" technique that's a mix of prescribed and improvised
steps.  In the end though, this process is a lot faster than having to start from
first principles.

Incorporating this template into your solution involves two phases:
* Phase 1 - Get template sample code running (this becomes your working roadmap)
* Phase 2 - Repurpose template sample code for your particular scenario
To make incorporating the template easier, Visual Studio **ToDo** Tasks have been 
added to the code to provide step-by-step instructions for accomplishing phase 1.

### Phase 1 - Get Template Code Running

1. In Visual Studio, open your Skills bot project, right-click the **Dialogs** folder
and choose **Add | Class...** and name the class after the LUIS Intent you're targeting 
with this dialog and then choose **Add**

2. Copy the entire source code of GetLibraryCard.cs found [here](GetLibraryCard.cs) into
your paste buffer by clicking the **Raw** button and then Control-A and then Control-C 
> <img src="/Images/RawButton.png" width="200">
3. Switch back Visual Studio and delete the entire contents of the file you created
in Step 1 and replace it with the current contents of your paste buffer (Control-V)

4. To expedite "*muddling*", Visual Studio **ToDo** tasks have been added to the code
which you can see in list form if you choose **View | Task List** (or Control-W,T).
Double-clicking a task will take you right were you need to be to carry out the task.

    When you've finished all the tasks the code should compile without errors and 
you'll be ready to integrate it into the `MainDialog`.
5. Dependency-inject the dialog into `MainDialog` by adding 
`GetLibraryCardDialog getLibraryCard,` to the constructor of the `MainDialog` class
in **MainDialog.cs**
> <img src="/Images/DialogDI.png" width="400">
6. While still in **MainDialog.cs**, navigate to the `RouteAsync()` method and add
the following code to the `switch(intent)` statement:

```c#
	case **YourSkillNameHere**.Intent.**YourIntentNameHere**:
	{
		turnResult = await dc.BeginDialogAsync(nameof(GetLibraryCardDialog));

		break;
	}
```

> <img src="/Images/BeginDialog.png" width="800">
If you don't have a LUIS Intent that corresponds to this new dialog yet, you
can go with "*Plan B*" and make the `case YourSkillName.Intent.Sample:`
statement look like the following:
> <img src="/Images/PlanBBeginDialog.png" width="600">

7. Finally, register the dialog for dependency injection by opening Startup.cs and
choosing **Edit | Find and Replace | Quick Find** (or **Ctrl-F**) and typing **Register
dialogs** in the search field and hit Return.  Now add 
`services.AddTransient<GetLibraryCardDialog>();` and it should look like the following:
> <img src="/Images/Startup.png" width="350">

8. Now your ready to test out Phase 1.  Set your Skill to be the StartUp Project and 
start the the degugger.  Now open your bot in the Bot Emulator and invoke your LUIS
Intent.  If you didn't have a LUIS Intent and instead used Plan "B" then type **sample 
dialog** to kick it off.

    Study the code to learn how to test out all the varios scenarios the template 
	supports.  For example, try and give it a non-integer for age to see built-in
	validation or an email without the '@' to see custom validation.  Branching is
	invoked if you type "already@member" or "county@employee" (which launches the
	SampleDialog).

### Phase 2 - *Muddle* through to Repurpose Template for Your Particular Scenario

Now that you've got the template running and you've explored all the scenarios, 
your ready accelerate the development of your dialog by repurposing the various
steps to suit your conversational flow.  But before you do that there are a few
things you should do to customize the new dialog to fit your scenario
* Modify the conversation state model class so it has the proper conversation 
state properties
* (Add other customizations)

The last task of phase 2 is to repurpose the steps to support the conversation
flow you have modeled out for this dialog.  Once you've done that it should be
mission complete and you're ready to repeat this same process for the rest of
the dialogs for your solution.