# TAuth

TAuth is a completely free and open source custizable IdP.

There are going to be three parts.

* The API (this repo for now)
* The UI
* The Database

The API will have two primary feature sets that can be controlled via ENV.

* Management Operations
* Auth Operations

This is so that you can lock the management operations behind your firewall while the auth operations are available to the public.

The main database for management operations will be postgresql.

For information pertaining to the roadmap please look at ROADMAP.md

 `winpty openssl req -x509 -newkey rsa:4096 -keyout key.pem -out cert.pem -days 365`
 
 `winpty openssl pkcs12 -export -in cert.pem -inkey key.pem -out out.pfx`
