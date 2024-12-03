function ChangeCheckboxesValues(querySelector) {
    const checkboxes = document.querySelectorAll(querySelector);
    if (checkboxes == undefined || checkboxes == null) {
        return;
    }

    checkboxes.forEach(c => c.addEventListener('click', () => ChangeCheckboxValue(c)));
    function ChangeCheckboxValue(checkbox) {
        const index = checkbox.id.split('-')[1];
        const hiddenInput = document.querySelector(`input[type="hidden"]#hidden-${index}`);
        if (hiddenInput) {
            hiddenInput.value = checkbox.checked ? "true" : "false";
        } else {
            console.error(`Скритото поле с ID "${checkbox.id}" не съществува.`);
        }
    }
}

document.addEventListener("DOMContentLoaded", () => {
    ChangeCheckboxesValues('.insurance-checkbox');
    ChangeCheckboxesValues('.feature-checkbox');
    ChangeCheckboxesValues('.category-checkbox');
});
