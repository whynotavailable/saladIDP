# TAuth

TAuth is a completely free and open source customizable IdP.

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

## Access Controls

t-auth will have it's own groups These groups can be made up of users
from any source. You can also setup group mapping from your source
to t-auth groups.

Here are the levels of control for each resource type (some resources
don't have all the types).

* admin (can control anything)
* owner (can control anything aside from access controls)
* approvers (can approve requests related to the resource)
* member (can do extra actions on a per resource basis (like pull the client_secret))

These can be users or groups (though admins should be users).

You can save the controls of a resource and use it as a template to apply
elsewhere (I'm thinking of making it so that changes to the template will
persist to any resource using that template).

## Generating an x509 cert

On linux/mac remove the `winpty` commands, they aren't needed but are
a fix for git for windows bash.

 `winpty openssl req -x509 -newkey rsa:4096 -keyout key.pem -out cert.pem -days 365`
 
 `winpty openssl pkcs12 -export -in cert.pem -inkey key.pem -out out.pfx`
