using System;
public abstract class TrainingComponent
{
    public string Description { get; set; }

    protected TrainingComponent(string description)
    {
        Description = description;
    }

    // Clone method for deep cloning
    public abstract TrainingComponent Clone();
}
