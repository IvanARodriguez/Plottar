# Plottar API

- [Plottar API](#backer-api)
  - [Auth](#auth)
    - [Register](#register)
      - [Register Request](#register-request)
      - [Register Response](#login-request)
    - [Login](#login)
      - [Login Request](#register-login)
      - [Login Response](#login-login)

## Auth

### Register

```js
POST {{host}}/auth/register
```

#### Register Request

```json
{
	"userName": "John",
	"lastName": "Doe",
	"email": "j.doe@sample.com",
	"password": "johnDoePass01"
}
```

#### Register Response

```json
HTTP/1.1 200 OK
Connection: close
Content-Type: application/json; charset=utf-8
Date: Wed, 28 Aug 2024 22:24:39 GMT
Server: Kestrel
Transfer-Encoding: chunked

{
  "id": "15b91c44-0b25-4ed6-81e9-242330ef348f",
  "firstName": "johnDoePass01",
  "lastName": "j.doe@sample.com",
  "email": "John",
  "token": "token"
}
```

### Login

```js
POST {{host}}/auth/login
```

#### Login Request

```json
{
	"email": "j.doe@sample.com",
	"password": "johnDoePass01"
}
```

#### Login Response

````json

```json
HTTP/1.1 200 OK
Connection: close
Content-Type: application/json; charset=utf-8
Date: Wed, 28 Aug 2024 22:24:39 GMT
Server: Kestrel
Transfer-Encoding: chunked

{
  "id": "15b91c44-0b25-4ed6-81e9-242330ef348f",
  "firstName": "johnDoePass01",
  "lastName": "j.doe@sample.com",
  "email": "John",
  "token": "token"
}
````
