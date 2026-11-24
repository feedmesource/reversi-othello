# README
## Author and project info
Name: Aimar Tuul  
student code: 266685TAI  
uni-id: aituul  
assigned game: reversi-othello  

## Screenshots

### Menu
![Game starting menu](images/screenshot-1.png)
![Load games](images/screenshot-2.png)
![Game configuration menu](images/screenshot-3.png)
![Load configurations](images/screenshot-4.png)
![Choose player types](images/screenshot-7.png)

### Gameplay
![Example game 1](images/screenshot-6.png)
![Example game 2](images/screenshot-5.png)

## EF Core migrations commands
```bash
# install and update commands
dotnet tool install --global dotnet-ef
dotnet tool update --global dotnet-ef       # later
dotnet ef --version

# add migration called "InitialCreate" 
dotnet ef migrations add InitialCreate --project DAL --startup-project ConsoleApp

# apply migrations
dotnet ef database update --project DAL --startup-project ConsoleApp

# list, remove migrations or drop database
dotnet ef migrations list --project DAL --startup-project ConsoleApp
dotnet ef migrations remove --project DAL --startup-project ConsoleApp         # delete the last migration, if it is not applied yet
dotnet ef database drop --project DAL --startup-project ConsoleApp             # delete the database file — cheap during development
```

## Nuget packages
```bash
# in DAL
Microsoft.EntityFrameworkCore.Sqlite

# in ConsoleApp
Microsoft.EntityFrameworkCore.Design
```