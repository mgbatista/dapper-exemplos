using Dapper.Contrib.Extensions;
using DapperExemplos.Domain.Entities.Fixed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DapperExemplos.Domain.Entities
{
    [Table(tableName:"[dbo].[Pet]")] //aqui estou informando que essa classe é minha tabela pet
    public class Pet
    {
        [ExplicitKey] //eu/aplicação que gerencia, quando for Key é o banco que gerencia
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public TypePet Type { get; set; }
    }
}
