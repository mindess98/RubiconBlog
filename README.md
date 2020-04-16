SQL Database publish instructions:

- Right click on the RubiconDb SQL Server Project -> Click Publish
- Next to the Target database connection string text field, click the Edit button
- Select the browse tab
- Select Local in the list 
- Select LocalDb (MSSQLLocalDB)
- In the bottom right, go to Advanced... -> Verify that Integrated Security is set to True
- Give the database a name, say RubiconDb
- Copy the connection string which should now be displayed in the Target database connection string field, Add to the connection string Database={YourDatabaseName};
- Click Publish


In order to run the application, copy the connection string into the appsettings.Development.json and appsettings.json "DefaultConnection" field.

Run the application.
