using Core.Domain.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TaskManager.Core.Domain.Entities;
using TaskManager.Core.Domain.Enums;
using TaskManager.Infaestructure.Persistence.Context;
using TaskManager.Infaestructure.Persistence.Repositories;
using Xunit;

namespace TaskManagerAPI.Test.UnitTesting
{
    public class TaskItemRepositoryTests
    {
        private List<TaskItem> GetMockTaskItems()
        {
            return new List<TaskItem>
            {
                new TaskItem { Id = 1, Description = "Tarea 1", Status = StatusTask.Pending, Type = TaskType.BugFix },
                new TaskItem { Id = 2, Description = "Tarea 2", Status = StatusTask.Completed, Type = TaskType.Feature },
                new TaskItem { Id = 3, Description = "Tarea 3", Status = StatusTask.Pending, Type = TaskType.Refactor }
            };
        }

        private ApplicationContext CreateDbContextWithMockData()
        {
            var options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Base de datos única por prueba
                .Options;

            var context = new ApplicationContext(options);
            context.Tasks.AddRange(GetMockTaskItems());
            context.SaveChanges();

            return context;
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnFilteredTasks_WhenQueryHasFilters()
        {
            var context = CreateDbContextWithMockData();
            var repository = new TaskItemRepository(context);

            var query = new GetAllTaskQuery
            {
                StatusTask = StatusTask.Pending,
                TaskType = TaskType.Feature
            };

            var result = await repository.GetAllAsync(query, CancellationToken.None);

            Assert.All(result, r =>
            {
                Assert.Equal(StatusTask.Pending, r.Status);
                Assert.Equal(TaskType.Feature, r.Type);
            });
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllTasks_WhenQueryHasNoFilters()
        {
            var context = CreateDbContextWithMockData();
            var repository = new TaskItemRepository(context);

            var query = new GetAllTaskQuery();

            var result = await repository.GetAllAsync(query, CancellationToken.None);

            Assert.Equal(3, result.Count());
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnTasksMatchingOnlyStatusFilter()
        {
            var context = CreateDbContextWithMockData();
            var repository = new TaskItemRepository(context);

            var query = new GetAllTaskQuery { StatusTask = StatusTask.Pending };

            var result = await repository.GetAllAsync(query, CancellationToken.None);

            Assert.Equal(2, result.Count());
            Assert.All(result, r => Assert.Equal(StatusTask.Pending, r.Status));
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnTasksMatchingOnlyTaskTypeFilter()
        {
            var context = CreateDbContextWithMockData();
            var repository = new TaskItemRepository(context);

            var query = new GetAllTaskQuery { TaskType = TaskType.Feature };

            var result = await repository.GetAllAsync(query, CancellationToken.None);

            Assert.Single(result);
            Assert.All(result, r => Assert.Equal(TaskType.Feature, r.Type));
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnEmpty_WhenNoTaskMatchesStatusFilter()
        {
            var context = CreateDbContextWithMockData();
            var repository = new TaskItemRepository(context);

            var query = new GetAllTaskQuery { StatusTask = (StatusTask)99 };

            var result = await repository.GetAllAsync(query, CancellationToken.None);

            Assert.Empty(result);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnEmpty_WhenNoTaskMatchesTypeFilter()
        {
            var context = CreateDbContextWithMockData();
            var repository = new TaskItemRepository(context);

            var query = new GetAllTaskQuery { TaskType = (TaskType)99 };

            var result = await repository.GetAllAsync(query, CancellationToken.None);

            Assert.Empty(result);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnEmpty_WhenNoTaskMatchesBothFilters()
        {
            var context = CreateDbContextWithMockData();
            var repository = new TaskItemRepository(context);

            var query = new GetAllTaskQuery
            {
                StatusTask = StatusTask.Completed,
                TaskType = TaskType.BugFix
            };

            var result = await repository.GetAllAsync(query, CancellationToken.None);

            Assert.Empty(result);
        }

        [Fact]
        public async Task GetAllAsync_ShouldNotThrow_WhenQueryIsEmpty()
        {
            var context = CreateDbContextWithMockData();
            var repository = new TaskItemRepository(context);

            var result = await repository.GetAllAsync(new GetAllTaskQuery(), CancellationToken.None);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnCorrectCount_WhenDatabaseHasSingleItem()
        {
            var options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var context = new ApplicationContext(options);
            context.Tasks.Add(new TaskItem { Description = "Única tarea", Status = StatusTask.Completed, Type = TaskType.BugFix });
            await context.SaveChangesAsync();

            var repository = new TaskItemRepository(context);

            var result = await repository.GetAllAsync(new GetAllTaskQuery(), CancellationToken.None);

            Assert.Single(result);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnEmpty_WhenDatabaseIsEmpty()
        {
            var options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var context = new ApplicationContext(options);
            var repository = new TaskItemRepository(context);

            var result = await repository.GetAllAsync(new GetAllTaskQuery(), CancellationToken.None);

            Assert.Empty(result);
        }
    }
}
