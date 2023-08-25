using Microsoft.EntityFrameworkCore;
using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Hackaton2.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder();
builder.Services.AddCors();
//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("AllowAllOrigins",
//        builder => builder.WithOrigins("*")
//    );

//});
string connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ModelBase>(options => options.UseSqlServer(connection));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});
var app = builder.Build();

app.UseCors(builder => builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader());



app.UseDefaultFiles();
app.UseStaticFiles();
app.UseCors("AllowAll");

//category

app.MapGet("/api/category/", async delegate (HttpContext context, ModelBase db)
{
    return db.Category.ToList();
    //QueryString queryString = context.Request.QueryString;

    //return queryString.HasValue
    //    ? Helper.FilterTasks(db, queryString.Value == "?completed=true")
    //    : db.Tasks.ToList();
}
).RequireCors(options => options.AllowAnyOrigin());

app.MapGet("/api/category/{id}", async delegate (int id, HttpContext context, ModelBase db)
{
    Category? resultFind = db.Category.FirstOrDefault(task => task.Id == id);

    if(resultFind == null)
    {
        context.Response.StatusCode = 404;
    }

    return resultFind;
    //QueryString queryString = context.Request.QueryString;

    //return queryString.HasValue
    //    ? Helper.FilterTasks(db, queryString.Value == "?completed=true")
    //    : db.Tasks.ToList();
}
).RequireCors(options => options.AllowAnyOrigin());

app.MapPost("/api/category", async delegate (HttpContext context, ModelBase modelBase)
{
    Category categoryToDB; 
    try
    {
        categoryToDB = await context.Request.ReadFromJsonAsync<Category>();
        if (categoryToDB != null)
        {
            await modelBase.Category.AddAsync(categoryToDB);
            await modelBase.SaveChangesAsync();
            context.Response.StatusCode = 201;
            return Results.Json(categoryToDB);
        }
        context.Response.StatusCode = 400;
    }
    catch
    {
        context.Response.StatusCode = 400;
    }
    return null;
});

app.MapPatch("/api/category/{id}", async delegate (int id, HttpContext context, ModelBase modelBase)
{
    Category? currentTask = modelBase.Category.FirstOrDefault(task => task.Id == id);
    Category? request = await context.Request.ReadFromJsonAsync<Category>();

    if (request != null && currentTask != null)
    {
        if (request.NameCategory != null)
        {
            currentTask.NameCategory = request.NameCategory;
        }

        await modelBase.SaveChangesAsync();
        context.Response.StatusCode = 201;
    }
    else
        context.Response.StatusCode = 400;

    return currentTask;
});

app.MapDelete("/api/category/{id}", async delegate (int id, HttpContext context, ModelBase modelBase)
{
    Category? currentTask = modelBase.Category.FirstOrDefault(u => u.Id == id);

    if (currentTask != null)
    {
        modelBase.Category.Remove(currentTask);
        await modelBase.SaveChangesAsync();
        return Results.StatusCode(200);
    }
    else
        return Results.StatusCode(204);
});



//subcategory


app.MapGet("/api/category/subCategory", async delegate (HttpContext context, ModelBase db)
{
    return db.SubCategory.ToList();
    //QueryString queryString = context.Request.QueryString;

    //return queryString.HasValue
    //    ? Helper.FilterTasks(db, queryString.Value == "?completed=true")
    //    : db.Tasks.ToList();
}
).RequireCors(options => options.AllowAnyOrigin());

app.MapGet("/api/category/subCategory/{id}", async delegate (int id, HttpContext context, ModelBase db)
{
    SubCategory? resultFind = db.SubCategory.FirstOrDefault(task => task.Id == id);

    if (resultFind == null)
    {
        context.Response.StatusCode = 404;
    }

    return resultFind;
    //QueryString queryString = context.Request.QueryString;

    //return queryString.HasValue
    //    ? Helper.FilterTasks(db, queryString.Value == "?completed=true")
    //    : db.Tasks.ToList();
}
).RequireCors(options => options.AllowAnyOrigin());

app.MapPost("/api/category/subCategory", async delegate (HttpContext context, ModelBase modelBase)
{
    SubCategory? subCategoryToDB;
    try
    {
        subCategoryToDB = await context.Request.ReadFromJsonAsync<SubCategory>();
        if (subCategoryToDB != null)
        {
          //  subCategoryToDB.Category = modelBase.Category.FirstOrDefault(task => task.Id == 3);
            await modelBase.SubCategory.AddAsync(subCategoryToDB);
            await modelBase.SaveChangesAsync();
            context.Response.StatusCode = 201;
            return Results.Json(subCategoryToDB);
        }
        context.Response.StatusCode = 400;
    }
    catch
    {
        context.Response.StatusCode = 400;
    }
    return null;
});

app.MapPatch("/api/category/subCategory/{id}", async delegate (int id, HttpContext context, ModelBase modelBase)
{
    SubCategory? currentTask = modelBase.SubCategory.FirstOrDefault(task => task.Id == id);
    SubCategory? request = await context.Request.ReadFromJsonAsync<SubCategory>();

    if (request != null && currentTask != null)
    {
        if (request.CategoryId != null)
        {
            currentTask.CategoryId = request.CategoryId;
        }

        if (request.name != null)
        {
            currentTask.name = request.name;
        }

        await modelBase.SaveChangesAsync();
        context.Response.StatusCode = 201;
    }
    else
        context.Response.StatusCode = 400;

    return currentTask;
});

app.MapDelete("/api/category/subCategory/{id}", async delegate (int id, HttpContext context, ModelBase modelBase)
{
    SubCategory? currentTask = modelBase.SubCategory.FirstOrDefault(u => u.Id == id);

    if (currentTask != null)
    {
        modelBase.SubCategory.Remove(currentTask);
        await modelBase.SaveChangesAsync();
        return Results.StatusCode(200);
    }
    else
        return Results.StatusCode(204);
});



//Sight
app.MapGet("/api/category/subCategory/sight", async delegate (HttpContext context, ModelBase db)
{
    QueryString queryString = context.Request.QueryString;

    if(queryString.HasValue)
    {
        string? category = context.Request.Query["category"];
        string? subcategory = context.Request.Query["subcategory"];

        if(category == null && subcategory == null)
        {
            context.Response.StatusCode = 400;
            return null;
        }

        if(category != null && subcategory != null)
        {
            return db.Sight.Where(p => p.subCategory == subcategory && p.category == category).ToList();
        }
        else if(category != null)
        {
            return db.Sight.Where(p => p.category == category).ToList();
        }
        else
        {
            return db.Sight.Where(p => p.subCategory == subcategory).ToList();
        }
    }
    else
        return db.Sight.ToList();
});

app.MapGet("/api/category/subCategory/sight/{id}", async delegate (double id, HttpContext context, ModelBase db)
{
    Sight? resultFind = db.Sight.FirstOrDefault(task => task.Id == id);

    if (resultFind == null)
    {
        context.Response.StatusCode = 404;
    }

    return resultFind;
}
).RequireCors(options => options.AllowAnyOrigin());

app.MapPost("/api/category/subCategory/sight/", async delegate (HttpContext context, ModelBase modelBase)
{
    Sight? sightToDB;
    try
    {
        sightToDB = await context.Request.ReadFromJsonAsync<Sight>();
        if (sightToDB != null)
        {
            //  subCategoryToDB.Category = modelBase.Category.FirstOrDefault(task => task.Id == 3);
            await modelBase.Sight.AddAsync(sightToDB);
            await modelBase.SaveChangesAsync();
            context.Response.StatusCode = 201;
            return Results.Json(sightToDB);
        }
        context.Response.StatusCode = 400;
    }
    catch
    {
        context.Response.StatusCode = 400;
    }
    return null;
});

app.MapPost("/api/category/subCategory/sights/", async delegate (HttpContext context, ModelBase modelBase)
{
    List<Sight>? sightToDB;
    try
    {
        sightToDB = await context.Request.ReadFromJsonAsync<List<Sight>>();
        if (sightToDB != null)
        {
            //  subCategoryToDB.Category = modelBase.Category.FirstOrDefault(task => task.Id == 3);
            foreach(var item in sightToDB)
            {
                await modelBase.Sight.AddAsync(item);
            }
            await modelBase.SaveChangesAsync();
            context.Response.StatusCode = 201;
            return Results.Json(sightToDB);
        }
        context.Response.StatusCode = 400;
    }
    catch
    {
        context.Response.StatusCode = 400;
    }
    return null;
});

app.MapPatch("/api/category/subCategory/sight/{id}", async delegate (int id, HttpContext context, ModelBase modelBase)
{
    Sight? currentTask = modelBase.Sight.FirstOrDefault(task => task.Id == id);
    Sight? request = await context.Request.ReadFromJsonAsync<Sight>();

    if (request != null && currentTask != null)
    {
        if (request.rating != null)
        {
            currentTask.rating = request.rating;
        }

        if (request.name != null)
        {
            currentTask.name = request.name;
        }
        if (request.category != null)
        {
            currentTask.category = request.category;
        }

        if (request.subCategory != null)
        {
            currentTask.subCategory = request.subCategory;
        }
        if (request.CoordinateX != null)
        {
            currentTask.CoordinateX = request.CoordinateX;
        }

        if (request.CoordinateY != null)
        {
            currentTask.CoordinateY = request.CoordinateY;
        }

        await modelBase.SaveChangesAsync();
        context.Response.StatusCode = 201;
    }
    else
        context.Response.StatusCode = 400;

    return currentTask;
});

app.MapDelete("/api/category/subCategory/sight/{id}", async delegate (int id, HttpContext context, ModelBase modelBase)
{
    Sight? currentTask = modelBase.Sight.FirstOrDefault(u => u.Id == id);

    if (currentTask != null)
    {
        modelBase.Sight.Remove(currentTask);
        await modelBase.SaveChangesAsync();
        return Results.StatusCode(200);
    }
    else
        return Results.StatusCode(204);
});


//Random

app.MapGet("/api/random/",  delegate  (ModelBase db)
{
    Random rnd = new Random();

    List<Sight> result = new List<Sight>();

    for(int i = 0; i < 4; i++)
    {
        int rndNumber = rnd.Next() % db.Sight.Count<Sight>() + 2;
       result.Add(db.Sight.FirstOrDefault(task => task.Id == rndNumber));
    }

    return result;
}
);

app.Run();
