using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;


namespace EZimba.Libs
{
    class carril
    {
        public int inicio { get; set; }
        public int fim { get; set; }
        public int pagina { get; set; }
        public carril()
        {
            this.inicio = 0;
            this.fim = 0;
            this.pagina = 0;
        }
        public carril(int inicio, int fim)
        {
            this.inicio = inicio;
            this.fim = fim;
        }
        public carril(int inicio, int fim, int pagina)
        {
            this.inicio = inicio;
            this.fim = fim;
            this.pagina = pagina;
        }
    }


    class DataTablePaginator
    {
        private DataTable dt;
        public int pageSize { get; set; }
        private int posicao;
        private List<carril> carris;
        public int nrPaginas { get; private set; }

        public DataTablePaginator()
        {
            this.pageSize = 0;
        }
        /// <summary>
        /// Inicializa o Paginador
        /// </summary>
        /// <param name="dt">Datatable original</param>
        /// <param name="pageSize">Numero de linhas por retornar</param>
        public DataTablePaginator(DataTable dt, int pageSize)
        {
            this.dt = dt;
            this.pageSize = pageSize;
            carris = new List<carril>();

            int p = 0;
            int pg = 0;
            while (p < (dt.Rows.Count-1))
            {
                if (p == 0)
                {
                    p = pageSize - 1;
                    pg = 1;
                    if(p+pageSize >= (dt.Rows.Count - 1))
                    {
                        carris.Add(new carril(0, (dt.Rows.Count - 1), pg));
                    }
                    else
                    {
                        carris.Add(new carril(0, (p), pg));
                    }                  
                }
                else
                {
                    pg = pg + 1;

                    if (p+pageSize >= dt.Rows.Count - 1)
                    {
                        carris.Add(new carril((p + 1), (dt.Rows.Count - 1),pg));
                    }
                    else
                    {
                        carris.Add(new carril((p + 1), ((p + 1) + (pageSize - 1)),pg));
                    }
                    p = (p + 1) + (pageSize - 1);
                }
            }

            this.nrPaginas = carris.Last().pagina;
        }
        public DataTable avancar()
        {

            validar();
            foreach (carril item in carris)
            {
                if(item.inicio == posicao+1)
                {
                    return offset(item);
                }
            }

            return fim();
            
        }
        private void validar()
        {
            if (this.pageSize == 0)
            {
                throw new Exception("tamanho da pagina[pageSize] não definido");
            }
            if (this.dt == null)
            {
                throw new Exception("A DataTable não foi especificada ou nao foi propriamente inicializada!");
            }
        }
        private DataTable copiarColunas()
        {
            DataTable retorno = new DataTable();
            foreach (DataColumn c in this.dt.Columns)
            {
                retorno.Columns.Add(c.ColumnName, c.DataType);
            }
            return retorno;
        }
        public DataTable retroceder()
        {
            foreach (carril item in carris)
            {
                if ((posicao-pageSize) == item.fim)
                {
                    return offset(item);
                }
            }
            return inicio();
        }
        public DataTable inicio()
        {
            return offset(carris.First());
        }
        public DataTable fim()
        {
            return offset(carris.Last());
        }
        private DataTable offset(carril carril)
        {
            validar();
            DataTable retorno = copiarColunas();
            
            for (int i = carril.inicio; i <= carril.fim; i++)
            {
                DataRow rw = retorno.NewRow();

                foreach (DataColumn c in dt.Columns)
                {
                    rw[c.ColumnName] = dt.Rows[i][c.ColumnName];
                }
                posicao = i;
                retorno.Rows.Add(rw);
            }

            return retorno;
        }
        public DataTable pagina(int pagina)
        {
            foreach (var item in carris)
            {
                if(item.pagina == pagina)
                {
                    return offset(item);
                }
            }
            throw new Exception("DataTablePaginator: Pagina inexistente!");
            
        }
    }
}
