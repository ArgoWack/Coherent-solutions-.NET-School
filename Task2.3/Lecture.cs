using System;
// Class for Lecture
public class Lecture : TrainingComponent
{
    public string Topic { get; set; }

    public Lecture(string description, string topic) : base(description)
    {
        Topic = topic;
    }

    // Implementing deep cloning
    public override TrainingComponent Clone()
    {
        return new Lecture(Description, Topic);
    }
}