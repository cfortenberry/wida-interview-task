namespace WidaUserDirectory.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public Company? Company { get; set; }
        public Address? Address { get; set; }
        public string? Phone { get; set; }

        public string? CompanyName 
        { 
            get { return Company?.Name; } 
        }
        public string? City
        {
            get { return Address?.City; }
        }
    }

    public class UserDetailModel
    {
        public UserModel User { get; set; }
        public IEnumerable<ToDo>? ToDo { get; set; }
    }

    public class Address
    {
        public string? Street { get; set; }
        public string? Suite { get; set; }
        public string? City { get; set; }
        public string? ZipCode { get; set; }
    }

    public class ToDo
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public bool Completed { get; set; }
    }

    public class Company
    {
        public string? Name { get; set; }
    }
}
