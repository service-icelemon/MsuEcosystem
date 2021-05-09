namespace Domain.Entitties.Schedule.ViewModels.TeacherScheduleViewModel
{
    public class TeacherDay
    {
        public int Index { get; set; }
        public bool IsCanceled { get; set; }
        public TeacherClass[] Classes { get; set; }
    }
}
