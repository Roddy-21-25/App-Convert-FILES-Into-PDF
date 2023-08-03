using AplicationDomainLayer___PizzaDay.Entities;
using AplicationDomainLayer___PizzaDay.Interfaces;
using AplicationDomainLayer___PizzaDay.Services;
using InfrastructureLayer___PizzaDay.Data;
using InfrastructureLayer___PizzaDay.Filters;
using InfrastructureLayer___PizzaDay.PasswordOptions;
using InfrastructureLayer___PizzaDay.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PresentationLayer___PizzaDay.ControllerOrder;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Exceptions Controller
builder.Services.AddControllers(
    options =>
    {
        options.Filters.Add<GlobalFilterBusinessExceptions>();
    }).AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
    });

// Uri Context
builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<IUrlServices>(provider =>
{
    var _HttpContext = provider.GetRequiredService<IHttpContextAccessor>();

    var request = _HttpContext.HttpContext.Request;

    var HostUrl = string.Concat(request.Scheme, "://", request.Host.ToUriComponent());

    return new UrlServices(HostUrl);
});

// Pagination Default Options
builder.Services.Configure<PaginationDefaultOptions>(builder.Configuration.GetSection("Pagination"));

// SQL PATH
builder.Services.AddDbContext<PIZZAContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("PizzaPathSql")
));

// DI BaseRepository
builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));

// Unit of Work DI
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();

// Mapping
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// PizzaServices DI
builder.Services.AddTransient<IPizzaServices, PizzaServices>();

// GetByRepository DI
builder.Services.AddTransient<IGetByRepository, GetByRepository>();

// GetByServices DI
builder.Services.AddTransient<IGetByServices, GetByServices>();

// SignUp Repository DI
builder.Services.AddTransient<ISignUpRepository, SignUpRepository>();

// SignUp Services DI
builder.Services.AddTransient<ISignUpServices, SignUpServices>();

// Password Options
builder.Services.Configure<PasswordOptionsValues>(builder.Configuration.GetSection("PasswordOptions"));

// Password Repository DI
builder.Services.AddSingleton<IPasswordHasher, PasswordHasherRepository>();

// CONTROLLER ORDERs
SwaggerControllerOrder<ControllerBase> swaggerControllerOrder = new SwaggerControllerOrder<ControllerBase>(Assembly.GetEntryAssembly());

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.OrderActionsBy((apiDesc) => $"{swaggerControllerOrder.SortKey(apiDesc.ActionDescriptor.RouteValues["controller"])}");

    // Documentation Config
    c.SwaggerDoc("v1", new OpenApiInfo 
    {
        Title = "Pizza Day Api",
        Version = "v1",
        Description = "This Api is a one of my Practice about how to create a Full RestFull API, in ASP.NET CORE 6 with C# and Clean Architecture. " +
                          "\n\nFollow these steps:\n" +
                          "1 - Create a Chef User in the Sign Up Method.\n" +
                          "2 - Login in the Login Method with the same username and password that you used to create a Chef User.\n" +
                          "3 - Test all the Methods.\n\n" +
                          "Here is my contact information:\n" +
                          "GitHub: [Roddy-21-25](https://github.com/Roddy-21-25)\n" +
                          "LinkedIn: [Roddy 2125 Rafael](https://www.linkedin.com/in/roddy-2125-rafael/)\n" +
                          "Email: Roddy3889@gmail.com",

        //github  : https://github.com/Roddy-21-25
        Contact = new OpenApiContact
        {
            Name = "Linkeding",
            Email = "Roddy3889@gmail.com",
            Url = new Uri("https://www.linkedin.com/in/roddy-2125-rafael/")
        }
});

    // Documentation Activator
    var DocToXmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, DocToXmlFile);
    c.IncludeXmlComments(xmlPath);
    
    // Authorization Button
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Get the token in SignUp EndPoint and Insert Here",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});
// JWT 
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,

        ValidIssuer = builder.Configuration["Authentication:Issuer"],
        ValidAudience = builder.Configuration["Authentication:Audience"],

        IssuerSigningKey = new SymmetricSecurityKey(
        Encoding.UTF8.GetBytes(
        builder.Configuration["Authentication:SecretKey"]))
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{

//}
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Pizza Day Api");
});

app.UseDeveloperExceptionPage();

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
