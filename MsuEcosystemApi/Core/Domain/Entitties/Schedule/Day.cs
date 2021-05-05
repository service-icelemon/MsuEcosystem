namespace Domain.Entitties.Schedule
{
    public class Day
    {
        public int Index { get; set; }
        public bool IsCanceled { get; set; }
        public Class[] Classes { get; set; }
    }
}
