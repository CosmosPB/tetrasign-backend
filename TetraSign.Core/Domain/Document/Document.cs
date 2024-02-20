using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TetraSign.Core.Domain.Documents;

public class Document<TEntity>: IAggregateRoot {

    [BsonId]
    [BsonElement("Id")]
    [BsonRepresentation(BsonType.ObjectId)]
    public virtual string? id { get; protected set; }
    public virtual string? document_id { get; protected set; }
    public virtual string? document_type { get; protected set; }
    public virtual string? issue_date { get; protected set; }
    public virtual string? upload_date { get; protected set; }
    public virtual string? sign_date { get; protected set; }
    public virtual string? send_date { get; protected set; }
    public virtual TEntity? data { get; protected set; }
    public virtual string? filename { get; protected set; }
    public virtual string? full_path { get; protected set; }
    public virtual string? extension { get; protected set; }
    public virtual string? state { get; protected set; }
    public virtual string? observation { get; protected set; }
    public virtual string? ticket_id { get; protected set; }

    public static Document<TEntity> Create(
        string? document_id,
        string? document_type,
        string? issue_date,
        string? upload_date,
        string? sign_date,
        string? send_date,
        TEntity? data,
        string? filename,
        string? full_path,
        string? extension,
        string? state,
        string? observation,
        string? ticket_id
    ) {
        Document<TEntity> document = new() {
            document_id = document_id,
            document_type = document_type,
            issue_date = issue_date,
            upload_date = upload_date,
            sign_date = sign_date,
            send_date = send_date,
            data = data,
            filename = filename,
            full_path = full_path,
            extension = extension,
            state = state,
            observation = observation,
            ticket_id = ticket_id
        };
        
        return document;
    }
}