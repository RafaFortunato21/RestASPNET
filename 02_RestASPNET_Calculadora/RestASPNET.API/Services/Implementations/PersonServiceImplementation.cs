using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using RestASPNET.API.Context;
using RestASPNET.API.Model;
using RestASPNET.API.Persist.Contracts;
using System;

namespace RestASPNET.API.Services.Implementations
{
    public class PersonServiceImplementation : IPersonService
    {
        private readonly IPersonPersist _personPersit;

        public PersonServiceImplementation(IPersonPersist personPersit)
        {
            _personPersit = personPersit;
        }

        public List<Person> FindAll()
        {
            return _personPersit.FindAll();
        }

        public Person FindByID(long id)
        {
            return _personPersit.FindByID(id);
        }

        public Person Create(Person person)
        {
            try
            {
                _personPersit.Add(person);
                _personPersit.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return person;
        }

        public void Delete(long id)
        {
            var result = _personPersit.FindByID(id);

            if (result != null)
            {
                try
                {
                    _personPersit.Delete(result);
                    _personPersit.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }


        public Person Update(Person model)
        {
            try
            {
                var person = _personPersit.FindByID(model.Id);
                if (person == null) return null;

                _personPersit.Update<Person>(model);
                _personPersit.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }


            return model;

        }


    }
}
