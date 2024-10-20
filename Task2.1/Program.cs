using static System.Console;
/*
 Task 2.1 
Suppose that IT-company develops the project – « Planetarium on the computer ». It is necessary to create and test class for a point with mass in three-dimensional space. Point coordinates X, Y, Z – arbitrary integers, mass Mass – real number greater than or equal to 0.
Requirements:
Create a class to represent a point in three-dimensional space that has mass. To store the coordinates of a point, use an array of three elements (private field).
Provide the class with separate public properties for reading and setting each coordinate and mass (properties named X, Y, Z and Mass).
If somebody tries to set the mass to a negative value, set the mass to 0.
Provide the class with the IsZero() method, which returns true if all coordinates of the point are 0.
Provide the class with a method that receives another point object as a parameter. The method must return the distance between the current point and the parameter point. To extract the square root, use the Math.Sqrt() method.
Test the created class, the operation of its properties and methods in a console application.
*/
class Program
{
    public class Point
    {
        private int[] coordinates = new int[3];
        private double mass;

        // properties for coordinates
        public int X
        {
            get { return coordinates[0]; }
            set { coordinates[0] = value; }
        }

        public int Y
        {
            get { return coordinates[1]; }
            set { coordinates[1] = value; }
        }

        public int Z
        {
            get { return coordinates[2]; }
            set { coordinates[2] = value; }
        }

        // property for mass
        public double Mass
        {
            get { return mass; }
            set { mass = value < 0 ? 0 : value; }
        }

        // constructor
        public Point(int x, int y, int z, double mass)
        {
            X = x;
            Y = y;
            Z = z;
            Mass = mass;
        }

        // method to check if the point is at (0,0,0)
        public bool IsZero()
        {
            return X == 0 && Y == 0 && Z == 0;
        }

        // method to calculate the distance to another point
        public double DistanceTo(Point otherPoint)
        {
            int deltaX = X - otherPoint.X;
            int deltaY = Y - otherPoint.Y;
            int deltaZ = Z - otherPoint.Z;
            return Math.Sqrt(deltaX * deltaX + deltaY * deltaY + deltaZ * deltaZ);
        }
    }
    public static void Main()
    {
        Point point1 = new Point(0, 0, 0, 5.5);
        Point point2 = new Point(3, 4, 0, 2.2);

        // testing properties
        WriteLine($"Point1: X={point1.X}, Y={point1.Y}, Z={point1.Z}, Mass={point1.Mass}");
        WriteLine($"Point2: X={point2.X}, Y={point2.Y}, Z={point2.Z}, Mass={point2.Mass}");

        // testing IsZero method
        WriteLine($"Is Point1 at origin? {point1.IsZero()}");
        WriteLine($"Is Point2 at origin? {point2.IsZero()}");

        // testing DistanceTo method
        double distance = point1.DistanceTo(point2);
        WriteLine($"Distance between Point1 and Point2: {distance}");

        // test setting a negative mass
        point1.Mass = -10;
        WriteLine($"Point1 Mass after setting to a negative value: {point1.Mass}");
    }
}