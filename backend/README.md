# MusicQuizz Backend API Usage

## Requirements:
- .NET Core 5.0 or higher;
- SQL Database; (MySQL and MySQL Workbench is reccomended).


## How to run:
1. Create Database in your SQL Workbench.
2. Put your connection string in `appsettings.json`.
3. In the same file put a Secret in base 64 format.
4. Try to build and apply initial migrations to the database.
```
> dotnet build
> dotnet ef migrations add Initial
> dotnet ef database update
```
##### \* if you were caught in error, sorry nothing can be done. :/

5. There is a special Controller called `InitializationController.cs`. Run the application and go to the following route `"[root]/api/init/top-animes?secret=[your secret]"`. This will fill the TopAnimes table in your database.
6. Do the same but with the next route `"[root]/api/init/animes-songs?secret=[your secret]"` to fill Animes and Songs tables.
7. Everything is ready if you are at this point.

---

## Controllers
Check controllers for more information about what you can do with the API.
- [Anime Controller](MusicQuizAPI/Docs/ANIME.md)
- [Authentication Controller](MusicQuizAPI/Docs/AUTH.md)
- [Song Controller](MusicQuizAPI/Docs/SONG.md)
- [User Controller](MusicQuizAPI/Docs/USER.md)

---


## Response structure
You can expect respone in the following structure depending on the status code.
<br /><br />

### Good response [`200` - `299`] status.
Most of these status code are 200 if everything is okay, 201 if the resource is created etc. This is the best we can give you.

```json
{
  "status": 200,
  "result": "<data from the result of the request>"
}
```
<br />
  
### Bad response [`400` - `499`] status.
You will really want to avoid these. Bad arguments, missing headers, unexisting resources and so on. Thank god we have custom messages with our custom error codes. <br />
Be aware that even with custom codes, argument validation will not give you exact information about what is wrong!

```json
{
  "status": 400,
  "errors": [
    {
      "code" : 0,
      "message" : "<message containing request info>"
    }
  ]
}
```
> Check [error codes]() for more info.