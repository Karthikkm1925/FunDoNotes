using FunDoNotes.Model.DTOs.Email;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunDoNotes.BusinessLogic.Messaging
{
    public interface IRabbitMqPublisher
    {
        void Publish<T>(T message);
    }
}
