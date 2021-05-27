using System;

namespace GamesCatalog.Core.Exceptions
{
    public sealed class GameCatalogInvalidUserException : Exception
    {
        public GameCatalogInvalidUserException() : base("Unable to find user or invalid id provided.")
        {

        }
    }
}
