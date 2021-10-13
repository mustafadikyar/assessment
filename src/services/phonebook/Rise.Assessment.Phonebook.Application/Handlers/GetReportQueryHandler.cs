using MediatR;
using Rise.Assessment.Phonebook.Application.DTOs;
using Rise.Assessment.Phonebook.Application.Queries;
using Rise.Assessment.Phonebook.Infrastructure;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Rise.Assessment.Phonebook.Application.Handlers
{
    public class GetReportQueryHandler : IRequestHandler<GetReportQuery, List<ReportDTO>>
    {
        private readonly PhonebookDbContext _context;

        public GetReportQueryHandler(PhonebookDbContext context)
        {
            _context = context;
        }

        public Task<List<ReportDTO>> Handle(GetReportQuery request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
