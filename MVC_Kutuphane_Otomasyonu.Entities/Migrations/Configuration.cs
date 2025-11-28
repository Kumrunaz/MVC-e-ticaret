namespace MVC_Kutuphane_Otomasyonu.Entities.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MVC_Kutuphane_Otomasyonu.Entities.Model.Context.KutuphaneContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(MVC_Kutuphane_Otomasyonu.Entities.Model.Context.KutuphaneContext context)
        {

        }
    }
}
