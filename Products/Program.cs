using Microsoft.EntityFrameworkCore;
using Products.Data;
using Products.Services.Client;
using Products.Services.Product;
using FluentValidation;
using Products.Validation;
using Products.DTO.InputDTO;
using Products.Models.UpdateModels;
using Products.Services;
using Products.Services.Buy;
using Products.Services.PaymentTerms;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
// Add services to the container.
builder.Services.AddTransient<IValidator<ProductInput>, ProductValidation<ProductInput>>();
builder.Services.AddTransient<IValidator<ProductUpdate>, ProductValidation<ProductUpdate>>();
builder.Services.AddTransient<IValidator<PurchasesInput>, PurchasesValidation<PurchasesInput>>();
builder.Services.AddTransient<IValidator<PurchasesUpdate>, PurchasesValidation<PurchasesUpdate>>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IPurchasesService, PurchasesService>();
builder.Services.AddScoped<IPaymentTermsService, PaymentTermsService>();

builder.Services.AddDbContext<DataContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("Products"));
});

var app = builder.Build();
using (var scope = app.Services.CreateScope()) {
    var services = scope.ServiceProvider;
    try {
        if (configuration["EF-MIGRATE"] == "true") {
            var context = services.GetRequiredService<DataContext>();            
            context.Database.Migrate();
            return;
        }
    }
    catch (Exception ex) {
        throw;
    }
}

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
