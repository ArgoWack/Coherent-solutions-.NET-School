namespace Task7
{
    public class Author
    {
        const int MAX_LENGTH = 200;
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Author(string firstName, string lastName)
        {
            if (string.IsNullOrWhiteSpace(firstName) || firstName.Length > MAX_LENGTH)
                throw new ArgumentException($"First name cannot be empty, null, or more than {MAX_LENGTH} characters.");

            if (string.IsNullOrWhiteSpace(lastName) || lastName.Length > MAX_LENGTH)
                throw new ArgumentException($"Last name cannot be empty, null, or more than {MAX_LENGTH} characters.");

            FirstName = firstName;
            LastName = lastName;
        }
        public override string ToString()
        {
            return $"{FirstName} {LastName} )";
        }
        public override bool Equals(object obj)
        {
            return obj is Author other &&
                   FirstName == other.FirstName &&
                   LastName == other.LastName;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(FirstName, LastName);
        }
    }
}