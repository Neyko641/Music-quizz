# Error codes
Custom codes for giving better information about the cause of the error.

## Basic
- #### [ **0** ] `Unknown` - The error isn't well known and in some cases it's wasn't meant to be thrown.
- #### [ **1** ] `Unauthorized` - The client hasn't provide a token for authentication. <br />Check [Authentication Controller](AUTH.md) for more info about how to prevent.
- #### [ **2** ] `Unavailable` - The service in unreachable state, in most cases for maintance.


## Arguments
- #### [ **10** ] `MissingArgument` - The arguments are not provided. (In most cases this code isn't given because validation doesn't throw error codes.)
- #### [ **11** ] `BadArgument` - The arguments are not acceptable. You will find more info in the message.
- #### [ **12** ] `AlreadyInOrDoesNotExistArgument` - It means that the resource has been already added (or removed) or that it doesn't exist.


## Headers
- #### [ **20** ] `MissingHeader` - Header is missing. More info in the message.
- #### [ **21** ] `BadHeader` - Something is wrong in the header.


## Users
- #### [ **30** ] `UnknownUser` - Cannot recognize the user from the given token.
- #### [ **31** ] `UserTaken` - The Username is already taken.

<br />
> More to be added or changed.