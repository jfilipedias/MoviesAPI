# MoviesAPI

A simple movie API example build with .NET 6

## Project setup

To run the UsersAPI project, will be needed to setups the project `user-secrets` related to the email provider:

``` bash
# Ensure that you're in the UsersAPI project directory
$ cd UsersAPI

# Initialize user-secrets
$ dotnet user-secrets init

# Setup the SmtpServer secret
$ dotnet user-secrets set "EmailSettings:SmtpServer" "smtp.gmail.com"

# Setup the Port secret
$ dotnet user-secrets set "EmailSettings:Port" "465"

# Setup the Password secret
$ dotnet user-secrets set "EmailSettings:Password" "GenericPassword123!"

# Setup the Name secret
$ dotnet user-secrets set "EmailSettings:Name" "EmailServiceName"

# Setup the From secret
$ dotnet user-secrets set "EmailSettings:From" "provideremailname@gmail.com"
```