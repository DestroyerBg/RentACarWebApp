function ChangeCheckboxesValues(querySelector) {
    const checkboxes = document.querySelectorAll(querySelector);

    checkboxes.forEach(c => c.addEventListener('click', () => ChangeCheckboxValue(c)));
    function ChangeCheckboxValue(checkbox) {
        const hiddenInput = document.querySelector(`input[type="hidden"]#hidden-${checkbox.id.split('-')[1]}`);
        if (hiddenInput) {
            hiddenInput.value = checkbox.checked ? "true" : "false";
        } else {
            console.error(`Скритото поле с ID "${checkbox.id}" не съществува.`);
        }
    }
}

ChangeCheckboxesValues('.insurance-checkbox');
ChangeCheckboxValue('.feature-checkbox');