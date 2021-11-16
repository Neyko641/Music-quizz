# Animes

Anime Controller manages the basic functionality with the animes like:
- [Search for Anime](#search-for-anime)
- [Add Favorite Anime](#add-favorite-anime)
- [Remove Favorite Anime](#remove-favorite-anime)
- [Update Favorite Anime](#update-favorite-anime)
- [Get Favorite Anime](#get-favorite-anime)

---

## Search for Anime 

### Request

```
GET /api/anime/search
```

#### Parameters

[REQUIRED] `title` - String with the title of the anime to search for.

### Response
[ `200` ] - List of objects for each anime corresponding to the title.
```json
[
    {
        "animeID": 0,
        "title": "",
        "score": 0,
        "user_score": 0,
        "popular": 0
    }
]
```
#### Possible errors status:

[ `400` ] - If title param is not given.

[ `401` ] - If token is not provided in the header.

#### Possible errors codes:
None

<br />

---

## Add Favorite Anime 

### Request

```
POST /api/anime/add-favorite
```

#### Parameters

[REQUIRED] `id` - Positive Number with the id of the anime.

[REQUIRED] `score` - Number with the score given to the anime from 1 to 10.

### Response
[ `201` ] - Message which gives information about the anime and user id.
```json
"The anime [0] was added successfully to the user [0]!"
```
#### Possible errors status:

[ `400` ] - If id and/or score params are not given or are not validated correctly.

[ `401` ] - If token is not provided in the header.

#### Possible errors codes:
[ `5` ] - If the anime is already added to favorites for the user.

<br />

---

## Remove Favorite Anime 

### Request

```
DELETE /api/anime/remove-favorite
```

#### Parameters

[REQUIRED] `id` - Positive Number with the id of the anime.

### Response
[ `200` ] - Message which gives information about the anime and user id.
```json
"The anime [0] was removed successfully from the user [0]!"
```
#### Possible errors status:

[ `400` ] - If id param is not given or not validated correctly.

[ `401` ] - If token is not provided in the header.

#### Possible errors codes:
[ `5` ] - If the anime is already not in favorites for the user.

<br />

---

## Update Favorite Anime 

### Request

```
PATCH /api/anime/update-favorite
```

#### Parameters

[REQUIRED] `id` - Positive Number with the id of the anime.

[REQUIRED] `score` - Number with the new score given to the anime from 1 to 10.

### Response
[ `200` ] - Message which gives information about the anime and user id.
```json
"The anime [0] updated successfully for the user [0]!"
```
#### Possible errors status:

[ `400` ] - If id and/or score params are not given or are not validated correctly.

[ `401` ] - If token is not provided in the header.

#### Possible errors codes:
[ `5` ] - If the anime is already not in favorites for the user.

<br />

---

## Get Favorite Anime 

### Request

```
GET /api/anime/get-favorites
```

#### Parameters

None

### Response
[ `200` ] - List of objects for each anime.
```json
[
    {
        "animeID": 0,
        "title": "",
        "score": 0,
        "user_score": 0,
        "popular": 0
    }
]
```
#### Possible errors status:

[ `401` ] - If token is not provided in the header.

#### Possible errors codes:

None

---