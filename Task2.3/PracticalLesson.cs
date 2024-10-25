using System;
public class PracticalLesson : TrainingComponent
{
    public string TaskLink { get; set; }
    public string SolutionLink { get; set; }

    public PracticalLesson(string description, string taskLink, string solutionLink) : base(description)
    {
        TaskLink = taskLink;
        SolutionLink = solutionLink;
    }

    // Implementing deep cloning
    public override TrainingComponent Clone()
    {
        return new PracticalLesson(Description, TaskLink, SolutionLink);
    }
}