namespace Domain.Entitties.Schedule.ViewModels
{
    public class DayViewModel
    {
        public int Index { get; set; }
        public bool IsCanceled { get; set; }
        public ClassViewModel[] Classes { get; set; }
    }
}
