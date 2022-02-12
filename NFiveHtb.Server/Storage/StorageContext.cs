﻿namespace NFiveHtb.Server.Storage
{
	using NFiveHtb.SDK.Core.Models.Player;
    using NFiveHtb.SDK.Server.Configuration;
    using NFiveHtb.SDK.Server.Storage;
    using NFiveHtb.Server.Models;
    using JetBrains.Annotations;
    using Microsoft.EntityFrameworkCore;
    using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
    using System;
    using NFiveHtb.Server.Storage.Configurations;

    public class StorageContext : EFContext<StorageContext>
	{
        public StorageContext() : base(
			new DbContextOptionsBuilder<StorageContext>()
				.UseMySql(ServerConfiguration.DatabaseConnection,
					opt => opt.ServerVersion(new Version(10, 6, 5), ServerType.MariaDb)
				)
				.Options)
        {
			ChangeTracker.LazyLoadingEnabled = false;
		}

        public DbSet<User> Users { get; set; }

		public DbSet<Session> Sessions { get; set; }

		public DbSet<BootHistory> BootHistory { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfiguration(new UserConfiguration());
			modelBuilder.ApplyConfiguration(new SessionConfiguration());
			modelBuilder.ApplyConfiguration(new BootHistoryConfiguration());

			modelBuilder.Entity<Session>().Ignore(c => c.Handle);

			base.OnModelCreating(modelBuilder);			
		}
	}
}
