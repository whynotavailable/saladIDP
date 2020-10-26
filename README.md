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
