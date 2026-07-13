using Microsoft.EntityFrameworkCore;
using SMS.Data;
using StudentManagementSystem.Services.Interfaces;

namespace StudentManagementSystem.Services
{
    public class ReportService : IReportService
    {
        private readonly ApplicationDbContext _context;

        public ReportService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<object> GetAttendanceSummaryAsync()
        {
            var attendances = await _context.Attendances.ToListAsync();

            var total = attendances.Count;

            var presentCount = attendances.Count(a =>
                a.Status != null &&
                a.Status.Equals("PRESENT", StringComparison.OrdinalIgnoreCase));

            var absentCount = attendances.Count(a =>
                a.Status != null &&
                a.Status.Equals("ABSENT", StringComparison.OrdinalIgnoreCase));

            return new
            {
                TotalAttendanceRecords = total,
                PresentCount = presentCount,
                AbsentCount = absentCount
            };
        }

        public async Task<object> GetFeeDueReportAsync()
        {
            var fees = await _context.Fees.ToListAsync();

            decimal totalFee = fees.Sum(f => f.Amount ?? 0);

            decimal totalPaid = fees.Sum(f => f.PaidAmount ?? 0);

            decimal totalDue = totalFee - totalPaid;

            return new
            {
                TotalFee = totalFee,
                TotalPaid = totalPaid,
                TotalDue = totalDue
            };
        }

        public async Task<object> GetPerformanceReportAsync()
        {
            var results = await _context.Results.ToListAsync();

            double averageMarks = results.Any()
                ? results.Average(r => Convert.ToDouble(r.MarksObtained ?? 0))
                : 0;

            return new
            {
                TotalResults = results.Count,
                AverageMarks = averageMarks
            };
        }
    }
}