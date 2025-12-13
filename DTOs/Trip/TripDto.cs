namespace MetroClaim.Api.DTOs.Trip;

// --- RESPONSE DTOs ---

public record TripParticipantDto(
    Guid UserId,
    string FullName,
    string ReimbursementStatus, // Info status: Pending/Approved
    decimal CurrentAmount // Info berapa yg sudah di-claim user
);

public record TripDetailDto(
    Guid Id,
    string Title,
    string Description,
    string Destination,
    DateTime StartDate,
    DateTime EndDate,
    decimal Cost, // Budget dari Finance
    string Status,
    string ManagerName,
    DateTime CreatedAt,
    List<TripParticipantDto> Participants
);

// --- REQUEST DTOs ---

public record CreateTripRequestDto(
    string Title,
    string Description,
    string Destination,
    DateTime StartDate,
    DateTime EndDate,
    // Guid CategoryId, // Kategori untuk Auto-Reimbursement (misal: "Perjalanan Dinas")
    List<Guid> ParticipantIds // List Pegawai
);

public record UpdateTripRequestDto(
    string Title,
    string Description,
    string Destination,
    DateTime StartDate,
    DateTime EndDate,
    Guid CategoryId, // Jika kategori berubah, reimbursement draft harus update
    List<Guid> ParticipantIds // Logic sinkronisasi (Add/Remove)
);

public record FinanceReviewTripDto(
    bool IsApproved,
    decimal AllocatedCost, // Budget yang disetujui
    string? RejectionReason
);