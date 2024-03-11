using Data.AlphaBank;

namespace Interfaces.Common {
    public interface IPersonFactory {

        public Person CreatePerson(string name, string firstName, 
            string secondName, bool deceased, DateOnly dateBirth,
            string address, byte typeIdentificationId, 
            byte nationalityId, byte maritalStatusId);

        public Employee CreateEmployee(bool status, DateOnly dateEntry, int personId, byte salaryCategoryId);

        public Customer CreateCustomer(bool status, string emailAddress, decimal averageMonthlySalary, 
            int personId, byte occupationId, byte customerStatusId);

    }
}