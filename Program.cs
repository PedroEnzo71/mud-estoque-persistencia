using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;
using MudEstoquePersistenciaDados.Components;
using MudEstoquePersistenciaDados.Data;
using MudEstoquePersistenciaDados.Services.Implementations;
using MudEstoquePersistenciaDados.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Program.cs: antes de AddRazorComponents
// Fábrica de DbContext: um contexto curto por operação
builder.Services.AddDbContextFactory<EstoqueDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add MudBlazor services
builder.Services.AddMudServices();

// Registro do servicos de domínio (interface > implementacao)
builder.Services.AddScoped<ICategoriaService, CategoriaService>();
builder.Services.AddScoped<IFornecedorService, FornecedorService>();
builder.Services.AddScoped<IProdutoService, ProdutoService>();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();


