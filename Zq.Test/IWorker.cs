using System;
using Zq.Domain;

namespace Zq.Test
{
    public interface IWorker
    {
        int Number { get; set; }
        string Name { get; }
    }

    public class Worker : AggregateRoot<string>, IWorker
    {
        public Worker() : this(new Tool())
        {
        }

        public Worker(ITool tool)
        {
            this._tool = tool;
            this.Name = "zzq";
            this.Number = new Random().Next(1, 100000);
        }

        private ITool _tool;
        public int Number { get; set; }
        public string Name { get; set; }
    }

    public interface ITool
    {
    }

    public class Tool : ITool
    {
    }
}
