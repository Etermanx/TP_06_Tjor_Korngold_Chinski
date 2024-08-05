using System.Data.SqlClient;
using Dapper;
public static class BD {
    private static string _connectionString = @"Server=locahost;DataBase=JJOO;Trusted_Connection=True";
    public static void AgregarDeportista(Deportista deportista)
    {
        string sql = "INSERT INTO Deportistas (IdDeportista, Apellido, Nombre, FechaNacimiento, Foto, IdPais, IdDeporte) VALUES (@pIdDeportista, @pApellido, @pNombre, @pFechaNacimiento, @pFoto, @pIdPais, @pIdDeporte)";
        using (SqlConnection BD = new SqlConnection(_connectionString))
        {
            BD.Execute(sql, new {
                pIdDeportista = deportista.IdDeportista,
                pApellido = deportista.Apellido,
                pNombre = deportista.Nombre,
                pFechaNacimiento = deportista.FechaNacimiento,
                pFoto = deportista.Foto,
                pIdPais = deportista.IdPais,
                pIdDeporte = deportista.IdDeporte
            });
        }
    }
}