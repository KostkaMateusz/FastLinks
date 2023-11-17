using System.Security.Claims;

namespace FastLinks.API.Endpoints;

public class UrlLinks : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {                
        app
            .MapGroup(this)
            .RequireAuthorization()
            .MapGet("/hello", GetUserInfo);
    //.MapPost(CreateLink);
    //.MapPost(CreateTodoItem)
    //.MapPut(UpdateTodoItem, "{id}")
    //.MapPut(UpdateTodoItemDetail, "UpdateDetail/{id}")
    //.MapDelete(DeleteTodoItem, "{id}");
    }

    //app.MapGet("/", (ClaimsPrincipal user) => $"Hello {user.Identity!.Name}").RequireAuthorization();

    public string GetUserInfo(ClaimsPrincipal user)
    {
        return $"Hello {user.Identity!.Name}";
    }

    //public async Task<Guid> CreateLink(ISender sender, CreateUrlLinkCommand command)
    //{
    //    return await sender.Send(command);
    //}

    //public async Task<PaginatedList<TodoItemBriefDto>> GetTodoItemsWithPagination(ISender sender, [AsParameters] GetTodoItemsWithPaginationQuery query)
    //{
    //    return await sender.Send(query);
    //}

    //public async Task<int> CreateTodoItem(ISender sender, CreateTodoItemCommand command)
    //{
    //    return await sender.Send(command);
    //}

    //public async Task<IResult> UpdateTodoItem(ISender sender, int id, UpdateTodoItemCommand command)
    //{
    //    if (id != command.Id) return Results.BadRequest();
    //    await sender.Send(command);
    //    return Results.NoContent();
    //}

    //public async Task<IResult> UpdateTodoItemDetail(ISender sender, int id, UpdateTodoItemDetailCommand command)
    //{
    //    if (id != command.Id) return Results.BadRequest();
    //    await sender.Send(command);
    //    return Results.NoContent();
    //}

    //public async Task<IResult> DeleteTodoItem(ISender sender, int id)
    //{
    //    await sender.Send(new DeleteTodoItemCommand(id));
    //    return Results.NoContent();
    //}
}
