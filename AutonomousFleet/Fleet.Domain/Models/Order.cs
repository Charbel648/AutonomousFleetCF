namespace AutonomousFleet.Domain.Models;

public class Order
{
    public DateTime CreatedAt { get; private set; }
    public bool Queued { get; private set; }
    public bool Assigned { get; private set; }
    public bool Running { get; private set; }
    public bool Completed { get; private set; }
    public bool Failed { get; private set; }
    public bool Cancelled { get; private set; }
    
}