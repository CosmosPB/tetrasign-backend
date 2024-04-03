namespace TetraSign.Core.Helpers.Sunat;

public record Error(string desError);
public record HttpResponse(string access_token, string numTicket, string codRespuesta, string arcCdr, Error error);