﻿using Microsoft.EntityFrameworkCore;
using Suls.Data;
using SUS.HTTP;
using SUS.MvcFramework;
using System.Collections.Generic;

namespace Suls
{
    public class Startup : IMvcApplication
    {
        public void Configure(List<Route> routeTable)
        {
            new ApplicationDbContext().Database.Migrate();
        }

        public void ConfigureServices(IServiceCollection serviceCollection)
        {
            
        }
    }
}
