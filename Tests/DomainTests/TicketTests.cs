using Domain.Entities;
using Domain.Enums;

namespace Tests.Domain;

[TestClass]
public class TicketTests
{

    [TestMethod]
    public void CreateNewTicket_ShouldInitializePropertiesCorrectly()
    {
        // Arrange
        var username = "user1";
        DateTime dateTime = DateTime.Now;

        // Act
        var ticket = new Ticket(username);

        // Assert
        Assert.IsNotNull(ticket.Id);
        Assert.AreEqual(username, ticket.Username);
        Assert.IsNull(ticket.UpdatedAt);
        Assert.IsTrue((dateTime - ticket.CreatedAt).TotalSeconds < 1);
        Assert.AreEqual(TicketStatus.Open, ticket.Status);
    }

    [TestMethod]
    public void ChangeUsername_ShouldUpdateUsernameAndUpdatedAt()
    {
        // Arrange
        var newUsername = "newUser";
        var ticket = new Ticket("user1");
        var oldUpdatedAt = ticket.UpdatedAt;

        // Act
        ticket.ChangeUsername(newUsername);

        // Assert
        Assert.IsNotNull(ticket.Id);
        Assert.AreEqual(newUsername, ticket.Username);
        Assert.IsNotNull(ticket.UpdatedAt);
        Assert.AreNotEqual(oldUpdatedAt, ticket.UpdatedAt);
        Assert.AreEqual(TicketStatus.Open, ticket.Status);
    }

    [TestMethod]
    public void ChangeStatus_ShouldUpdateStatusAndUpdatedAt()
    {
        // Arrange
        var username = "user1";
        var ticket = new Ticket(username);
        var oldUpdatedAt = ticket.UpdatedAt;

        // Act
        ticket.ChangeStatus(TicketStatus.Closed);

        // Assert
        Assert.IsNotNull(ticket.Id);
        Assert.AreEqual(username, ticket.Username);
        Assert.IsNotNull(ticket.UpdatedAt);
        Assert.AreNotEqual(oldUpdatedAt, ticket.UpdatedAt);
        Assert.AreEqual(TicketStatus.Closed, ticket.Status);
    }
}