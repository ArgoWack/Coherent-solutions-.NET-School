namespace Task7
{
    public class Author
    {
        const int MAX_LENGTH = 200;
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Author() { }
        public Author(string firstName, string lastName, DateTime dateOfBirth)
        {
            if (string.IsNullOrWhiteSpace(firstName) || firstName.Length > MAX_LENGTH)
                throw new ArgumentException($"First name cannot be empty, null, or more than {MAX_LENGTH} characters.");

            if (string.IsNullOrWhiteSpace(lastName) || lastName.Length > MAX_LENGTH)
                throw new ArgumentException($"Last name cannot be empty, null, or more than {MAX_LENGTH} characters.");

            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
        }
        public override string ToString()
        {
            return $"{FirstName} {LastName} (Born: {DateOfBirth.ToShortDateString()})";
        }
        public override bool Equals(object obj)
        {
            return obj is Author other &&
                   FirstName == other.FirstName &&
                   LastName == other.LastName &&
                   DateOfBirth == other.DateOfBirth;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(FirstName, LastName, DateOfBirth);
        }
    }
}