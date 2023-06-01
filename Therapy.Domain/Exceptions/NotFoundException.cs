namespace Therapy.Domain.Exceptions
{
    public class NotFoundException : TherapyException
    {
        public NotFoundException(string entityName, int entityId)
            : base($"The {entityName} with id {entityId} was not found.")
        {
            this.StatusCode = (int)ExceptionStatusCode.NotFound;
        }
        public NotFoundException(int entityId)
            : base($"The id {entityId} was not found.")
        {
            this.StatusCode = (int)ExceptionStatusCode.NotFound;
        }
    }
}