// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using System.Threading;
using System.Threading.Tasks;

// ToDo Phase 1 - Step 01: Replace "LibraryBotSkill" in the following lines with the name of your skill
using LibraryBotSkill.Services;
namespace LibraryBotSkill.Dialogs
{
    // ToDo Phase 1 - Step 02: Right-click GetLibraryCardDialog and choose "Rename..." and 
    // give your dialog it's proper name and then change the name of this file
    // to match the class name you chose by doing a rename in VS Solution Explorer 
    public class GetLibraryCardDialog : ComponentDialog
    {
        // To localize a multilingual bot, use the approach outlined in the main VA Assistant's OnboardingDialog
        //private static OnboardingResponses _responder = new OnboardingResponses();

        // ToDo Phase 1 - Step 03: Right-click MembershipState and choose "Rename..." and 
        // give your conversation state model class it's proper name 
        private IStatePropertyAccessor<MembershipState> _accessor;
        private MembershipState _state;

        public GetLibraryCardDialog(
            BotServices botServices,
            ConversationState conversationState,
            IBotTelemetryClient telemetryClient)
            : base(nameof(GetLibraryCardDialog))
        {
            // Get the conversation state accessor so waterfall steps can use it
            _accessor = conversationState.CreateProperty<MembershipState>(nameof(MembershipState));
            InitialDialogId = nameof(GetLibraryCardDialog);

            // The steps of this WaterfallDialog show:
            //    - Simple prompts (TextPrompt, NumberPrompt<int>)
            //          - Browse here to see how to code other prompt types: https://docs.microsoft.com/en-us/azure/bot-service/bot-builder-compositcontrol?view=azure-bot-service-4.0&tabs=csharp
            //    - Built-in validation (NumberPrompt<int> only allow integers)
            //    - Custom validation (email and verification code)
            //    - How to skip asking users questions that already have answers (sc.NextAsync(<known answer>)
            //    - "Branch" conversation by calling nested dialog. Good for dialog reuse or modularizing a conversation (BeginDialogAsync in CheckIfCountyEmployee())
            //    - How to localize the prompts of a multilingual bot (commented out currently in AskForName())
            // For more advanced conversation flow:
            //    - Looping: https://docs.microsoft.com/en-us/azure/bot-service/bot-builder-dialog-manage-complex-conversation-flow?view=azure-bot-service-4.0&tabs=csharp
            //    - Handling interruptions: https://docs.microsoft.com/en-us/azure/bot-service/bot-builder-howto-handle-user-interrupt?view=azure-bot-service-4.0&tabs=csharp
            //    - Overview of dialogs: https://docs.microsoft.com/en-us/azure/bot-service/bot-builder-concept-dialog?view=azure-bot-service-4.0
            // ToDo Phase 1 - Step 04: Right-click getLibraryCard member and choose "Rename..." and 
            // give this WaterfallStep variable it's proper name. Typically, this will
            // match the name of the intent
            var getLibraryCard = new WaterfallStep[]
            {
                AskForName,
                AskForAge,
                AskForEmail,
                CheckIfAlreadyMember,
                CheckIfCountyEmployee,
                GetVerificationCode,
                FinishGetLibraryCardDialog,
            };

            // To capture built-in waterfall dialog telemetry, set the telemetry client
            // of the component dialog and new waterfall dialog (i.e. the next 2 lines of code)
            TelemetryClient = telemetryClient;
            AddDialog(new WaterfallDialog(InitialDialogId, getLibraryCard) { TelemetryClient = telemetryClient });
            AddDialog(new TextPrompt(DialogIds.NamePrompt));
            AddDialog(new NumberPrompt<int>(DialogIds.AgePrompt));
            AddDialog(new TextPrompt(DialogIds.EmailPrompt, ValidateEmailAddress));
            AddDialog(new TextPrompt(DialogIds.VerificationPrompt, ValidateVerificationCode));
        }

        /// <summary>
        /// This step shows how a simple text prompts works
        /// </summary>
        /// <remarks>
        /// To see sample code for other prompt types: https://docs.microsoft.com/en-us/azure/bot-service/bot-builder-compositcontrol?view=azure-bot-service-4.0&tabs=csharp
        /// </remarks>
        public async Task<DialogTurnResult> AskForName(WaterfallStepContext sc, CancellationToken cancellationToken)
        {
            // Get converation state so we can check to see if name has already been provided
            _state = await _accessor.GetAsync(sc.Context, () => new MembershipState());

            await sc.Context.SendActivityAsync("I can help you get a library card!");

            // If first name has already been provided
            if (!string.IsNullOrEmpty(_state.Name))
            {
                // Skip this step and pass first name to next step
                return await sc.NextAsync(_state.Name);
            }
            else
            {
                return await sc.PromptAsync(DialogIds.NamePrompt, new PromptOptions
                {
                    Prompt = MessageFactory.Text("What's your name?")
                });

                // To localize a multilingual bot, use the approach below instead of the one above
                //return await sc.PromptAsync(DialogIds.NamePrompt, new PromptOptions()
                //{
                //    Prompt = await _responder.RenderTemplate(sc.Context, sc.Context.Activity.Locale, OnboardingResponses.ResponseIds.NamePrompt),
                //});
            }
        }

        /// <summary>
        /// This step shows how to code a numeric prompt which shows an example of 
        /// the framework's built-in validation
        /// </summary>
        /// <remarks>
        /// The built-in validation for a NumberPrompt<int> will only accept an integer
        /// so you can enter any non-integer value to see the built-in validation in action
        /// </remarks>
        public async Task<DialogTurnResult> AskForAge(WaterfallStepContext sc, CancellationToken cancellationToken)
        {
            _state = await _accessor.GetAsync(sc.Context, () => new MembershipState());
            // Save the answer from the previous step in conversation state
            _state.Name = (string)sc.Result;
            await _accessor.SetAsync(sc.Context, _state, cancellationToken);

            // If age has already been provided
            if (_state.Age != 0)
            {
                // Skip this step and pass age to next step
                return await sc.NextAsync(_state.Age);
            }
            else
            {
                return await sc.PromptAsync(DialogIds.AgePrompt, new PromptOptions
                {
                    Prompt = MessageFactory.Text("How old are you?"),
                    // Override the built-in integer retry prompt with a scenario-friendly one
                    RetryPrompt = MessageFactory.Text("Please enter age as an integer")
                });
            }
        }

        /// <summary>
        /// This step shows how to provide a custom retry prompt for custom validation
        /// </summary>
        /// <remarks>
        /// Other than providing the custom retry prompt, this step is just another
        /// example of a simple prompt
        /// </remarks>
        public async Task<DialogTurnResult> AskForEmail(WaterfallStepContext sc, CancellationToken cancellationToken)
        {
            _state = await _accessor.GetAsync(sc.Context, () => new MembershipState());
            // Save the answer from the previous step in conversation state
            _state.Age = (int)sc.Result;
            await _accessor.SetAsync(sc.Context, _state, cancellationToken);

            if (!string.IsNullOrEmpty(_state.Email))
            {
                // Skip this step and pass email to next step
                return await sc.NextAsync(_state.Email);
            }
            else
            {
                return await sc.PromptAsync(DialogIds.EmailPrompt, new PromptOptions
                {
                    Prompt = MessageFactory.Text("What is your email?"),
                    // Provide retry prompt for custom email validation on a TextPrompt
                    RetryPrompt = MessageFactory.Text("That's not a valid email address, please try again")
                });
            }
        }

        /// <summary>
        /// Custom email validator logic that was assigned in the call to AddDialog() 
        /// in the constructor
        /// </summary>
        /// <remarks>
        /// To get the proper signature for a prompt's validation delegate, right-click
        /// the prompt in the AddDialog() call (TextPrompt in this case) and then select
        /// "Go To Definition" option and then right-click PromptValidator<string> and 
        /// choose "Go To Definition" and copy the delegate signature and paste it in 
        /// here dropping the "T" in method name and changing the "T" in PromptValidatorContext
        /// to "string"
        /// </remarks>
        async private Task<bool> ValidateEmailAddress(PromptValidatorContext<string> promptContext, CancellationToken cancellationToken)
        {
            // Get the user's response
            string email = promptContext.Recognized.Value;

            // Simple validation rule says email is only valid if it contains an '@'
            return email.Contains('@');
        }

        /// <summary>
        /// Silent branching step to decide what direction the conversation should go 
        /// </summary>
        /// <remarks>
        /// This step shows another example of how to manage conversation flow. 
        /// It's also interesting in that its a step without an associatede dialog
        /// prompt so its sole job is deciding how to proceed given the current 
        /// context/state of the conversation (i.e. end dialog or continue to 
        /// next step)
        /// 
        /// To test the "end dialog" branch, enter "already@member" when asked for
        /// email.
        /// </remarks>
        public async Task<DialogTurnResult> CheckIfAlreadyMember(WaterfallStepContext sc, CancellationToken cancellationToken)
        {
            _state = await _accessor.GetAsync(sc.Context, () => new MembershipState());
            // Save the answer from the previous step in conversation state
            _state.Email = (string)sc.Result;
            await _accessor.SetAsync(sc.Context, _state, cancellationToken);

            if (_state.Email == "already@member")
            {
                await sc.Context.SendActivityAsync($"Looks like you already have a library card.  We'll send your membership information to {_state.Email}");

                return await sc.EndDialogAsync();
            }
            else
            {
                // We can safely move to next next step
                return await sc.NextAsync();
            }
        }

        /// <summary>
        /// This step shows how to "Branch" a conversation by calling a another dialog
        /// </summary>
        /// <remarks>
        /// To exercise this branched conversation, enter "county@employee" as the
        /// email address.  This will begin a separate dialog (SampleDialog in this
        /// case) and once that dialog finishes the conversation will pick back up 
        /// with the next step in this dialog (which is GetVerificationCode())
        /// </remarks>
        public async Task<DialogTurnResult> CheckIfCountyEmployee(WaterfallStepContext sc, CancellationToken cancellationToken)
        {
            if (_state.Email == "county@employee")
            {
                await sc.Context.SendActivityAsync($"I see you are county employee.  I'll need to ask you a few additional questions.");

                // To make it easier to reuse this sample code, we'll use SampleDialog which
                // comes with the Virtual Assistant template code to show how to branch a 
                // conversation and call a nested dialog
                return await sc.BeginDialogAsync(nameof(SampleDialog), null, cancellationToken);
            }

            return await sc.NextAsync();
        }

        /// <summary>
        /// This step shows how bot can creatively tie into existing business process (email in this case)
        /// </summary>
        /// <remarks>
        /// This step shows how a bot might integrate with an existing business process
        /// that is email based to verify a code that is sent to the bot user.  It's 
        /// another example of leveraging custom validation.
        /// </remarks>
        public async Task<DialogTurnResult> GetVerificationCode(WaterfallStepContext sc, CancellationToken cancellationToken)
        {
            // Get conversation state so we can have access to email that was provided previously
            _state = await _accessor.GetAsync(sc.Context, () => new MembershipState());

            return await sc.PromptAsync(DialogIds.VerificationPrompt, new PromptOptions
            {
                Prompt = MessageFactory.Text($"We've sent a verification email to {_state.Email}.  Please enter the verification code"),
                // Provide retry prompt for custom verification code validation for TextPrompt
                RetryPrompt = MessageFactory.Text("Sorry, that is an invalid verification code. Please check code and try again.")
            });
        }

        /// <summary>
        /// Custom verification code validator logic that was assigned in the call to AddDialog() in the constructor
        /// </summary>
        /// <remarks>
        /// To get the proper signature for a prompt's validation delegate, right-click
        /// the prompt in the AddDialog() call (TextPrompt in this case) and then select
        /// "Go To Definition" option and then right-click PromptValidator<string> and choose
        /// "Go To Definition" and copy the delegate signature and paste it in here dropping
        /// the "T" in method name and changing the "T" in PromptValidatorContext to "string"
        /// </remarks>
        async private Task<bool> ValidateVerificationCode(PromptValidatorContext<string> promptContext, CancellationToken cancellationToken)
        {
            string verificationCode = promptContext.Recognized.Value;

            return verificationCode == "verified";
        }

        /// <summary>
        /// The last step sums up the conversation
        /// </summary>
        /// <remarks>
        /// The last step in a dialog generally sums up the conversation and
        /// performs an action that achieves the goal of the conversation which
        /// in this example would be to call a back end service to create a
        /// library membership for the user and send them a confirmation email.
        /// </remarks>
        public async Task<DialogTurnResult> FinishGetLibraryCardDialog(WaterfallStepContext sc, CancellationToken cancellationToken)
        {
            _state = await _accessor.GetAsync(sc.Context, () => new MembershipState());
            // Save the answer from the previous step in conversation state
            string verificationCode = (string)sc.Result;

            // Provide summary of conversation for this dialog
            await sc.Context.SendActivityAsync($"We're all set {_state.Name}!  A confirmation will all your membership details will be emailed to {_state.Email}");

            // ToDo Phase 2 - Step 01: Perform goal of conversation here (call membership service in this case)
            // libraryService.CreateMembership(_state);

            return await sc.EndDialogAsync();
        }

        // ToDo Phase 2 - Step 02: Customize DialogIds for your dialog's requirements
        private class DialogIds
        {
            public const string NamePrompt = "FirstNamePrompt";
            public const string LastNamePrompt = "LastNamePrompt";
            public const string AgePrompt = "AgePrompt";
            public const string EmailPrompt = "EmailPrompt";
            public const string VerificationPrompt = "VerificationPrompt";
        }
    }

    // ToDo Phase 2 - Step 03: Move this class to it's own file in a "Models" solution folder
    // ToDo Phase 2 - Step 04: Customize conversation state class for your dialog's requirements
    public class MembershipState
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
    }
}
