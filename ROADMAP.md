# TAuth Roadmap

There are a couple of features I want in the initial version (v0.1.0). I want
to keep it limited so that I can keep motivation.

* Database Users
* client_credentials
* auth_code

For version v0.2.0 I want to add access controls and ownership on a per-resource level.

For version v0.3.0 I want to implement federation through either openid or SAML.

For version v0.4.0 I want to add integration to allow people to run their own code during the login
process. At first this will be simply http integrations but lambda's would be cool too. I don't
want it to be like Auth0's where you add the code in the portal because that sucks.

For the version v1.0.0 the thing I want to add is refresh tokens.
