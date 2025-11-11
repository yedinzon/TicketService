using Application.Common;
using Application.Dtos;
using Application.Interfaces.Repositories;
using Application.Services;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using NSubstitute;

namespace Tests.ApplicationTests;

[TestClass]
public class TicketServiceTests
{
    private ITicketRepository _repositoryMock = null!;
    private IMapper _mapperMock = null!;
    private TicketService _service = null!;

    [TestInitialize]
    public void Setup()
    {
        _repositoryMock = Substitute.For<ITicketRepository>();
        _mapperMock = Substitute.For<IMapper>();
        _service = new TicketService(_repositoryMock, _mapperMock);
    }

    [TestMethod]
    public async Task GetAllAsync_ShouldReturnMappedTickets()
    {
        // Arrange
        var tickets = new List<Ticket> { new("user1"), new("user2") };

        _repositoryMock
            .GetAllAsync()
            .Returns(tickets);

        _mapperMock
            .Map<IEnumerable<TicketDto>>(tickets)
            .Returns(tickets.Select(t => new TicketDto { Id = t.Id, Username = t.Username, Status = t.Status }));

        // Act
        var result = await _service.GetAllAsync();

        // Assert
        Assert.AreEqual(2, result.Count());
        Assert.AreEqual("user1", result.First().Username);
        Assert.AreEqual("user2", result.Last().Username);

        // Verify
        await _repositoryMock.Received().GetAllAsync();
    }

    [TestMethod]
    public async Task GetPagedAsync_ShouldReturnMappedPagedResult()
    {
        // Arrange
        var tickets = new List<Ticket> { new("user1"), new("user2") };

        var pagedTickets = new PagedResult<Ticket>(
            tickets,
            pageNumber: 1,
            pageSize: 2,
            totalCount: 2
        );

        var mappedDtos = tickets.Select(t => new TicketDto
        {
            Id = t.Id,
            Username = t.Username,
            Status = t.Status
        }).ToList();

        _repositoryMock
            .GetPagedAsync(1, 2)
            .Returns(pagedTickets);

        _mapperMock
            .Map<IReadOnlyList<TicketDto>>(Arg.Any<IReadOnlyList<Ticket>>())
            .Returns(mappedDtos);

        // Act
        var result = await _service.GetPagedAsync(1, 2);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(1, result.PageNumber);
        Assert.AreEqual(2, result.PageSize);
        Assert.AreEqual(2, result.TotalCount);
        Assert.IsFalse(result.HasPrevious);
        Assert.IsFalse(result.HasNext);
        Assert.AreEqual(mappedDtos.Count, result.Items.Count);
        Assert.AreEqual("user1", result.Items[0].Username);
        Assert.AreEqual("user2", result.Items[1].Username);

        // Verify
        await _repositoryMock.Received().GetPagedAsync(1, 2);
    }

    [TestMethod]
    public async Task GetByIdAsync_ShouldReturnMappedTicket_WhenFound()
    {
        // Arrange
        var ticket = new Ticket("user1");
        var id = ticket.Id;

        _repositoryMock
            .GetByIdAsync(id)
            .Returns(ticket);

        _mapperMock
            .Map<TicketDto>(ticket)
            .Returns(new TicketDto { Id = ticket.Id, Username = ticket.Username, Status = ticket.Status });

        // Act
        var result = await _service.GetByIdAsync(ticket.Id);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual("user1", result.Username);

        // Verify
        await _repositoryMock.Received().GetByIdAsync(id);
    }

    [TestMethod]
    public async Task GetByIdAsync_ShouldReturnNull_WhenNotFound()
    {
        // Arrange
        var id = Guid.NewGuid();
        _repositoryMock
            .GetByIdAsync(id)
            .Returns((Ticket?)null);

        // Act
        var result = await _service.GetByIdAsync(id);

        // Assert
        Assert.IsNull(result);

        // Verify
        await _repositoryMock.Received().GetByIdAsync(id);
    }

    [TestMethod]
    public async Task CreateAsync_ShouldReturnMappedTicket()
    {
        // Arrange
        var createDto = new CreateTicketDto { Username = "newUser" };
        var ticket = new Ticket("newUser");

        _repositoryMock
            .CreateAsync(Arg.Any<Ticket>())
            .Returns(ticket);

        _mapperMock
            .Map<TicketDto>(ticket)
            .Returns(new TicketDto { Id = ticket.Id, Username = ticket.Username, Status = ticket.Status });

        // Act
        var result = await _service.CreateAsync(createDto);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual("newUser", result.Username);

        // Verify
        await _repositoryMock.Received().CreateAsync(Arg.Any<Ticket>());
    }

    [TestMethod]
    public async Task UpdateAsync_ShouldReturnMappedTicket_WhenFound()
    {
        // Arrange
        var ticket = new Ticket("user1");
        var updateDto = new UpdateTicketDto { Username = "updatedUser", Status = TicketStatus.Closed };

        _repositoryMock
            .GetByIdAsync(ticket.Id)
            .Returns(ticket);

        _repositoryMock
          .UpdateAsync(ticket)
          .Returns(ticket);

        _mapperMock
            .Map<TicketDto>(Arg.Any<Ticket>())
            .Returns(callInfo =>
            {
                var t = callInfo.Arg<Ticket>();
                return new TicketDto
                {
                    Id = t.Id,
                    Username = t.Username,
                    Status = t.Status
                };
            });

        // Act
        var result = await _service.UpdateAsync(ticket.Id, updateDto);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual("updatedUser", result.Username);
        Assert.AreEqual(TicketStatus.Closed, result.Status);

        // Verify
        await _repositoryMock.Received().GetByIdAsync(ticket.Id);
        await _repositoryMock.Received().UpdateAsync(ticket);
    }

    [TestMethod]
    public async Task UpdateAsync_ShouldReturnNull_WhenNotFound()
    {
        // Arrange
        _repositoryMock
            .GetByIdAsync(Arg.Any<Guid>())
            .Returns((Ticket?)null);

        // Act
        var result = await _service.UpdateAsync(Guid.NewGuid(), new UpdateTicketDto());

        // Assert
        Assert.IsNull(result);

        // Verify
        await _repositoryMock.Received().GetByIdAsync(Arg.Any<Guid>());
        await _repositoryMock.DidNotReceive().UpdateAsync(Arg.Any<Ticket>());
    }

    [TestMethod]
    public async Task PatchAsync_ShouldUpdateOnlyProvidedFields()
    {
        // Arrange
        var ticket = new Ticket("user1");
        var patchDto = new PatchTicketDto { Username = "patchedUser" };

        _repositoryMock
            .GetByIdAsync(ticket.Id)
            .Returns(ticket);

        _repositoryMock
            .UpdateAsync(ticket)
            .Returns(ticket);

        _mapperMock
            .Map<TicketDto>(Arg.Any<Ticket>())
            .Returns(callInfo =>
            {
                var t = callInfo.Arg<Ticket>();
                return new TicketDto
                {
                    Id = t.Id,
                    Username = t.Username,
                    Status = t.Status
                };
            });

        // Act
        var result = await _service.PatchAsync(ticket.Id, patchDto);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual("patchedUser", result.Username);
        Assert.AreEqual(TicketStatus.Open, result.Status);

        // Verify
        await _repositoryMock.Received().GetByIdAsync(ticket.Id);
        await _repositoryMock.Received().UpdateAsync(ticket);
    }

    [TestMethod]
    public async Task DeleteAsync_ShouldReturnTrue_WhenTicketExists()
    {
        // Arrange
        var ticket = new Ticket("user1");

        _repositoryMock
            .GetByIdAsync(ticket.Id)
            .Returns(ticket);

        _repositoryMock
            .DeleteAsync(ticket)
            .Returns(Task.CompletedTask);

        // Act
        var result = await _service.DeleteAsync(ticket.Id);

        // Assert
        Assert.IsTrue(result);

        // Verify
        await _repositoryMock.Received().GetByIdAsync(ticket.Id);
        await _repositoryMock.Received().DeleteAsync(ticket);
    }

    [TestMethod]
    public async Task DeleteAsync_ShouldReturnFalse_WhenTicketDoesNotExist()
    {
        // Arrange
        _repositoryMock
            .GetByIdAsync(Arg.Any<Guid>())
            .Returns((Ticket?)null);

        // Act
        var result = await _service.DeleteAsync(Guid.NewGuid());

        // Assert
        Assert.IsFalse(result);

        // Verify
        await _repositoryMock.Received().GetByIdAsync(Arg.Any<Guid>());
        await _repositoryMock.DidNotReceive().DeleteAsync(Arg.Any<Ticket>());
    }
}
