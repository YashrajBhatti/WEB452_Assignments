
WEB452_Assignments YashrajBhatti 0832320
A Collection of Assignments for WEB452

Initiating a fresh start with Assignment 1 prerequisites, this project is based on a .NET 6 MVC application. The setup involved using the following commands to install essential tools:

dotnet tool install --global dotnet-aspnet-codegenerator --version 6
dotnet tool install --global dotnet-ef --version 6
dotnet add package Microsoft.EntityFrameworkCore.Design --version 6
dotnet add package Microsoft.EntityFrameworkCore.SQlite --version 6
dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Design --version 6
dotnet add package Microsoft.EntityFrameworkCore.sqlServer --version 6
Scaffolding commenced on December 3, 2023, at 7:03 PM EST, successfully executed with:

dotnet aspnet-codegenerator controller -name BikesController -m Bikes -dc MvcBikesContext --relativeFolderPath Controllers --useDefaultLayout --referenceScriptLibraries -sqlite
By December 3, 2023, at 7:35 PM EST, the initial database setup was completed with:

dotnet ef migrations add InitialCreate
dotnet ef database update
A 'not found' page was integrated into the controller by 7:50 PM EST.

Moving to Assignment 2 on December 4, 2023, the first two steps included adding genre and developer properties to the bike model. The search functionality was integrated using Dr. Majid's code as a reference but was only functional for all and model attributes, not working for rating and LaunchDate.

Challenges arose while implementing the hiding functionality in index.cshtml. Although the capability to display hidden bikes was successfully achieved, hiding bikes in the Index view was not functioning as expected.

The 'IsHidden' boolean property, added to the model to facilitate the hiding of bikes, aided in creating the feature but faced issues in hiding bikes on the Index page. The bikes were not getting hidden despite the implementation of checkboxes and a hide button.

Furthermore, the search functionality, while initially seeming straightforward, only worked for all and model attributes. The filtering for rating and LaunchDate did not operate as intended, indicating a need for further refinement of the search logic.

Despite these setbacks, progress was made in understanding and applying MVC principles, with plans to revisit and rectify these functionalities. The project's completion by 2:20 PM EST on December 4, 2023, marked a significant learning experience, with appreciation for Dr. Majid's guidance throughout the semester