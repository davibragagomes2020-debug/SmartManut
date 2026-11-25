using Microsoft.EntityFrameworkCore;
using SmartManut.API.Data;
using SmartManut.API.Models;

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

var builder = WebApplication.CreateBuilder(args);

var connectionString =
    builder.Configuration.GetConnectionString("DefaultConnection")
    ?? Environment.GetEnvironmentVariable("DATABASE_URL")
    ?? "Host=ep-royal-mud-bd8q4c2m-pooler.c-8.us-east-2.aws.neon.tech;Database=neondb;Username=neondb_owner;Password=npg_IAl5T2QXUxvC;SSL Mode=Require;Trust Server Certificate=true";

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirTudo", policy =>
    {
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();

    if (!db.Perfis.Any())
    {
        db.Perfis.Add(new Perfil { NomeCargo = "Administrador", Permissoes = "Todas" });
        db.SaveChanges();
    }

    if (!db.Utilizadores.Any())
    {
        var perfilId = db.Perfis.First().Id;
        db.Utilizadores.Add(new Utilizador { Nome = "Administrador Sistema", Email = "admin@smartmanut.com", SenhaHash = "123456", Ativo = true, PerfilId = perfilId });
        db.SaveChanges();
    }

    if (!db.Setores.Any())
    {
        db.Setores.Add(new Setor { NomeSetor = "Produção Geral", Localizacao = "Bloco A" });
        db.SaveChanges();
    }

    if (!db.Equipamentos.Any())
    {
        var setorId = db.Setores.First().Id;
        db.Equipamentos.Add(new Equipamento { Codigo = "EQ-00", Nome = "Equipamento Padrão", Status = "Ativo", SetorId = setorId });
        db.SaveChanges();
    }
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("PermitirTudo");
app.UseDefaultFiles();
app.UseStaticFiles();
app.UseAuthorization();
app.MapControllers();

app.Run();
