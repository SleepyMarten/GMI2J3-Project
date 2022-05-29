using System;
using projekt.Model;
using Microsoft.AspNetCore.Mvc;
using projekt.Database;
namespace projekt.Repository
{
    public interface IAirplaneRepository
    {
        Task<(string?, IEnumerable<IAirplane>?)> Get();
        Task<(string?, IAirplane?)> Get(Guid Id);
        Task<(string?, IEnumerable<IAirplane>?)> Search(string keyword);
        Task<string?> Post(IAirplane airplane);
        Task<string?> Update(IAirplane airplane);
        Task<string?> Delete(Guid Id);
    }
}

