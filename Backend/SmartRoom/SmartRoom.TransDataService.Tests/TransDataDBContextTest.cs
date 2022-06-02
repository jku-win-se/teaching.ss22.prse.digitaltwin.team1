using Microsoft.EntityFrameworkCore;
using Moq;
using SmartRoom.CommonBase.Core.Entities;
using SmartRoom.TransDataService.Persistence;
using System;
using System.Threading.Tasks;
using Xunit;

namespace SmartRoom.TransDataService.Tests
{
    public class TransDataDBContextTest
    {

        [Fact]
        public void Ctor_ValidParam_Ok()
        {
            DbContextOptions<TransDataDBContext> options;
            var builder = new DbContextOptionsBuilder<TransDataDBContext>();
            builder.UseInMemoryDatabase("TestDB");
            options = builder.Options;
            TransDataDBContext context = new TransDataDBContext(options);

            Assert.NotNull(context);
        }

        [Fact]
        public async Task GetDbSets_ExistingType_ExistingTypes()
        {
            TransDataDBContext context = await GetInMemoryDBContext();
            var measures = context.MeasureStates;
            var binaries = context.BinaryStates;
        
            Assert.IsAssignableFrom<DbSet<MeasureState>>(measures);
            Assert.IsAssignableFrom<DbSet<BinaryState>>(binaries);
        }

        [Fact]
        public async Task RawSql_NoElements_ThrowsException()
        {
            TransDataDBContext context = await GetInMemoryDBContext();
            Assert.ThrowsAny<Exception>(() => context.RawSqlQuery("", d => new BinaryState()));
        }


        private async Task<TransDataDBContext> GetInMemoryDBContext()
        {
            DbContextOptions<TransDataDBContext> options;
            var builder = new DbContextOptionsBuilder<TransDataDBContext>();
            builder.UseInMemoryDatabase("TestDB");
            options = builder.Options;
            TransDataDBContext context = new TransDataDBContext(options);

            await context.Database.EnsureDeletedAsync();
            await context.Database.EnsureCreatedAsync();

            return context;
        }
    }
}
