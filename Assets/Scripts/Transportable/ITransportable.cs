public interface ITransportable {

    /// <summary>
    /// Getter for viewing the resource
    /// </summary>
    /// <returns>The resource to peek at</returns>
    Resource OnPeek();

    /// <summary>
    /// Callled when the transport has been picked up
    /// </summary>
    void OnPickup();

    /// <summary>
    /// Callled when the transport tried to be picked up and was rejected
    /// </summary>
    void OnRejected();

}
