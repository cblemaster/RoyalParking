using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RoyalParking.Core.DTO;
using RoyalParking.Core.Entities;
using RoyalParking.Core.Mappers;
using RoyalParking.Core.Validation;
using RoyalParking.Core.Validation.DTOValidation;
using RoyalParking.Security;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using DbContext = RoyalParking.API.DataContext.RoyalParkingContext;

var builder = WebApplication.CreateBuilder(args);

IConfigurationRoot config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
            .Build();

string connectionString = config.GetConnectionString("Project") ?? "Error retrieving connection string!";
string jwtSecret = config.GetValue<string>("JwtSecret") ?? "Error retreiving jwt config!";

byte[] key = Encoding.ASCII.GetBytes(jwtSecret);
JwtSecurityTokenHandler.DefaultInboundClaimTypeMap[JwtRegisteredClaimNames.Sub] = "sub";

builder.Services.AddAuthentication(x =>
    {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
.AddJwtBearer(x =>
    {
        x.RequireHttpsMetadata = false;
        x.SaveToken = true;
        x.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false,
            NameClaimType = "name"
        };
    })
.Services.AddAuthorizationBuilder()
    .AddPolicy("customerpolicy", policy => policy.RequireRole("customer"))
    .AddPolicy("valetpolicy", policy => policy.RequireRole("valet"))
    .AddPolicy("allrolespolicy", policy => policy.RequireRole("customer", "valet"))
.Services.AddDbContext<DbContext>(options =>
    options.UseSqlServer(connectionString))
.AddSingleton<ITokenGenerator>(tk => new JwtGenerator(jwtSecret))
.AddSingleton<IPasswordHasher>(ph => new PasswordHasher());

var app = builder.Build();

app.MapGet("/", () => "Welcome to Royal Parking!");

app.MapPost("/user/register", async Task<Results<BadRequest<string>,
    Created<UserDTO>, ProblemHttpResult>> (DbContext context, RegisterUserDTO dto,
    IPasswordHasher passwordHasher) =>
{
    // validate the dto
    IEnumerable<ValidationResult> validationResults =
        ValidateRegisterUserDTO.Validate(dto);
    if (validationResults.Any(v => !v.IsValid))
    {
        return TypedResults.BadRequest(GetErrorsAsString(validationResults));
    }
    // hash the password
    PasswordHash hash = passwordHasher.ComputeHash(dto.Password);

    // create the user entity
    User createUser = DTOToEntity.MapRegisterUserDTOToUser(dto);
    createUser.PasswordHash = hash.Password;
    createUser.Salt = hash.Salt;
    createUser.CreateDate = DateTime.Now;

    if (dto.Role == "customer") { createUser.Customer = new(); }
    else if (dto.Role == "valet") { createUser.Valet = new(); }

    // add the user entity to the db
    try
    {
        context.Users.Add(createUser);
        await context.SaveChangesAsync();

        return TypedResults.Created($"/user/{createUser.Id}",
            EntityToDTO.MapUserToUserDTO(await context.
                Users.Include(u => u.Customer).Include(u => u.Valet)
                .SingleOrDefaultAsync(u => u.Id == createUser.Id) ?? new()));
    }
    catch (Exception)
    {
        return TypedResults.Problem("Error registering the new user.");
    }
});

app.MapPost("/user/login", async Task<Results<BadRequest<string>,
    UnauthorizedHttpResult, Ok<UserDTO>>> (DbContext context, LoginUserDTO loginDTO,
    IPasswordHasher passwordHasher, ITokenGenerator tokenGenerator) =>
{
    // validate the dto
    IEnumerable<ValidationResult> validationResults =
        ValidateLoginUserDTO.Validate(loginDTO);
    if (validationResults.Any(v => !v.IsValid))
    {
        return TypedResults.BadRequest(GetErrorsAsString(validationResults));
    }

    // look for a matching user by username
    User existingUser = (await context.Users
        .Include(u => u.Customer).Include(u => u.Valet)
        .SingleOrDefaultAsync(u => u.Username == loginDTO.Username))!;

    if (existingUser == null)
    {
        return TypedResults.Unauthorized();
    }

    if (!passwordHasher.VerifyHashMatch(existingUser.PasswordHash,
        loginDTO.Password, existingUser.Salt))
    {
        return TypedResults.Unauthorized();
    }
    else
    {
        string token = tokenGenerator.GenerateToken(existingUser.Id,
            existingUser.Username,
            existingUser.Customer is not null ? "customer"
            : (existingUser.Valet is not null ? "valet" : "unknown role"));

        UserDTO authUserDTO = EntityToDTO.MapUserToUserDTO(existingUser);

        authUserDTO.Token = token;

        return TypedResults.Ok(authUserDTO);
    }
});

app.MapGet("/user/{id:int}", async Task<Results<NotFound<string>, Ok<UserDTO>>>
    (DbContext context, int id) => await context.Users
        .Include(u => u.Customer).Include(u => u.Valet)
        .SingleOrDefaultAsync(u => u.Id == id) is User user
            ? TypedResults.Ok(EntityToDTO.MapUserToUserDTO(user))
            : TypedResults.NotFound($"User with id {id} not found."));

app.MapGet("user", Ok<IEnumerable<UserDTO>> (DbContext context) =>
    TypedResults.Ok(EntityToDTO.CollectionMapUserToUserDTO(
        (context.Users.Include(u => u.Customer).Include(u => u.Valet)))));

string GetErrorsAsString(IEnumerable<ValidationResult> validationResults)
{
    StringBuilder sb = new();
    validationResults.ToList().ForEach(vr => sb.AppendLine(vr.ErrorMessage));
    return sb.ToString();
}

app.Run();
