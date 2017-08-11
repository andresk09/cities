# cities

## Deployment:
* Create the Database:
    * Use OpLab.Database to publish the database.
    * Use the provided Post deployment script to seed the DB

* Change the application connection string
    * src\OpLab.API\appsettings.json

* Set Multiple Startup Projects in the solution:
    * OpLab.API
    * OpLab.Web