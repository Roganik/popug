# Hola!

## How to start?

Stark Keycloak in background: 
`docker run -p 8080:8080 -e KEYCLOAK_ADMIN=admin -e KEYCLOAK_ADMIN_PASSWORD=admin -d quay.io/keycloak/keycloak:20.0.1 start-dev`

Add `popug` realm, see [this link](https://www.keycloak.org/getting-started/getting-started-docker)

Add new `popug-admin` user in your realm, set password

Check login: http://localhost:8080/realms/popug/account

