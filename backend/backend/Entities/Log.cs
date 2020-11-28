using System;
namespace backend.Entities
{
    public class Log : IEntityBase
    {
        public int Id { get; set; }

        public string LogText { get; set; }

        public DateTime Created { get; set; }

        public Log()
        {
        }
    }
}
