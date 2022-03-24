using NetTopologySuite.Geometries;
using Npgsql;

namespace WEBAPI_SIGUI_TEST_BACKEND.Services
{
    public class MapeamentoService : IMapeamentoService
    {
        public string Constr { get; set; }
        public IConfiguration _configuration;

        public MapeamentoService(IConfiguration configuration) {
            _configuration = configuration;
            Constr = _configuration.GetConnectionString("PostGISConnection");
        }

        public async Task<List<object>> SelectDataGeometry()
        {
            List<object> geometries = new List<object>();
            //object point = new Point(new Coordinate(1d, 1d));            
                        
            await using var conn = new NpgsqlConnection(Constr);
            await conn.OpenAsync();

            await using (var cmd = new NpgsqlCommand("SELECT * FROM nyc_neighborhoods;", conn))

            await using (NpgsqlDataReader reader = await cmd.ExecuteReaderAsync())
            {
                reader.Read();
                object test = reader[3];
                geometries.Add(test);
                Console.WriteLine(test);

                //Assert.That(reader[0], Is.EqualTo(Point));

                /* while (await reader.ReadAsync())
                 {
                     object test = reader[3];
                     geometries.Add(test);
                     //var test1 = reader.GetString(3);
                     // Console.WriteLine(reader.GetString(1));
                 }*/
            }
            return geometries;
        }

        public async Task<List<object>> SelectTransformGeometry()
        {
            List<object> geometries = new List<object>();
            //object point = new Point(new Coordinate(1d, 1d));            

            await using var conn = new NpgsqlConnection(Constr);
            await conn.OpenAsync();

            await using (var cmd = new NpgsqlCommand("SELECT name, st_Transform(geom,4326) FROM public.nyc_neighborhoods;", conn))

            await using (NpgsqlDataReader reader = await cmd.ExecuteReaderAsync())
            {
                reader.Read();
                object test = reader[1];
                geometries.Add(test);
                Console.WriteLine(test);

                //Assert.That(reader[0], Is.EqualTo(Point));

                /* while (await reader.ReadAsync())
                 {
                     object test = reader[3];
                     geometries.Add(test);
                     //var test1 = reader.GetString(3);
                     // Console.WriteLine(reader.GetString(1));
                 }*/
            }
            return geometries;
        }
        public async void SelectData()
        {
            await using var conn = new NpgsqlConnection(Constr);
            await conn.OpenAsync();

            await using (var cmd = new NpgsqlCommand("SELECT gid, boroname, name FROM nyc_neighborhoods;", conn))

            await using (var reader = await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    var test = reader.GetString(2);
                    var test1 = reader.GetString(3);
                    Console.WriteLine(reader.GetString(1));
                }
            }
        }
    }
    public interface IMapeamentoService
    {
        //SELECT gid, boroname, name, geom
        //FROM public.nyc_neighborhoods;
        public Task<List<object>> SelectDataGeometry();//SelectDataGeometry
        public Task<List<object>> SelectTransformGeometry();//SelectDataGeometry
        public void SelectData();
    }
}
