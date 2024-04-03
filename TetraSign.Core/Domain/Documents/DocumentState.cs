namespace TetraSign.Core.Domain.Documents;

public enum DocumentState {
    Unprocessed = 0,
    Signed = 1,
    Dispatched = 2,
    Accepted = 3,
    Rejected  = 4,
    ErrorDispatched = 5
}