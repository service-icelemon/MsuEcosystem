using System;
namespace Domain.Entitties.Schedule
{
    public class Subject
    {
        public bool IsInUpperWeek { get; set; }
        public bool IsInLowerWeek { get; set; }
        public bool IsCanceled { get; set; }
        public string SubjectId { get; set; }
        public string TeacherId { get; set; }
        public int BuildingNumber { get; set; }
        public int Audience { get; set; }
    }
}
