document.addEventListener("DOMContentLoaded", () => {
    function handleCheckboxGroup(groupClass, hiddenInputId) {
        const checkboxes = document.querySelectorAll(`.${groupClass}`);
        const hiddenInput = document.getElementById(hiddenInputId);

        checkboxes.forEach(checkbox => {
            checkbox.addEventListener("change", () => {
                if (checkbox.checked) {
         
                    hiddenInput.value = checkbox.value;

                    checkboxes.forEach(otherCheckbox => {
                        if (otherCheckbox !== checkbox) {
                            otherCheckbox.checked = false;
                        }
                    });
                } else {
                    hiddenInput.value = "";
                }
            });
        });
    }

    handleCheckboxGroup("category-checkbox", "selectedCategory");
    handleCheckboxGroup("location-checkbox", "selectedLocation");
});