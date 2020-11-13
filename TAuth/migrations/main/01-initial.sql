/*
So there needs to be a couple of items here to start with.

* clients
* resource_servers
* sources (this is how you get your users)
* roles (these have a one-to-many FK back to resource_servers and are managed by the owner of a that resource server)
* users_roles (mapping from a user to a role note: not a FK)
* acl_templates (templates for ACLs that can be applied to any resource

When we have federated logins, we will also need a way to map groups of federated users to roles.
This mapping will be done by the resource server owners.
 */
