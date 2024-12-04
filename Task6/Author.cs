namespace Task6
{
    public class Author
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Author() { }
        public Author(string firstName, string lastName, DateTime dateOfBirth)
        {
            if (string.IsNullOrWhiteSpace(firstName) || firstName.Length > 200)
                throw new ArgumentException("First name cannot be empty, null, or more than 200 characters.");

            if (string.IsNullOrWhiteSpace(lastName) || lastName.Length > 200)
                throw new ArgumentException("Last name cannot be empty, null, or more than 200 characters.");

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