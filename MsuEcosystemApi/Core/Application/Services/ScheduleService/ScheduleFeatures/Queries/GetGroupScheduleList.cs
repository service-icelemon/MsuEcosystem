﻿using Domain.Entitties.Schedule;
using Domain.Entitties.Schedule.ViewModels;
using MediatR;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services.ScheduleService.ScheduleFeatures.Queries
{
    public class GetGroupScheduleList
    {
        public record Query(string FacultyId) : IRequest<IEnumerable<GroupScheduleViewModel>>;

        public class Handler : IRequestHandler<Query, IEnumerable<GroupScheduleViewModel>>
        {
            private IMongoCollection<GroupShedule> _groupScheduleCollection;
            private IMongoCollection<ClassType> _classTypesCollection;
            private IMongoCollection<ClassTime> _classTimesCollection;

            public Handler(IMongoClient client)
            {
                var database = client.GetDatabase("MsuScheduleDb");
                _groupScheduleCollection = database.GetCollection<GroupShedule>("GroupSchedule");
                _classTypesCollection = database.GetCollection<ClassType>("ClassTypes");
                _classTimesCollection = database.GetCollection<ClassTime>("ClassTimes");
            }

            //this must be rewritten
            public async Task<IEnumerable<GroupScheduleViewModel>> Handle(Query request, CancellationToken cancellationToken)
            {
                var result = await _groupScheduleCollection.Find(i => i.FacultyId == request.FacultyId).ToListAsync();
                var schedules = result.Select(item => new GroupScheduleViewModel
                {
                    Id = item.Id,
                    GroupNumber = item.GroupNumber,
                    Days = item.Days.Select(day => new DayViewModel
                    {
                        Index = day.Index,
                        IsCanceled = day.IsCanceled,
                        Classes = day.Classes.Select(i => new ClassViewModel
                        {
                            Index = i.Index,
                            IsCanceled = i.IsCanceled,
                            Time = _classTimesCollection.Find(t => t.Id == i.TimeId).FirstOrDefault(),
                            Type = _classTypesCollection.Find(t => t.Id == i.TypeId).FirstOrDefault(),
                            Subjects = i.Subjects
                        }).OrderBy(d => d.Index).ToArray()
                    }).OrderBy(d => d.Index).ToArray()
                });
                return schedules;
            }
        }
    }
}
