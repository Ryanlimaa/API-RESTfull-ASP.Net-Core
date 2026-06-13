using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;
using CadastroProdutos.Database;
using CadastroProdutos.Models;
using CadastroProdutos.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Adicionando métodos para trabalhar com controllers
builder.Services.AddControllers();

// Adicionando métodos para adicionar Endpoints depois de instalar o Swagger
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(x =>
{
    x.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme()
    {
        Description = @"Insira o JWT no campo a baixo seguindo o seguinte formato: Bearer {seu_token}.",
        Name = "Authorization",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    x.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement()
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = Microsoft.OpenApi.Models.ParameterLocation.Header
            },
            new List<string>()
        }
    });
});

// Registrando a interface para injeção de dependência
// builder.Services.AddScoped<IProdutosService, ProdutosService>();
builder.Services.AddScoped<IProdutosService, ProdutosDatabaseService>();

// Configurando a string de conexão para o banco de dados MySQL, obtendo-a do appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DataBase");

// Registrando o repositório de usuário e tarefas para injeção de dependência, permitindo que ele seja utilizado em outras partes da aplicação, como nos controladores, para acessar os dados no banco de dados
builder.Services.AddDbContext<ApplicationDbContext>(options => 
options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

var jwtConfig = builder.Configuration.GetSection("Jwt");
var key = Encoding.ASCII.GetBytes(jwtConfig["Key"]);
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtConfig["Issuer"],
        ValidAudience = jwtConfig["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});

var app = builder.Build();

// Adicionando mapeamento para controllers
app.MapControllers();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    // Adicionando a configuração Swagger para uma melhor visualização no navegador
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/test", () => "Esse é um endpoint de teste");

var produtos = new List<Produto>()
{
    new Produto() {Id = 1, Nome = "Mouse sem Fio", Preco = 99.90, Estoque = 50},
    new Produto() {Id = 2, Nome = "Telcado", Preco = 249.90, Estoque = 30}
};

// Método para listar os produtos
app.MapGet("/produtos", () =>
{
    return produtos;
});

// Método para buscar os produtos por id
app.MapGet("/produtos/{id}", (int id) =>
{
    var produto = produtos.FirstOrDefault(x => x.Id == id);

    return produto is not null
        ? Results.Ok(produto)
        : Results.NotFound($"Produto com ID {id} não encontrado.");
});

// Método para inserir um novo produto
app.MapPost("/produtos", (Produto novoProduto) => 
{
    produtos.Add(novoProduto);
    return Results.Ok("Produto criado com sucesso");
});

// Método para atualizar um produto
app.MapPut("/produtos/{id}", (int id, Produto prodAtualizado) =>
{
    var produto = produtos.FirstOrDefault(x => x.Id == id);

    if(produto is null)
    {
        return Results.NotFound($"Produto com ID {id} não encontrado.");
    }

    produto.Nome = prodAtualizado.Nome;
    produto.Preco = prodAtualizado.Preco;
    produto.Estoque = prodAtualizado.Estoque;

    return Results.Ok(produto);
});

// Método para excluir um produto
app.MapDelete("/produtos/{id}", (int id) =>
{
    var produto = produtos.FirstOrDefault(x => x.Id == id);

    if (produto is null)
    {
        return Results.NotFound($"Produto com ID {id} não encontrado.");
    }

    produtos.Remove(produto);

    return Results.Ok("Produto excluido com sucesso!");
});

app.Run();
