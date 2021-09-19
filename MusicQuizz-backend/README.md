# MusicQuizz Backend API Usage

## Controllers / Routes

- ### SongList

  ```
  /SongList
  ```
  
  #### Parameters
  
  
  `count` - The number of songs. (From 1 to 100)
  
  `difficulty` - The difficulty of the song/s. (easy, medium or hard)
  
  
  #### Response format:
  
  
  ```
  {
    title: <info about the song>
    file: <url to the .webm file of the song>
    type: <type of the song (OP or ED)>
    uid: <more info about the song>
    song: {
      title: <title of the song>
      artist: <name of the artist>
    }
    source: <title of the anime>
    difficulty: <difficulty of the song (easy, medium or hard)>
  }
  ```
