﻿using Library.API.Entities;
using Library.API.Persistence;
using Library.Client.Models;
using System.Collections.Generic;
using System.Linq;

namespace Library.API.Services
{
    public class BooksService : IBooksService
    {
        private readonly IUnitOfWork unitOfWork;

        public BooksService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Book Add(BookToCreate bookToCreate)
        {
            var book = Book.CreateFrom(bookToCreate);

            unitOfWork.BooksRepository.Insert(book);

            unitOfWork.Complete();
            return book;
        }

        public IEnumerable<Book> AddRange(IEnumerable<BookToCreate> booksToCreate)
        {
            var listOfBooks = booksToCreate.Select(x => { return Book.CreateFrom(x); });

            unitOfWork.BooksRepository.InsertRange(listOfBooks);

            unitOfWork.Complete();

            return listOfBooks;
        }

        public BookToReturn Edit(string id, BookToEdit bookToEdit)
        {
            var book = unitOfWork.BooksRepository.GetByID(id);

            if (book == null)
                return null;

            book.UpdateFrom(bookToEdit);

            unitOfWork.Complete();

            return book.ToBookToReturn();
        }
        public Book GetById(string id)
        {
            return unitOfWork.BooksRepository.GetByID(id);
        }

        public IEnumerable<BookToReturn> GetAll()
        {
            var booksToReturn = unitOfWork
                .BooksRepository
                .GetAllAsQueryable()
                .OrderBy(x => x.Name)
                .ToList();

            return booksToReturn
                .Select(x =>
                {
                    return x.ToBookToReturn();
                });
        }

        public Book Delete(string id)
        {
            unitOfWork.BooksRepository.Delete(id);

            unitOfWork.Complete();
            return null;
        }
    }
}
