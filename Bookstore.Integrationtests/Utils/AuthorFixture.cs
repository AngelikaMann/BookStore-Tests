﻿using Bookstore.Domain.Entities;

namespace Bookstore.Integrationtests.Utils
{
    public class AuthorFixture
    {
        public Author Author { get; }
        public AuthorFixture()
        {
            Author = new Author()
            {
                FirstName = "Test",
                LastName = "Test"

            };
        }
    }
}
