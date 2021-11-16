# Users

User Controller manages the basic functionality with the users like:
- [Get User](#get-user)
- [Search for User](#search-for-user)
- [Add Friend](#add-friend)
- [Accept Friend Request](#accept-friend-request)
- [Decline Friend Request](#decline-friend-request)
- [Remove Friend](#remove-friend)
- [Get Friends](#get-friends)

---
## Get User 

### Request

```
GET /api/user
```

#### Parameters

None

### Response
[ `200` ] - Object with the user information.
```json
{
    "id": 0,
    "username": "",
    "avatar": null,
    "guess_count": 0,
    "played_count": 0,
    "registered_date": "",
    "is_friend": false
}
```
#### Possible errors status:

[ `401` ] - If token is not provided in the header.

#### Possible errors codes:
None

<br />

---

## Search for User 

### Request

```
GET /api/user/search
```

Parameters

[REQUIRED] `name` - String with the name of the users to search for.
[OPTIONAL] `limit` - Number from 1 to 100 with the limit of users to search for *(DEFAULT=10).

### Response
[ `200` ] - List of objects for each user corresponding to the name.
```json
[
    {
        "id": 0,
        "username": "",
        "avatar": null,
        "guess_count": 0,
        "played_count": 0,
        "registered_date": "",
        "is_friend": false
    }
]
```
#### Possible errors status:

[ `400` ] - If name and/or limit params are not given or limit is not in range from 1 to 100.

[ `401` ] - If token is not provided in the header.

#### Possible errors codes:
None

<br />

---

## Add Friend 

### Request

```
POST /api/user/add-friend
```

Parameters

[REQUIRED] `id` - Positive Number with the id of the user.

### Response
[ `201` ] - Message which gives information about both users.
```json
"Successfully sended friend request from user [0] to user [0]."
```
#### Possible errors status:

[ `400` ] - If id param is not given or not validated correctly.

[ `401` ] - If token is not provided in the header.

#### Possible errors codes:
[ `5` ] - If the user is already friend or doesn't exist.

<br />

---

## Accept Friend Request

### Request

```
PUT /api/user/accept-friend
```

Parameters

[REQUIRED] `id` - Positive Number with the id of the user.

### Response
[ `200` ] - Message which gives information about both users.
```json
"Successfully accepted user[0] for user[0]."
```
#### Possible errors status:

[ `400` ] - If id param is not given or not validated correctly.

[ `401` ] - If token is not provided in the header.

#### Possible errors codes:
[ `4` ] - If haven't recieved any request from the user, is already accepted or the user doesn't exist.

<br />

---

## Decline Friend Request 

### Request

```
PUT /api/user/decline-friend
```

Parameters

[REQUIRED] `id` - Positive Number with the id of the user.

### Response
[ `200` ] - Message which gives information about both users.
```json
"Successfully declined user[0] for user[0]."
```
#### Possible errors status:

[ `400` ] - If id param is not given or not validated correctly.

[ `401` ] - If token is not provided in the header.

#### Possible errors codes:
[ `4` ] - If haven't recieved any request from the user, is already declined or the user doesn't exist.

<br />

---

## Remove Friend 

### Request

```
DELETE /api/user/remove-friend
```

Parameters

[REQUIRED] `id` - Positive Number with the id of the user.

### Response
[ `200` ] - Message which gives information about both users.
```json
"Successfully removed user[0] for user[0]."
```
#### Possible errors status:

[ `400` ] - If id param is not given or not validated correctly.

[ `401` ] - If token is not provided in the header.

#### Possible errors codes:
[ `4` ] - If the user is already not friend or doesn't exist.

<br />

---

## Get Friends

### Request

```
GET /api/user/get-friends
```

Parameters

[OPTIONAL] `limit` - Number from 1 to 100 with the limit of users to search for *(DEFAULT=10).

### Response
[ `200` ] - List of objects for each friend.
```json
[
    {
        "id": 0,
        "username": "",
        "avatar": null,
        "guess_count": 0,
        "played_count": 0,
        "registered_date": "",
        "is_friend": false
    }
]
```
#### Possible errors status:

[ `400` ] - If limit param is not given or is not in range from 1 to 100.

[ `401` ] - If token is not provided in the header.

#### Possible errors codes:

None

---