using EldoMvideoAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Drawing;

var builder = WebApplication.CreateBuilder(args);

string connection = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<DataBaseContext>(options => options.UseSqlServer(connection));

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

//CRUD products

app.MapGet("/products", async (DataBaseContext db) => await db.Products.ToListAsync());

app.MapGet("/products/{id}", async (int id, DataBaseContext db) =>
{
    var product = await db.Products.FindAsync(id);
    return product is not null ? Results.Ok(product) : Results.NotFound();
});

app.MapPost("/products", async (DataBaseContext db, Product product) =>
{
    db.Products.Add(product);
    await db.SaveChangesAsync();
    return Results.Created($"/products/{product.id}", product);
});

app.MapPut("/products/{id}", async (int id, DataBaseContext db, Product updateProduct) =>
{
    var product = await db.Products.FindAsync(id);
    if (product is null) return Results.NotFound();
    product.product_name = updateProduct.product_name;
    product.category_id = updateProduct.category_id;
    product.pic_link = updateProduct.pic_link;
    product.price = updateProduct.price;
    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.MapDelete("/products/{id}", async (int id, DataBaseContext db) =>
{
    var product = await db.Products.FindAsync(id);
    if (product is null) return Results.NotFound();
    db.Products.Remove(product);
    await db.SaveChangesAsync();
    return Results.Ok();
});

//CRUD category

app.MapGet("/categories", async (DataBaseContext db) => await db.Categories.ToListAsync());

app.MapGet("/categories/{id}", async (int id, DataBaseContext db) =>
{
    var category = await db.Categories.FindAsync(id);
    return category is not null ? Results.Ok(category) : Results.NotFound();
});

app.MapPost("/categories", async (DataBaseContext db, Category category) =>
{
    db.Categories.Add(category);
    await db.SaveChangesAsync();
    return Results.Created($"/categories/{category.id}", category);
});

app.MapPut("/categories/{id}", async (int id, DataBaseContext db, Category updateCategory) =>
{
    var category = await db.Categories.FindAsync(id);
    if (category is null) return Results.NotFound();
    category.category = updateCategory.category;
    category.descript = updateCategory.descript;
    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.MapDelete("/categories/{id}", async (int id, DataBaseContext db) =>
{
    var category = await db.Categories.FindAsync(id);
    if (category is null) return Results.NotFound();
    db.Categories.Remove(category);
    await db.SaveChangesAsync();
    return Results.Ok();
});

//CRUD roles

app.MapGet("/roles", async (DataBaseContext db) => await db.Roles.ToListAsync());

app.MapGet("/roles/{id}", async (int id, DataBaseContext db) =>
{
    var role = await db.Roles.FindAsync(id);
    return role is not null ? Results.Ok(role) : Results.NotFound();
});

app.MapPost("/roles", async (DataBaseContext db, Role role) =>
{
    db.Roles.Add(role);
    await db.SaveChangesAsync();
    return Results.Created($"/roles/{role.id}", role);
});

app.MapPut("/roles/{id}", async (int id, DataBaseContext db, Role updateRole) =>
{
    var role = await db.Roles.FindAsync(id);
    if (role is null) return Results.NotFound();
    role.role_name = updateRole.role_name;
    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.MapDelete("/roles/{id}", async (int id, DataBaseContext db) =>
{
    var role = await db.Roles.FindAsync(id);
    if (role is null) return Results.NotFound();
    db.Roles.Remove(role);
    await db.SaveChangesAsync();
    return Results.Ok();
});

//CRUD users

app.MapGet("/users", async (DataBaseContext db) => await db.Users.ToListAsync());

app.MapGet("/users/{id}", async (int id, DataBaseContext db) =>
{
    var user = await db.Users.FindAsync(id);
    return user is not null ? Results.Ok(user) : Results.NotFound();
});

app.MapPost("/users", async (DataBaseContext db, User user) =>
{
    db.Users.Add(user);
    await db.SaveChangesAsync();
    return Results.Created($"/users/{user.id}", user);
});

app.MapPut("/users/{id}", async (int id, DataBaseContext db, User updateUser) =>
{
    var user = await db.Users.FindAsync(id);
    if (user is null) return Results.NotFound();
    user.firstname = updateUser.firstname;
    user.midname = updateUser.midname;
    user.surname = updateUser.surname;
    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.MapDelete("/users/{id}", async (int id, DataBaseContext db) =>
{
    var user = await db.Users.FindAsync(id);
    if (user is null) return Results.NotFound();
    db.Users.Remove(user);
    await db.SaveChangesAsync();
    return Results.Ok();
});

//CRUD accounts

app.MapGet("/accounts", async (DataBaseContext db) => await db.Accounts.ToListAsync());

app.MapGet("/accounts/{id}", async (int id, DataBaseContext db) =>
{
    var account = await db.Accounts.FindAsync(id);
    return account is not null ? Results.Ok(account) : Results.NotFound();
});

app.MapPost("/accounts", async (DataBaseContext db, Account account) =>
{
    db.Accounts.Add(account);
    await db.SaveChangesAsync();
    return Results.Created($"/accounts/{account.id}", account);
});

app.MapPut("/accounts/{id}", async (int id, DataBaseContext db, Account updateAccount) =>
{
    var account = await db.Accounts.FindAsync(id);
    if (account is null) return Results.NotFound();
    account.acc_login = updateAccount.acc_login;
    account.password = updateAccount.password;
    account.role_id = updateAccount.role_id;
    account.user_id = updateAccount.user_id;
    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.MapDelete("/accounts/{id}", async (int id, DataBaseContext db) =>
{
    var account = await db.Accounts.FindAsync(id);
    if (account is null) return Results.NotFound();
    db.Accounts.Remove(account);
    await db.SaveChangesAsync();
    return Results.Ok();
});

//CRUD delivery

app.MapGet("/deliveries", async (DataBaseContext db) => await db.Deliveries.ToListAsync());
app.MapGet("/deliveries/{id}", async (DataBaseContext db, int id) =>
{
    var delivery = await db.Deliveries.FindAsync(id);
    return delivery is not null ? Results.Ok(delivery) : Results.NotFound();
});

app.MapPost("/deliveries", async (DataBaseContext db, Delivery delivery) =>
{
    db.Deliveries.Add(delivery);
    await db.SaveChangesAsync();
    return Results.Created($"/deliveries/{delivery.id}", delivery);
});

app.MapPut("/deliveries/{id}", async (DataBaseContext db, int id, Delivery updateDelivery) =>
{
    var delivery = await db.Deliveries.FindAsync(id);
    if (delivery is null) return Results.NotFound();
    delivery.address = updateDelivery.address;
    delivery.date = updateDelivery.date;
    delivery.time = updateDelivery.time;
    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.MapDelete("/deliveries/{id}", async (DataBaseContext db, int id) =>
{
    var delivery = await db.Deliveries.FindAsync(id);
    if (delivery is null) return Results.NotFound();
    db.Deliveries.Remove(delivery);
    await db.SaveChangesAsync();
    return Results.Ok();
});

//CRUD orders

app.MapGet("/orders", async (DataBaseContext db) => await db.Orders.ToListAsync());

app.MapGet("/orders/{id}", async (int id, DataBaseContext db) =>
{
    var order = await db.Orders.FindAsync(id);
    return order is not null ? Results.Ok(order) : Results.NotFound();
});

app.MapPost("/orders", async (DataBaseContext db, Order order) =>
{
    db.Orders.Add(order);
    await db.SaveChangesAsync();
    return Results.Created($"/orders/{order.id}", order);
});

app.MapPut("/orders/{id}", async (int id, DataBaseContext db, Order updateOrder) =>
{
    var order = await db.Orders.FindAsync(id);
    if (order is null) return Results.NotFound();
    order.account_id = updateOrder.account_id;
    order.delivery_id = updateOrder.delivery_id;
    order.date = updateOrder.date;
    order.sum = updateOrder.sum;
    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.MapDelete("/orders/{id}", async (int id, DataBaseContext db) =>
{
    var order = await db.Orders.FindAsync(id);
    if (order is null) return Results.NotFound();
    db.Orders.Remove(order);
    await db.SaveChangesAsync();
    return Results.Ok();
});

//CRUD productorders

app.MapGet("/productorders", async (DataBaseContext db) => await db.ProductOrders.ToListAsync());

app.MapGet("/productorders/{id}", async (int id, DataBaseContext db) =>
{
    var productOrder = await db.ProductOrders.FindAsync(id);
    return productOrder is not null ? Results.Ok(productOrder) : Results.NotFound();
});

app.MapPost("/productorders", async (DataBaseContext db, ProductOrder productOrder) =>
{
    db.ProductOrders.Add(productOrder);
    await db.SaveChangesAsync();
    return Results.Created($"/productorders/{productOrder.id}", productOrder);
});

app.MapPut("/productorders/{id}", async (int id, DataBaseContext db, ProductOrder updateProductOrder) =>
{
    var productOrder = await db.ProductOrders.FindAsync(id);
    if (productOrder is null) return Results.NotFound();
    productOrder.product_id = updateProductOrder.product_id;
    productOrder.order_id = updateProductOrder.order_id;
    productOrder.quantity = updateProductOrder.quantity;
    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.MapDelete("/productorders/{id}", async (int id, DataBaseContext db) =>
{
    var productOrder = await db.ProductOrders.FindAsync(id);
    if (productOrder is null) return Results.NotFound();
    db.ProductOrders.Remove(productOrder);
    await db.SaveChangesAsync();
    return Results.Ok();
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
