using Dapper;
using Dapper.Contrib.Extensions;
using DapperExemplos.Domain.Entities;
using DapperExemplos.Infra.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DapperExemplos.Infra.Repositories
{
    public class PetRepository : IPetRepository
    {
        private readonly ISqlConnectionFactory _connectionFactory;

        public PetRepository(ISqlConnectionFactory connection)
        {
            _connectionFactory = connection;
        }

        //Add
        public async Task AddAsync(Pet pet)
        {
            using var connection = _connectionFactory.Connection();
            connection.Open();
            await connection.InsertAsync(pet);
        }

        //GetById
        public async Task<Pet> GetByIdAsync(Guid id)
        {
            const string sql = "SELECT * FROM Pet WHERE Id = @id";

            var parameters = new DynamicParameters();

            parameters.Add("@id", id);

            using var connection = _connectionFactory.Connection();
            var pet = await connection
                .QuerySingleOrDefaultAsync<Pet>(
                    sql,
                    parameters,
                    commandType: System.Data.CommandType.Text);
            return pet;
        }

        //Alteração
        public async Task<Pet> PutByIdAsync(Pet pet)
        {
            using var connection = _connectionFactory.Connection();
            await connection.UpdateAsync(pet);
            return pet;
        }

        //Listagem
        public async Task<IEnumerable<Pet>> GetAsync()
        {

            using var connection = _connectionFactory.Connection();
            var pets = await connection.GetAllAsync<Pet>();

            return pets;
        }

        //Remoção
        public async Task<bool> DeleteByAsync(Guid id)
        {
            var pet = await GetByIdAsync(id);

            using var connection = _connectionFactory.Connection();

            return await connection.DeleteAsync(pet) ? true : false;
        }
    }
}
