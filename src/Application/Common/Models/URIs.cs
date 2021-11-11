namespace Application.Common.Models;

/// <summary>
/// PharmacyUri for getting the pharmacy uri
/// </summary>
public record PharmacyUri(Uri Pharmacy);

/// <summary>
/// PrescriptionUri for getting the prescription
/// </summary>
public record PrescriptionUri(Uri Prescription);

/// <summary>
/// QuotationUri for getting the quotaiton
/// </summary>
public record QuotationUri(Uri Quotation);
