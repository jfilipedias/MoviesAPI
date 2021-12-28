# MoviesAPI
A theater API build with .NET 6

## Project Setup
To run the UsersAPI project, will be needed to setups the project `user-secrets`:

``` bash
# Ensure that you're in the UsersAPI project directory
# Initialize user-secrets
$ dotnet user-secrets init

# Secret to the admin seed
# Setup the AdminInfo:Password
$ dotnet user-secrets set "AdminInfo:Password" "Admin123456!"

# Secrets to the email provider
# Setup the EmailSettings:SmtpServer secret
$ dotnet user-secrets set "EmailSettings:SmtpServer" "smtp.gmail.com"

# Setup the EmailSettings:Port secret
$ dotnet user-secrets set "EmailSettings:Port" "465"

# Setup the EmailSettings:Password secret
$ dotnet user-secrets set "EmailSettings:Password" "YourPassword123456!"

# Setup the EmailSettings:Name secret
$ dotnet user-secrets set "EmailSettings:Name" "YourEmailServiceName"

# Setup the EmailSettings:From secret
$ dotnet user-secrets set "EmailSettings:From" "yourprovideremailname@gmail.com"
```

## Database Setups
Execute the docker compose in the project root:

``` bash
# Ensure that you're in the project root directory
# Execute the command
$ docker-compose up -d
```

## Run Migrations
Will be needed to run the migrations for `AppDbContext` and the `UserDbContext` in the **Package Manager Console**:

``` bash
# Ensure that you're with the MoviesAPI project selected
# Execute the command to AppDbContext
$ Update-Database

# Ensure that you're with the UsersAPI project selected
# Execute the command to UserDbContext
$ Update-Database
```
