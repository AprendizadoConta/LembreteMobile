using System;
using System.Collections.Generic;
using System.Text;
using SQLite;//adicionei

namespace AppLembrete.Models
{
    [Table("Anotacoes")]//início das Anotations:[]; e cria uma tabela Anotações
    public class ModelNotas
    {
        [PrimaryKey, AutoIncrement]//Anotations
        public int Id { get; set; }

        [NotNull]
        public String Titulo { get; set; }

        [NotNull]
        public String Dados { get; set; }

        [NotNull]
        public Boolean Favorito { get; set; }

        public ModelNotas()
        {
            //Essa parte fizemos, para otimização do banco de dados
            this.Id = 0;
            this.Dados = "";
            this.Favorito = false;
            this.Titulo = "";
        }
    }
}
