using System;
using System.Collections.Generic;

namespace Domain.Entitties.Library
{
    public class Author
    {
        public string Id { get; set; }
        public string Image { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FatherName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Description { get; set; }

        public IEnumerable<Edition> Editions { get; set; }
    }
}
