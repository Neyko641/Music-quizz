# Users

User Controller manages the basic functionality with the users like:
- [Get User](#get-user)
- [Update User](#update-user)
- [Search for User](#search-for-user)
- [Add Friend](#add-friend)
- [Manage Friend Request](#manage-friend-request)
- [Remove Friend](#remove-friend)
- [Get Friend Requests](#get-friend-requests)
- [Get Friends](#get-friends)

---
## Get User 

### Request

```
GET /api/user
```

#### **Parameters**
[*OPTIONAL*] `id` - Id for searching a user or the current on if it's not given.

### Response
[ `200` ] - Object with the user information.
```json
{
    "userID": 0,
    "username": "",
    "email": "",
    "avatar": "",
    "guessCount": 0,
    "playedCount": 0,
    "registeredDate": "",
    "isFriend": false
}
```
#### ***Possible errors status:***
[ `401` ] - If token is not provided in the header.

#### ***Possible errors codes:***
[ `31` ] - If the user with the given id doesn't exist.

<br />

---

## Update User

### Request

```
PUT /api/user
```

#### **Parameters**
None

#### **Body**
[*REQUIRED*] `password` - The password of the user.<br/>
[*OPTIONAL*] `newPassword` - The new password or keep the old one if it's not given.<br/>
[*OPTIONAL*] `username` - The new username or keep the old one if it's not given.<br/>
[*OPTIONAL*] `avatar` - The new url of a picture for the avatar or keep the old one if it's not given.
```json
{
    "password": "",
    "newPassword": "",
    "username": "",
    "avatar": ""
}
```

### Response
[ `200` ] - Message for the change.
```json
"... updated successfully!"
```
#### ***Possible errors status:***
[ `400` ] - If limit param is not given or is not in range from 1 to 100.<br/>
[ `401` ] - If token is not provided in the header.

#### ***Possible errors codes:***
[ `10` ] - If the password is wrong.

<br />
---

## Search for User 

### Request

```
GET /api/user/search
```

#### **Parameters**
[*REQUIRED*] `name` - String with the name of the users to search for.<br/>
[*OPTIONAL*] `limit` - Number from 1 to 100 with the limit of users to search for *(DEFAULT=10).<br/>
[*OPTIONAL*] `type` - How to be represent the user object:
- `simple` - only id, username and isFriend *(DEFAULT)
- `detailed` - full detailed model

#### **Body**
Empty

### Response
[ `200` ] - List of objects for each user corresponding to the name.
```json
[
    {
        "userID": 0,
        "username": "",
        "email": "",
        "avatar": "",
        "guessCount": 0,
        "playedCount": 0,
        "registeredDate": "",
        "isFriend": false
    },
    {
        "userID": 0,
        "username": "",
        "isFriend": false
    }
]
```

#### ***Possible errors status:***
[ `400` ] - If name and/or limit params are not given or limit is not in range from 1 to 100.<br/>
[ `401` ] - If token is not provided in the header.

#### ***Possible errors codes:***
None

<br />

---

## Send Friend Request

### Request

```
POST /api/user/friends
```

#### **Parameters**
None

#### **Body**
[*REQUIRED*] `id` - Positive Number with the id of the user.
```json
{
    "id": 0
}
```

### Response
[ `201` ] - Message which gives information about both users.
```json
"Successfully sended friend request from user [0] to user [0]."
```
#### ***Possible errors status:***
[ `400` ] - If id param is not given or not validated correctly.<br/>
[ `401` ] - If token is not provided in the header.

#### ***Possible errors codes:***
[ `30` ] - If the user is already friend.<br/>
[ `31` ] - If the user doesn't exist.

<br />

---

## Manage Friend Request

### Request

```
PUT /api/user/friends
```

#### **Parameters**
None

#### **Body**
[*REQUIRED*] `id` - Positive Number with the id of the user.<br/>
[*REQUIRED*] `isAccepted` - Boolean `true` to accept and `false` to decline.
```json
{
    "id": 0,
    "isAccepted": false
}
```

### Response
[ `200` ] - Message which gives information about both users.
```json
"Successfully accepted user[0] for user[0]."
```
#### ***Possible errors status:***
[ `400` ] - If id param is not given or not validated correctly.<br/>
[ `401` ] - If token is not provided in the header.

#### ***Possible errors codes:***
[ `30` ] - If both users are already friends.<br/>
[ `31` ] - If haven't recieved any request from the user.

<br />

---

## Remove Friend 

### Request

```
DELETE /api/user/friends
```

#### **Parameters**
None

#### **Body**
[*REQUIRED*] `id` - Positive Number with the id of the user.
```json
{
    "id": 0
}
```

### Response
[ `200` ] - Message which gives information about both users.
```json
"Successfully removed user[0] for user[0]."
```
#### ***Possible errors status:***
[ `400` ] - If id param is not given or not validated correctly.<br/>
[ `401` ] - If token is not provided in the header.

#### ***Possible errors codes:***
[ `31` ] - If the user is already not friend or doesn't exist.

<br />

---

## Get Friends

### Request

```
GET /api/user/friends
```

#### **Parameters**
[*OPTIONAL*] `limit` - Number from 1 to 100 with the limit of users to search for *(DEFAULT=10).

#### **Body**
Empty

### Response
[ `200` ] - List of objects for each friend.
```json
[
    {
        "userID": 0,
        "username": "",
        "email": "",
        "avatar": "",
        "guessCount": 0,
        "playedCount": 0,
        "registeredDate": "",
        "isFriend": false
    },
    {}
]
```
#### ***Possible errors status:***
[ `400` ] - If limit param is not given or is not in range from 1 to 100.<br/>
[ `401` ] - If token is not provided in the header.

#### ***Possible errors codes:***
None

---

## Get Friend Requests

### Request

```
GET /api/user/requests
```

#### **Parameters**
[*OPTIONAL*] `limit` - Number from 1 to 100 with the limit of sended friend requests to search for *(DEFAULT=10).

#### **Body**
Empty

### Response
[ `200` ] - List of objects for each request.
```json
[
    {
        "id": 0,
        "userID": "",
        "username": "",
        "startDate": "",
        "didCurrentUserSendRequest": false
    },
    {}
]
```
#### ***Possible errors status:***
[ `400` ] - If limit param is not given or is not in range from 1 to 100.<br/>
[ `401` ] - If token is not provided in the header.

#### ***Possible errors codes:***
None


---