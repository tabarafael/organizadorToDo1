using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizadorV1._0
{
    class Entradas : IEquatable<Entradas> , IComparable <Entradas>
    {
        public int Inscricao { get; set; }
        public string Nome { get; set; }
        public string Tipo { get; set; }
        public string Status { get; set; }
        public int Index { get; set; }

        public Entradas(int inscricao, string nome, string tipo, string status)
        {
            this.Inscricao = inscricao;
            this.Nome = nome;
            this.Tipo = tipo;
            this.Status = status;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            Entradas objAsEntradas = obj as Entradas;
            if (objAsEntradas == null) return false;
            else return Equals(objAsEntradas);
        }

        public int SortByNome(string nome1, string nome2)
        {
            return nome1.CompareTo(nome2);
        }

        public int CompareTo (Entradas compareEntradas)
        {
            if (compareEntradas == null)
                return 1;
            else
                return this.Inscricao.CompareTo(compareEntradas.Inscricao);
        }

        public override int GetHashCode()
        {
            return Inscricao;
        }

        public bool Equals(Entradas outra)
        {
            if (outra == null) return false;
            return (this.Inscricao.Equals(outra.Inscricao));
        }

    }
}
