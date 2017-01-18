using CQRSlite.Commands;
using Models.Common.Subject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SachaBarber.CQRS.Demo.Orders.Commands
{
    public class Command 
    {
        [DataMember]
        public Guid Id { get; set; }

        private readonly ISession _session;
        public void Handle(Subject command)
        {
            var Course = new Subject(command.Id, command.Name, command.Credits);
            _session.Add(Course);
            _session.Commit();
        }
    }
}
