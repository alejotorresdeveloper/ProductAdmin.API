namespace ProductAdmin.Domain.SharedKernel;

public class Status
{
    public int StatusId { get; }
    public string Name { get; private set; }

    private Status()
    { }

    public Status(int statusId, string name)
    {
        StatusId = statusId;
        Name = name;
    }

    public static Status BuildActive()
        => new(1, "Active");

    public static Status BuildInactive()
        => new(0, "Inactive");
}