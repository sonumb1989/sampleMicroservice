﻿using Microsoft.EntityFrameworkCore;
using Polly;
using Serilog;
using System;
using System.Collections.Generic;
using System.Text;

namespace WorkshopManagementEventHandler.DataAccess
{
    public static class DBInitializer
    {
        public static void Initialize(WorkshopManagementDBContext context)
        {
            Log.Information("Ensure WorkshopManagement Database");

            Policy
            .Handle<Exception>()
            .WaitAndRetry(5, r => TimeSpan.FromSeconds(5), (ex, ts) => { Log.Error("Error connecting to DB. Retrying in 5 sec."); })
            .Execute(() => context.Database.Migrate());

            Log.Information("WorkshopManagement Database available");
        }
    }
}
