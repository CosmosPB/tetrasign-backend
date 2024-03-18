namespace TetraSign.Core.Domain.Documents;

public enum DocumentState {
    Unprocessed = 0,
    Signed = 1,
    Dispatched = 2,
    Rejected  = 3
}