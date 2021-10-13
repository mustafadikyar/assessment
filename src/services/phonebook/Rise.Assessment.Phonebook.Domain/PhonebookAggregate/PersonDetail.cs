using Rise.Assessment.Phonebook.Domain.Core;

namespace Rise.Assessment.Phonebook.Domain.PhonebookAggregate
{
    public class PersonDetail : Entity
    {
        public PersonDetail()
        {
        }

        public PersonDetail(string phoneNumber, string mailAddress, string location)
        {
            PhoneNumber = phoneNumber;
            MailAddress = mailAddress;
            Location = location;
        }

        public string PhoneNumber { get; private set; }
        public string MailAddress { get; private set; }
        public string Location { get; private set; }

        public void UpdatePersonDetail(string phoneNumber, string mailAddress, string location)
        {
            this.PhoneNumber = phoneNumber;
            this.MailAddress = mailAddress;
            this.Location = location;
        }
    }
}