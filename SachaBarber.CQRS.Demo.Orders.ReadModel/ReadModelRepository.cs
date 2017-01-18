using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Raven.Abstractions.Data;
using Raven.Client;
using Raven.Client.Embedded;
using Raven.Client.Indexes;
using Models.Common.Student;
using Models.Common.Subject;

namespace SachaBarber.CQRS.Demo.Orders.ReadModel
{


    public interface IReadModelRepository
    {
        Task<List<T>> GetAllSubjects<T>();
    }


    public class ReadModelRepository : IReadModelRepository
    {

        private IDocumentStore documentStore = null;
        private string dataDir = @"C:\temp\RavenDb";

        public ReadModelRepository()
        {

        }

        public Task<List<T>> GetAllSubjects<T>()
        {
            List<T> items = new List<T>();

            return Task.Run(() =>
                {
                    using (IDocumentSession session = documentStore.OpenSession())
                    {
                        int start = 0;
                        while (true)
                        {
                            var current = session.Query<T>()
                                .Customize(x => x.WaitForNonStaleResults())
                                .Take(1024).Skip(start).ToList();
                            if (current.Count == 0) break;

                            start += current.Count;
                            items.AddRange(current);

                        }
                    }
                    return items;
                });
        }
    }
}
