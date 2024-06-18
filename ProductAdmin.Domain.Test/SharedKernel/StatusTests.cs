using ProductAdmin.Domain.SharedKernel;

namespace ProductAdmin.Domain.Test.SharedKernel;

public class StatusTests
{
    [Fact]
    public void BuildActive_StateUnderTest_ExpectedBehavior()
    {
        // Arrange
        int statusId = 1;
        string statusName = "Active";

        // Act
        var result = Status.BuildActive();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(result.StatusId, statusId);
        Assert.Equal(result.Name, statusName);
    }

    [Fact]
    public void BuildInactive_StateUnderTest_ExpectedBehavior()
    {
        // Arrange
        int statusId = 0;
        string statusName = "Inactive";

        // Act
        var result = Status.BuildInactive();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(result.StatusId, statusId);
        Assert.Equal(result.Name, statusName);
    }
}
