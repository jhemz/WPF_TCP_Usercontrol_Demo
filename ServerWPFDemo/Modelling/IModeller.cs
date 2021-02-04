using ServerWPFDemo.Models;
using System.Collections;

namespace ServerWPFDemo.Modelling
{
    public interface IModeller
    {
        Queue Process(modelMain model);
    }
}