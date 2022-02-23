# Error codes
Custom codes for giving better information about the cause of the error.

## Basic
- #### [ **0** ] `Unknown` - The error isn't well known. Can be thrown in very rare cases.


## Arguments
- #### [ **10** ] `BadArgument` - The arguments are not acceptable. You will find more info in the message.


## Headers
- #### [ **20** ] `MissingHeader` - Header is missing. More info in the message.
- #### [ **21** ] `BadHeader` - Something is wrong in the header.


## Data
- #### [ **30** ] `AlreadyExist` - It means that the resource has been already added (or removed) or that it doesn't exist.
- #### [ **31** ] `NotExist` - The date doesn't exist by the given arguments.


## Other
- #### [ **401** ] `Unauthorized` - The client hasn't provide a token for authentication. <br />Check [Authentication Controller](AUTH.md) for more info about how to prevent.
- #### [ **503** ] `Unavailable` - The service is in unreachable state, in most cases for maintance or eternal error. 

<br />
> More to be added or changed.