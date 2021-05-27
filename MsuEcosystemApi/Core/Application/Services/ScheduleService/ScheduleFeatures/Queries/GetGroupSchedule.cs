using Domain.Entitties.MsuInfo;
using Domain.Entitties.MsuInfo.ViewModels;
using Domain.Entitties.Schedule;
using Domain.Entitties.Schedule.ViewModels;
using MediatR;
using MongoDB.Driver;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services.ScheduleService.ScheduleFeatures.Queries
{
    public class GetGroupSchedule
    {
        public record Query(int GroupNumber) : IRequest<GroupScheduleViewModel>;

        public class Handler : IRequestHandler<Query, GroupScheduleViewModel>
        {
            private IMongoCollection<GroupShedule> _groupScheduleCollection;
            private IMongoCollection<ClassType> _classTypesCollection;
            private IMongoCollection<ClassTime> _classTimesCollection;
            private IMongoCollection<Subject> _subjectCollection;
            private IMongoCollection<Teacher> _teacherCollection;

            public Handler(IMongoClient client)
            {
                var scheduleDb = client.GetDatabase("MsuScheduleDb");
                var infoDb = client.GetDatabase("MsuInfoDb");
                _groupScheduleCollection = scheduleDb.GetCollection<GroupShedule>("GroupSchedule");
                _classTypesCollection = scheduleDb.GetCollection<ClassType>("ClassTypes");
                _classTimesCollection = scheduleDb.GetCollection<ClassTime>("ClassTimes");
                _subjectCollection = infoDb.GetCollection<Subject>("Subjects");
                _teacherCollection = infoDb.GetCollection<Teacher>("Teachers");
            }

            //this must be rewritten
            public async Task<GroupScheduleViewModel> Handle(Query request, CancellationToken cancellationToken)
            {
                var result = await _groupScheduleCollection.Find(i => i.GroupNumber == request.GroupNumber).FirstOrDefaultAsync();
                return new GroupScheduleViewModel
                {
                    Id = result.Id,
                    GroupNumber = result.GroupNumber,
                    Days = result.Days.Select(day => new DayViewModel
                    {
                        Index = day.Index,
                        IsCanceled = day.IsCanceled,
                        Classes = day.Classes.Select(i => new ClassViewModel
                        {
                            Index = i.Index,
                            IsCanceled = i.IsCanceled,
                            Time = _classTimesCollection.Find(t => t.Id == i.TimeId).FirstOrDefault(),
                            Type = _classTypesCollection.Find(t => t.Id == i.TypeId).FirstOrDefault(),
                            Subjects = i.Subjects.Select(s => new ClassSubjectViewModel
                            { 
                                IsCanceled = s.IsCanceled,
                                IsInLowerWeek = s.IsInLowerWeek,
                                IsInUpperWeek = s.IsInUpperWeek,
                                Audience = s.Audience,
                                BuildingNumber = s.BuildingNumber,
                                Subject = _subjectCollection.Find(subject => subject.Id == s.SubjectId).FirstOrDefault(),
                                Teacher = _teacherCollection.AsQueryable().Where(teacher => teacher.Id == s.TeacherId).Select(t => new TeacherPreviewModel
                                { 
                                    Id = t.Id,
                                    FirstName = t.FirstName,
                                    LastName  = t.LastName,
                                    FatherName = t.FatherName,
                                    ScienceDegree = t.ScienceDegree,
                                    PhotoUrl = t.PhotoUrl
                                }).FirstOrDefault()
                            }).ToArray()
                        }).OrderBy(d => d.Index).ToArray()
                    }).OrderBy(d => d.Index).ToArray()
                };
            }
        }
    }
}
