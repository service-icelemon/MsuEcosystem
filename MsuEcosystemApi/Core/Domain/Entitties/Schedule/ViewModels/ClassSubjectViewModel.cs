using Domain.Entitties.MsuInfo;
using Domain.Entitties.MsuInfo.ViewModels;

namespace Domain.Entitties.Schedule.ViewModels
{
    public class ClassSubjectViewModel
    {
        public bool IsInUpperWeek { get; set; }
        public bool IsInLowerWeek { get; set; }
        public bool IsCanceled { get; set; }
        public Subject Subject { get; set; }
        public TeacherPreviewModel Teacher { get; set; }
        public int BuildingNumber { get; set; }
        public int Audience { get; set; }
    }
}
