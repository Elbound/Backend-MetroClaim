namespace MetroClaim.Api.Models;

public enum Period
{
    Monthly,
    Yearly,
}

public enum TripStatus
{
    ManagerSubmited,
    FinanceApproved,
    Ongoing,
    Closed,
    Canceled,
}

public enum ReimbursementStatus
{
    Pending,
    Rejected,
    Approved,
}

public enum ApprovalLogStatus
{
    Submitted,
    ManagerApproved,
    ManagerRejected,
    FinanceApproved,
    FinanceRejected,
}