using System;
using System.Runtime.Serialization;

namespace DbBackuperBL.Model
{
    [Serializable]
    public class Client
    {
        public int Id { get; private set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string LastName { get; set; }

        public override int GetHashCode()
        {
            return Id;
        }

        public override string ToString()
        {
            return $"{Id} {FirstName} {SecondName} {LastName}";
        }
    }
}
