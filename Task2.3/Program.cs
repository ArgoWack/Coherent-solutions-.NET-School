using static System.Console;
/*
Task 2.3. 
Suppose that Coherent solutions develops a training management system. Each training consists of a set of lectures and practical exercises.
It is necessary to create classes to represent the following entities: training, lecture, practical lesson.
All specified entities have a text description (this is an arbitrary string, possibly empty or equal to null)
The lecture has a topic (an arbitrary string, possibly empty or equal to null), and a practical lesson has a link to the task condition (an arbitrary string, possibly empty or equal to null) and a link to the solution (an arbitrary string, possibly empty or equal to null).
The training must store a set of lectures and practical exercises in an internal array and have an Add() method for adding a lecture or practical exercise. 
In addition, the training must have a method IsPractical() - returns true if the training contains only practical exercises.
Implement an instance method Clone() in the training class to clone the training. Note: Deep cloning must be performed, not shallow cloning.
 */
class Program
{
    public static void Main()
    {
        // Create Lectures and Practical Lessons
        Lecture lecture1 = new Lecture("text1", "text2");
        Lecture lecture2 = new Lecture("text3", "text4");
        PracticalLesson practical1 = new PracticalLesson("text5", "http://tasklink1", "http://solutionlink1");
        PracticalLesson practical2 = new PracticalLesson("text6", "http://tasklink2", "http://solutionlink2");

        // Create Training and add components
        Training training = new Training("Training Course Description", 5); // 5 is max length
        training.Add(lecture1);
        training.Add(lecture2);
        training.Add(practical1);

        // Display details
        WriteLine("Original Training:");
        training.DisplayDetails();

        // Check if it contains only practical lessons
        WriteLine($"Is the training practical only? {training.IsPractical()}");

        // Clone the training
        Training clonedTraining = training.Clone();
        WriteLine("\nCloned Training:");
        clonedTraining.DisplayDetails();

        // Add a new practical lesson to demonstrate the cloned version is independent
        clonedTraining.Add(practical2);

        WriteLine("\nModified Cloned Training (added new PracticalLesson):");
        clonedTraining.DisplayDetails();

        WriteLine("\nOriginal Training remains unchanged:");
        training.DisplayDetails();

        // **Additional Checks for Empty or Null Arguments**
        WriteLine("\nTesting with empty and null arguments:");

        // Create lectures and practical lessons with empty and null values
        Lecture emptyLecture = new Lecture("", "");
        Lecture nullLecture = new Lecture(null, null);
        PracticalLesson emptyPractical = new PracticalLesson("", "", "");
        PracticalLesson nullPractical = new PracticalLesson(null, null, null);

        // Add them to the training
        Training testTraining = new Training("Test Training", 10); // larger array for tests
        testTraining.Add(emptyLecture);
        testTraining.Add(nullLecture);
        testTraining.Add(emptyPractical);
        testTraining.Add(nullPractical);
        testTraining.Add(null); // Add null directly, should be ignored

        // Display the details of the test training
        WriteLine("\nTest Training with empty and null arguments:");
        testTraining.DisplayDetails();

        // Check if the training is practical-only
        WriteLine($"Is the test training practical only? {testTraining.IsPractical()}");
    }
}
