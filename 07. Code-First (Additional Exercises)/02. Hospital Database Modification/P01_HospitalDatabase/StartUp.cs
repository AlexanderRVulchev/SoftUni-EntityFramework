using P01_HospitalDatabase.Data;

namespace P01_HospitalDatabase
{
    public class StartUp
    {
        static void Main()
        {            
            HospitalContext context = new HospitalContext();
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }
    }
}
