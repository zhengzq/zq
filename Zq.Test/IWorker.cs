using System;
using Zq.Domain;

namespace Zq.Test
{
    public interface IWorker
    {
        ITool Tool { get; }
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
            this.Tool = tool;
            this.Name = "Worker";
            this.Number = new Random().Next(1, 100000);
        }

        public ITool Tool { get; private set; }
        public int Number { get; set; }
        public string Name { get; set; }
    }
    public interface IEmployee
    {
        ITool Tool { get; }
        int Number { get; set; }
        string Name { get; }
    }
    public class Employee : AggregateRoot<string>, IEmployee
    {
        public Employee() : this(new Tool())
        {
        }

        public Employee(ITool tool)
        {
            this.Tool = tool;
            this.Name = "Employee";
            this.Number = new Random().Next(1, 100000);
        }

        public ITool Tool { get; private set; }
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
