namespace Domain.Entitties.Schedule.ViewModels
{
    public class ClassViewModel
    {
        public int Index { get; set; }
        public bool IsCanceled { get; set; }
        public ClassType Type { get; set; }
        public ClassTime Time { get; set; }
        public Subject[] Subjects { get; set; }
    }
}
