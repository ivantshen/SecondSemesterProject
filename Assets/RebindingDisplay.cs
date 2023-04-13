using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class RebindingDisplay : MonoBehaviour
{
    // variables
    [SerializeField] private InputActionReference jumpAction = null;
    [SerializeField] private InputActionReference dashAction = null;
    //[SerializeField] private NewPlayerMovement playerController = null;
    [SerializeField] private TMP_Text bindingJumpDisplayNameText = null;
    [SerializeField] private TMP_Text bindingDashDisplayNameText = null;
    [SerializeField] private GameObject startRebindJumpObject = null;
    [SerializeField] private GameObject startRebindDashObject = null;
    [SerializeField] private GameObject waitingForInputObject = null;

    [SerializeField] private InputActionReference attackAction = null;
    [SerializeField] private TMP_Text bindingAttackDisplayNameText = null;
    [SerializeField] private GameObject startRebindAttackObject = null;

    [SerializeField] private InputActionReference collectAction = null;
    [SerializeField] private TMP_Text bindingCollectDisplayNameText = null;
    [SerializeField] private GameObject startRebindCollectObject = null;

    [SerializeField] private InputActionReference openAction = null;
    [SerializeField] private TMP_Text bindingOpenDisplayNameText = null;
    [SerializeField] private GameObject startRebindOpenObject = null;

    [SerializeField] private InputActionReference switchAction = null;
    [SerializeField] private TMP_Text bindingSwitchDisplayNameText = null;
    [SerializeField] private GameObject startRebindSwitchObject = null;

    private InputActionRebindingExtensions.RebindingOperation rebindingOperation;


    

    // rebinds start here
    // jump
    public void StartJumpRebinding() {
        startRebindJumpObject.SetActive(false);
        waitingForInputObject.SetActive(true);

        //playerController.PlayerInput.SwitchCurrentActionMap("Menu");

        rebindingOperation = jumpAction.action.PerformInteractiveRebinding()
            .WithControlsExcluding("Mouse")
            .OnMatchWaitForAnother(0.1f)
            .OnComplete(operation => RebindJumpComplete())
            .Start();
    }

    private void RebindJumpComplete() {
        int bindingIndex = jumpAction.action.GetBindingIndexForControl(jumpAction.action.controls[0]);

        bindingJumpDisplayNameText.text = InputControlPath.ToHumanReadableString(
            jumpAction.action.bindings[0].effectivePath,
            InputControlPath.HumanReadableStringOptions.OmitDevice);

        rebindingOperation.Dispose();

        startRebindJumpObject.SetActive(true);
        waitingForInputObject.SetActive(false);

        //playerController.PlayerInput.SwitchCurrentActionMap("Player");

    }

    // dash
    public void StartDashRebinding() {
        startRebindDashObject.SetActive(false);
        waitingForInputObject.SetActive(true);

        rebindingOperation = dashAction.action.PerformInteractiveRebinding()
            .WithControlsExcluding("Mouse")
            .OnMatchWaitForAnother(0.1f)
            .OnComplete(operation => RebindDashComplete())
            .Start();
    }

    private void RebindDashComplete() {
        int bindingIndex = dashAction.action.GetBindingIndexForControl(dashAction.action.controls[0]);

        bindingDashDisplayNameText.text = InputControlPath.ToHumanReadableString(
            dashAction.action.bindings[0].effectivePath,
            InputControlPath.HumanReadableStringOptions.OmitDevice);

        rebindingOperation.Dispose();

        startRebindDashObject.SetActive(true);
        waitingForInputObject.SetActive(false);
    }

    // attack
    public void StartAttackRebinding() {
        startRebindAttackObject.SetActive(false);
        waitingForInputObject.SetActive(true);

        rebindingOperation = attackAction.action.PerformInteractiveRebinding()
            .WithControlsExcluding("Mouse")
            .OnMatchWaitForAnother(0.1f)
            .OnComplete(operation => RebindAttackComplete())
            .Start();
    }

    private void RebindAttackComplete() {
        int bindingIndex = attackAction.action.GetBindingIndexForControl(attackAction.action.controls[0]);

        bindingAttackDisplayNameText.text = InputControlPath.ToHumanReadableString(
            attackAction.action.bindings[0].effectivePath,
            InputControlPath.HumanReadableStringOptions.OmitDevice);

        rebindingOperation.Dispose();

        startRebindAttackObject.SetActive(true);
        waitingForInputObject.SetActive(false);
        Debug.Log(bindingAttackDisplayNameText.text);
    }

    // collect
    public void StartCollectRebinding() {
        startRebindCollectObject.SetActive(false);
        waitingForInputObject.SetActive(true);

        rebindingOperation = collectAction.action.PerformInteractiveRebinding()
            .WithControlsExcluding("Mouse")
            .OnMatchWaitForAnother(0.1f)
            .OnComplete(operation => RebindCollectComplete())
            .Start();
    }

    private void RebindCollectComplete() {
        int bindingIndex = collectAction.action.GetBindingIndexForControl(collectAction.action.controls[0]);

        bindingCollectDisplayNameText.text = InputControlPath.ToHumanReadableString(
            collectAction.action.bindings[0].effectivePath,
            InputControlPath.HumanReadableStringOptions.OmitDevice);

        rebindingOperation.Dispose();

        startRebindCollectObject.SetActive(true);
        waitingForInputObject.SetActive(false);
    }

    // open inventory
    public void StartOpenRebinding() {
        startRebindOpenObject.SetActive(false);
        waitingForInputObject.SetActive(true);

        rebindingOperation = openAction.action.PerformInteractiveRebinding()
            .WithControlsExcluding("Mouse")
            .OnMatchWaitForAnother(0.1f)
            .OnComplete(operation => RebindOpenComplete())
            .Start();
    }

    private void RebindOpenComplete() {
        int bindingIndex = openAction.action.GetBindingIndexForControl(openAction.action.controls[0]);

        bindingOpenDisplayNameText.text = InputControlPath.ToHumanReadableString(
            openAction.action.bindings[0].effectivePath,
            InputControlPath.HumanReadableStringOptions.OmitDevice);

        rebindingOperation.Dispose();

        startRebindOpenObject.SetActive(true);
        waitingForInputObject.SetActive(false);
    }

    // switch weapon
    public void StartSwitchRebinding() {
        startRebindSwitchObject.SetActive(false);
        waitingForInputObject.SetActive(true);

        rebindingOperation = switchAction.action.PerformInteractiveRebinding()
            .WithControlsExcluding("Mouse")
            .OnMatchWaitForAnother(0.1f)
            .OnComplete(operation => RebindSwitchComplete())
            .Start();
    }

    private void RebindSwitchComplete() {
        int bindingIndex = switchAction.action.GetBindingIndexForControl(switchAction.action.controls[0]);

        bindingSwitchDisplayNameText.text = InputControlPath.ToHumanReadableString(
            switchAction.action.bindings[0].effectivePath,
            InputControlPath.HumanReadableStringOptions.OmitDevice);

        rebindingOperation.Dispose();

        startRebindSwitchObject.SetActive(true);
        waitingForInputObject.SetActive(false);
    }

    // // template
    // public void Start[]Rebinding() {
    //     startRebind[]Object.SetActive(false);
    //     waitingForInputObject.SetActive(true);

    //     rebindingOperation = []Action.action.PerformInteractiveRebinding()
    //         .WithControlsExcluding("Mouse")
    //         .OnMatchWaitForAnother(0.1f)
    //         .OnComplete(operation => Rebind[]Complete())
    //         .Start();
    // }

    // private void Rebind[]Complete() {
    //     int bindingIndex = []Action.action.GetBindingIndexForControl([]Action.action.controls[0]);

    //     binding[]DisplayNameText.text = InputControlPath.ToHumanReadableString(
    //         []Action.action.bindings[0].effectivePath,
    //         InputControlPath.HumanReadableStringOptions.OmitDevice);

    //     rebindingOperation.Dispose();

    //     startRebind[]Object.SetActive(true);
    //     waitingForInputObject.SetActive(false);
    // }
}
