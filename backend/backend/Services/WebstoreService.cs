using System;
using backend.Entities;
using backend.Interfaces;

namespace backend.Services
{
    public class WebstoreService : IWebstoreService
    {
        private readonly IDbRepository _repository;

        public WebstoreService(IDbRepository repository)
        {
            this._repository = repository;
        }

        //regisztrálás
        public void registrate(User user)
        {
            throw new NotImplementedException();
        }

        //bejelentkezés
        public void login(User user)
        {
            throw new NotImplementedException();
        }

        //feltöltés
        public void upload(File file)
        {
            throw new NotImplementedException();
        }

        //letöltés
        public File download(string fileName)
        {
            throw new NotImplementedException();
        }

        //keresés

        //kommentelés

        //fájl törlése

        //komment törlése
    }
}
