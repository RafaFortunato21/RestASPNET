using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using RestASPNET.API.Context;
using RestASPNET.API.Model;
using RestASPNET.API.Persist;
using RestASPNET.API.Persist.Contracts;
using System;

namespace RestASPNET.API.Services.Implementations
{
    public class BookServiceImplementation : IBookService
    {
        private readonly IBookPersist _bookPersit;

        public BookServiceImplementation(IBookPersist bookPersit)
        {
            _bookPersit = bookPersit;
        }


        public List<Book> FindAll()
        {
            return _bookPersit.FindAll();
        }

        public Book FindByID(long id)
        {
            return _bookPersit.FindByID(id);
        }

        public Book Create(Book book)
        {
            try
            {
                _bookPersit.Add<Book>(book);
                _bookPersit.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return book;
        }

        public void Delete(long id)
        {
            var book = _bookPersit.FindByID(id);

            if (book != null)
            {
                try
                {
                    _bookPersit.Delete<Book>(book);
                    _bookPersit.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public Book Update(Book model)
        {
            var book = _bookPersit.FindByID(model.Id);

            if (book == null) return null;

            try
            {
                _bookPersit.Update<Book>(model);
                _bookPersit.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return book;

        }

       
    }
}
