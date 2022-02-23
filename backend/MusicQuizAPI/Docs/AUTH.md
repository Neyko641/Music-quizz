# Authentication

Authentication Controller manages the authentication and authorization of the users like:
- [Register User](#register-user)
- [Log User](#log-user)

---
## Register User 

### Request

```
POST /api/auth/register
```

#### **Headers**
[*REQUIRED*] `Authorization` - String encoded with base 64 containing email and password in format `email:password`.

#### **Body**
[*REQUIRED*] `username` - The username of the user.
```json
{
    "username": ""
}
```

### Response
[ `201` ] - A JWT token that you need to provide in the same `Authorization` header for any other controller for authorization in format: `Bearer token`.
```json
{
    "token": ""
}
```
#### ***Possible errors status:***
- [ `400` ] - If registration is unsuccessful.

#### ***Possible errors codes:***
- [ `10` ] - if the username in the body is not provided, unvalid or empty.
- [ `20` ] - If the authentication header is missing.
- [ `21` ] - If the authentication header is in bad format.
- [ `30` ] - If the email is already taken.

<br />

---

## Log User 

### Request

```
POST /api/auth/login
```

#### **Headers**
[*REQUIRED*] `Authorization` - String encoded with base 64 containing email and password in format `email:password`.

#### **Body**
Empty

### Response
[ `201` ] - A JWT token that you need to provide in the same `Authorization` header for any other controller for authorization in format: `Bearer token`.
```json
{
    "token": ""
}
```
#### ***Possible errors status:***
- [ `400` ] - If loggin is unsuccessful.

#### ***Possible errors codes:***
- [ `20` ] - If the authentication header is missing.
- [ `21` ] - If the authentication header isn't correct.
- [ `31` ] - If the user with the given email doesn't exist.

<br />

---