using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;

namespace first_MinimalApi;

public static class toDoRequest
{
    public static WebApplication RegisterEndpoints(this WebApplication app)
    {
        app.MapGet("/todos", toDoRequest.GetAll)
            .Produces<List<toDo>>()
            .WithTags("To Dos")
            .RequireAuthorization();

        app.MapGet("/todos/{id}", toDoRequest.GetById)
            .Produces<toDo>()
            .Produces(StatusCodes.Status404NotFound)
            .WithTags("To Dos")
            .AllowAnonymous();

        app.MapPost("/todos", toDoRequest.Create)
            .Produces<toDo>(StatusCodes.Status201Created)
            .Accepts<toDo>("application/json")
            .WithTags("To Dos")
            .WithValidator<toDo>();

        app.MapPut("/todos/{id}", toDoRequest.Update)
            .WithValidator<toDo>()
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .Accepts<toDo>("application/json")
            .WithTags("To Dos");

        app.MapDelete("/todos/{id}", toDoRequest.Delete)
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .WithTags("To Dos")
            .ExcludeFromDescription();

        return app;
    }

    public static IResult GetAll(ItoDoService service)
    {
        var todos = service.GetAll();
        return Results.Ok(todos);
    }

    [AllowAnonymous]
    public static IResult GetById(ItoDoService service, Guid Id)
    {
        var todo = service.GetById(Id);
        if(todo == null)
        {
            return Results.NotFound();
        }
        return Results.Ok(todo);
    }

    [Authorize]
    public static IResult Create(ItoDoService service, toDo ToDo)
    {
        service.Create(ToDo);
        return Results.Created($"/todos/{ToDo.Id}", ToDo);
    }

    public static IResult Update(ItoDoService service, Guid Id, toDo ToDo)
    {
        var todo = service.GetById(Id);
        if(todo == null)
        {
            Results.NotFound();
        }
        service.Update(todo);
        return Results.NoContent();
    }   

    public static IResult Delete(ItoDoService service, Guid Id)
    {
       var todo = service.GetById(Id);
        if(todo == null)
        {
            return Results.NotFound();
        }
        service.Delete(Id);
        return Results.NoContent();
    }

}
