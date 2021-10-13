using Rise.Assessment.Phonebook.Domain.Core;
using System.Collections.Generic;

namespace Rise.Assessment.Phonebook.Domain.PhonebookAggregate
{
    public class Person : Entity, IAggregateRoot
    {
        public string Name { get; private set; }
        public string Surname { get; private set; }
        public string Company { get; private set; }
        

        private readonly List<PersonDetail> _personDetails;
        public IReadOnlyCollection<PersonDetail> PersonDetails => _personDetails;

        public Person()
        {
        }

        public Person(string name, string surname, string company)
        {
            _personDetails = new List<PersonDetail>();
            Name = name;
            Surname = surname;
            Company = company;
        }

        public void AddPersonDetail(string phoneNumber, string mailAddress, string location, int personId)
        {
            PersonDetail added = new PersonDetail(phoneNumber, mailAddress, location, personId);
            _personDetails.Add(added);
        }
    }
}
