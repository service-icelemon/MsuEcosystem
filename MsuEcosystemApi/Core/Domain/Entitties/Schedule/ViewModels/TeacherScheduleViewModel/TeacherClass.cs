namespace Domain.Entitties.Schedule.ViewModels.TeacherScheduleViewModel
{
    public class TeacherClass
    {
        public int GroupNumber { get; set; }
        public int Index { get; set; }
        public bool IsCanceled { get; set; }
        public ClassType Type { get; set; }
        public ClassTime Time { get; set; }
        public Subject[] Subjects { get; set; }
    }
}
