#Fineas
Fineas is an experimental, unofficial, open source C# bot that aims to provide financial assistance for budgeting. Intended audience: Team Leads.
Fineas will answer your questions taking into consideration multiple different ways people will want to ask. Multiple functionalities are outlined below, as I explain what each main usage path looks like.

# Required Pieces to Get the Bot Running
* Set up Azure endpoint as type "App Service Plan"
* Get the code running
* Add the bot in the [BotFramework](https://dev.botframework.com/). If you're in Microsoft - make sure you use your live account, not your work account.

# Usage

## Help?
When the user in confused, they can just type "help" to see a list of commands. LUIS can also try to parse an intent if what the user says isn't recognized as a command.
![image](https://cloud.githubusercontent.com/assets/8432124/18220546/ee46cce6-7127-11e6-88f1-4aea91dcc14a.png)

## Who?
Whenever a user wants to see who they're logged in as, they can just ask "who"? Wih your own LUIS model, you can of course add more phrases to understand this intent.
![image](https://cloud.githubusercontent.com/assets/8432124/18220778/4e2460e0-712a-11e6-973a-9276281f3586.png)

## Login
We use [AuthBot](https://github.com/matvelloso/AuthBot) to verify Active Directory Credentials. You'll need to fill out the Web.config section labelled "AAD Auth v2 settings," take a look at [AuthBot](https://github.com/matvelloso/AuthBot) to see how to do this.
The below screenshot shows you what a login scenario looks like. Clicking "connect" opens up a browse window which allows the user to login using Azure Active Directory. Doing so gives them a code which you see is given to the bot for verification. Once a user is logged in, their state is saved, and all this dialog is handled by AuthBot!
![image](https://cloud.githubusercontent.com/assets/8432124/18220580/5bef8076-7128-11e6-8cc1-cb3689e11ebb.png)

## Logout
Pretty simple, once LUIS recognizes the intent to logout, AuthBot handles logout. See the below. The external link just loads a page that the user does not need to interact with, just to ensure their login doesn't stay active for any reason. After a few seconds the page will confirm logout has succeeded.
![image](https://cloud.githubusercontent.com/assets/8432124/18221025/d85ce1c2-712c-11e6-88d4-2e8fc5fa1afd.png)

## Refreshing the data
While data is automatically refreshed when the user logs in, refreshing happens in other instances as well. This data is intended to be live if the database you use supports it, so other ways to ensure you always query against new data is by allowing the user to refresh the data whenever they want.
![image](https://cloud.githubusercontent.com/assets/8432124/18220585/79792714-7128-11e6-9709-50b6fca6408b.png)

## Requesting a data refresh
We don't ever want to force a data refresh, so if they user requests data but it hasn't been refreshed in 5 minutes (can easily change this time range), we ask the user if they want to refresh it or not.
![image](https://cloud.githubusercontent.com/assets/8432124/18221079/bb5ba1ca-712d-11e6-99e3-cb26727ff801.png)

## Using a command to start the query prompt
My goal was to make a bot easy to use. In order to make all the functionality easily accessbile, I thought it would be cool to allow users to use commands. So every functionality that's accessible through LUIS communication is also accessible through simple words.
![image](https://cloud.githubusercontent.com/assets/8432124/18220595/8a7ab5be-7128-11e6-9c7f-6382424b5cdf.png)

## Finishing the query prompt we started in the above description
This is what happened next after I said I wanted to query the database. Since we only support one style of report, this is the only query I could have possibly meant when I asked to query. Buttons allow users to fill out the necessary information to query the database for what the user wants to see.
![image](https://cloud.githubusercontent.com/assets/8432124/18220895/69ad7904-712b-11e6-8178-af5ad528fa50.png)

## Using a sentence to make the whole query
Some users prefer to put all the data needed at once in sentence form. That works too, thanks to LUIS!
![image](https://cloud.githubusercontent.com/assets/8432124/18220921/b1d8c454-712b-11e6-9237-4d9a07eef186.png)

## Using a sentence then filling out the rest afterwards
In a combined effort between LUIS and Promts, we are able to gather what information the user gave when they asked their question and then use dialogs to fill out the rest.
![image](https://cloud.githubusercontent.com/assets/8432124/18220823/89e1ed78-712a-11e6-8b5d-ab04e995ddae.png)

## Refreshing data
Whenever we run a query, Fineas verifies that his version of the data has been refreshed recently. If not (the bound I set is 5 minutes), then he will ask if it's OK not to refresh.
![image](https://cloud.githubusercontent.com/assets/8432124/18220873/20a543ae-712b-11e6-811b-1f42cb4956a7.png)

## Polite questions work too!
..And of course, adding some formality to your question shouldn't hurt.
![image](https://cloud.githubusercontent.com/assets/8432124/18220865/06739f8a-712b-11e6-8691-56e5129e0445.png)

#Add him on Skype
He's not public on Skype because he's more of an internal enterprise tool. Ideally, a perfect environment would be Skype for Business.

#Notes
Please feel free to add issues here on github to represent any bugs you find or features you'd like to see! Please use the proper labels.

#FAQ
###Is this a supported application from Microsoft?
Nope! This was a project started by a group of evanglists within Microsoft looking for a cool enterprise solution for financial planning.
###What if I want to report a bug or help out with the project?
Be my guest, but keep in mind that this isn't a supported repository.
###Can I use this bot as-is?
No, you need to create a solution and project in Visual Studio but can drop these files in it. You also need to add your unique strings to the Web.config. You may also need to set up the Properties folder with AssemblyInfo.cs file. I suggest you just make a new Bot Framework project in Visual Studio and this should all be taken care of.
####Is your LUIS model perfect?
Is any human being perfect? No. Therefore, our AI's imperfections lie in his perfections. He's still learning :)

#Release notes
Currently it hasn't been released at all. Still in "Alpha"

#DB
The database holds information as described in the Finance Item class. As for the tables we query, our names are listed in the DataRetriever class. But you can use any table names you want if you modify the code, just as long as you include all columns listed in FinanceItem.

#AD Verification: [AuthBot](https://github.com/matvelloso/AuthBot)
My manager made this tool I used for AD verification called AuthBot. 
