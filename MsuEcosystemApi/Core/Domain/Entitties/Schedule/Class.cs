namespace Domain.Entitties.Schedule
{
    public class Class
    {
        public int Index { get; set; }
        public bool IsCanceled { get; set; }
        public string TypeId { get; set; }
        public string TimeId { get; set; }
        public Subject[] Subjects { get; set; }
    }
}
