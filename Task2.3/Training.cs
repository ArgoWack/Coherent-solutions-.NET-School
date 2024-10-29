using System;
using static System.Console;
public class Training : TrainingComponent
{
    private TrainingComponent[] components;
    private int componentCount;

    // Constructor for Training to set description and initialize array
    public Training(string description, int maxComponents) : base(description)
    {
        components = new TrainingComponent[maxComponents];
        componentCount = 0;
    }

    // Method to add Lecture or PracticalLesson
    public void Add(TrainingComponent component)
    {
        if (component != null && componentCount < components.Length)
        {
            components.SetValue(component, componentCount++);
        }
    }

    // Method to check if the training only contains practical lessons
    public bool IsPractical()
    {
        foreach (var component in components)
        {
            if (!(component is PracticalLesson))
            {
                return false;
            }
        }
        return true;
    }

    // Deep clone method for Training
    public override Training Clone()
    {
        Training clonedTraining = new Training(Description, components.Length);
        foreach (TrainingComponent component in components)
        {
            if (component != null)
            {
                clonedTraining.Add(component.Clone());
            }
        }
        return clonedTraining;
    }

    // Method to get details for testing purposes
    public void DisplayDetails()
    {
        WriteLine($"Training Description: '{Description}'");
        foreach (var component in components)
        {
            if (component is Lecture lecture)
            {
                WriteLine($"Lecture: Description = '{lecture.Description}', Topic = '{lecture.Topic}'");
            }
            else if (component is PracticalLesson practical)
            {
                WriteLine($"PracticalLesson: Description = '{practical.Description}', TaskLink = '{practical.TaskLink}', SolutionLink = '{practical.SolutionLink}'");
            }
        }
    }
}