using Capstone.EndUser.GraphQl;
using Capstone.Shared;
using Common.Shared.Utils;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCapstoneHttpClient();

builder.Services.AddControllers().AddJsonOptions(o =>
{
    o.JsonSerializerOptions.Converters.Add(new TimeOnlyJsonConverter());
    o.JsonSerializerOptions.Converters.Add(new DateOnlyConverter());
});

builder.Services.AddGraphQLServer().AddQueryType<Query>().AddMutationType<Mutation>().UseField<ExceptionMiddleware>();
var app = builder.Build();

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
app.UseSwagger();
app.UseSwaggerUI();
// }

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapGraphQL();
app.Run();