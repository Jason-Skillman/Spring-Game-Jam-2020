public interface IMachineInput {

    /// <summary>
    /// Called when an InputTrigger has detected an ITransportable item
    /// </summary>
    /// <param name="transportable">The item that is being transported</param>
    void OnMachineInput(ITransportable transportable);

}

