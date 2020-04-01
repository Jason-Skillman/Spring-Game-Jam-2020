using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputTrigger : MonoBehaviour {

    public IMachineInput MachineInput {
        get; set;
    }


    private void OnTriggerEnter(Collider other) {
        ITransportable transportable = other.gameObject.GetComponent<ITransportable>();

        //Is this not transportable?
        if(transportable == null) {
            return;
        }

        //Notify the machine of the input
        MachineInput.OnMachineInput(transportable);
    }

}
