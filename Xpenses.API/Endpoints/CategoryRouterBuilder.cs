using Xpenses.Application.Common.Interfaces.Persistence;
using Xpenses.Domain.Entities;

public static class CategoryRouterBuilder
{
    public static void BuildCategoryEndpoints(this WebApplication app)
    {
        app.MapGet("/categories", GetAllCategories);
        app.MapGet("/categories/{id}", GetCategoryById);
        app.MapPost("/categories/{id}", CreateNewCategory);
        app.MapPut("/categories/{id}", UpdateCategory);
        app.MapDelete("/categories/{id}", DeleteCategory);
    }

    internal static IResult GetAllCategories(ICategoryRepository repo)
    {
        return Results.Ok(repo.GetAllAsync());
    }

    internal static async Task<IResult> GetCategoryById(ICategoryRepository repo, Guid id)
    {
        Category category = await repo.GetByIdAsync(id);
        return category is not null
        ? Results.Ok(category)
        : Results.NotFound();
    }

    internal static IResult CreateNewCategory(ICategoryRepository repo, Category category)
    {
        repo.AddAsync(category);
        return Results.Created($"/categories/{category.Id}", category);
    }

    internal static async Task<IResult> UpdateCategory(ICategoryRepository repo, Guid id, Category updatedValue)
    {
        var category = await repo.GetByIdAsync(id);
        if (category is null)
        {
            return Results.NotFound();
        }
        await repo.UpdateAsync(updatedValue);
        return Results.Ok(updatedValue);
    }

    internal static async Task<IResult> DeleteCategory(ICategoryRepository repo, Guid id)
    {
        await repo.DeleteAsync(id);
        return Results.Ok();
    }
}