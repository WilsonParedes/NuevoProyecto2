using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NuevoProyecto2.DataSystem
{
    class GestorBDD
    {

		public void GuardarBDD(Repositorio repositorio)
		{
			try
			{
				string dml = "INSERT INTO \"Bitacora\" VALUES(" + repositorio.getContador() + ",'" + repositorio.getComentario() + "'" +
						",'" + repositorio.getContenido() + "'" + ",'" + repositorio.getFecha() + "')";
				Console.WriteLine("dml = " + dml);
				NpgsqlCommand ejecutor = new NpgsqlCommand(dml, Global<object>.ConectaBDD);
				ejecutor.ExecuteNonQuery();
				Console.WriteLine("Se grabo correctamente");
			}
			catch (Exception throwables)
			{
				Console.WriteLine("No se puede crear");
			}
		}

		public void ExtraerTabla()
        {
			string dml = "SELECT * FROM \"Bitacora\"";
			Console.WriteLine("dml = " + dml);
			NpgsqlCommand ejecutor = new NpgsqlCommand(dml, Global<object>.ConectaBDD);
			NpgsqlDataReader reader = ejecutor.ExecuteReader();
			int NoVersion;
			string NombreVersion, Contenido, Fecha;
			Repositorio repositorio;
            while (reader.Read())
            {
				NoVersion = Convert.ToInt32(reader["No. Version"]);
				NombreVersion = (string)reader["Nombre Version"];
				Contenido = (string)reader["Contenido"];
				Fecha = (string)reader["Fecha y Hora"];
				repositorio = new Repositorio(NoVersion.ToString(), NombreVersion, Contenido, Fecha, 1);
				Global<object>.MT.CrearVersionEnListaEnlazadaDeLaBDD(repositorio);
			}

			Global<object>.manejoAr.RecorreListaVersiones();

		}

	}
}

