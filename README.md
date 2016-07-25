# Paginador de DataTable
O objectivo dessa classe é de receber uma DataTable e devolver-la em fatias ou paginadas.

Por exemplo: se introduzir uma DataTable com 1500 linhas e definir o numero de paginas como 10
poderás 150 DataTable cada com 10 registos.

Sei que existem muitos exemplos para DataGridView.
Escrevi esse código pk um amigo está a trabalhar com controles modificados.

Estou aberto a criticas e sugestões.

Abraços!

## Como Utilizar

**importa a biblioteca se bem que vais mudar**
using EZimba.Libs;

**considerando que dt é uma DataTable válida.**
DataTablePaginator dtp = new DataTablePaginator(dt, 5);

**DataTable dt2 = dtp.inicio();**

**Próxima pagina**
DataTable dt3 = dtp.avancar();

**Retroceder**
DataTable dt4 = dtp.retroceder();

**Ultima pagina**
DataTable dt5 = dtp.fim();

**Página Nr página**
considerando que nr séja um número Inteiro válido
dt6 = dtp.pagina(nr);

**Número de paginas**
int nrPaginas = dtp.nrPaginas;



# Exemplo de Utilização

using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Data;
using EZimba.Libs;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            MySqlConnection con = new MySqlConnection("server=localhost; database=escola; userid=root");
            MySqlDataAdapter adpt;
            DataTable dt = new DataTable();

            var registos = new List<string>();

            try
            {
                con.Open();
                adpt = new MySqlDataAdapter("Select * from aluno",con);
                adpt.Fill(dt);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
            finally
            {
                con.Close();
            }


            DataTablePaginator dtp = new DataTablePaginator(dt, 5);

            Console.WriteLine("===INICIO===");
            DataTable dt2 = dtp.inicio();

            foreach (DataRow r in dt2.Rows)
            {
                foreach (DataColumn c in dt2.Columns)
                {
                    Console.Write(" "+c.Caption + " : " + r[c.Caption].ToString());
                }
                Console.WriteLine();
            }

            Console.WriteLine("===AVANCAR===");
            DataTable dt3 = dtp.avancar();
            foreach (DataRow r in dt3.Rows)
            {
                foreach (DataColumn c in dt3.Columns)
                {
                    Console.Write(" " + c.Caption + " : " + r[c.Caption].ToString());
                }
                Console.WriteLine();
            }

            Console.WriteLine("===RETROCEDER===");
            DataTable dt4 = dtp.retroceder();
            foreach (DataRow r in dt4.Rows)
            {
                foreach (DataColumn c in dt4.Columns)
                {
                    Console.Write(" " + c.Caption + " : " + r[c.Caption].ToString());
                }
                Console.WriteLine();
            }

            Console.WriteLine("===FIM===");
            DataTable dt6 = dtp.fim();

            foreach (DataRow r in dt6.Rows)
            {
                foreach (DataColumn c in dt3.Columns)
                {
                    Console.Write(" " + c.Caption + " : " + r[c.Caption].ToString());
                }
                Console.WriteLine();
            }

            Console.WriteLine("===PAGINA===");
            DataTable dt7 = dtp.pagina(2);

            foreach (DataRow r in dt7.Rows)
            {
                foreach (DataColumn c in dt3.Columns)
                {
                    Console.Write(" " + c.Caption + " : " + r[c.Caption].ToString());
                }
                Console.WriteLine();
            }
            Console.ReadKey();
        }
    }
}
