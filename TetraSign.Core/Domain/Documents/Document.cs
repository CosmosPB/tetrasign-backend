using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TetraSign.Core.Domain.Documents;

[BsonIgnoreExtraElements]
public class Document<TEntity>: IAggregateRoot {

    [BsonId]
    [BsonElement("Id")]
    [BsonRepresentation(BsonType.ObjectId)]
    public virtual string? id { get; protected set; }
    public virtual string? document_id { get; protected set; }
    public virtual string? document_type { get; protected set; }
    public virtual DateTime issue_date { get; protected set; }
    public virtual DateTime upload_date { get; protected set; }
    public virtual DateTime? sign_date { get; protected set; }
    public virtual DateTime? send_date { get; protected set; }
    public virtual TEntity? data { get; protected set; }
    public virtual string? filename { get; protected set; }
    public virtual string? extension { get; protected set; }
    public virtual string? state { get; protected set; }
    public virtual string? observation { get; protected set; }
    public virtual string? ticket_id { get; protected set; }

    public static Document<TEntity> Create(
        string? document_id,
        string? document_type,
        DateTime issue_date,
        DateTime upload_date,
        DateTime? sign_date,
        DateTime? send_date,
        TEntity? data,
        string? filename,
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
            extension = extension,
            state = state,
            observation = observation,
            ticket_id = ticket_id
        };
        
        return document;
    }

    public virtual void ChangeState(string state) => this.state = state;
    public virtual void ChangeTicketId(string ticket_id) => this.ticket_id = ticket_id;
}