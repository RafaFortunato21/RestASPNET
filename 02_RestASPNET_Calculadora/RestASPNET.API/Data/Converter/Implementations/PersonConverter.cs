﻿using RestASPNET.API.Data.Converter.Contract;
using RestASPNET.API.Data.DTO;
using RestASPNET.API.Model;

namespace RestASPNET.API.Data.Converter.Implementations
{
    public class PersonConverter : IParser<PersonDTO, Person>, IParser<Person, PersonDTO>
    {
        public Person Parse(PersonDTO origin)
        {
            if (origin == null) return null;

            return new Person
            {
                Id = origin.Id,
                First_Name = origin.First_Name,
                Last_Name = origin.Last_Name,
                Address = origin.Address,
                Gender = origin.Gender

            };





        }

        public List<Person> Parse(List<PersonDTO> origin)
        {
            if (origin == null) return null;
            return origin.Select(item => Parse(item)).ToList();
        }

        public List<PersonDTO> Parse(List<Person> origin)
        {
            if (origin == null) return null;
            return origin.Select(item => Parse(item)).ToList();
        }


        public PersonDTO Parse(Person origin)
        {
            if(origin == null) return null;

            return new PersonDTO
            {
                Id = origin.Id,
                First_Name = origin.First_Name,
                Last_Name = origin.Last_Name,
                Address = origin.Address,
                Gender = origin.Gender

            };
        }

    }
}
