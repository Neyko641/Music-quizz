# Songs

Song Controller manages the basic functionality with the songs like:
- [Seach for Song](#search-for-song)
- [Add Favorite Song](#add-favorite-song)
- [Remove Favorite Song](#remove-favorite-song)
- [Update Favorite Song](#update-favorite-song)
- [Get All Favorite Songs](#get-all-favorite-songs)
- [Get Random Song](#get-random-song)

---

## Search for Song 

### Request
```
GET /api/song/search
```

#### **Parameters**
[*REQUIRED*] `title` - String with the title of the song to search for.<br/>
[*OPTIONAL*] `searchtype` - String for the type of search:
- `song-title` - Search by song title *(DEFAULT)
- `anime-title` - Search by anime title

#### **Body**
Empty

### Response
[ `200` ] - List of objects for each song corresponding to the title by the type
```json
[
    {
        "songID": 0,
        "animeID": 0,
        "title": "",
        "animeTitle": "",
        "artist": "",
        "url": "url.webm",
        "songType": "OP or ED",
        "detailedSongType": "Opening1",
        "difficulty": "",
        "score": 0.0,
        "userScore": 0,
        "popularity": 0
    }
]
```
#### ***Possible errors status:***
[ `400` ] - If title param is not given<br/>
[ `401` ] - If token is not provided in the header

#### ***Possible errors codes:***
None

<br />

---

## Add Favorite Song 

### Request

```
POST /api/song/favorites
```

#### **Parameters**
None

#### **Body**
[*REQUIRED*] `id` - Positive Number with the id of the song<br/>
[*REQUIRED*] `score` - Number with the score given to the song from 1 to 10
```json
{
    "id": 0,
    "score": 0
}
```

### Response
[ `201` ] - Message which gives information about the song and user id
```json
"The song [0] was added successfully to the user [0]!"
```
#### ***Possible errors status:***
[ `400` ] - If id and/or score params are not given or are not validated correctly<br/>
[ `401` ] - If token is not provided in the header

#### ***Possible errors codes:***
[ `30` ] - If the song is already added to favorites for the user<br/>
[ `31` ] - If the song with the given id doesn't exist

<br />

---

## Remove Favorite Song 

### Request

```
DELETE /api/song/favorites
```

#### **Parameters**
None

#### **Body**
[*REQUIRED*] `id` - Positive Number with the id of the song.
```json
{
    "id": 0
}
```

### Response
[ `200` ] - Message which gives information about the song and user id.
```json
"The song [0] was removed successfully from the user [0]!"
```
#### ***Possible errors status:***
[ `400` ] - If id param is not given or not validated correctly<br/>
[ `401` ] - If token is not provided in the header

#### ***Possible errors codes:***
[ `31` ] - If the song is already not in favorites for the user

<br />

---

## Update Favorite Song 

### Request

```
PATCH /api/song/favorites
```

#### **Parameters**
None

#### **Body**
[*REQUIRED*] `id` - Positive Number with the id of the song.<br/>
[*REQUIRED*] `score` - Number with the new score given to the song from 1 to 10.
```json
{
    "id": 0,
    "score": 0
}
```

### Response
[ `200` ] - Message which gives information about the song and user id.
```json
"The song [0] updated successfully for the user [0]!"
```
#### ***Possible errors status:***
[ `400` ] - If id and/or score params are not given or are not validated correctly<br/>
[ `401` ] - If token is not provided in the header

#### ***Possible errors codes:***
[ `31` ] - If the song is already not in favorites for the user

<br />

---

## Get All Favorite Songs

### Request

```
GET /api/song/favorites
```

#### **Parameters**
None

### Response
[ `200` ] - List of objects for each song
```json
[
    {
        "songID": 0,
        "animeID": 0,
        "title": "",
        "animeTitle": "",
        "artist": "",
        "url": "url.webm",
        "songType": "OP or ED",
        "detailedSongType": "Opening1",
        "difficulty": "",
        "score": 0.0,
        "userScore": 0,
        "popularity": 0
    },
    {}
]
```
#### ***Possible errors status:***
[ `401` ] - If token is not provided in the header

#### ***Possible errors codes:***
None

---

## Get Random Song 

### Request

```
GET /api/song/random
```

#### **Parameters**
[*OPTIONAL*] `count` - Number from 1 to 100 with the number of the songs *(DEFAULT=10)<br/>
[*OPTIONAL*] `difficulty` - String with the difficulty of the song:
- `easy` - Easy difficulty songs *(DEFAULT)
- `medium` - Medium difficulty songs
- `hard` - Hard difficulty songs

### Response
[ `200` ] - List of objects for each random song
```json
[
    {
        "songID": 0,
        "animeID": 0,
        "title": "",
        "animeTitle": "",
        "artist": "",
        "url": "url.webm",
        "songType": "OP or ED",
        "detailedSongType": "Opening1",
        "difficulty": "",
        "score": 0.0,
        "userScore": 0,
        "popularity": 0
    },
    {}
]
```
#### ***Possible errors status:***
[ `400` ] - If count and/or difficulty params are not given or are not validated correctly<br/>
[ `401` ] - If token is not provided in the header

#### ***Possible errors codes:***
None

<br />

---