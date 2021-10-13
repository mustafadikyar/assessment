using MediatR;
using Rise.Assessment.Phonebook.Application.DTOs;
using System.Collections.Generic;

namespace Rise.Assessment.Phonebook.Application.Queries
{
    public class GetReportQuery : IRequest<List<ReportDTO>>
    {
    }
}