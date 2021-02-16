using ClientWPFDemo.EventHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientWPFDemo.Services
{
  public interface IMessageCore
  {
    void Disconnect();
    Task Connect();
    void AddItemToQueueToSendToServer(KeyValuePair<string, object> keyValuePair);
    event UpdateModelHandler UpdateModelEvent; // not sure this should be declared like this. But for now ...
    void Start();
  }
}
