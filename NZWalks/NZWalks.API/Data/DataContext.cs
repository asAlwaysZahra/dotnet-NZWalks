using Microsoft.EntityFrameworkCore;

namespace NZWalks.API.Data
{
    public class DataContext :DbContext
    {
        public DataContext(DbContextOptions options) : base(options) {}

        public int MyProperty { get; set; }
    }
}
