namespace Xpenses.Tests.UnitTests.RepositoryTests;

public class ExpenseRepositoryTests
{
    [Fact]
    public async Task GetAllAsync_ReturnsAllExpenses()
    {
        // Arrange
        var expenses = new List<Expense>
            {
                new Expense
                {
                    Id = Guid.NewGuid(),
                    Description = "Expense 1",
                    Amount = 100,
                    Date = DateTime.UtcNow
                },
                new Expense
                {
                    Id = Guid.NewGuid(),
                    Description = "Expense 2",
                    Amount = 200,
                    Date = DateTime.UtcNow
                }
            };

        var context = GetInMemoryDbContext();
        await context.Expenses.AddRangeAsync(expenses);
        await context.SaveChangesAsync();

        var repository = new ExpenseRepository(context);

        // Act
        var result = await repository.GetAllAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsExpense_WhenIdExists()
    {
        // Arrange
        var expense = new Expense
        {
            Id = Guid.NewGuid(),
            Description = "Expense 1",
            Amount = 100,
            Date = DateTime.UtcNow
        };

        var context = GetInMemoryDbContext();
        await context.Expenses.AddAsync(expense);
        await context.SaveChangesAsync();

        var repository = new ExpenseRepository(context);

        // Act
        var result = await repository.GetByIdAsync(expense.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expense.Id, result.Id);
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsNull_WhenIdDoesNotExist()
    {
        // Arrange
        var context = GetInMemoryDbContext();
        var repository = new ExpenseRepository(context);

        // Act
        var result = await repository.GetByIdAsync(Guid.NewGuid());

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task UpdateAsync_UpdatesExpense_WhenExpenseExists()
    {
        // Arrange
        var expense = new Expense 
        { 
            Id = Guid.NewGuid(), 
            Description = "Original Expense", 
            Amount = 100, 
            Date = DateTime.UtcNow 
        };

        var context = GetInMemoryDbContext();
        await context.Expenses.AddAsync(expense);
        await context.SaveChangesAsync();

        var repository = new ExpenseRepository(context);

        // Update the existing expense instance
        expense.Description = "Updated Expense";
        expense.Amount = 150;
        expense.Date = DateTime.UtcNow;

        // Act
        await repository.UpdateAsync(expense);
        var result = await repository.GetByIdAsync(expense.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expense.Description, result.Description);
        Assert.Equal(expense.Amount, result.Amount);
    }

    [Fact]
    public async Task UpdateAsync_DoesNotUpdateExpense_WhenExpenseDoesNotExist()
    {
        // Arrange
        var context = GetInMemoryDbContext();
        var repository = new ExpenseRepository(context);

        var nonExistingExpense = new Expense
        {
            Id = Guid.NewGuid(),
            Description = "Non-existing Expense",
            Amount = 100,
            Date = DateTime.UtcNow
        };

        // Act
        await repository.UpdateAsync(nonExistingExpense);
        var result = await repository.GetByIdAsync(nonExistingExpense.Id);

        // Assert
        Assert.Null(result);
    }

    private XpensesDbContext GetInMemoryDbContext()
    {
        var options = new DbContextOptionsBuilder<XpensesDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new XpensesDbContext(options);
    }
}

