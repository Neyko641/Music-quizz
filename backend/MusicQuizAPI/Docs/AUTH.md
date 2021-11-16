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

~~Parameters~~ Headers

[REQUIRED] `Authorization` - String encoded with base 64 containing username and password in format `username:password`.

### Response
[ `201` ] - A JWT token that you need to provide in the same `Authorization` header for any other controller for authorization in format: `Bearer token`.
```json
{
    "token": ""
}
```
#### Possible errors status:
- [ `400` ] - If registration is unsuccessful.

#### Possible errors codes:
- [ `0` ] - If the authentication header isn't correct.
- [ `6` ] - If the authentication header is missing.
- [ `9` ] - If the username is already taken.

<br />

---

## Log User 

### Request

```
POST /api/auth/login
```

~~Parameters~~ Headers

[REQUIRED] `Authorization` - String encoded with base 64 containing username and password in format `username:password`.

### Response
[ `201` ] - A JWT token that you need to provide in the same `Authorization` header for any other controller for authorization in format: `Bearer token`.
```json
{
    "token": ""
}
```
#### Possible errors status:
- [ `400` ] - If loggin is unsuccessful.

#### Possible errors codes:
- [ `0` ] - If the authentication header isn't correct.
- [ `6` ] - If the authentication header is missing.

<br />

---