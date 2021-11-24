using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace EFCoreExperiment.JsonSearching
{
    public class App : IHostedService
    {
        private readonly DatabaseContext _db;

        public App(DatabaseContext db)
        {
            _db = db;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await Setup(cancellationToken);

            var entities = _db.MyEntities
                .Where(x => 
                    MyDbFunctions.GetTranslations(x.Translations, "de", "title").Contains("Meine")
                );

            foreach (var myEntity in entities)
            {
                PrintInfo(myEntity, "en");
                PrintInfo(myEntity, "de");
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        private async Task Setup(CancellationToken cancellationToken)
        {
            Log.Information("Migrating DB");
            await _db.Database.MigrateAsync(cancellationToken);

            Log.Information("Clearing MyEntities Table");
            _db.RemoveRange(_db.MyEntities);
            await _db.SaveChangesAsync(cancellationToken);
            
            Log.Information("Setting up test data");
            _db.MyEntities.Add(new MyEntity()
            {
                Id = Guid.NewGuid(),
                Translations = new Translations()
                    .Add("en", "title", "My New Entity")
                    .Add("de", "title", "Meine neue Entität")
            });
            _db.MyEntities.Add(new MyEntity()
            {
                Id = Guid.NewGuid(),
                Translations = new Translations()
                    .Add("en", "title", "Cool new thing!")
                    .Add("de", "title", "Cooles neues Ding!")
            });
            await _db.SaveChangesAsync(cancellationToken);

            Log.Information("We have {Count} things in the db!", _db.MyEntities.Count());
        }

        private void PrintInfo(MyEntity entity, string language)
        {
            Log.Information("----------------------------");
            Log.Information(" ID: {Id}", entity.Id);
            Log.Information(" Title ({Language}): {Value}", language, entity.Translations[language]["title"]);
            Log.Information("----------------------------");
        }
    }
}
