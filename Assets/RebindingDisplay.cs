using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class RebindingDisplay : MonoBehaviour
{
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

    private InputActionRebindingExtensions.RebindingOperation rebindingOperation;

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
    }
}
