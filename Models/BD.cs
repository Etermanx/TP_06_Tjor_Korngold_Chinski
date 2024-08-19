using System.Data.SqlClient;
using Dapper;
public static class BD {
    const string _connectionString = @"Server=localhost;Database=JJOO;Trusted_Connection=True;";

    public static void AgregarDeporte(Deporte deporte)
    {
        string sql = "INSERT INTO Deportes (Nombre, Foto, Banner) VALUES (@pNombre, @pFoto, @pBanner)";
        using (SqlConnection BD = new SqlConnection(_connectionString))
        {
            BD.Execute(sql, new {
                pNombre = deporte.Nombre,
                pFoto = deporte.Foto,
                pBanner = deporte.Banner
            });
        }
    }
    public static void AgregarDeportista(Deportista deportista)
    {
        string sql = "INSERT INTO Deportistas (Apellido, Nombre, FechaNacimiento, Foto, IdPais, IdDeporte) VALUES (@pApellido, @pNombre, @pFechaNacimiento, @pFoto, @pIdPais, @pIdDeporte)";
        using (SqlConnection BD = new SqlConnection(_connectionString))
        {
            BD.Execute(sql, new {
                pApellido = deportista.Apellido,
                pNombre = deportista.Nombre,
                pFechaNacimiento = deportista.FechaNacimiento,
                pFoto = deportista.Foto,
                pIdPais = deportista.IdPais,
                pIdDeporte = deportista.IdDeporte
            });
        }
    }
    public static int EliminarDeportista(int IdDeportista)
    {
        int deportistasEliminados = 0;
        string sql = "DELETE FROM Deportistas WHERE IdDeportista = @pDeportista";
        using (SqlConnection BD = new SqlConnection(_connectionString))
        {
            deportistasEliminados = BD.Execute(sql, new{ pDeportista = IdDeportista });
        }
        return deportistasEliminados;
    }


    public static Deporte? VerInfoDeporte(int idDeporte)
    {
        Deporte? depMuestra;
        using (SqlConnection BD = new SqlConnection(_connectionString))
        {
            string sql = "SELECT * FROM Deportes WHERE IdDeporte = @pIdDeporte";
            depMuestra = BD.QueryFirstOrDefault<Deporte>(sql, new { pIdDeporte = idDeporte });
        }
        return depMuestra;
    }
    public static Pais? VerInfoPais(int idPais)
    {
        Pais? paisMuestra;
        using (SqlConnection BD = new SqlConnection (_connectionString))
        {
            string sql = "SELECT * FROM Paises WHERE IdPais = @pIdPais";
            paisMuestra = BD.QueryFirstOrDefault<Pais>(sql, new { pIdPais = idPais });
        }
        return paisMuestra;
    }
    public static Deportista? VerInfoDeportista(int idDeportista)
    {
        Deportista? deportistaMuestra;
        using (SqlConnection BD  = new SqlConnection (_connectionString))
        {
            string sql = "SELECT * FROM Deportistas WHERE IdDeportista = @pIdDeportista";
            deportistaMuestra = BD.QueryFirstOrDefault<Deportista>(sql, new { pIdDeportista = idDeportista });
        }
        return deportistaMuestra;
    }


    public static List<Pais> ListarPaises() 
    {
        List<Pais> ListaPaises = new List<Pais>();
        using (SqlConnection BD  = new SqlConnection(_connectionString))
        {
            string sql = "SELECT * FROM Paises ORDER BY Nombre";
            ListaPaises = BD.Query<Pais>(sql).ToList();
        }
        return ListaPaises;

    }
    public static List<Deporte> ListarDeportes()
    {
        List<Deporte> ListaDeportes;
        using (SqlConnection BD = new SqlConnection(_connectionString))
        {
            string sql = "SELECT * FROM Deportes ORDER BY Nombre";
            ListaDeportes = BD.Query<Deporte>(sql).ToList();
        }
        return ListaDeportes;
    }
    public static List<Deportista> ListarDeportistasPorDeporte(int idDeporte)
    {
        List<Deportista> ListaDeportistas;
        using (SqlConnection BD = new SqlConnection(_connectionString))
        {
            string sql = "SELECT * FROM Deportistas WHERE IdDeporte = @pIdDeporte ORDER BY Apellido, Nombre";
            ListaDeportistas = BD.Query<Deportista>(sql, new { pIdDeporte = idDeporte }).ToList();
        }
        return ListaDeportistas;
    }
    public static List<Deportista> ListarDeportistasPorPais(int idPais)
    {
        List<Deportista> ListaDeportistas;
        using (SqlConnection BD = new SqlConnection(_connectionString))
        {
            string sql = "SELECT * FROM Deportistas WHERE IdPais = @pIdPais ORDER BY Apellido, Nombre";
            ListaDeportistas = BD.Query<Deportista>(sql, new { pIdPais = idPais }).ToList();
        }
        return ListaDeportistas;
    }
    public static List<MedallasPais> ListarCantMedallasPais(int cantidad)
    {
        List<MedallasPais> ListaMedallasPais;
        using (SqlConnection BD = new SqlConnection(_connectionString))
        {
            string sql = "SELECT TOP(@pCantidad) IdPais, TipoMedalla, COUNT(*) AS Cantidad FROM Medallas GROUP BY IdPais, TipoMedalla";
            ListaMedallasPais = BD.Query<MedallasPais>(sql, new { pCantidad = cantidad }).ToList();
        }
        return ListaMedallasPais;
    }
    public static List<MedallasPais> ListarMedallasPais()
    {
        List<MedallasPais> ListaMedallasPais;
        using (SqlConnection BD = new SqlConnection(_connectionString))
        {
            string sql = "SELECT IdPais, TipoMedalla, COUNT(*) AS Cantidad FROM Medallas GROUP BY IdPais, TipoMedalla";
            ListaMedallasPais = BD.Query<MedallasPais>(sql).ToList();
        }
        return ListaMedallasPais;
    }
}