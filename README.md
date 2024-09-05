
# .NET Developer Assessment

## Setup & Dependencies

### Dependencies 
- Visual Studio with .NET 8
- LocalDb

### How to Setup
1. Clone or download the repository.
2. Open the solution file on Visual Studio.
3. Open the command line and run the migration using `dotnet ef database update`
4. After that you can run the server by entering `dotnet run` command on the cli or clicking the play button at the top of Visual Studio.
5. Open the your browser and access the `localhost:5286` url.

## JWT Token
#### JWT Configuration
In Implementing JWT Authentication first we need to install the necessary nuget packages such as `Microsoft.AspNetCore.Authentication.JwtBearer` and `System.IdentityModel.Tokens.Jwt`.

After that JWT is configured on the appSettings.json .
```
"Jwt": {
  "Key": "PbDWkeY4B37Y6DO0ToaHJIhdJCE4MgZR",
  "Issuer": "TestApp", 
  "Audience":  "TestApp",
  "ExpireMinutes": 60 
}
```
Then we configure the Authentication and Authorization on the Program.cs file. 
```
// Get JWT settings from appsettings.json
var jwtSettings = builder.Configuration.GetSection("Jwt");

builder.Services.AddAuthentication();

// Add Authentication service with JWT
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]!))
    };

	// I added this code for cases when jwt token is not in the header but passed as a cookie
    options.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            var token = context.HttpContext.Request.Cookies["jwtToken"];
            if (!string.IsNullOrEmpty(token))
            {
                context.Token = token;
            }
            return Task.CompletedTask;
        }
    };
});

builder.Services.AddAuthorizationBuilder()
    .AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"))
    .AddPolicy("UserOnly", policy => policy.RequireRole("User"));

// add this after the var app = builder.Build();
app.UseAuthentication();
```
I also added additional configuration for automatic redirection of Unauthorized request to a corresponding error page.
```
app.UseStatusCodePages(context =>
{
    var response = context.HttpContext.Response;
    if (response.StatusCode == 401)
    {
        response.Redirect("/errors/unauthorized");
    }
    else if (response.StatusCode == 403)
    {
        response.Redirect("/errors/forbidden");
    }
    return Task.CompletedTask;
});
```
#### Obtaining JWT Token
For generating JWT token I created a method GenerateJWTToken on the AuthService.cs where inside it I created a custom claims to set the Role and the Id of the User. I then use this method on the Authenticate method of the AuthService so that whenever the user credentials of the User is match or valid a new JWT token will be generated and will be returned as a response to the request.
```
public string? GenerateJWTToken(int id, string role)
{
    var jwtSettings = _config.GetSection("Jwt");
    var claims = new[]
    {
        new Claim(JwtRegisteredClaimNames.Sub, id.ToString()),
        new Claim(ClaimTypes.Name, id.ToString()),
        new Claim("Id", id.ToString()),
        new Claim(ClaimTypes.Role, role) // sets the role
    };

    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]!));
    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

    var token = new JwtSecurityToken(
        issuer: jwtSettings["Issuer"],
        audience: jwtSettings["Audience"],
        claims: claims,
        expires: DateTime.Now.AddMinutes(Convert.ToInt32(jwtSettings["Exp"])),
        signingCredentials: creds);

    return new JwtSecurityTokenHandler().WriteToken(token);
}
```
After successful login the jwt-token will then be save on the sessionStorage to be use for ajax request or api call and it is also saved as Cookie so that everytime the user navigate to the app the token will be included. 

To implement the role-based authorization on routes I also added [Authorize] attribute with specific roles on every method on the controller that needs to be protected form unauthorized access.
Example :
`[Authorize(Roles = "Admin")]`

## Assumptions
- Implementing a Service Layer - You will notice that I have a Service Layer to make the code more readable and make the Controller easier to understand.
- I decided to make the database with only one table which is Employee table to make it simpler and lessen the time for development, but this doing that is a bad idea specially for real world applications.
- I still need to study more about .NET Security (Authentication and Authorization) to implement this better in the future.

## Challenges
- Lack of familiarity on the role-based authorization. To overcome this problem I've watch some video tutorials on youtube and read some documentation from Microsoft which took most of the time in the development of this application.
- Encountered some difficulties on configuration of the app specially on the jwt-token authentication.
- One of the challenges I faced was burnout, which I managed to overcome by taking regular short breaks. These breaks helped me refresh my mind, maintain focus, and approach problems with renewed energy and clearer perspective, ultimately leading to more effective solutions.
