# MusicQuizz Backend API Usage


## Response structure
Ok [200] status:
```json
{
  "status": 200,
  "result": "<data from the result of the request>"
}
```
<br />
  
Bad [400] status:
```json
{
  "status": 400,
  "errors": ["<array of error messages>"]
}
```


## Controllers / Routes

- ### SongList

  ```
  /song/random
  ```
  <br />
  
  #### Parameters
  
  
  `count` - The number of songs. (From 1 to 100)
  
  `difficulty` - The difficulty of the song/s. (easy, medium or hard)

  <br />

  #### Result structure:
  
  ```json
  [{
    "title": "<info about the song>",
    "file": "<url to the .webm file of the song>",
    "type": "<type of the song (OP or ED)>",
    "uid": "<more info about the song>",
    "song": {
      "title": "<title of the song>",
      "artist": "<name of the artist>",
    },
    "source": "<title of the anime>",
    "difficulty": "<difficulty of the song (easy, medium or hard)>"
  }, {}, {}, ...]
  ```
