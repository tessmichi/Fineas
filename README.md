#Fineas
Fineas is an experimental, unofficial, open source C# bot that aims to provide financial assistance for budgeting. Intended audience: Team Leads.

##Add on Skype
https://join.skype.com/bot/41b08395-a988-46ee-9ff2-f93382bc0086

##Add on Telegram
@Fineas_bot

##Notes
Please feel free to add issues here on github to represent any bugs you find or features you'd like to see! Please use the proper labels.

##FAQ
###Is this a supported application from Microsoft?
Nope! This was a project started by a group of evanglists within Microsoft looking for a cool enterprise solution for financial planning.
###What if I want to report a bug or help out with the project?
Be my guest, but keep in mind that this isn't a supported repository.
###Can I use this bot as-is?
No, you need to create a solution and project in Visual Studio but can drop these files in it. You also need to add your unique strings to the Web.config. You may also need to set up the Properties folder with AssemblyInfo.cs file. I suggest you just make a new Bot Framework project in Visual Studio and this should all be taken care of.
###Can I add your bot on Skype and use it?
Fineas isn't published officially yet. You can reach him on Skype, but I ask you not to as there is a limited amount of friends he can have until I publish him! Also, if you're not a part of Microsoft, he should shun you anyway..

##Release notes
Currently it hasn't been released at all. Still in "Alpha"

##DB
The database holds information as described in the Finance Item class. As for the tables we query, our names are listed in the DataRetriever class. But you can use any table names you want if you modify the code, just as long as you include all columns listed in FinanceItem.

##Point to AuthBot
My manager made this tool I used for AD verification called AuthBot. https://github.com/matvelloso/AuthBot
