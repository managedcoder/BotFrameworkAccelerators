# Secured Web Chat Control
This accelerator shows how to securely embed a Web Chat control on a web page.  Use it to replace the default.htm
in your Virtual Assistant to change the default landing page into a test page that others can use to explore or test 
your bot.

The implementation of this page follows the recommended approach for embedding the Microsoft Bot Framework's Web Chat
control is to use an access token instead of the Bot Secret.  For more information, see the Direct Line documentation
[here](https://docs.microsoft.com/en-us/azure/bot-service/rest-api/bot-framework-rest-direct-line-3-0-authentication?view=azure-bot-service-4.0)

## To Use
1. Copy TokenController.cs to your Controllers folder in your Virtual Assistant and follow the instructions at the top
of the file
2. Copy default.htm to the wwwroot folder and replace the existing file of the same name